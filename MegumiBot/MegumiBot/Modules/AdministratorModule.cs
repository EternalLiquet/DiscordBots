using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace MegumiBot.Modules
{
    [Name("Administrator Commands")]
    public class AdministratorModule : ModuleBase<SocketCommandContext>
    {
        //Admin Commands: Kick, Ban, Delete Messages
        [Command("kick")]
        [Alias("boot")]
        [Summary("I will kick a user from your server!")]
        [Remarks("~kick @User Reason...")]
        [RequireBotPermission(GuildPermission.KickMembers)]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task KickCommand(SocketGuildUser user, [Remainder] string reason = null)
        {
            if (user == null)
                await ReplyAsync("Request denied! You must mention a user!");
            if (string.IsNullOrWhiteSpace(reason))
                await ReplyAsync("Please provide a reason!");

            var gld = Context.Guild as SocketGuild;
            var kickEmbed = new EmbedBuilder()
            {
                Title = $"**{user.Username}** was kicked",
                Description = $"**Username: **{user.Username}\n**Server Name: **{user.Guild.Name}\n**Kicked By: **{Context.User.Mention}\n**Reason: **{reason}",
                Color = new Color(218, 112, 214),
                ThumbnailUrl = $"{user.GetAvatarUrl()}"
            };
            await user.KickAsync();
            await ReplyAsync($"I have kicked {user.Username} from the server! What a silly baka-chan!", false, kickEmbed);
        }

        [Command("ban")]
        [Alias("hammer")]
        [Summary("I will ban a user from your server!")]
        [Remarks("~ban @User Reason...")]
        [RequireBotPermission(GuildPermission.BanMembers)]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public async Task BanCommand(SocketGuildUser user = null, [Remainder] string reason = null)
        {
            if (user == null)
                await ReplyAsync("Request denied! You must mention a user!");
            if (string.IsNullOrWhiteSpace(reason))
                await ReplyAsync("Please provide a reason!");

            var gld = Context.Guild as SocketGuild;
            var banEmbed = new EmbedBuilder()
            {
                Title = $"**{user.Username}** was banned",
                Description = $"**Username: **{user.Username}\n**Server Name: **{user.Guild.Name}\n**Banned By: **{Context.User.Mention}\n**Reason: **{reason}",
                Color = new Color(218, 112, 214),
                ThumbnailUrl = $"{user.GetAvatarUrl()}"
            };
            await gld.AddBanAsync(user);
            await ReplyAsync($"I have banned {user.Username} from the server! What a silly baka-chan!", false, banEmbed);
        }

        [Command("purge")]
        [Alias("delete")]
        [Summary("I will delete a set number of messages from the channel! (Limited to 100 messages)")]
        [Remarks("~purge 100")]
        [RequireBotPermission(GuildPermission.ManageMessages)]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        public async Task PurgeCommand([Remainder]int amtOfMessages)
        {
            if (amtOfMessages <= 100 && amtOfMessages > 0)
            {
                var messagesToDelete = await Context.Channel.GetMessagesAsync(amtOfMessages + 1).Flatten();
                await Context.Channel.DeleteMessagesAsync(messagesToDelete);
            }
            else
                await ReplyAsync("An error has occured! Please try again!");
        }
    }
}
