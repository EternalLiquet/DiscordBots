using System;
using System.Reflection;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.IO;

namespace MonikaBot
{
    public class CommandHandler
    {
        private DiscordSocketClient _client;

        private CommandService _service;



        public async Task InstallAsync(DiscordSocketClient client)
        {
            this._client = client;

            this._service = new CommandService();

            await _service.AddModulesAsync(Assembly.GetEntryAssembly());


            _client.MessageReceived += HandleCommandAsync;

            _client.UserJoined += AnnounceUserJoined;
        }



        public async Task HandleCommandAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage;
            if (msg == null)
                return;

            var context = new SocketCommandContext(_client, msg);

            int argPos = 0;

            if (msg.HasCharPrefix('!', ref argPos))
            {
                var result = await _service.ExecuteAsync(context, argPos);

                if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                {
                    await context.Channel.SendMessageAsync(result.ErrorReason);
                }
            }
        }

        public async Task AnnounceUserJoined(SocketGuildUser user)
        {
            var guildName = user.Guild.Name;
            var _userDMChannel = await user.GetOrCreateDMChannelAsync();
            var filePath = Support.config.Get("botFilePath") + "JoinText.monikafile";

            if (File.Exists(filePath))
            {
                Console.WriteLine("File found!");
                StreamReader sr = new StreamReader(filePath);
                string joinText = sr.ReadLine();
                await _userDMChannel.SendMessageAsync($"Disclaimer for server {guildName}: {joinText}");
            }
            else
            {
                Console.WriteLine();
                await _userDMChannel.SendMessageAsync($"Disclaimer for server {guildName}: This Discord server is not to be affiliated with any company so no one should be bringing up HR since this is a place to feel free to just do whatever and foster a sense of community hopefully making new friendships, making plans, making smaller communities based on shared likes, etc.  no one is forcing anyone to be here, and if you don't feel comfortable here you can speak up about it, or leave the community, all choices are up to you.  I also have set up hidden communities where if you want access, let us know and it can just as easily be taken away if you don't even want those to be seen by you anymore.");
            }
        }
    }
}
