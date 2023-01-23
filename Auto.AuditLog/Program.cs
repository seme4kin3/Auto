﻿using Auto.Messages;
using EasyNetQ;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;

namespace Auto.AuditLog
{

    class Program
    {
        private static readonly IConfigurationRoot config = ReadConfiguration();

        private const string SUBSCRIBER_ID = "Auto.AuditLog";

        static async Task Main(string[] args)
        {
            using var bus = RabbitHutch.CreateBus(config.GetConnectionString("AutoRabbitMQ"));
            Console.WriteLine("Connected! Listening for NewVehicleMessage, NewOwnerMessage messages.");
            await bus.PubSub.SubscribeAsync<NewVehicleMessage>(SUBSCRIBER_ID, HandleNewVehicleMessage);
            await bus.PubSub.SubscribeAsync<NewOwnerMessage>(SUBSCRIBER_ID, HandleNewOwnerMessage);

            Console.ReadKey(true);
        }

        private static void HandleNewVehicleMessage(NewVehicleMessage message)
        {
            var csv =
                $"{message.Registration},{message.Manufacturer},{message.ModelName},{message.Color},{message.Year},{message.ListedAtUtc:O}";
            Console.WriteLine(csv);
        }

        private static void HandleNewOwnerMessage(NewOwnerMessage message)
        {
            var csv =
                $"{message.NumberAd},{message.FirstName},{message.LastName},{message.VehicleCode},{message.ListedAtUtc:O}";
            Console.WriteLine(csv);
        }

        private static IConfigurationRoot ReadConfiguration()
        {
            var basePath = Directory.GetParent(AppContext.BaseDirectory).FullName;
            return new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
        }
    }
}
