using System;
using System.Net.Sockets;
using System.Net;
using System.Resources;
using System.IO;

namespace TCPPalvelin
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket Palvelin = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Loopback, 25000);
            Palvelin.Bind(iep);

            Palvelin.Listen(5);
            // while (true) {
            Socket Asiakas = Palvelin.Accept();

            IPEndPoint iap = (IPEndPoint) Asiakas.RemoteEndPoint;
            Console.WriteLine("Yhteys osoitteesta: {0} portista {1}", iap.Address, iap.Port);

            NetworkStream ns = new NetworkStream(Asiakas);
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);

            String rec = sr.ReadLine();

            sw.WriteLine("Arin Palvelin;" + rec);
            sw.Flush();
            Asiakas.Close();
            //} //while
            Console.ReadKey();
            Palvelin.Close();
        }
    }
}
