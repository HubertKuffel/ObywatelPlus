using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ObywatelPlus
{
    public static class RejestracjaChecker
    {
        public static List<string> Rejestracje = new List<string>();
        public static void CheckerLoop()
        {
            while (true)
            {
                foreach (var rejestracja in Rejestracje)
                {
                    CheckRej(rejestracja);
                }
                Thread.Sleep(5000);
            }
        }

        public static void CheckRej(string rej)
        {
            var message = "check_rej;" + rej + ";";
            var client = new TcpClient();
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("149.56.102.192"), 5678);
            try
            {
                client.Connect(ipEndPoint);
                if (client.Connected)
                {
                    var reader = new StreamReader(client.GetStream());
                    var writer = new StreamWriter(client.GetStream());
                    writer.AutoFlush = true;

                    if (client.Connected)
                    {
                        writer.WriteLine(message);
                    }
                    var result = bool.Parse(reader.ReadLine());
                    if (result)
                    {
                        RejestracjaOznaczona?.Invoke(rej);
                    }
                }
                client.Dispose();
            }
            catch (Exception e)
            {
            }
        }

        public static void Alarm(string rejestracja)
        {
            var message = "new_rej;" + rejestracja + ";" + "opis;";
            var client = new TcpClient();
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("149.56.102.192"), 5678);
            try
            {
                client.Connect(ipEndPoint);
                if (client.Connected)
                {
                    var reader = new StreamReader(client.GetStream());
                    var writer = new StreamWriter(client.GetStream());
                    writer.AutoFlush = true;

                    if (client.Connected)
                    {
                        writer.WriteLine(message);
                    }
                }
                client.Dispose();
            }
            catch (Exception e)
            {
            }
        }

        public static event Action<string> RejestracjaOznaczona;
    }
}
