using System;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;


using Discord;
using Discord.Commands;
using Discord.WebSocket;

using Microsoft.Extensions.DependencyInjection;

namespace MonikaBot
{
    class Program
    {

        private DiscordSocketClient _client;
        private CommandHandler _handler;
        private string _token;
        private string _botFilesPath;

        static void Main(string[] args)
        => new Program().StartAsync().GetAwaiter().GetResult();

        public async Task StartAsync()
        {
            Support.StartupOperations();
            _token = Support.config.Get("botToken");
            _botFilesPath = Support.config.Get("botFilesPath");

            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose
            });

            await _client.LoginAsync(TokenType.Bot, _token);
            await _client.StartAsync();
            await _client.SetGameAsync("I just give out disclaimers lol");

            _handler = new CommandHandler();

            await _handler.InstallAsync(_client);
            _client.Log += (logMessages)
                       => Console.Out.WriteLineAsync(logMessages.ToString());

            await Task.Delay(-1);
        }
    }
}
