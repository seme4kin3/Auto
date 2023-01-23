
using Auto.EmailServer;
using Auto.Messages;
using EasyNetQ;
using Grpc.Net.Client;

//using var channel = GrpcChannel.ForAddress("http://localhost:5012");
//var grpcClient = new Emailer.EmailerClient(channel);
//Console.WriteLine("Ready! Press any key to send a gRPC request (or Ctrl-C to quit).");
//while (true)
//{
//    Console.ReadKey(true);
//    var request = new EmailRequest
//    {
//        NumberAd = "5",
//        FirstName = "Lion",
//        LastName = "Woody",
//    };

//    var reply = grpcClient.GetEmail(request);
//    Console.WriteLine($"Your email {reply.Email}");
//}
 class Program
{
    private static Emailer.EmailerClient grpcClient;
    private static IBus bus;

    static async Task Main(string[] args)
    {
        Console.WriteLine("Starting Auto.EmailClient");
        var amqp = "amqp://user:rabbitmq@localhost:5672";
        bus = RabbitHutch.CreateBus(amqp);
        Console.WriteLine("Connected to bus; Listening for NewOwnerMessage");
        var grpsAddress = "http://localhost:5012";
        using var channel = GrpcChannel.ForAddress(grpsAddress);
        grpcClient = new Emailer.EmailerClient(channel);
        Console.WriteLine($"Connected to gRPC on {grpsAddress}");
        var subscriberId = $"Auto.EmailServer@{Environment.MachineName}";
        await bus.PubSub.SubscribeAsync<NewOwnerMessage>(subscriberId, HandleNewOwnerMessage);
        Console.WriteLine("Press Enter to exit");
        Console.ReadLine();
    }

    private static async Task HandleNewOwnerMessage(NewOwnerMessage message)
    {
        Console.WriteLine($"new owner; {message.NumberAd} {message.VehicleCode}");
        var emailRequest = new EmailRequest()
        {
            FirstName = message.FirstName,
            LastName = message.LastName,
            VehicleCode = message.VehicleCode,
            NumberAd = message.NumberAd
        };

        var emailReply = await grpcClient.GetEmailAsync(emailRequest);
        Console.WriteLine($"Owner {message.NumberAd} has {emailReply.Email}");
        var newOwnerEmailMessage = new NewOwnerEmailMessage(message, emailReply.Email,"Russia");
        await bus.PubSub.PublishAsync(newOwnerEmailMessage);
    }
}
