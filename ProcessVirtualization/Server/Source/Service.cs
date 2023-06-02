using Common;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Server
{
    public class Service : ILoad, IAudit
    {
        Storage.Storage storage;

        public Service()
        {
            string path = Environment.ExpandEnvironmentVariables(ConfigurationManager.AppSettings["data_dir"]);
            storage = new Storage.Storage(path);
        }

        public Audit GetLastAudit()
        {
            return storage.GetLastAudit();
        }

        public List<Load> ListLoads(DateTime dt)
        {
            List<Load> list = storage.Read(dt);
            if (list == null || list.Count == 0)
            {
                storage.Write(new Audit(0, DateTime.Now, AuditMessageType.Info, "Datum ne postoji"));
            }
            return list;
        }
    }
}
