using System;
using System.Reflection;
using System.Threading.Tasks;

using Discord.Commands;
using Discord.WebSocket;

namespace MegumiBot
{
    public class CommandHandler
    {
        private DiscordSocketClient _client;
        private CommandService _service;
        private IntroPic _introPic;
        int megumiPicToUse;

        public async Task InstallAsync(DiscordSocketClient client)
        {
            this._client = client;
            this._service = new CommandService();
            _introPic = new IntroPic();
            megumiPicToUse = 0;
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
            if (msg.HasCharPrefix('~', ref argPos))
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
            var channel = user.Guild.DefaultChannel;
            await channel.SendFileAsync(_introPic.createPic(user.Username, guildName, user.GetAvatarUrl(), megumiPicToUse), $"Welcome to {guildName}, {user.Mention}!", false);
            megumiPicToUse++;
            megumiPicToUse = megumiPicToUse % _introPic.megumiPicsLength();
        }
    }
}
