using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.Configuration;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ChannelFactory<ILoad> load_factory = new ChannelFactory<ILoad>("LoadService");
            ChannelFactory<IAudit> audit_factory = new ChannelFactory<IAudit>("AuditService");

            ILoad load_channel = load_factory.CreateChannel();
            IAudit audit_channel = audit_factory.CreateChannel();
            while (true)
            {
                try
                {
                    Console.WriteLine("Izaberite opciju koju zelite da izvrsite:");
                    Console.WriteLine("1 - Pretraga merenja po datumu");
                    Console.WriteLine("2 - Izlaz iz aplikacije\n");
                    string izbor = Console.ReadLine();

                    if (izbor == "1")
                    {

                        Console.Write("Unesite datum u zadatom formatu (yyyy-mm-dd): ");
                        string inp = Console.ReadLine();

                        List<Load> list = load_channel.ListLoads(DateTime.Parse(inp));

                        if (list == null || list.Count == 0)
                        {
                            Console.WriteLine(audit_channel.GetLastAudit());
                            continue;
                        }

                        string path = Environment.ExpandEnvironmentVariables(ConfigurationManager.AppSettings["csv_save_dir"]);
                        StreamWriter sw = new StreamWriter(path + "\\" + inp + ".csv");
                        sw.WriteLine("Id, Timestamp, ForecastValue, MeasuredValue");
                        foreach (Load l in list)
                        {
                            sw.WriteLine(l);
                            Console.WriteLine(l);
                        }
                        sw.Close();
                        Console.WriteLine("Uspesno kreirana CSV datoteka sa putanjom: " + path + " i nazivom: " + inp + ".csv");

                    } else if(izbor == "2")
                    {
                        Console.WriteLine("Klijent se gasi");
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
