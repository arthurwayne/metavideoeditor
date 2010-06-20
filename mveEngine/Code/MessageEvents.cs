using System;
using System.Windows.Forms;

namespace mveEngine
{
	public class MessageEvents : EventArgs
	{
		private string _message = null;			

		public MessageEvents(string message)
		{			
			if (message == null) throw new NullReferenceException();
			_message = message; 
		}
									
		public string Message
		{
			get { return this._message; }
		}	
	}

    public class NodeEvents : EventArgs
    {
        private TreeNode _node = null;

        public NodeEvents(TreeNode node)
        {
            if (node == null) throw new NullReferenceException();
            _node = node;
        }

        public TreeNode Node
        {
            get { return this._node; }
        }
    }

    public class GenerateMessage
    {
        
        public delegate void MessageGeneratedEventHandler(object sender, MessageEvents e);
        public delegate void RefreshNodeEventHandler(object sender, NodeEvents e);
                
        public event MessageGeneratedEventHandler OnMessageAdded;
        public event MessageGeneratedEventHandler OnProcessStart;
        public event MessageGeneratedEventHandler OnProcessSuccess;
        public event MessageGeneratedEventHandler OnProcessFails;
        public event RefreshNodeEventHandler OnRefreshNode;
        

        public GenerateMessage() { }

        public void AddMessage(string message)
        {
            Logger.ReportInfo(message);
            MessageEvents e = new MessageEvents(message);
            if (e != null && OnMessageAdded != null) OnMessageAdded(this, e);
        }

        public void ProcessSuccess(string message)
        {
            MessageEvents e = new MessageEvents(message);
            if (e != null && OnProcessSuccess != null) OnProcessSuccess(this, e);
        }

        public void ProcessStart(string message)
        {
            MessageEvents e = new MessageEvents(message);
            if (e != null && OnProcessStart != null) OnProcessStart(this, e);
        }

        public void ProcessFails(string message)
        {
            MessageEvents e = new MessageEvents(message);
            if (e != null && OnProcessFails != null) OnProcessFails(this, e);
        }

        public void RefreshNode(TreeNode node)
        {
            NodeEvents e = new NodeEvents(node);
            if (OnRefreshNode != null) OnRefreshNode(this, e);
        }


    }
}