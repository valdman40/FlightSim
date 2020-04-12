using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FilghtSim.telnet
{
    public class MyTelnetClient : ITelnetClient
    {
        private System.Net.Sockets.TcpClient client;
        private NetworkStream stream;
        public MyTelnetClient() { }

        public void connect(string ip, int port)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
            client = new TcpClient();


            while (!isConnected())
            {
                try
                {
                    client.Connect(ep);
                }
                catch (Exception e)
                {
                }
            }

            Console.WriteLine("You are connected");
            this.stream = client.GetStream();
        }

        public void disconnect()
        {
            if (client != null)
            {
                client.Close();
            }
        }

        public bool isConnected()
        {
            return client != null && client.Connected;
        }

        public string read()
        {
            byte[] bytes = new byte[2048];
            this.stream.Read(bytes, 0, 2048);
            string a = Encoding.ASCII.GetString(bytes);
            return a;

        }

        public void write(string command)
        {
            // command += "\r\n";
            byte[] bytes = Encoding.ASCII.GetBytes(command);
            this.stream.Write(bytes, 0, bytes.Length);
        }
    }
}