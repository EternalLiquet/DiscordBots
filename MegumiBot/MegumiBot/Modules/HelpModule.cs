using System.Linq;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

namespace MegumiBot.Modules
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
        [Summary("I will private message you all of my functions")]
        [Remarks("~help")]
        public async Task HelpCommand()
        {
            string charPrefix = "~";
            EmbedBuilder helpBuilder = new EmbedBuilder()
            {
                Title = "Megumi Bot Commands\n",
                Description = "Here are some of the things that I can do!",
                Color = new Color(218, 112, 214),
                ThumbnailUrl = "https://cdn.discordapp.com/avatars/327667735171432449/00f399c0b94f1d101a5a16aa53d5db39.webp?size=256"
            };

            foreach (var module in _service.Modules)
            {
                string description = null;
                foreach (var command in module.Commands)
                {
                    var result = await command.CheckPreconditionsAsync(Context);
                    if (result.IsSuccess && !command.Name.Equals("IAm"))
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
            }
            _channel = await Context.User.GetOrCreateDMChannelAsync();
            await _channel.SendMessageAsync("", false, helpBuilder.Build());
        }
    }
}
