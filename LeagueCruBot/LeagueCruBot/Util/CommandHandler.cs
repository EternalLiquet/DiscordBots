using System.Reflection;
using System.Threading.Tasks;

using Discord.Commands;
using Discord.WebSocket;

namespace LeagueCruBot.Util
{
    public class CommandHandler
    {
        //Declare a _client variable type DiscordSocketClient
        private DiscordSocketClient _client;

        //Declare _service cariable type CommandServnce
        private CommandService _service;


        //Sets up the CommandService
        public async Task InstallAsync(DiscordSocketClient client)
        {
            //Set up _client variable to the client object we passed in from the main program
            this._client = client;

            //Instantiates a new CommandService object
            this._service = new CommandService();

            //Not too sure how the command service works yet
            await _service.AddModulesAsync(Assembly.GetEntryAssembly());

            //on message recieved event, execute this async task
            _client.MessageReceived += HandleCommandAsync;

            //on user join server event, execute this async task
            _client.UserJoined += AnnounceUserJoined;
        }


        //Method to handle what to do when we recieve a message, takes the discord messages as a parameter
        public async Task HandleCommandAsync(SocketMessage UserMsg)
        {
            //Makes sure this message isn't coming from a bot or anything like that, explicitly cast the SocketMessage into a SocketUserMessage
            SocketUserMessage msg = UserMsg as SocketUserMessage;

            //If the message is empty, don't even bother
            if (msg == null)
                return;

            //Instantiate new SocketCommandContext object with parameters taking in the _client and the message we defined earlier
            SocketCommandContext context = new SocketCommandContext(_client, msg);

            //define variable argPos of type integer with a value of 0
            int argPos = 0;

            //Check the first character of a message. If it has the '!' in front of it, we will take it as a command
            if (msg.HasCharPrefix('!', ref argPos))
            {

                //Attempt to execute a command with the message, store the results of that command in a variable called result of type IResult
                IResult result = await _service.ExecuteAsync(context, argPos);

                //If the result is not successful and the result isn't the fact that it's an unknown command, send the error reason back to the channel where
                //The command was read
                if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                {
                    await context.Channel.SendMessageAsync(result.ErrorReason);
                }
            }
        }


        public async Task AnnounceUserJoined(SocketGuildUser user)
        {
            //The bot is only going to be used in League Cru's Discord server but might as well make it so that the bot can go to any server and use
            //the name of whatever server it goes to, anyways.. this gets the name of the server that the user just joined
            string guildName = user.Guild.Name;

            //Declare a variable called channel of type SocketTextChannel
            SocketTextChannel channel;

            //Check to see if the Server's guild ID matches League Cru Discord Server's guild ID, if it matches, then the user is in the League
            //Cru Discord Server
            if (user.Guild.Id == 114439001703710725)
                //Set the channel variable to a channel with specified channel id
                channel = _client.GetChannel(114439001703710725) as SocketTextChannel;
            else
                //If the server the user joined isn't the League Cru server, use their default channel (Interestingly enough,
                //the default channel of a discord server is a channel with the same ID as the server itself. Assumably this is the first text channel
                //To get created upon server creation)
                channel = user.Guild.DefaultChannel;
            //Welcome the user to the server
            await channel.SendMessageAsync($"Welcome to {guildName}, {user.Mention}!");
        }
    }
}
