using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace TutorialBot.Modules
{
    [Name("Admin")]
    [Group("admin")] // Group allows you to use subcommands, for example in this module, !admin ban @Carbon#1959 or !admin kick @Carbon#1959
    class AdminModule : ModuleBase<SocketCommandContext> {
        [Command("ban")]
        [Name("Ban")]
        [Summary("Bans the user")]
        public async Task Ban(SocketUser user = null) {
            // SocketUser will take a mention (@username) as a parameter, and give you a SocketUser object for that user
            await Context.Guild.AddBanAsync(user); // bans the user
        }

        [Command("kick")]
        [Name("Kick")]
        [Summary("Kicks a user")]
        public async Task kick(SocketUser user = null) {
            await Context.Guild.GetUser(user.Id).KickAsync();
        }
    }
}
