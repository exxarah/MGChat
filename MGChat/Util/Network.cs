using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace MGChat.Util
{
    public static class Network
    {
        public static string NetDataIn = "";
        public static string NetDataOut = "";
        
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

        public static void Send(List<NetInput> exports)
        {
            NetDataOut = JsonConvert.SerializeObject(exports);
        }

        public static List<NetInput> Receive()
        {
            List<NetInput> result = JsonConvert.DeserializeObject<List<NetInput>>(NetDataIn);
            return result;
        }

        public static void NetThread(object? o)
        {
            Debug.WriteLine("Connecting to server");
            byte[] bytes = new byte[1024];

            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 1272);
            
            Socket sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            
            Debug.WriteLine("Attempting to connect...");
            sender.Connect(remoteEP);  
            
            Debug.WriteLine("Socket connected to {0}",  
                sender.RemoteEndPoint.ToString());  

            while (true)
            {
                // send 2 server
                byte[] msg = Encoding.ASCII.GetBytes(NetDataOut);
                int bytesSent = sender.Send(msg);  
                
                // recieve from server
                int bytesRec = sender.Receive(bytes);
                // TODO: only output this if we recieved bytes and stuff you know how it is
                NetDataIn = Encoding.ASCII.GetString(bytes,0,bytesRec);
                //Debug.WriteLine("Echoed test = {0}", NetDataIn);
                
                //Thread.Sleep(35);
                //NetDataIn = NetDataOut;
            }
        }
    }
}