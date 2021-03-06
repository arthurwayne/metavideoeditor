﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace mveEngine
{

    public static class Async
    {

        public const string STARTUP_QUEUE = "Startup Queue";

        class ThreadPool
        {
            List<Action> actions = new List<Action>();
            List<Thread> threads = new List<Thread>();
            string name;
            volatile int maxThreads = 1;

            public ThreadPool(string name)
            {
                Debug.Assert(name != null);
                if (name == null)
                {
                    throw new ArgumentException("name should not be null");
                }
                this.name = name;
            }


            public void SetMaxThreads(int maxThreads)
            {
                Debug.Assert(maxThreads > 0);
                if (maxThreads < 1)
                {
                    throw new ArgumentException("maxThreads should be larger than 0");
                }

                this.maxThreads = maxThreads;
            }

            public void Queue(Action action, bool urgent)
            {
                Queue(action, urgent, 0);
            }

            public void Queue(Action action, bool urgent, int delay)
            {                
                if (delay > 0)
                {
                    Timer t = null;
                    t = new Timer(_ =>
                    {
                        Queue(action, urgent, 0);
                        t.Dispose();
                    }, null, delay, Timeout.Infinite);
                    return;
                }

                lock (threads)
                {
                    // we are spinning up too many threads
                    // should be fixed 
                    if (maxThreads > threads.Count)
                    {
                        Thread t = new Thread(new ThreadStart(ThreadProc));
                        t.IsBackground = true;
                        t.Name = "Worker thread for " + name;
                        t.Start();
                        threads.Add(t);
                    }
                }

                lock (actions)
                {
                    if (urgent)
                    {
                        actions.Insert(0, action);
                    }
                    else
                    {
                        actions.Add(action);
                    }

                    Monitor.Pulse(actions);
                }
            }

            private void ThreadProc()
            {

                while (true)
                {

                    lock (threads)
                    {
                        if (maxThreads < threads.Count)
                        {
                            threads.Remove(Thread.CurrentThread);
                            break;
                        }
                    }

                    List<Action> copy;

                    lock (actions)
                    {
                        while (actions.Count == 0)
                        {
                            Monitor.Wait(actions);
                        }
                        copy = new List<Action>(actions);
                        actions.Clear();
                    }

                    foreach (var action in copy)
                    {
                        action();
                    }
                }
            }
        }


        static Dictionary<string, ThreadPool> threadPool = new Dictionary<string, ThreadPool>();

        public static Timer Every(int milliseconds, Action action)
        {
            Timer timer = new Timer(_ => action(), null, 0, milliseconds);
            return timer;
        }

        public static void SetMaxThreads(string uniqueId, int threads)
        {
            GetThreadPool(uniqueId).SetMaxThreads(threads);
        }

        public static void Queue(string uniqueId, Action action)
        {
            Queue(uniqueId, action, null);
        }

        public static void Queue(string uniqueId, Action action, bool urgent)
        {
            Queue(uniqueId, action, null, urgent);
        }

        public static void Queue(string uniqueId, Action action, bool urgent, string message)
        {
            Queue(uniqueId, action, null, urgent, 0, message);
        }

        public static void Queue(string uniqueId, Action action, int delay)
        {
            Queue(uniqueId, action, null, false, delay, uniqueId);
        }

        public static void Queue(string uniqueId, Action action, Action done)
        {
            Queue(uniqueId, action, done, false);
        }

        public static void Queue(string uniqueId, Action action, Action done, bool urgent)
        {
            Queue(uniqueId, action, done, urgent, 0, uniqueId);
        }

        public static void Queue(string uniqueId, Action action, Action done, bool urgent, int delay, string message)
        {

            Debug.Assert(uniqueId != null);
            Debug.Assert(action != null);

            Action workItem;
            ThreadPool currentPool;
            if (done != null && threadPool.TryGetValue(uniqueId, out currentPool) && uniqueId == message)
            {
                workItem = () =>
                    {
                        //Kernel.Instance.Message.ProcessSuccess(uniqueId);
                        done();
                    };
            }
            else
            {
                Kernel.Instance.Message.AddMessage(message);
                workItem = () =>
                {
                    
                    Kernel.Instance.Message.ProcessStart(message);
                    try
                    {
                        action();
                    }
                    catch (ThreadAbortException) { Kernel.Instance.Message.ProcessFails(message); }
                    catch (Exception ex)
                    {
                        Kernel.Instance.Message.ProcessFails(message);
                        Debug.Assert(false, "Async thread crashed! This must be fixed. " + ex.ToString());
                        Logger.ReportException("Async thread crashed! This must be fixed. ", ex);
                        return;
                    }
                    Kernel.Instance.Message.ProcessSuccess(message);
                    if (done != null) done();
                };
            }

            GetThreadPool(uniqueId).Queue(workItem, urgent, delay);
        }

        private static ThreadPool GetThreadPool(string uniqueId)
        {
            ThreadPool currentPool;
            lock (threadPool)
            {
                if (!threadPool.TryGetValue(uniqueId, out currentPool))
                {
                    currentPool = new ThreadPool(uniqueId);
                    threadPool[uniqueId] = currentPool;
                }
            }
            return currentPool;
        }
    }

}
