using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace TutorialBot.Modules // Namespace MUST be <namespace>.Modules
{
    // module classes MUST be explicitly marked as public
    public class BasicModule : ModuleBase<SocketCommandContext> {
        private static int number; // Module class instances only last as long as the command has been called, theya re destroyed once a method ends
                                   // So you should make variables static if you want them to persist after the command has ended.

        [Command("ping")] // the name of the command that you will use in discord
        [Name("Ping")] // The display name of the command (used for making a help command)
        [Summary("Responds with pong")] // This is the summary of the command (also used for the help command)
        public async Task Ping() {
            // For commands, the method must be public, async, and return a Task object.
            // The name of the method does not matter.
            await ReplyAsync("Pong"); // ReplyAsync() sends a message with the specified text in the same channel as the command.
        }

        [Command("echo")]
        [Name("Echo")]
        [Summary("Echoes the parameter text back to the user")]
        public async Task Echo(params string[] words) {
            // anything after the command will be passed as a parameter, each thing will be seperated by a space. 
            // In order to take in an unknown number of words to the command, use the params keyword. The variable words in this case will contain all text after the command name.

            string s = "";

            foreach(string word in words) {
                s += word + " ";
            }

            await ReplyAsync($"{Context.Message.Author} says {s}"); // the Context object holds all information about the context of the command (it holds the message object, etc.)
        }

        [Command("add")]
        [Name("Add")]
        [Summary("Adds to a variable")]
        public async Task AddAsync(int num = 0) {
            // Disciord.NET will interpret numbers as an int if the parameter is of type int
            number += num;
            await ReplyAsync($"Added {num} to number, number is now {number}.");
        }
    }
}
