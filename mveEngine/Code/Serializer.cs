using System;
using System.Text;
using System.IO;
using System.Net;
using System.Web;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;


namespace mveEngine
{
    public class Serializer
    {

        static Object FileLockObj = new Object();

        public static void Serialization(string file, Object o)
        {
            lock (FileLockObj)
            {
                BinaryFormatter sf = new BinaryFormatter();
                using (FileStream fs = new FileStream(file, FileMode.Create))
                {
                    sf.Serialize(fs, o);
                }
            }
        }

        public static Object Deserialization(string file)
        {
            Object res;
            lock (FileLockObj)
            {
                BinaryFormatter sf = new BinaryFormatter();
                using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    res = sf.Deserialize(fs);
                }
            }
            return res;
        }
    }
}