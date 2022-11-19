using System;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Drawing;

namespace TCP_Test1
{
    internal class sendServer
    {
        enum Datatype
        {
            UTF8 = 0,
            PNG = 1
        }

        static int Port = 11000;

        internal static void StartServer()
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
                    byte[] buffer = new byte[1];

                    //netzwerkstream hört auf nachricht und schreibt diese in den puffer
                    stream.Read(buffer, 0, buffer.Length);

                    //emfangene bytes werden dekodiert
                    Datatype reqType = (Datatype)BitConverter.ToInt32(buffer, 0);

                    byte[] data = GetReturnData(reqType);



                    //antwort wird zurückgesendet:
                    sw.WriteLine();
                    sw.Flush();

                }
                catch (Exception e)
                {
                    Console.WriteLine($"Fehler: {e.Message}");
                }
            }
        }

        static byte[] GetReturnData(Datatype _type)
        {
            if (_type == Datatype.PNG)
                return GetBytesFromPNG();
            else if (_type == Datatype.UTF8)
                return Encoding.UTF8.GetBytes("e");
            else
                return null;
        }

        static byte[] GetBytesFromPNG()
        {
            byte[] data;

            Image image = Image.FromFile("bliss.png");

            using (var ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                data = ms.ToArray();
            }

            return data;
        }
    }
}
