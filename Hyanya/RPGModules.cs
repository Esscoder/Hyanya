using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;
using Discord.Commands;

using Hyanya.Attributes;

/*
 *[Command("embed")]
        public async Task embed() {
            await buildEmbed(Context.User);
        }

        private async Task buildEmbed(IUser user) {
            var builder = new EmbedBuilder()
                .WithColor(new Color(255, 255, 255))
                .WithAuthor(x => x.WithName(user.Username)
                    .WithIconUrl(user.AvatarUrl)
                    .WithUrl(""))
                .WithDescription("")
                .WithThumbnailUrl("")
                .WithFooter(x => x.WithText(user.Id.ToString()));

            builder = builder.AddField(x => x.WithName("")
            .WithValue(""));

            await this.ReplyAsync("", embed: builder);
        }
 */

namespace Hyanya.RPG {
    public enum RPGClass {
        WARRIOR,
        SPELLCASTER,
        ROGUE,
        BARD,
        PRIEST,

        KITSUNE
    }

    class RPGGeneral {
        
    }

    [Group("rpg.new")]
    public class NewModule : ModuleBase{

        [Command("warrior")]
        public async Task Warrior() {
            await Context.Channel.SendMessageAsync(
                "```FIX\nAlright! I'll step you through the process! (-ω-ゞ```");
        }

        [Command("spellcaster")]
        public async Task Spellcaster() {
            await Context.Channel.SendMessageAsync(
                "```FIX\nAlright! I'll step you through the process! (-ω-ゞ```");
        }

        [Command("rogue")]
        public async Task Rogue() {
            await Context.Channel.SendMessageAsync(
                "```FIX\nAlright! I'll step you through the process! (-ω-ゞ```");
        }

        [Command("bard")]
        public async Task Bard() {
            await Context.Channel.SendMessageAsync(
                "```FIX\nAlright! I'll step you through the process! (-ω-ゞ```");
        }

        [Command("priest")]
        public async Task Priest() {
            await Context.Channel.SendMessageAsync(
                "```FIX\nAlright! I'll step you through the process! (-ω-ゞ```");
        }

        [Command("custom")]
        public async Task Help() {
            await Context.Channel.SendMessageAsync(
                "```FIX\nI'm sorry! Custom classes aren't available yet! (⋟﹏⋞)");
        }

        [Command("Kitsune")]
        [RequireOwnerAttribute]
        public async Task Kitsune() {
            await Context.Channel.SendMessageAsync(
                "```FIX\nHello Ashe! Let me get you setup. Default setup?```");
        }
    }

    [Group("rpg.help")]
    public class HelpModule : ModuleBase {
        public const String commandList =
        "```css\n" +
        "Mention me <prefix> to change my prefix!\n" +
        "[General Command List]\n" +
        "  hya.rpg.help    Display this message ^.~\n" +
        "  \n" +
        "  \n" +
        "  \n" +
        "  \n" +
        "  \n" +
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
}
