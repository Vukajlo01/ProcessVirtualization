using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Common
{
    [DataContract]
    public class Load
    {
        public Load()
        {
        }

        public Load(int id, DateTime timestamp, double forecastValue, double measuredValue)
        {
            Id = id;
            Timestamp = timestamp;
            ForecastValue = forecastValue;
            MeasuredValue = measuredValue;
        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [XmlIgnore]
        public DateTime Timestamp { get; set; }

        [XmlElement("Timestamp")]
        public string DummyTimestamp {
            get { return this.Timestamp.ToString("yyyy-MM-dd HH:mm"); }
            set { this.Timestamp = DateTime.Parse(value); }
        }

        [DataMember]
        public double ForecastValue { get; set; }
        [DataMember]
        public double MeasuredValue { get; set; }

        public override string ToString()
        {
            return Id + ", " + Timestamp + ", " + ForecastValue.ToString(new CultureInfo("en-US")) + ", " + MeasuredValue;
        }
    }
}