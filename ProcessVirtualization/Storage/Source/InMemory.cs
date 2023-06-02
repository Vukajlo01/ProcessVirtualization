using Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Storage
{
    public class InMemory : IStorage
    {
        public static Dictionary<int, Load> loads = new Dictionary<int, Load>();
        public static Dictionary<int, Audit> audits = new Dictionary<int, Audit>();
        public static Dictionary<int, DateTime> time = new Dictionary<int, DateTime>();
        public static object lockObject = new object();


        public InMemory() { }

        public Audit GetLastAudit()
        {
            return audits[audits.Keys.Max()];
        }

        public List<Load> Read(DateTime dt)
        {
                List<Load> list = new List<Load>();

                foreach (Load l in loads.Values)
                {
                    if (l.Timestamp.Date == dt.Date)
                    {
                        list.Add(l);
                    }
                }

                return list;
            
        }

        public void Write(Audit audit)
        {
                audits[audit.Id] = audit;
                
            
        }
    }
}
