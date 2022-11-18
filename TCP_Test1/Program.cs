using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TCP_Test1
{
    internal class Program
    {
        //Beginn-Methode
        static void Main(string[] args)
        {
            Console.Title = "TCP Server v0.1";

            StartServer();

            Console.ReadKey();

        }

        static int Port = 11000;

        static void StartServer()
        {
            TcpListener listener = new TcpListener(IPAddress.IPv6Any, Port);
            listener.Start();
            Console.WriteLine($"Server Online on port: {Port}");

            while (true)
            {
                Console.WriteLine("Waiting for connection");

                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("Client Accepted.");

                var stream = client.GetStream();
                var sr = new StreamReader(stream);
                var sw = new StreamWriter(stream);

                try
                {
                    //puffer, in dem die empfange nachricht als bytearray gespeichert wird
                    byte[] buffer = new byte[1024];

                    //netzwerkstream hört auf nachricht und schreibt diese in den puffer
                    stream.Read(buffer, 0, buffer.Length);

                    int msgLen = 0;
                    foreach (byte _b in buffer)
                    {
                        if(_b != 0)
                            msgLen++;
                    }

                    //emfangene bytes werden in string dekodiert
                    string recievedData = Encoding.UTF8.GetString(buffer, 0, msgLen);
                    //und ausgegeben
                    Console.WriteLine($"Empfangen: {recievedData}");

                    //antwort wird zurückgesendet:
                    sw.WriteLine("Empfangen");
                    sw.Flush();
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Fehler: {e.Message}");
                }
            }
        }
    }
}
