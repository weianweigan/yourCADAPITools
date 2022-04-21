using System.Collections.Generic;
using System.IO;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using System.Linq;

namespace yourCADAPITools
{
    public static class RevitInfoManager
    {
        private static List<RevitInfo> deserializeObject;

        public static int Version { get; internal set; } = 2022;

        public static void Init()
        {
            if (deserializeObject != null)
                return;

            LoadFromDirectory();
            //LoadFromDirectory();
        }

        private static void LoadFromDirectory()
        {
            var directory = Path.GetDirectoryName(typeof(RevitUrlNavigation).Assembly.Location);

            string filepath = $"{directory}\\Resources\\RevitAPI2022.json";

            if (!File.Exists(filepath))
            {
                throw new FileNotFoundException($"{filepath} Not Found");
            }

            using (StreamReader r = new StreamReader(filepath))
            {
                string json = r.ReadToEnd();
                deserializeObject = JsonConvert.DeserializeObject<List<RevitInfo>>(json);
            }
        }

        public static RevitInfo Find(string apiName)
        {
            Init();
            string nameSp = "System.Collections.Generic.ICollection";
            if (apiName.Contains(nameSp))
            {
                apiName = apiName.Split('`').First();
            }
            return deserializeObject.FirstOrDefault(x => x.APIName == apiName);
        }

        public static string FindLink(string apiName)
        {
            Init();
            var revitInfo = Find(apiName);
            if (revitInfo == null)
            {
                throw new KeyNotFoundException(apiName);
            }
            else
            {
                return revitInfo.GetUrl(Version.ToString());
            }
        }
    }
}
