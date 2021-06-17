using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using MGChat.TileMap;
using MGChat.Util;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MGChat.Factories
{
    public static class TileMapFactory
    {
        public static string MapPath = ScreenManager.ContentMgr.RootDirectory + "/Data/Map/";
        public static string TilePath = ScreenManager.ContentMgr.RootDirectory + "/Data/Tiles/";
        public static string SpriteSheet = "EnvironmentArt/TS_Dirt";

        public static Tile LoadTile(int type)
        {
            switch (type)
            {
                case 0: // Grass
                    return LoadTile("GrassTile.json");
                case 1: // Dirt
                    return LoadTile("DirtTile.json");
            }

            return null;
        }

        public static Tile LoadTile(string path)
        {
            Tile tile;
            using (StreamReader file = File.OpenText(TilePath + path))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                var o = JToken.ReadFrom(reader);
                string json = o.ToString(Formatting.None);

                tile = JsonConvert.DeserializeObject<Tile>(json, new JsonSerializerSettings()
                {
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                    TypeNameHandling = TypeNameHandling.Auto
                });
            }

            return tile;
        }

        public static TileMap.TileMap LoadMapCsv(string path, int cellWidth, int cellHeight)
        {
            List<string[]> csvValue = new List<string[]>();
            using (var reader = new StreamReader(MapPath + path))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line != null)
                    {
                        var values = line.Split(',');
                        csvValue.Add(values);
                    }
                }
            }

            int width = csvValue[0].Length;
            int height = csvValue.Count;
            TileMap.TileMap map = new TileMap.TileMap(width, height, new Vector2(cellWidth, cellHeight), 0, 0, SpriteSheet);

            for (int xIndex = 0; xIndex < width; xIndex++)
            {
                for (int yIndex = 0; yIndex < height; yIndex++)
                {
                    int value;
                    try
                    {
                        value = Int32.Parse(csvValue[xIndex][yIndex]);
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine($"Invalid CSV {csvValue[xIndex][yIndex]} is not an int");
                        Debug.WriteLine(e);
                        throw;
                    }

                    map.ChangeTile(xIndex, yIndex, LoadTile(value));
                }
            }

            return map;
        }

        // Json is only for chunks, can base off that assumption
        public static TileMap.TileMap LoadMapJson(string path)
        {
            return null;
        }
    }
}