﻿using System;
using System.Threading;
using System.Net.Sockets;
using System.Collections;

namespace Server
{
    public static class HandleClinet
    {
        static TcpClient clientSocket;
        static string clNo;
        static Hashtable clientsList;

        public static void StartClient(TcpClient inClientSocket, string clineNo, Hashtable cList)
        {
            clientSocket = inClientSocket;
            clNo = clineNo;
            clientsList = cList;
            Thread ctThread = new Thread(DoChat);
            ctThread.Start();
        }

        private static void DoChat()
        {
            int requestCount = 0;
            byte[] bytesFrom = new byte[65536];
            string dataFromClient = null;
            string rCount = null;
            requestCount = 0;

            while (true)
            {
                try
                {
                    requestCount = requestCount + 1;
                    NetworkStream networkStream = clientSocket.GetStream();
                    networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                    dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                    dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
                    Console.WriteLine("From client - " + clNo + " : " + dataFromClient);
                    rCount = Convert.ToString(requestCount);

                    Program.Broadcast(dataFromClient, clNo, true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    break;
                }
            }
        }
    } 
}