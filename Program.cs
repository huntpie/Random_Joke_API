using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ConsoleTables;

namespace Random_Joke_API
{
  class Program
  {
    class Item
    {
      [JsonPropertyName("id")]
      public int Id { get; set; }

      [JsonPropertyName("type")]
      public string Type { get; set; }

      [JsonPropertyName("setup")]
      public string Setup { get; set; }

      [JsonPropertyName("punchline")]
      public string Punchline { get; set; }
    }
    static async Task RandomTenJokes(string token)
    {
      var client = new HttpClient();

      var randomTenUrl = $"https://official-joke-api.appspot.com/jokes/ten/";
      var responseAsStreamRTen = await client.GetStreamAsync(randomTenUrl);

      var items = await JsonSerializer.DeserializeAsync<List<Item>>(responseAsStreamRTen);

      var table = new ConsoleTable("Joke Type", "Joke Setup", "Joke Punchline");

      foreach (var item in items)
      {
        table.AddRow(item.Type, item.Setup, item.Punchline);
      }
      table.Write(Format.Minimal);
    }

    static async Task TypeTenJokes(string token, string type)
    {
      try
      {
        var client = new HttpClient();

        var typeJokeUrl = $"https://official-joke-api.appspot.com/jokes/{type}/ten";
        var responseAsStreamTT = await client.GetStreamAsync(typeJokeUrl);

        var items = await JsonSerializer.DeserializeAsync<List<Item>>(responseAsStreamTT);

        var table = new ConsoleTable("Joke Type", "Joke Setup", "Joke Punchline");

        foreach (var item in items)
        {
          table.AddRow(item.Type, item.Setup, item.Punchline);
        }

        table.Write(Format.Minimal);

      }
      catch (HttpRequestException)
      {
        Console.WriteLine("I could not find that type of joke!");
      }

    }

    static async Task Main(string[] args)
    {
      var token = "";

      if (args.Length == 0)
      {
        Console.WriteLine("What joke list do you want?");
        token = Console.ReadLine();
      }
      else
      {
        token = args[0];
      }

      var keepGoing = true;
      while (keepGoing)
      {
        Console.Clear();
        Console.Write("Get Ten (R)andom Jokes or Get Ten Jokes by (T)ype or (Q)uit: ");
        var choice = Console.ReadLine().ToUpper();

        switch (choice)
        {
          case "Q":
            keepGoing = false;
            break;

          case "R":
            await RandomTenJokes(token);

            Console.WriteLine("Press ENTER to continue");
            Console.ReadLine();
            break;

          case "T":
            Console.Write("Enter the type of joke you want: ");
            var type = Console.ReadLine();

            await TypeTenJokes(token, type);

            Console.WriteLine("Press ENTER to continue");
            Console.ReadLine();
            break;

          default:
            break;
        }
      }
    }
  }
}