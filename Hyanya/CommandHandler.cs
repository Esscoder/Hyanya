using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

using Hyanya.Services;
using Hyanya.General;
using Hyanya.RPG;

namespace Hyanya {
    public enum State {

    }

    public class CommandHandler {
        private DiscordSocketClient _client;
        private CommandService _cmds;
        private IDependencyMap map;

        public static State _state;

        public CommandHandler() { }

        public async Task Load(DiscordSocketClient c) {
            // Hook the MessageReceived Event into our Command Handler
            _client = c;
            _client.MessageReceived += HandleCommand;
            _cmds = new CommandService(new CommandServiceConfig {
                CaseSensitiveCommands = true
            });
            map = new DependencyMap();
            // Discover all of the commands in this assembly and load them.
            map.Add(_client);
            map.Add(_cmds);
            await _cmds.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        public async Task HandleCommand(SocketMessage messageParam) {
            // Don't process the command if it was a System Message
            var message = messageParam as SocketUserMessage;
            if (message == null) return;

            int argPos = 0;

            var m_channel = message.Channel;
            if ((m_channel as ISocketPrivateChannel) != null) {
                //Private message
                Console.Write("Private: ");
                Console.WriteLine(message.Content);
            }
            else {
                //Guild Message
                var channel = message.Channel as IGuildChannel;
                if (channel == null) return;
                //GENERAL
                if (!(message.HasStringPrefix(Configuration.activeServers[channel.Guild.Id].Prefix_general, ref argPos) ||
                    message.HasMentionPrefix(_client.CurrentUser, ref argPos))) return;
                Console.Write("Guild: ");
                Console.WriteLine(message.Content);
                // Create a Command Context
                var context_general = new CommandContext(_client, message);
                // Execute the command. (result does not indicate a return value, 
                // rather an object stating if the command executed succesfully)
                var result = await _cmds.ExecuteAsync(context_general, argPos, map);
            }
        }
    }
}