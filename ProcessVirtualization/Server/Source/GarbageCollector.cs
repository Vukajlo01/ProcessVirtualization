using System;
using System.Configuration;

namespace Server
{
    public class GarbageCollector
    {
        public delegate void GarbageCollectedEventHandler(object source, EventArgs args);

        public event GarbageCollectedEventHandler GarbageCollected;

        public GarbageCollector() {
            int timeout_secs = int.Parse(ConfigurationManager.AppSettings["data_timeout"]);

            long now = DateTimeOffset.Now.ToUnixTimeSeconds();
            while (true)
            {
                if (DateTimeOffset.Now.ToUnixTimeSeconds() - now >= timeout_secs)
                {
                    now = DateTimeOffset.Now.ToUnixTimeSeconds();
                    GarbageCollected(this, EventArgs.Empty);
                }
            }
        }
    }
}
