using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace MGChat.Util
{
    public class Network
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
            int yPos = 0;
            while (true)
            {
                Thread.Sleep(35);
                NetDataIn = NetDataOut;
                yPos++;
            }
        }
    }
}