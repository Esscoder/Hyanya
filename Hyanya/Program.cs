using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program {
    // Convert our sync-main to an async main method
    static void Main(string[] args) {
        Hyanya.Bot bot = new Hyanya.Bot();
        bot.Start().GetAwaiter().GetResult();
    }
}