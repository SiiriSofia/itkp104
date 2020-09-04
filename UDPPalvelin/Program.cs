using System;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Text.Unicode;
using System.Text;

namespace UDPPalvelin
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket palvelin = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            int port = 9999;
            IPEndPoint iep = new IPEndPoint(IPAddress.Loopback, port);
            
            palvelin.Bind(iep);

            byte[] rec = new byte[1024];
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint senderRemote = (EndPoint)sender;
            int count = palvelin.ReceiveFrom(rec, ref senderRemote);
            String rec_string = Encoding.UTF8.GetString(rec, 0, count);
            String teksti = ("Arin Palvelin;" + rec_string);
            byte[] snd = Encoding.UTF8.GetBytes(teksti);

            palvelin.SendTo(snd, sender);

            Console.ReadKey();
            palvelin.Close();
        }
    }
}

