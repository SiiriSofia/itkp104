using System;
using System.Net.Sockets;
using System.Text;

namespace Kaikuasiakas
{
    class Program
    {
        static void Main()
        {
            Socket soketti = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            soketti.Connect("localhost", 25000);
            String snd = Console.ReadLine();

            byte[] buffer = Encoding.UTF8.GetBytes(snd);

            soketti.Send(buffer);

            String vastaus = "";
            int count = 0;
            byte[] rec = new byte[1024];

            count = soketti.Receive(rec);

            vastaus = Encoding.UTF8.GetString(rec, 0, count);
            String[] osat = vastaus.Split(';');  // pilkotaan vastaus ; kohdalta
            Console.Write("Palvelin: {0}\r\nTeksti: {1}\r\n", osat[0], osat[1]);
            Console.ReadKey();
            soketti.Close();
        }
    }
}
