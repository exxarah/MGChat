using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MGChat.Commands;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace MGChat.Util
{
    public static class Network
    {
        public static string NetDataIn = "";
        public static string NetDataOut = "";
        
        public static ConcurrentQueue<Command> CommandsOut = new ConcurrentQueue<Command>();
        public static ConcurrentQueue<Command> CommandsIn = new ConcurrentQueue<Command>();

        private static string partialReceiveData;
        private static Socket conn;
        private static Task sendThread;
        private static Task receiveThread;
        
        public struct NetInput
        {
            public string NetId;
            public Vector2 Position;

            public NetInput(string netId, Vector2 position)
            {
                NetId = netId;
                Position = position;
            }
        }

        public static void SendCommand(Command command)
        {
            CommandsOut.Enqueue(command);   
        }

        public static List<Command> ReceiveCommands()
        {
            List<Command> allCommands = new List<Command>();
            while (CommandsIn.TryDequeue(out var command))
            {
                allCommands.Add(command);
            }

            //Debug.WriteLine(allCommands.Count);
            return allCommands;
        }

        public static void NetThread(object? o)
        {
            Debug.WriteLine("Connecting to server");
            byte[] bytes = new byte[1024];

            IPHostEntry ipHostInfo = Dns.GetHostEntry("home.ss23.geek.nz");  
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            //IPAddress ipAddress = IPAddress.Loopback;
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 1272);
            
            conn = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            conn.NoDelay = true;
            
            Debug.WriteLine("Attempting to connect...");
            conn.Connect(remoteEP);  
            
            Debug.WriteLine("Socket connected to {0}",  
                conn.RemoteEndPoint.ToString());
            
            Thread.Sleep(1000);
            
            // After we connect, we need to register ourselves with the server. By enqueing the command first, it will be sent first.
            var connectCommand = new ServerConnectCommand(ScreenManager.LocalPlayerName, Vector2.Zero);
            CommandsOut.Enqueue(connectCommand);
            
            sendThread = Task.Factory.StartNew(Util.Network.SendThread, "sendThread");
            receiveThread = Task.Factory.StartNew(Util.Network.ReceiveThread, "receiveThread");
        }

        private static void SendThread(object? o)
        {
            while (true)
            {
                // send 2 server
                Thread.Sleep(2);
                List<Command> commandsToSend = new List<Command>();
                Command mostRecentPosition = null;
                Command queuedCommand;
                while (CommandsOut.TryDequeue(out queuedCommand))
                {
                    if (queuedCommand is SetPositionCommand)
                    {
                        mostRecentPosition = queuedCommand;
                        continue;
                    }

                    commandsToSend.Add(queuedCommand);
                }

                if (mostRecentPosition != null)
                {
                    commandsToSend.Add(mostRecentPosition);
                }
                foreach (var commandToSend in commandsToSend)
                {
                    // Send the command for real now
                    string commandString = JsonConvert.SerializeObject(commandToSend, new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.All,
                    });
                    byte[] msg = Encoding.ASCII.GetBytes(commandString + "\n");
                    int bytesSent = conn.Send(msg);
                }

            }
        }

        private static void ReceiveThread(object? o)
        {
            while (true)
            {
                // recieve from server
                byte[] bytes = new byte[1024];
                int bytesRec = conn.Receive(bytes);
                if (bytesRec == 0) continue;

                // We need to buffer the data we receive in case it is fragmented
                partialReceiveData += Encoding.ASCII.GetString(bytes,0,bytesRec);
                if (partialReceiveData.Length > 100000)
                {
                    Debug.WriteLine("Server sent us too much data at once!!!!");
                    Debug.WriteLine(partialReceiveData);
                    // TODO: Handle disconnection from server more gracefully
                    return;
                }
                var dataList = partialReceiveData.Split("\n");
                partialReceiveData = dataList[^1];
                //Debug.WriteLine("Length of commands:");
                //Debug.WriteLine(dataList.Length);
                foreach (var s in dataList[0..^1])
                {
                    ParseReceivedCommand(s);
                }
            }
        }

        private static void ParseReceivedCommand(string command)
        {
            var parsedCommand = JsonConvert.DeserializeObject<Command>(command, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
            CommandsIn.Enqueue(parsedCommand);
        }
    }
}