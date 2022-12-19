using Auto.EmailServer;
using Grpc.Net.Client;

using var channel = GrpcChannel.ForAddress("http://localhost:5012");
var grpcClient = new Emailer.EmailerClient(channel);
Console.WriteLine("Ready! Press any key to send a gRPC request (or Ctrl-C to quit).");
while (true)
{
    Console.ReadKey(true);
    var request = new EmailRequest
    {
        NumberAd = "5",
        FirstName = "Lion",
        LastName = "Woody",
    };

    var reply = grpcClient.GetEmail(request);
    Console.WriteLine($"Your email {reply.Email}");
}
