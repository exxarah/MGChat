using MGChat.Util;

namespace MGChat.Factories
{
    public static class TileFactory
    {
        public static string MapPath = ScreenManager.ContentMgr.RootDirectory + "/Data/Map/";
        public static string TilePath = ScreenManager.ContentMgr.RootDirectory + "/Data/Tiles/";
        public static string SpriteSheet = "EnvironmentArt/TS_Dirt";

        public static int LoadTile(int type)
        {
            switch (type)
            {
                case 0: // Grass
                    return LoadTile("GrassTile.json");
                case 1: // Dirt
                    return LoadTile("DirtTile.json");
            }

            return 0;
        }

        public static int LoadTile(string path)
        {
            string data = FileParser.ReadJson(TilePath + path);
            int tile = ECS.Manager.Instance.CreateEntity(data);

            return tile;
        }
    }
}