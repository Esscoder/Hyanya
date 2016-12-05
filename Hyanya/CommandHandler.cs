using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

using Discord_Bot.Services;

namespace Discord_Bot {
    public class CommandHandler {
        private DiscordSocketClient _client;
        private CommandService _cmds;
        private IDependencyMap map;

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

            if (!(message.HasCharPrefix(Configuration.Prefix, ref argPos) || 
                message.HasMentionPrefix(_client.CurrentUser, ref argPos))) return;
            // Create a Command Context
            var context = new CommandContext(_client, message);
            // Execute the command. (result does not indicate a return value, 
            // rather an object stating if the command executed succesfully)
            var result = await _cmds.ExecuteAsync(context, argPos, map);
        }
    }
}