using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConsoleApp1.Commands
{
    [SlashCommandGroup("blague", "group of command blague")]
    internal class BlagueCommands : ApplicationCommandModule
    {
        [SlashCommand("add", "add blague")]
        public async Task AddBlagueCommand(
            InteractionContext ctx,
            [Option("question", "question de la blague")] string question,
            [Option("reponse", "reponse de la blague")] string reponse
        )
        {
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(new HttpMethod("POST"), $"https://localhost:7057/api/Blagues?blague={question}&reponse={reponse}");
            request.Headers.TryAddWithoutValidation("accept", "*/*");

            request.Content = new StringContent("");
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

            var response = await httpClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                // send reponse message
                var embed = new DiscordEmbedBuilder()
                {
                    Title = "La blague a bien etait ajouter"
                };
                embed.AddField("Question", question);
                embed.AddField("Reponse", reponse);
                var messageBuilder = new DiscordInteractionResponseBuilder()
                    .AddEmbed(embed)
                    .AsEphemeral();
                await ctx.CreateResponseAsync(messageBuilder);
            } else
            {
                // send reponse message
                var embed = new DiscordEmbedBuilder()
                {
                    Title = "Error " + response.StatusCode.GetName(),
                    Description = "la blague na pas etait trouvé"
                };
                var messageBuilder = new DiscordInteractionResponseBuilder()
                    .AddEmbed(embed)
                    .AsEphemeral();
                await ctx.CreateResponseAsync(messageBuilder);
            }

        }

        [SlashCommand("random", "random blague")]
        public async Task RandomBlagueCommand(
            InteractionContext ctx
        )
        {
            // create client http
            HttpClient httpClient = new HttpClient();

            
            // create request
            var request = new HttpRequestMessage(new HttpMethod("GET"), "https://localhost:7057/api/Blagues");
            request.Headers.TryAddWithoutValidation("accept", "text/plain");

            // send request
            var response = await httpClient.SendAsync(request);

            // reponse -> json
            string responceBody = await response.Content.ReadAsStringAsync();
            Root myDeserializedClass = JsonSerializer.Deserialize<Root>(responceBody);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                // send reponse message
                var embed1 = new DiscordEmbedBuilder()
                {
                    Title = myDeserializedClass.question
                };
                var messageBuilder1 = new DiscordInteractionResponseBuilder()
                    .AddEmbed(embed1)
                    .AsEphemeral();
                await ctx.CreateResponseAsync(messageBuilder1);
                await Task.Delay(5000);
                var embed2 = new DiscordEmbedBuilder()
                {
                    Title = myDeserializedClass.response
                };
                var messageBuilder2 = new DiscordWebhookBuilder()
                    .AddEmbed(embed2);
                await ctx.EditResponseAsync(messageBuilder2);
                await Task.Delay(1000);
                var embed3 = new DiscordEmbedBuilder()
                {
                    Title = "Blague"
                };
                embed3.AddField("question", myDeserializedClass.question);
                embed3.AddField("Reponse", myDeserializedClass.response);
                var messageBuilder3 = new DiscordWebhookBuilder()
                    .AddEmbed(embed3);
                await ctx.EditResponseAsync(messageBuilder3);
            }
            else
            {
                // send reponse message
                var embed = new DiscordEmbedBuilder()
                {
                    Title = "Error " + response.StatusCode.GetName(),
                    Description = "la blague na pas etait trouvé"
                };
                var messageBuilder = new DiscordInteractionResponseBuilder()
                    .AddEmbed(embed)
                    .AsEphemeral();
                await ctx.CreateResponseAsync(messageBuilder);
            }
        }

        [SlashCommand("remove", "remove blague")]
        public async Task RemoveBlagueCommand(
            InteractionContext ctx,
            [Option("id", "id de la blague")] string id
        )
        {
            // create a client http
            HttpClient httpClient = new HttpClient();

            // create request
            var request = new HttpRequestMessage(new HttpMethod("DELETE"), $"https://localhost:7057/api/Blagues/{id}");
            request.Headers.TryAddWithoutValidation("accept", "*/*");

            // send request
            var response = await httpClient.SendAsync(request);

            // reponse -> json
            string responceBody = await response.Content.ReadAsStringAsync();
            Root myDeserializedClass = JsonSerializer.Deserialize<Root>(responceBody);

            // verify status
            if (response.StatusCode == HttpStatusCode.OK)
            {
                // send reponse message
                var embed = new DiscordEmbedBuilder()
                {
                    Title = $"La blague avec l'id \"{id}\" c'est bien suprimer"
                };
                var messageBuilder = new DiscordInteractionResponseBuilder()
                    .AddEmbed(embed)
                    .AsEphemeral();
                await ctx.CreateResponseAsync(messageBuilder);
            }
            else
            {
                // send reponse message
                var embed = new DiscordEmbedBuilder()
                {
                    Title = "Error " + response.StatusCode.GetName(),
                    Description = "la blague na pas etait trouvé"
                };
                var messageBuilder = new DiscordInteractionResponseBuilder()
                    .AddEmbed(embed)
                    .AsEphemeral();
                await ctx.CreateResponseAsync(messageBuilder);
            }
        }

        [SlashCommand("get", "get blague")]
        public async Task RandomBlagueCommand(
            InteractionContext ctx,
            [Option("id", "id")] string id
        )
        {
            // create client http
            HttpClient httpClient = new HttpClient();


            // create request
            var request = new HttpRequestMessage(new HttpMethod("GET"), $"https://localhost:7057/api/Blagues/{id}");
            request.Headers.TryAddWithoutValidation("accept", "text/plain");

            // send request
            var response = await httpClient.SendAsync(request);

            // reponse -> json
            string responceBody = await response.Content.ReadAsStringAsync();
            Root myDeserializedClass = JsonSerializer.Deserialize<Root>(responceBody);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                // send reponse message
                var embed1 = new DiscordEmbedBuilder()
                {
                    Title = myDeserializedClass.question
                };
                var messageBuilder1 = new DiscordInteractionResponseBuilder()
                    .AddEmbed(embed1)
                    .AsEphemeral();
                await ctx.CreateResponseAsync(messageBuilder1);
                await Task.Delay(5000);
                var embed2 = new DiscordEmbedBuilder()
                {
                    Title = myDeserializedClass.response
                };
                await Task.Delay(1000);
                var embed3 = new DiscordEmbedBuilder()
                {
                    Title = "Blague"
                };
                embed3.AddField("question", myDeserializedClass.question);
                embed3.AddField("Reponse", myDeserializedClass.response);
                var messageBuilder3 = new DiscordWebhookBuilder()
                    .AddEmbed(embed3);
                await ctx.EditResponseAsync(messageBuilder3);
            }
            else
            {
                // send reponse message
                var embed = new DiscordEmbedBuilder()
                {
                    Title = "Error " + response.StatusCode.GetName(),
                    Description = "la blague na pas etait trouvé"
                };
                var messageBuilder = new DiscordInteractionResponseBuilder()
                    .AddEmbed(embed)
                    .AsEphemeral();
                await ctx.CreateResponseAsync(messageBuilder);
            }
        }
    }

    public class Root
    {
        [JsonPropertyName("question")]
        public string? question { get; set; }

        [JsonPropertyName("response")]
        public string? response { get; set; }
    }
}
