using System;
using System.Runtime.Serialization;

namespace Common
{
    public enum AuditMessageType
    {
        Info,
        Warning,
        Error
    }

    [DataContract]
    public class Audit
    {
        public Audit()
        {
        }

        public Audit(int id, DateTime timestamp, AuditMessageType messageType, string message)
        {
            Id = id;
            Timestamp = timestamp;
            MessageType = messageType;
            Message = message;
        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public DateTime Timestamp { get; set; }
        [DataMember]
        public AuditMessageType MessageType { get; set; }
        [DataMember]
        public string Message { get; set; }

        public override string ToString()
        {
            return Id + ", " + Timestamp + ", " + MessageType + ", " + Message;
        }
    }
}
