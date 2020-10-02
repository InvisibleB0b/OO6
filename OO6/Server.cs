using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FanLibrary;
using Newtonsoft.Json;

namespace OO6
{
    class Server
    {

        public static void Start()
        {

            try
            {
                TcpListener server = null;

                IPAddress localAddress = null;
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        Console.WriteLine(ip.ToString());

                        localAddress = IPAddress.Parse(ip.ToString());

                    }
                }

                int port = 4646;


                server = new TcpListener(IPAddress.Loopback, port);

                server.Start();

                Console.WriteLine("Waiting for a connection........");




                while (true)
                {


                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    Task.Run(() =>
                    {
                        TcpClient tempSocket = client;
                        DoClient(tempSocket);
                    });


                }

                server.Stop();
                Console.WriteLine("server stopped");




            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


        }

        public static void DoClient(TcpClient socket)
        {

            List<FanOutput> fanlist = new List<FanOutput>() { new FanOutput() { Id = 1 }, new FanOutput() { Id = 2 } };

            Stream ns = socket.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true;

            sw.WriteLine("You are connected!!!");


            while (true)
            {



                string message = sr.ReadLine();

                if (message.ToLower().Contains("luk"))
                {
                    break;
                }

                switch (message.ToLower())
                {
                    case "hentalle":

                        string data = JsonConvert.SerializeObject(fanlist);
                        sw.WriteLine(data);

                        break;

                    case "hent":
                        string meassage2 = sr.ReadLine();

                        int idet = Convert.ToInt32(meassage2);

                        FanOutput fan = fanlist.Find(f => f.Id == idet);
                        string data2 = JsonConvert.SerializeObject(fan);

                        sw.WriteLine(data2);
                        break;

                    case "gem":

                        string fanstring = sr.ReadLine();
                        FanOutput fan2 = JsonConvert.DeserializeObject<FanOutput>(fanstring);
                        fanlist.Add(fan2);

                        break;

                    default:
                        sw.Write("Please select method");
                        break;

                }




            }
            sw.WriteLine("Luk");

            ns.Close();

            Console.WriteLine("net stream closed");

            socket.Close();
            Console.WriteLine("client closed");
        }
    }
}
