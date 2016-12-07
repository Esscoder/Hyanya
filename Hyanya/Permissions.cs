using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Hyanya.Attributes {
    class RequireRPGAttribute : PreconditionAttribute {
        public async override Task<PreconditionResult> CheckPermissions(CommandContext context, 
            CommandInfo command, IDependencyMap map) {
            // Get the ID of the bot's owner
            var ownerId = (await map.Get<DiscordSocketClient>().GetApplicationInfoAsync()).Owner.Id;
            // If this command was executed by that user, return a success
            if (context.User.Id == ownerId)
                return PreconditionResult.FromSuccess();
            // Since it wasn't, fail
            else
                return PreconditionResult.FromError("You must be the owner of the bot to run this command.");
        }
    }
}
