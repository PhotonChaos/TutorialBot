using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace TutorialBot
{
    class Program {
        /// <summary>
        /// This is the main class that the bot will run in.
        /// Some bots may only have one class, as it is possible to have all commands implemented manually.
        /// This is generally not a good idea as it can lead to source files with tens of thousands of lines.
        /// The most common practice is to split the mot into multiple classes, or "Modules," which allow for
        /// easier portability of features, as well as an easier way to make commands with aliases, permissions, etc.
        /// The main and startup methods are the hardest parts of the entire bot, but lickily you can just copy-paste most of it.
        /// </summary>

        private const string token = ""; // Your bot token, keep this private!
        private const string prefix = "!"; // The prefix for the commands

        private CommandService _commands;
        private DiscordSocketClient _client;
        private IServiceProvider _services;

        static void Main(string[] args) {
            new Program().StartAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        /// This is the method that starts the bot and connects to discord.
        /// </summary>
        /// <returns></returns>
        public async Task StartAsync() {
            _client = new DiscordSocketClient();
            _commands = new CommandService();

            _services = new ServiceCollection().AddSingleton(_client).AddSingleton(_commands).BuildServiceProvider();
            await InstallCommandsAsync();


            _client.Log += Log;

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        /// <summary>
        /// This method searches the current project for all module classes, and adds them to the command service
        /// </summary>
        /// <returns></returns>
        public async Task InstallCommandsAsync() {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly()); // this is what searches the program for module classes
        }

        /// <summary>
        /// This method handles the commands for the message passed in.
        /// </summary>
        /// <param name="messageParam">The message with the command</param>
        /// <returns></returns>
        private async Task HandleCommandAsync(SocketMessage messageParam) {
            SocketUserMessage message = messageParam as SocketUserMessage;

            if (message == null) return; // Null Checking
            if (message.Author.IsBot) return;
            int argPos = 0;
            if (!message.HasStringPrefix(prefix, ref argPos)) return; // if the message does not have the prefix 

            SocketCommandContext context = new SocketCommandContext(_client, message); // this is the command context, this is what provides information about the command (who called it, etc.)
            IResult result = await _commands.ExecuteAsync(context, argPos, _services); // This is what calls the command methods

            // The error handling
            if (!result.IsSuccess) {
                string error = result.ErrorReason;
                CommandError? e = result.Error;
                Console.WriteLine(error.ToString());
            }
        }

        /// <summary>
        /// This is the method to log things
        /// </summary>
        /// <param name="msg">The log message</param>
        /// <returns></returns>
        private Task Log(LogMessage msg) {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
