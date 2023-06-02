using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(Service)))
            {
                ServiceDebugBehavior debug = host.Description.Behaviors.Find<ServiceDebugBehavior>();
                if (debug == null)
                {
                    host.Description.Behaviors.Add(
                         new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });
                }
                else
                {
                    if (!debug.IncludeExceptionDetailInFaults)
                    {
                        debug.IncludeExceptionDetailInFaults = true;
                    }
                }

                host.Open();
                Console.WriteLine("Service is running.");

                Thread thread = new Thread(Proveri);
                thread.IsBackground = true;
                thread.Start();
                

                Console.ReadKey();
            }
        }

        public static void Proveri()
        {
            while (true)
            {
                Storage.Storage.Delete();
                Thread.Sleep(1000);
            }
        }
    }
}
