using Newtonsoft.Json;
using System.Configuration;
using System.Globalization;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config")]

namespace HundirLaFlota
{
    internal class Program
    {
        public static ResourceManager? rm;
        static void Main(string[] args)
        {
            string json = File.ReadAllText("config.json");
            Config config = JsonConvert.DeserializeObject<Config>(json);
            if(config == null)
            {
                throw new InvalidDataException();
            }

            rm = new ResourceManager(config.Lang);

            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                Console.SetWindowSize(67, 40);

            Game game = new Game();
            game.Launch();
        }
    }
}