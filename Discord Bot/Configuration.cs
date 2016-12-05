using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot {
    public static class Configuration {
        public static char Prefix;
        public static string Game;
        public static string Version;

        public static void Load() {
            string[] text = File.ReadAllLines("../../externRes/config.config", Encoding.UTF8);
            Configuration.Prefix = text[0].Split(':')[1][0];
            Configuration.Game = text[1].Split(':')[1];
            Configuration.Version = text[2].Split(':')[1];
            Console.WriteLine("Configuration loaded:"+
                $"\n  Prefix: {Configuration.Prefix}" +
                $"\n  Game: {Configuration.Game}" +
                $"\n  Version: {Configuration.Version}");
        }
    }
}
