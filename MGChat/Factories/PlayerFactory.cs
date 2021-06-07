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
        public static int CreatePlayerJson(string jsonPath)
        {
            int player;
            using (StreamReader file = File.OpenText(jsonPath))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                var o = JToken.ReadFrom(reader);
                string json = o.ToString(Formatting.None);
                //Debug.WriteLine(json);

                player = ECS.Manager.Instance.CreateEntity(json);
            }

            return player;
        }
    }
}