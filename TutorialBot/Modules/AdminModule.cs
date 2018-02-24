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
        [RequireBotPermission(GuildPermission.BanMembers)] // This makes it so that the command will only execute if the bot has the Ban Members permission.
        [RequireUserPermission(GuildPermission.BanMembers)] // This makes it so that the command must be invoked by someone with the Ban Members permission.
        public async Task Ban(SocketUser user = null) {
            // SocketUser will take a mention (@username) as a parameter, and give you a SocketUser object for that user
            await Context.Guild.AddBanAsync(user); // bans the user
        }

        [Command("kick")]
        [Name("Kick")]
        [Summary("Kicks a user")]
        [RequireBotPermission(GuildPermission.KickMembers)]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task Kick(SocketUser user = null) {
            await Context.Guild.GetUser(user.Id).KickAsync();
        }

        [Command("shutdown")]
        [Name("Shutdown")]
        [Summary("Shuts down the bot")]
        [RequireOwner] // makes it so that the command must be invoked by the owner of the bot (not the server)
        public async Task Shutdown() {
            Environment.Exit(0); // Quits the program
        }
    }
}
