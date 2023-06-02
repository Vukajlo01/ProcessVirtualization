using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Storage
{
    public class XML : IStorage
    {
        string load_path;
        string audit_path;

        public XML(string load_path, string audit_path)
        {
            this.load_path = load_path;
            this.audit_path = audit_path;
        }

        public Audit GetLastAudit()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Audit>));

            if (!File.Exists(audit_path))
            {
                FileStream fs = new FileStream(audit_path, FileMode.OpenOrCreate);
                serializer.Serialize(fs, new List<Audit>());
                fs.Close();
            }

            StreamReader sr = new StreamReader(audit_path);
            List<Audit> list = (List<Audit>)serializer.Deserialize(sr);
            sr.Close();

            return list[list.Count - 1];
        }

        public List<Load> Read(DateTime dt)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Load>));

            if (!File.Exists(load_path))
            {
                FileStream fs = new FileStream(load_path, FileMode.OpenOrCreate);
                serializer.Serialize(fs, new List<Load>());
                fs.Close();
            }

            StreamReader sr = new StreamReader(load_path);
            List<Load> list = (List<Load>)serializer.Deserialize(sr);
            sr.Close();

            List<Load> newList = new List<Load>();

            foreach (Load l in list)
            {
                if (l.Timestamp.Date == dt.Date)
                {
                    newList.Add(l);
                }
            }

            return newList;
        }

        public void Write(Audit audit)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Audit>));

            if (!File.Exists(audit_path))
            {
                FileStream fs = new FileStream(audit_path, FileMode.OpenOrCreate);
                serializer.Serialize(fs, new List<Audit>());
                fs.Close();
            }

            StreamReader sr = new StreamReader(audit_path);
            List<Audit> list = (List<Audit>)serializer.Deserialize(sr);
            sr.Close();

            audit.Id = list.Count;
            list.Add(audit);

            StreamWriter sw = new StreamWriter(audit_path);
            serializer.Serialize(sw, list);
            sw.Close();
        }
    }
}
