using System;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Serwer
{

    class TcpHelper
    {
        private static TcpListener listener { get; set; }
        private static bool accept { get; set; } = false;
        public static List<Rej> rejList;
        public static List<Destruction> desList;


        public static void StartServer(int port)
        {
            IPAddress address = IPAddress.Any;
            listener = new TcpListener(address, port);

            listener.Start();
            accept = true;
            rejList = new List<Rej>();
            desList = new List<Destruction>();
            Console.WriteLine($"Server started. Listening to TCP clients at {port}");
        }
        public static void Send(TcpClient client, string message)
        {
            byte[] data = Encoding.ASCII.GetBytes(message);
            client.GetStream().Write(data, 0, data.Length);
        }

        public static void Listen()
        {
            if (listener != null && accept)
            {

                // Continue listening.  
                while (true)
                {
                    Console.WriteLine("Waiting for client...");
                    var clientTask = listener.AcceptTcpClientAsync(); // Get the client  

                    if (clientTask.Result != null)
                    {
                        Console.WriteLine("Client connected. Waiting for data.");
                        var client = clientTask.Result;
                        string message = "";

                        while (message != null && !message.StartsWith("quit"))
                        {
                            byte[] buffer = new byte[1024];
                            client.GetStream().Read(buffer, 0, buffer.Length);

                            message = Encoding.ASCII.GetString(buffer);
                            Console.WriteLine(message);
                            string qResult = "query has a wrong syntax";
                            if (message.StartsWith("new_rej"))
                            {
                                var splitM = message.Split(";");
                                rejList.Add(new Rej(splitM[1], splitM[2]));
                                qResult = "Dodano rejestracje do bazy";
                            }
                            if (message.StartsWith("check_rej"))
                            {
                                var splitM = message.Split(";");
                                var rejestracja = rejList.FirstOrDefault(rej => rej.RejNumber == splitM[1]);
                                if (rejestracja != null)
                                {
                                    rejList.Remove(rejestracja);

                                    qResult = "true";
                                }
                                else
                                {
                                    qResult = "false";
                                }
                            }
                            
                            if (message.StartsWith("new_des")) // syntax of query: new_des;Picture.jpg;GPSCoordinates;Description;
                            {
                                var splitM = message.Split(";");
                                desList.Add(new Destruction(splitM[1], splitM[2], splitM[3]));
                                Console.WriteLine("Destruction report added to dB " + splitM[1]+" "+splitM[2] + " " + splitM[3]);
                                qResult = "true";
                            }
                            else
                            {
                                qResult = "false";
                            }
                            Send(client, qResult);
                            Console.WriteLine("Closing connection.");
                            client.GetStream().Dispose();
                            message = null;
                        }
                    }
                }
            }
        }
    }
}
