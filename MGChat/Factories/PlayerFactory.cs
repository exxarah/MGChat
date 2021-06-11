using System;
using System.Diagnostics;
using System.IO;
using MGChat.Components;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MGChat.Factories
{
    public static class PlayerFactory
    {
        public static string DataPath = "../../../Content/Data/";
        public static int CreatePlayer(string jsonPath)
        {
            int player;
            using (StreamReader file = File.OpenText(DataPath + jsonPath))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                var o = JToken.ReadFrom(reader);
                string json = o.ToString(Formatting.None);
                //Debug.WriteLine(json);

                player = ECS.Manager.Instance.CreateEntity(json);
            }
            
            Util.Events.Instance.NewPlayer(player);

            return player;
        }

        public static int CreateRemotePlayer(string jsonPath, Util.Network.NetInput input)
        {
            int remotePlayer = CreatePlayer(jsonPath);

            var remote = (RemoteInputComponent)ECS.Manager.Instance.Fetch<RemoteInputComponent>(remotePlayer)[0];
            remote.LastPosition = input.Position;
            remote.NewPosition = input.Position;
            remote.NetId = input.NetId;
            
            var info = (InformationComponent)ECS.Manager.Instance.Fetch<InformationComponent>(remotePlayer)[0];
            info.Name = input.NetId;

            return remotePlayer;
        }
    }
}