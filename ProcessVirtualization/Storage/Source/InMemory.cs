using Common;
using System;
using System.Collections.Generic;

namespace Storage
{
    public class InMemory : IStorage
    {
        public Dictionary<int, Load> loads;
        public Dictionary<int, Audit> audits;

        public InMemory()
        {
            loads = new Dictionary<int, Load>();
            audits = new Dictionary<int, Audit>();
        }

        public Audit GetLastAudit()
        {
            throw new NotImplementedException();
        }

        public List<Load> Read(DateTime dt)
        {
            List<Load> list = new List<Load>();

            foreach(Load l in loads.Values)
            {
                if(l.Timestamp.Date == dt.Date)
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
