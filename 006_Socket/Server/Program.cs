using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections;

namespace Server
{
    class Program
    {
        public static Hashtable clientsList = new Hashtable();

        public static void Broadcast(string msg, string uName, bool flag)
        {
            foreach (DictionaryEntry Item in clientsList)
            {
                TcpClient broadcastSocket;
                broadcastSocket = (TcpClient)Item.Value;
                NetworkStream broadcastStream = broadcastSocket.GetStream();
                Byte[] broadcastBytes = null;

                if (flag == true)
                {
                    broadcastBytes = Encoding.ASCII.GetBytes(uName + " : " + msg);
                }
                else
                {
                    broadcastBytes = Encoding.ASCII.GetBytes(msg);
                }

                broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
                broadcastStream.Flush();
            }
        }

        static void Main(string[] args)
        {
            IPAddress iPAddress = IPAddress.Any;
            TcpListener serverSocket = new TcpListener(iPAddress, 8001);
            TcpClient clientSocket = default(TcpClient);
            int counter = 0;

            serverSocket.Start();
            Console.WriteLine("Chat Server Started ....");
            try
            {
                while (true)
                {
                    counter += 1;
                    clientSocket = serverSocket.AcceptTcpClient();

                    byte[] bytesFrom = new byte[65536];
                    string dataFromClient = null;

                    NetworkStream networkStream = clientSocket.GetStream();
                    networkStream.Read(bytesFrom, 0, clientSocket.ReceiveBufferSize);
                    dataFromClient = Encoding.ASCII.GetString(bytesFrom);
                    dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));

                    clientsList.Add(dataFromClient, clientSocket);

                    Broadcast(dataFromClient + " Joined ", dataFromClient, false);

                    Console.WriteLine(dataFromClient + " Joined chat ");
                    HandleClinet client = new HandleClinet();
                    client.StartClient(clientSocket, dataFromClient, clientsList);
                }
            }

            finally
            {
                clientSocket.Close();
                serverSocket.Stop();
                Console.WriteLine("Exit");
                Console.ReadKey();
            }
        }
    }
}