using System;
using System.IO;
using System.Threading.Tasks;

//importing our project dependencies for Discord
using Discord;
using Discord.WebSocket;

//we use this using statement to see everything inside the config folder
using LeagueCruBot.Util;

namespace LeagueCruBot
{
    class Program
    {
        //Declare variable of types DiscordSocketClient and CommandHandler
        private DiscordSocketClient _client;
        private CommandHandler _handler;
        private string _token;
        private string _botFilesPath;

        //Turn this program into an asynchronous program
        static void Main(string[] args)
            => new Program().StartAsync().GetAwaiter().GetResult();

        public async Task StartAsync()
        {
            //Set the Console Title for the application 
            Console.Title = "League Cru Bot 1.0";

            //Call the static method "StartupOpertations" in the static class "Support"
            Support.StartupOperations();
            //This will set our token equal to token found in the App.config file
            _token = Support.config.Get("botToken");
            //This will set our default bot files directory to what is found in the App.config file
            _botFilesPath = Support.config.Get("botFilesPath");

            //Instantiate a new Instance of DiscordSocketClient object
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                //This line tells you how thorough the logging will be, verbose is generally good for most applications. 
                //For more detailed information, user LogSeverity.Debug
                LogLevel = LogSeverity.Verbose
            });

            //Logs in using the token that was given into the JSON file. 
            await _client.LoginAsync(TokenType.Bot, _token);
            //Start the bot
            await _client.StartAsync();
            //Sets the bot's "Playing" status to "!help for commands" to let people in the server know how to use the help command
            await _client.SetGameAsync("!help for commands");

            //Instantiate a new instance of CommandHandler class. 
            _handler = new CommandHandler();

            //Passes the _client object into the _handler object
            await _handler.InstallAsync(_client);

            //Write log messages to the console.
            _client.Log += (logMessages)
                       => Console.Out.WriteLineAsync(logMessages.ToString());

            //Make sure the console does not exit.
            await Task.Delay(-1);
        }
    }
}
