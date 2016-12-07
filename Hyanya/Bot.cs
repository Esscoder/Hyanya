using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace Hyanya {
    public class Bot {
        public Bot() { }

        public async Task Start() {
            _client = new DiscordSocketClient(new DiscordSocketConfig() {
                LogLevel = LogSeverity.Info
            });
            _client.Log += (message) => {
                Console.WriteLine($"[{message.Severity}] {message.Source}: {message.Exception?.ToString() ?? message.Message}");
                return Task.CompletedTask;
            };

            handler = new CommandHandler();
            await handler.Load(_client);

            // Configure the client to use a Bot token, and use our token
            await _client.LoginAsync(TokenType.Bot, token);
            // Connect the client to Discord's gateway
            await _client.ConnectAsync();
            foreach (SocketGuild guild in _client.Guilds) {
                Configuration.Load(guild.Id);
                await _client.SetGame(Configuration.activeServers[guild.Id].Game);
            }

            // Block this task until the program is exited.
            await Task.Delay(-1);
        }

        //Vars\\
        const string token = "MjU0ODU5MTYzODUzNzgzMDQy.CyVLtw.aiq7KfbEshR6iGOd49jjlK51C1A";

        private DiscordSocketClient _client;
        CommandHandler handler;
    }
}
