using Discord;
using Discord.WebSocket;
using Loria.Lib.Parser;
using Loria.Lib.Player;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loria.App
{
    class Program
    {
        static DiscordSocketClient Client { get; set; }
        static AdventurePlayer AdventurePlayer { get; set; }

        static void Main(string[] args)
        {
            var app = new CommandLineApplication(throwOnUnexpectedArg: false);
            var debug = app.Option(
                "-d|--debug",
                "Lancer en mode debug.",
                CommandOptionType.NoValue
            );

            Console.OutputEncoding = Encoding.UTF8;

            app.OnExecute(async () =>
            {
                if (debug.HasValue())
                {
                    var adventure = AdventureParser.Parse(@"C:\Users\Othane\source\repos\Loria\src\LoneWolf_TestAdventure.json");
                    var player = new AdventurePlayer(adventure,
                        speak: Console.WriteLine
                    );

                    player.Play();

                    var input = string.Empty;

                    while (player.Continue(Console.ReadLine()))
                    {
                    }

                }
                else
                {
                    var configurationBuilder = new ConfigurationBuilder()
                        .SetBasePath(AppContext.BaseDirectory)
                        .AddJsonFile("appsettings.json");
                    var configuration = configurationBuilder.Build();

                    var token = configuration["token"];

                    Client = new DiscordSocketClient();
                    Client.Log += Client_Log;
                    Client.MessageReceived += Client_MessageReceived;

                    await Client.LoginAsync(TokenType.Bot, token);
                    await Client.StartAsync();

                    while (true)
                    {
                        var input = Console.ReadLine();
                        if (input == "quit") break;

                        await Task.Delay(100);
                    }

                    await Client.StopAsync();
                    await Client.LogoutAsync();
                }

                return 0;
            });
            app.Execute(args);
        }

        private static async Task Client_Log(LogMessage logMessage)
        {
            await Task.Delay(0);

            Console.WriteLine(logMessage.Message);
        }

        private static async Task Client_MessageReceived(SocketMessage message)
        {
            if (!(message is SocketUserMessage userMessage))
            {
                return;
            }

            if (userMessage.Author.Id == Client.CurrentUser.Id || userMessage.Author.IsBot)
            {
                return;
            }

            if (userMessage.MentionedUsers.Any(user => user.Id == Client.CurrentUser.Id))
            {
                if (userMessage.Content.Contains("aventure"))
                {
                    var adventure = AdventureParser.Parse(@"C:\Users\Othane\source\repos\Loria\src\LoneWolf_TestAdventure.json");

                    AdventurePlayer = new AdventurePlayer(adventure, 
                        speak: async (text) => await userMessage.Channel.SendMessageAsync(text)
                    );
                    AdventurePlayer.Play();
                }
            }
            else
            {
                AdventurePlayer?.Continue(userMessage.Content);
            }
        }
    }
}
