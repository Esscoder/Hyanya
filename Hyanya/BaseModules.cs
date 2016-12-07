using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using Discord;
using Discord.WebSocket;
using Discord.Commands;

//var channel = (Context.Channel as SocketGuildChannel)

namespace Hyanya.General {

    /* General Commands */
    public class InfoModule : ModuleBase {
        private DiscordSocketClient _client;

        public InfoModule(DiscordSocketClient client) {
            _client = client;
        }

        [Command("ping")]
        public async Task Ping() {
            var message = await Context.Channel.SendMessageAsync("```FIX\nPong!（˶′◡‵˶）```");
            await message.ModifyAsync(m => m.Content = $"```FIX\nPong! {_client.Latency}ms（˶′◡‵˶）``` ");
        }

        [Command("personal.call")]
        [RequireOwnerAttribute]
        public async Task Call([Remainder]string name) {
            Console.WriteLine(name);
            try {
                await _client.CurrentUser.ModifyAsync(u => u.Username = name);
                await Context.Channel.SendMessageAsync(
                    $"```FIX\nAlright, you can call me {name.ToString()} from now on, Ashe! (〃▽〃)```");
            }
            catch (Discord.Net.HttpException ex) {
                Console.WriteLine(ex.ToString());
                await Context.Channel.SendMessageAsync(
                    "```FIX\nI'm sorry! There was an error! (ᗒᗣᗕ)```");
            }
        }

        [Command("prefix")]
        public async Task Prefix(string prefix) {
            var server = Configuration.activeServers[Context.Guild.Id];
            server.Prefix_general = prefix;
            await Context.Channel.SendMessageAsync(
                $"Alright, use {prefix} whenever you want to talk to me! ❤");
        }

        [Command("clean")]
        public async Task Prefix(int num) {
            var msgs = (await Context.Channel.GetMessagesAsync().Flatten() as IEnumerable<IMessage>);
            List<IMessage> botmsgs = msgs.Where(
                m => m.Author.DiscriminatorValue == _client.CurrentUser.DiscriminatorValue).ToList();

            await Context.Channel.DeleteMessagesAsync(botmsgs.GetRange(0, num));
            await Context.Channel.SendMessageAsync(
                $"```FIX\nSuccessfully removed {num} of my messages! (-ω-ゞ```");
        }

        [Command("omg")]
        public async Task OMG() {
            await Context.Channel.SendFileAsync("res/sealO.png");
        }
    }

    [Group("help")]
    public class HelpModule : ModuleBase {
        public const String commandList =
        "```css\n" +
        "Mention me <prefix> to change my prefix!\n" +
        "[General Command List]\n" +
        "  hya.help          Display this message ^.~\n" +
        "  hya.admin.help    Show admin commands ^.~" +
        "  hya.ping          Pong! ^.^\n" +
        "  hya.prefix        Change my command prefix! (-ω-ゞ\n" +
        "  hya.clean         Delete my messages >.<\n" +
        "  hya.purge         Delete a number of messages (｀ω´)\n" +
        "  hya.omg           OMG! (/□＼*)・゜\n" +
        "  " +
        "```";

        [Command(null)]
        public async Task Help() {
            await Context.Channel?.SendMessageAsync(
                $"```FIX\n{Context.User.Username}, check your messages! ^.~```");
            //var dmChannel = (await Context.User.CreateDMChannelAsync()) as IDMChannel;
            //await dmChannel.SendMessageAsync(commandList);
            await Context.Channel.SendMessageAsync(commandList);
        }
        [Command("clean")]
        public async Task Clean() {
            await Context.Channel.SendMessageAsync(
                "```FIX\nEnter in the number of my messages for me to remove and"
                + "I'll remove them as long as they are within 100 messages! ^.^```");
        }
    }
    /////////////////////////////////////////////////////////////////
}
