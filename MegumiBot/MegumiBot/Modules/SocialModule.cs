using System;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace MegumiBot.Modules
{
    [Name("Social Commands")]
    public class SocialModule : ModuleBase<SocketCommandContext>
    {
        [Command("hello")]
        [Summary("I will say hello to you!")]
        [Remarks("~hello")]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        public async Task HelloCommand()
        {
            await ReplyAsync($"Hello {Context.User.Mention}!");
        }

        [Command("noticeme")]
        [Summary("I will let your senpai know that you want to be noticed!")]
        [Remarks("~noticeme @User")]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        public async Task NoticeMeCommand(SocketGuildUser user)
        {
            if (user == null)
            {
                await ReplyAsync($"No user specified! You must mention a user!");
            }
            else
            {
                await ReplyAsync($"{Context.User.Mention}-chan wants {user.Mention}-senpai to notice them!");
            }
        }

        [Command("goodmorning")]
        [Summary("I will say good morning to someone for you!")]
        [Remarks("~goodmorning, ~goodmorning here, or ~goodmorning @user")]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        public async Task GoodMorningCommand(string goodmorningTo = null)
        {
            if (goodmorningTo != null && goodmorningTo.Equals("here"))
            {
                await ReplyAsync($"Good morning @here!");
            }
            else if (goodmorningTo != null)
            {
                await ReplyAsync($"Good morning, {goodmorningTo}");
            }
            else
            {
                await ReplyAsync($"Good morning, {Context.User.Mention}!");
            }
        }

        [Command("goodnight")]
        [Summary("I will say goodnight to you or to someone for you!")]
        [Remarks("~goodnight, ~goodnight here, or ~goodnight @user")]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        public async Task GoodNightCommand(string goodnightTo = null)
        {
            if (goodnightTo != null && goodnightTo.Equals("here"))
            {
                await ReplyAsync($"Goodnight @here! Sweet Dreams!");
            }
            else if (goodnightTo != null)
            {
                await ReplyAsync($"Good night, {goodnightTo}! Sweet Dreams!");
            }
            else
            {
                await ReplyAsync($"Goodnight, {Context.User.Mention}! Sweet Dreams!");
            }
        }
    }
}
