using Common;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Collections.Concurrent;
using System.Security.Claims;
using System.Runtime.InteropServices;

namespace Storage
{
    public class Storage : IStorage
    {
        InMemory inMemory;
        XML xml;    
        public delegate void DeleteDelegate();
        static int Timeout;


        public Storage(string path, int timeout)
        {
            inMemory = new InMemory();
            xml = new XML(path + "\\LOAD.xml", path + "\\AUDIT.xml");
            Timeout = timeout;
        }

        public Audit GetLastAudit()
        {
                if (InMemory.audits.Count == 0) return xml.GetLastAudit();
                return inMemory.GetLastAudit();
            
        }

        public List<Load> Read(DateTime dt)
        {
                List<Load> loads;

                loads = inMemory.Read(dt);
                if (loads.Count == 0)
                {
                    loads = xml.Read(dt);
                    foreach (Load load in loads)
                    {
                        InMemory.loads[load.Id] = load;
                        InMemory.time[load.Id] = DateTime.Now;
                    }
                }
                return loads;
            
        }

        public void Write(Audit audit)
        {
                xml.Write(audit);
                inMemory.Write(audit);
            
        }

        private static void DeleteCallback()
        {
            Dictionary<int, DateTime> copy;
            lock (InMemory.lockObject)
            {
                copy = new Dictionary<int, DateTime>(InMemory.time);
            }
            

            foreach (var t in copy)
            {
                if ((DateTime.Now.Subtract(t.Value)).TotalSeconds >= Timeout )
                {
                    lock (InMemory.lockObject)
                    {
                        if (InMemory.time.ContainsKey(t.Key))
                        {
                            InMemory.time.Remove(t.Key);
                            InMemory.loads.Remove(t.Key);
                            Console.WriteLine("Izbrisana vrednost:" + t.Key);
                        }
                    }
                }
            }
        }

        public static void Delete()
        {            
            DeleteDelegate deleteDelegate = new DeleteDelegate(DeleteCallback);
            deleteDelegate.BeginInvoke(null, null);

        }
    }
}
