using Newtonsoft.Json;
using System.IO;

namespace Loria.Lib.Parser
{
    public static class AdventureParser
    {
        public static Adventure Parse(string path)
        {
            var fileContent = File.ReadAllText(path);
            var adventure = JsonConvert.DeserializeObject<Adventure>(fileContent);

            return adventure;
        }
    }
}
