using Common;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System;

namespace Storage
{
    public interface IStorage
    {
        List<Load> Read(DateTime dt);

        void Write(Audit audit);

        Audit GetLastAudit();
    }
}
