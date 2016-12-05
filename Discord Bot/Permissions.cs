using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Discord_Bot {
    class Permissions : PreconditionAttribute {
        public async override Task<PreconditionResult> CheckPermissions(CommandContext context, 
            CommandInfo command, IDependencyMap map) {

            var ownerId = (await map.Get<DiscordSocketClient>().GetApplicationInfoAsync()).Owner.Id;
            // If this command was executed by that user, return a success
            if (context.User.Id == ownerId)
                return PreconditionResult.FromSuccess();
            else
                return PreconditionResult.FromError("You must be the owner of the bot to run this command.");
        }
    }

}
