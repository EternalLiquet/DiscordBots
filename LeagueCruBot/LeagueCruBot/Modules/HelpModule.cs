using System.Linq;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

namespace LeagueCruBot.Modules
{
    [Name("Help Commands")]
    public class HelpModule : ModuleBase<SocketCommandContext>
    {
        private CommandService _service;
        private IDMChannel _channel;

        public HelpModule(CommandService service)
        {
            this._service = service;
        }

        [Command("help")]
        [Summary("Use this command for a list of all available functions")]
        [Remarks("~help")]
        public async Task HelpCommand()
        {
            string charPrefix = "!";
            EmbedBuilder helpBuilder = new EmbedBuilder()
            {
                Title = "League Cru Bot Commands\n",
                Description = "I don't actually have any commands yet, check back later!",
                Color = new Color(255, 0, 0),
                ThumbnailUrl = "https://cdn.discordapp.com/icons/114439001703710725/23e60957d50903a2ad7d0d9bfea64cc4.webp?size=256"
            };

            /*foreach (var module in _service.Modules)
            {
                string description = null;
                foreach (var command in module.Commands)
                {
                    var result = await command.CheckPreconditionsAsync(Context);
                    if (result.IsSuccess)
                        description += $"**{charPrefix}{command.Aliases.First()}**\nFunction: {command.Summary}\n";
                }

                if (!string.IsNullOrWhiteSpace(description))
                {
                    helpBuilder.AddField(x =>
                    {
                        x.Name = $"**{module.Name}**";
                        x.Value = description;
                        x.IsInline = false;
                    });
                }
            }*/
            _channel = await Context.User.GetOrCreateDMChannelAsync();
            await _channel.SendMessageAsync("", false, helpBuilder.Build());
        }
    }
}
