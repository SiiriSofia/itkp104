using System;
using System.IO;
using System.Net.Sockets;

namespace SMTPAsiakas
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                s.Connect("localhost", 25000);
            }
            catch (Exception e)
            {
                Console.Write("Virhe: " + e.Message);
                Console.ReadKey();
                return;
            }
            
            NetworkStream ns = new NetworkStream(s);
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);

            String email = "testi posti";

            Boolean on = true;
            String viesti = "";
            while (on)
            {
                // luetaan:
                viesti = sr.ReadLine();
                Console.WriteLine(viesti);
                String[] status = viesti.Split(' ');

                switch (status[0])
                {
                    case "220":
                        sw.WriteLine("HELO jyu.fi");
                        break;
                    case "250":
                        switch (status[1])
                        {
                            case "2.1.0":
                                sw.WriteLine("RCPT TO: sina");
                                break;
                            case "2.1.5":
                                sw.WriteLine("DATA");
                                break;
                            case "2.0.0":
                                sw.WriteLine("QUIT");
                                break;
                            default:
                                sw.WriteLine("MAIL FROM: mina");
                                break;
                        }
                        break;
                    case "221":
                        on = false;
                        break;
                    case "354":
                        sw.WriteLine(email);
                        sw.WriteLine(".");
                        break;
                    default:
                        Console.WriteLine("Virhe...");
                        sw.WriteLine("QUIT");
                        break;
                }
                sw.Flush();
            }

            Console.ReadKey();

            sw.Close();
            sr.Close();
            ns.Close();
            s.Close();
        }
    }
}