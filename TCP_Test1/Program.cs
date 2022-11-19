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

            //SimpleServer.StartServer();
            sendServer.StartServer();

            Console.ReadKey();

        }

     
    }
}
