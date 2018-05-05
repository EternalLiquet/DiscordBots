﻿using System;
using System.Reflection;
using System.Threading.Tasks;
using System.IO;

using Discord.Commands;
using Discord.WebSocket;

namespace MegumiBot
{
    public class CommandHandler
    {
        private DiscordSocketClient _client;
        private CommandService _service;
        public static int megumiPicToUse;
        public static IntroPic introPic = new IntroPic();

        public async Task InstallAsync(DiscordSocketClient client)
        {
            this._client = client;
            this._service = new CommandService();
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
            string guildID = user.Guild.Id.ToString();
            string settingsFilePath = Support.config.Get("botFilesPath") + Support.config.Get("botIntroductionFilesPath") + $"/{guildID}.kato";
            if (File.Exists(settingsFilePath))
            {
                var guildName = user.Guild.Name;
                StreamReader sr = new StreamReader(settingsFilePath);
                ulong channelID =  ulong.Parse(await sr.ReadLineAsync());
                sr.Close();
                var channel = _client.GetChannel(channelID) as SocketTextChannel;
                Console.WriteLine($"{DateTime.Now.ToString()}: \t{user.Username} has joined {guildName}");
                await channel.SendFileAsync(introPic.createPic(user.Username, guildName, user.GetAvatarUrl(), megumiPicToUse), $"Welcome to {guildName}, {user.Mention}!", false);
                megumiPicToUse++;
                megumiPicToUse = megumiPicToUse % introPic.megumiPicsLength();
            }
        }
    }
}
