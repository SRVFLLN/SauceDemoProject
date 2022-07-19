using System.Xml;

namespace SauceDemoProject
{
    public static class ConfigTool
    {
        private static readonly XmlDocument Config = new XmlDocument();

        private const string filename = "Resources/Config.xml";

        public static string GetTagValue(string nodeName)
        {
            try
            {
                Logger.Info($"Getting {nodeName} from config file: {filename}...");
                Config.Load(filename);
                return Config.SelectSingleNode($"config/{nodeName}").InnerText;
            }
            catch (System.IO.FileNotFoundException)
            {
                Logger.Error($"File with path {filename} not found!");
                return null;
            }
        }
    }
}