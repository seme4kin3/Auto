using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json.Linq;

public class Program
{
    const string SIGNALR_HUB_URL = "http://localhost:5067/hub";
    private static HubConnection hub;

    static async Task Main(string[] args)
    {
        hub = new HubConnectionBuilder().WithUrl(SIGNALR_HUB_URL).Build();
        await hub.StartAsync();
        Console.WriteLine("Hub started!");
        Console.WriteLine("Press any key...");
        var i = 0;
        while (true)
        {
            var owner = new
            {
                id = "5",
                name = "John",
                surname = "Black",
                vehicleCode = "a123aa",
                email = "hello@ya.ru"
            };
            var json = JObject.FromObject(owner).ToString();
            var input = Console.ReadLine();

            var message = $"Message #{i++} from SOP.SignalRClient {input}";
            await hub.SendAsync("NotifyWebUsers", "SOP.SignalRClient", json);
            Console.WriteLine($"Sent: {message}");
        }
    }
}
