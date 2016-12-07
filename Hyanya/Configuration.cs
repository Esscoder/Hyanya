using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using System.Collections.Concurrent;

using Hyanya.RPG;
namespace Hyanya {
    public struct PlayerData {
        public ulong ID;
        public string name;
        public RPGClass rpgClass;
        public int level;
        public int experience;
    }
    public struct Server {
        public Server(ulong id) {
            ID = id;
            Prefix_general = "undef";
            Game = "undef";
            Version = "undef";

            players = new ConcurrentDictionary<ulong, PlayerData>();
        }
        public ulong ID { get; internal set; }
        public string Prefix_general { get; set; }
        public string Game { get; set; }
        public string Version { get; internal set; }

        public ConcurrentDictionary<ulong, PlayerData> players;

        public int Fail() {
            if(ID == 0) return -1;
            if (Prefix_general == "undef") return -2;
            if (Game == "undef") return -3;
            if (Version == "undef") return -4;

            return 0;
        }

        public int loadPlayer(ref XmlReader xmlReader) {
            var player = new PlayerData();
            if(!(UInt64.TryParse(xmlReader.GetAttribute("id"), out player.ID))) {
                Console.WriteLine($"ID: {xmlReader.GetAttribute("id")}");
                return -1;
            }
            player.name = xmlReader.GetAttribute("name");
            if (!(Enum.TryParse<RPGClass>(xmlReader.GetAttribute("class"), out player.rpgClass))) {
                Console.WriteLine($"class: {xmlReader.GetAttribute("class")}");
                return -1;
            }
            if (!(Int32.TryParse(xmlReader.GetAttribute("level"), out player.level))) {
                Console.WriteLine($"level: {xmlReader.GetAttribute("level")}");
                return -1;
            }
            if (!(Int32.TryParse(xmlReader.GetAttribute("exp"), out player.experience))) {
                Console.WriteLine($"experience: {xmlReader.GetAttribute("exp")}");
                return -1;
            }
            players[player.ID] = player;
            return 0;
        }
    }

    public static class Configuration { 

        public static ConcurrentDictionary<ulong, Server> activeServers = new ConcurrentDictionary<ulong, Server>();

        public static void Load(ulong guildID) {
            activeServers[guildID] = new Server(guildID);
            var server = activeServers[guildID];

            XmlReader xmlReader = XmlReader.Create(File.OpenRead($"res/servers/{guildID}.xml"));
            while (xmlReader.Read()) {
                if (xmlReader.NodeType == XmlNodeType.Element) {
                    switch (xmlReader.Name) {
                        case "GeneralPrefix":
                            server.Prefix_general = xmlReader.ReadElementContentAsString();
                            Console.WriteLine(server.Prefix_general);
                            break;
                        case "Game":
                            server.Game = xmlReader.ReadElementContentAsString();
                            Console.WriteLine(server.Game);
                            break;
                        case "Version":
                            server.Version = xmlReader.ReadElementContentAsString();
                            Console.WriteLine(server.Version);
                            break;
                        case "player":
                            if(server.loadPlayer(ref xmlReader) == -1) {
                                Console.WriteLine($"Failed to parse player {xmlReader.GetAttribute("id")}");
                            }
                            break;
                        case "server":
                        case "configuration":
                        case "players":
                            break;
                        default:
                            Console.WriteLine($"Unknown node {xmlReader.Name}");
                            break;

                    }
                }
            }
            activeServers[guildID] = server;
            if (activeServers[guildID].Fail() != 0) {
                Console.WriteLine($"Failed to load configuration and players for {guildID}: {activeServers[guildID].Fail()}");
            }
            else Console.WriteLine($"Successfully loaded configuration and players for {guildID}");
        }

        
    }
}
