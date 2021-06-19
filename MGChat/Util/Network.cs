using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace MGChat.Util
{
    public static class Network
    {
        public static string NetDataIn = "";
        public static string NetDataOut = "";

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

        public static void Send(List<NetInput> exports)
        {
            NetDataOut = JsonConvert.SerializeObject(exports);
        }

        public static List<NetInput> Receive()
        {
            List<NetInput> result = JsonConvert.DeserializeObject<List<NetInput>>(NetDataIn);
            //Debug.WriteLine(NetDataIn);
            return result;
        }

        public static void NetThread(object? o)
        {
            Debug.WriteLine("Connecting to server");
            byte[] bytes = new byte[1024];

            IPHostEntry ipHostInfo = Dns.GetHostEntry("home.ss23.geek.nz");  
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 1272);
            
            conn = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            
            Debug.WriteLine("Attempting to connect...");
            conn.Connect(remoteEP);  
            
            Debug.WriteLine("Socket connected to {0}",  
                conn.RemoteEndPoint.ToString());
            
            Thread.Sleep(1000);
            sendThread = Task.Factory.StartNew(Util.Network.SendThread, "sendThread");
            receiveThread = Task.Factory.StartNew(Util.Network.ReceiveThread, "receiveThread");
        }

        private static void SendThread(object? o)
        {
            while (true)
            {
                // send 2 server
                Thread.Sleep(16);
                byte[] msg = Encoding.ASCII.GetBytes(NetDataOut + "\n");
                int bytesSent = conn.Send(msg);
            }
        }

        private static void ReceiveThread(object? o)
        {
            while (true)
            {
                // recieve from server
                byte[] bytes = new byte[1024];
                int bytesRec = conn.Receive(bytes);
                // TODO: only output this if we recieved bytes and stuff you know how it is
                string dataIn = Encoding.ASCII.GetString(bytes,0,bytesRec);
                var dataList = dataIn.Split("\n");
                var singleLine = dataList[dataList.Length - 2];
                NetDataIn = singleLine;
                //Debug.WriteLine(NetDataIn);
            }
        }
    }
}