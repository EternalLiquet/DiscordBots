using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace MegumiBot.Modules
{
    [Name("Automated Introductions")]
    public class IntroductionPicModule : ModuleBase<SocketCommandContext>
    {

        private string botIntroductionPath = Support.config.Get("botFilesPath") + Support.config.Get("botIntroductionFilesPath");

        [Command("intro setup")]
        [Summary("I will introduce new people to the server in the channel this command gets typed!")]
        [Remarks("~intro setup")]
        [RequireBotPermission(ChannelPermission.AttachFiles & ChannelPermission.SendMessages)]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task IntroductionSetupCommand()
        {
            string guildID = Context.Guild.Id.ToString();
            string channelID = Context.Channel.Id.ToString();
            string settingsFilePath = botIntroductionPath + $"/{guildID}.kato";
            if (File.Exists(settingsFilePath))
            {
                File.Delete(settingsFilePath);
            }
            StreamWriter sw = new StreamWriter(settingsFilePath);
            await sw.WriteLineAsync(channelID);
            sw.Close();
            await ReplyAsync($"I've successfully set the introduction channel for {Context.Guild.Name} to \"#{Context.Channel.Name}\"!");
        }

        [Command("intro create")]
        [Summary("I will create an introductory picture for anyone I might've missed for some reason! (Or if you just want me to!)")]
        [Remarks("~intro create @user")]
        [RequireBotPermission(ChannelPermission.AttachFiles & ChannelPermission.SendMessages)]
        public async Task IntroductionCreationCommand(SocketGuildUser user = null)
        {
            string avatarURL;
            string guildID = Context.Guild.Id.ToString();
            string settingsFilePath = botIntroductionPath + $"/{guildID}.kato";
            var guildName = Context.Guild.Name;
            if (user == null)
                user = Context.User as SocketGuildUser;
            if (user.GetAvatarUrl() == null || user.GetAvatarUrl() == "")
                avatarURL = "https://storage.googleapis.com/rapid_connect_packages/discordapp.png";
            else
                avatarURL = user.GetAvatarUrl();
            Console.WriteLine($"{DateTime.Now.ToString()}: \t{user.Username} has joined {guildName}");
            await Context.Channel.SendFileAsync(CommandHandler.introPic.createPic(user.Username, guildName, avatarURL, CommandHandler.megumiPicToUse), $"Welcome to {guildName}, {user.Mention}!", false);
            CommandHandler.megumiPicToUse++;
            CommandHandler.megumiPicToUse = CommandHandler.megumiPicToUse % CommandHandler.introPic.megumiPicsLength();
        }

        [Command("intro unset")]
        [Summary("I won't introduce people into your server anymore")]
        [Remarks("~intro unset")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task IntroductionUnsetCommand()
        {
            string guildID = Context.Guild.Id.ToString();
            string channelID = Context.Channel.Id.ToString();
            string settingsFilePath = botIntroductionPath + $"/{guildID}.kato";
            if (File.Exists(settingsFilePath))
            {
                File.Delete(settingsFilePath);
            }
            await ReplyAsync($"I've successfully unset the introduction channel for {Context.Guild.Name}!");
        }
    }
}
