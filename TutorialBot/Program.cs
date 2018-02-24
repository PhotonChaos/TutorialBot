using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace TutorialBot
{
    class Program {
        /// <summary>
        /// This is the main class that the bot will run in.
        /// Some bots may only have one class, as it is possible to have all commands implemented manually.
        /// This is generally not a good idea as it can lead to source files with over ten thousand lines.
        /// The most common practice is to split the mot into multiple classes, or "Modules," which allow for
        /// easier portability of features, as well as an easier way to make commands with aliases, permissions, etc.
        /// The main and startup methods are the hardest parts of the entire bot, but lickily you can just copy-paste most of it.
        /// </summary>

        private const string token = ""; 

        private CommandService _commands;
        private DiscordSocketClient _client;
        private IServiceProvider _services;

        static void Main(string[] args) {
            new Program().StartAsync().GetAwaiter().GetResult();
        }
    }
}
