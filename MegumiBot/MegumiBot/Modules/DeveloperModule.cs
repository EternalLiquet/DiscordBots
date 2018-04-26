using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using Discord.WebSocket;

namespace MegumiBot.Modules
{
    [Name("Developer Information")]
    public class DeveloperModule : ModuleBase<SocketCommandContext>
    {

        private string devDiscordID = Support.config.Get("devDiscordID");

        [Command("dev")]
        [Summary("I will tag my amazing developer")]
        [Remarks("~dev")]
        public async Task DeveloperCommand()
        {
            await Context.Channel.SendMessageAsync($"<@{devDiscordID}> is my developer and the person who turned me into a heroine that everyone is jealous of! " +
                        "\nhttp://i.imgur.com/kUmaeWi.jpg");
        }

        [Command("socialmedia")]
        [Summary("I will give you links to my developer's social media accounts")]
        [Remarks("~socialmedia")]
        public async Task SocialMediaCommand()
        {
            await Context.Channel.SendMessageAsync($"Feel free to follow my developer, <@{devDiscordID}>!" +
                        "\nGithub: https://github.com/EternalLiquet" + 
                        "\nTwitter: https://twitter.com/EternalLiquet" +
                        "\nTwitch: https://twitch.tv/EternalLiquet");
        }

        [Command("support")]
        [Summary("I will give you the information to go to my support center/home discord server, Cherry Blessing!")]
        [Remarks("~support")]
        public async Task MegumiSupportDiscordServer()
        {
            EmbedBuilder embedSupportServer;
            embedSupportServer = new EmbedBuilder();
            embedSupportServer.WithColor(new Color(218, 112, 214));

            var client = Context.Client as DiscordSocketClient;
            var guildName = "Cherry Blessing";
            var inviteLink = "https://discord.gg/uKNBGQJ";
            var twitterLink = "https://twitter.com/EternalLiquet";
            var guildIconURL = "https://cdn.discordapp.com/icons/327649178773749760/22efd92205d8bb08fad37524fa30d83c.jpg";

            embedSupportServer.ThumbnailUrl = guildIconURL;

            embedSupportServer.Title = $"Megumi Bot Support Information";
            embedSupportServer.Description = $"Server Owner/Megumi Bot Developer: **<@{devDiscordID}>**" +
                $"\nMegumi Bot's Support Discord Server: {guildName}" +
                $"\nDeveloper's Twitter: **{twitterLink}**" +
                $"\nInvite Link: **{inviteLink}**";
            await ReplyAsync($"The official support server for Megumi Bot is Cherry Blessing! Please message <@{devDiscordID}>, or contact him on Twitter for comments, questions, or concerns over my functions!", false, embedSupportServer);
        }
    }
}
