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
        public static int CreatePlayer(Game1 game)
        {
            string spriteSheet = "Char_One";
            Texture2D texture = game.Content.Load<Texture2D>("Char_One");

            int player = ECS.Manager.Instance.CreateEntity();
            new SpriteComponent(player,spriteSheet, 8, 6);
            new AnimatedSpriteComponent(player, 8, 6);
            //new SpriteStateComponent(player, "Idle_Down", "Idle_Left", "Idle_Right", "Idle_Up", "Walk_Down", "Walk_Left", "Walk_Right", "Walk_Up");
            new TransformComponent(player, 100, 100);
            new InputComponent(player);
            new MovableComponent(player, 0.25f);
            new CommandComponent(player);

            return player;
        }

        public static int CreatePlayerJson(string jsonPath)
        {
            int player;
            using (StreamReader file = File.OpenText(jsonPath))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                var o = JToken.ReadFrom(reader);
                string json = o.ToString(Formatting.None);
                Debug.WriteLine(json);

                player = ECS.Manager.Instance.CreateEntity(json);
            }

            return player;
        }
    }
}