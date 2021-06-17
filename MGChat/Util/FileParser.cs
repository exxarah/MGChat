using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MGChat.Util
{
    public static class FileParser
    {
        public static string ReadJson(string path)
        {
            string value;
            using (StreamReader file = File.OpenText(path))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                var o = JToken.ReadFrom(reader);
                string json = o.ToString(Formatting.None);
                //Debug.WriteLine(json);

                value = json;
            }

            return value;
        }
    }
}