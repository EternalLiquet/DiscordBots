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
    [Name("Announcements")]
    public class AnnouncementModule : ModuleBase<SocketCommandContext>
    {

        private string devDiscordID = Support.config.Get("devDiscordID");
        private string botAnnouncementPath = Support.config.Get("botFilesPath") + Support.config.Get("botAnnouncementPath");

        [Command("IAm")]
        public async Task EditWhereCommand([Remainder] string whereText)
        {
            if (Context.User.Id.Equals(devDiscordID))
            {
                string filePath = botAnnouncementPath + Support.config.Get("botWhereIsLiquetPath") + "whereisliquet.katofile";
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                StreamWriter sw = new StreamWriter(filePath);
                await sw.WriteLineAsync(whereText);
                sw.Close();
                await ReplyAsync($"I've successfully updated your status, <@{devDiscordID}>");
            }
            else
            {
                await ReplyAsync($"Hey.. you can't really edit this since you're not <@{devDiscordID}>");
            }
        }

        [Command("WhereIsLiquet?")]
        [Summary("I will let you know where my developer is at the moment!")]
        [Remarks("~WhereIsLiquet?")]
        public async Task AnnounceLiquetCommand()
        {
            string filePath = botAnnouncementPath + Support.config.Get("botWhereIsLiquetPath") + "whereisliquet.katofile";
            if (File.Exists(filePath))
            {
                StreamReader sr = new StreamReader(filePath);
                await ReplyAsync($"<@{devDiscordID}> is {await sr.ReadLineAsync()}");
                sr.Close();
            }
            else
            {
                await ReplyAsync($"<@{devDiscordID}> hasn't really told me where he is right now.. sorry!");
            }
        }
    }
}
