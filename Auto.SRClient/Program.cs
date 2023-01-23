using Auto.Messages;
using EasyNetQ;
using Microsoft.AspNetCore.SignalR.Client;
using System.Text.Json;

public class Program
{
    const string SIGNALR_HUB_URL = "http://localhost:5067/hub";
    private static HubConnection hub;
    private static IBus bus;

    static async Task Main(string[] args)
    {
        var amqp = "amqp://user:rabbitmq@localhost:5672";
        bus = RabbitHutch.CreateBus(amqp);
        Console.WriteLine("Conected to bus");

        hub = new HubConnectionBuilder().WithUrl(SIGNALR_HUB_URL).Build();
        await hub.StartAsync();
        Console.WriteLine("Hub started!");
        Console.WriteLine("Press any key...");

        Console.WriteLine("Connected to bus! Listening newOwnerMessage");
        var subscriberId = $"Auto.Website@{Environment.MachineName}";
        await bus.PubSub.SubscribeAsync<NewOwnerEmailMessage>(subscriberId, HandleNewOwnerMessage);
        Console.ReadLine();
    }

    private static async void HandleNewOwnerMessage(NewOwnerEmailMessage noem)
    {
        var csvRow =
            $" {noem.Email} {noem.Country}: {noem.NumberAd}, {noem.FirstName}, {noem.LastName} ";
        Console.WriteLine(csvRow);
        var json = System.Text.Json.JsonSerializer.Serialize(noem, JsonSettings());
        await hub.SendAsync("NotifyWebUsers", "Auto.Website", json);
    }

    static JsonSerializerOptions JsonSettings() =>
        new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
}