using MGChat.Components;
using MGChat.Util;

namespace MGChat.Factories
{
    public static class DecorationFactory
    {
        public static string DataPath = ScreenManager.ContentMgr.RootDirectory + "/Data/Decorations/";

        public static int CreateBush()
        {
            string path = "Bush.json";
            string data = FileParser.ReadJson(DataPath + path);
            int bush = ECS.Manager.Instance.CreateEntity(data);

            return bush;
        }
    }
}