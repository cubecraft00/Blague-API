using ConsoleApp1.Commands;
using DSharpPlus;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;
using System.Text.Json;

namespace ConsoleApp1
{
    class Proram
    {
        public static DiscordClient? discord;
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }
        static async Task MainAsync()
        {
            discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = "MTA1ODAxMzc3ODMxNjM4MjIwOQ.GGGLXq.nv7dJKYR-bvmM8CU_x0MyywkqXziRR3INu_74c",
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.All,
            });

            var slashCommands = discord.UseSlashCommands();
            discord.UseInteractivity();

            slashCommands.RegisterCommands<BlagueCommands>(1058013386186706985);

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }

        //private static async Task Update()
        //{
        //    await Task.Factory.StartNew(async () =>
        //    {
        //        while (true)
        //        {
        //            await Task.Delay(TimeSpan.FromSeconds(5));
        //        }
        //    });
       // }
    }
}