using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Random_Joke_API
{
  class Item
  {
    public int id { get; set; }
    public string type { get; set; }
    public string setup { get; set; }
    public string punchline { get; set; }

  }




  class Program
  {
    static async Task Main(string[] args)
    {
      Console.WriteLine("Hello User! Would you like 10 {R}andom jokes or grab joke by {T}ype?");
      var userAnswer = Console.ReadLine();


      var client = new HttpClient();

      var responseAsStream = await client.GetStreamAsync("https://official-joke-api.appspot.com/random_ten");

      var items = await JsonSerializer.DeserializeAsync<List<Item>>(responseAsStream);
      if (userAnswer == "R")
      {
        foreach (var item in items)
        {
          Console.WriteLine($"Joke Id : {item.id}. {item.setup}");
          Console.WriteLine($"{item.punchline}");
        }
      }

      if (userAnswer == "T")
      {

      }
      else
      {
        Console.WriteLine("Invalid answer. Thanks for requesting the API");
      }



    }
  }
}