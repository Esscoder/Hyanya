using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program {
    // Convert our sync-main to an async main method
    static void Main(string[] args) {
        Discord_Bot.Bot bot = new Discord_Bot.Bot();
        bot.Start().GetAwaiter().GetResult();
    }
}