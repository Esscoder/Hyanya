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

namespace Discord_Bot {

    /* General Commands */
    public class InfoModule : ModuleBase {
        private DiscordSocketClient _client;

        public InfoModule(DiscordSocketClient client) {
            _client = client;
        }

        [Command("ping")]
        public async Task Ping() {
            await Context.Channel?.SendMessageAsync("```FIX\nPong!（˶′◡‵˶）```");
        }

        [Command("call")]
        public async Task Call([Remainder]string name) {
            Console.WriteLine(name);
            try {
                await _client.CurrentUser.ModifyAsync(u => u.Username = name);
                await Context.Channel.SendMessageAsync(
                    $"```FIXAlright, you can call me {name.ToString()} from now on! (〃▽〃)```");
            }
            catch (Discord.Net.HttpException ex)
            {
            	Console.WriteLine(ex.ToString());
                await Context.Channel.SendMessageAsync(
                    "```FIX\nI'm sorry! There was an error! (ᗒᗣᗕ)```");
            }
        }

        [Command("prefix")]
        public async Task Prefix(char prefix) {
            Configuration.Prefix = prefix;
            await Context.Channel.SendMessageAsync(
                $"Alright, use {prefix} whenever you want to talk to me! ❤");
        }

        [Command("clean")]
        public async Task Prefix(int num) {
            var msgs = ( await Context.Channel.GetMessagesAsync().Flatten() as IEnumerable<IMessage>);
            List<IMessage> botmsgs = msgs.Where(
                m => m.Author.DiscriminatorValue == _client.CurrentUser.DiscriminatorValue).ToList();

            await Context.Channel.DeleteMessagesAsync(botmsgs.GetRange(0, num));
            await Context.Channel.SendMessageAsync(
                $"```FIX\nSuccessfully removed {num} of my messages! (-ω-ゞ```");
        }

        [Command("quest")]
        public async Task Quest() {
            await Context.Channel.SendFileAsync("../externRes/questers.jpeg");
        }
    }

    [Group("help")]
    public class HelpModule : ModuleBase {
        public const String commandList =
        "```css\n" +
        "Mention me <prefix> to change my prefix!\n" +
        "[General Command List]\n" +
        "  .help    Display this message ^.~\n" +
        "  .ping    Pong! ^.^\n" +
        "  .prefix  Change my command prefix! (-ω-ゞ\n" +
        "  .clean   Delete my messages >.<\n" +
        "  .purge   Delete a number of messages (｀ω´)\n" +
        "  .call    Call me something different (⋟﹏⋞)\n" +
        "  " +
        "  " +
        "```";

        [Command(null)]
        public async Task Help() {
            await Context.Channel?.SendMessageAsync(
                $"```FIX\n{Context.User.Username}, check your messages! ^.~```");
            //IDMChannel dmChannel = await Context.User.CreateDMChannelAsync();
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
