using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPKaikuasiakas
{
    class Program
    {
        static void Main()
        {
            Socket soketti = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            int portti = 8888;
            IPEndPoint vastottaja = new IPEndPoint(IPAddress.Loopback, portti);
            String teksti = "kaiuettava teksti";
            byte[] snd = Encoding.UTF8.GetBytes(teksti);

            soketti.SendTo(snd, vastottaja);

            byte[] rec = new byte[1024];
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint senderRemote = (EndPoint)sender;
            int count = soketti.ReceiveFrom(rec, ref senderRemote);
           
            String vastaus = "";
            
            vastaus = Encoding.UTF8.GetString(rec, 0, count);

            String[] osat = vastaus.Split(';');  // pilkotaan vastaus ; kohdalta
            Console.Write("Palvelin: {0}\r\nTeksti: {1}\r\n", osat[0], osat[1]);

            Console.ReadKey();
            soketti.Close();
        }
    }
}
