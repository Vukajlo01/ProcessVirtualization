using Common;
using System;
using System.Collections.Generic;

namespace Storage
{
    public class Storage : IStorage
    {
        InMemory inMemory;
        XML xml;

        public Storage(string path)
        {
            inMemory = new InMemory();
            xml = new XML(path + "\\LOAD.xml", path + "\\AUDIT.xml");
        }

        public Audit GetLastAudit()
        {
            if (inMemory.audits.Count == 0) return xml.GetLastAudit();
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
                    inMemory.loads[load.Id] = load;
                }
            }
            return loads;
        }

        public void Write(Audit audit)
        {
            xml.Write(audit);
            inMemory.Write(audit);
        }
    }
}
