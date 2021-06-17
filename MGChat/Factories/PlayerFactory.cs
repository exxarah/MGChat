using System;
using System.Diagnostics;
using System.IO;
using MGChat.Components;
using MGChat.Util;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MGChat.Factories
{
    public static class PlayerFactory
    {
        public static string DataPath = ScreenManager.ContentMgr.RootDirectory + "/Data/Actors/";

        public static int CreateRemotePlayer(string jsonPath, Util.Network.NetInput input)
        {
            string data = FileParser.ReadJson(DataPath + jsonPath);
            int remotePlayer = ECS.Manager.Instance.CreateEntity(data);
            Events.Instance.NewPlayer(remotePlayer);

            var remote = (RemoteInputComponent)ECS.Manager.Instance.Fetch<RemoteInputComponent>(remotePlayer)[0];
            remote.LastPosition = input.Position;
            remote.NewPosition = input.Position;
            remote.NetId = input.NetId;
            
            var info = (InformationComponent)ECS.Manager.Instance.Fetch<InformationComponent>(remotePlayer)[0];
            info.Name = input.NetId;

            return remotePlayer;
        }

        public static int CreateLocalPlayer(string jsonPath, string name)
        {
            string data = FileParser.ReadJson(DataPath + jsonPath);
            int localPlayer = ECS.Manager.Instance.CreateEntity(data);
            Events.Instance.NewPlayer(localPlayer);

            var export = (RemoteExportComponent) ECS.Manager.Instance.Fetch<RemoteExportComponent>(localPlayer)[0];
            export.NetId = name;
            
            var info = (InformationComponent)ECS.Manager.Instance.Fetch<InformationComponent>(localPlayer)[0];
            info.Name = name;

            return localPlayer;
        }
    }
}