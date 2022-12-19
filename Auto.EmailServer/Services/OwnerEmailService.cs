using Grpc.Core;

namespace Auto.EmailServer.Services
{
    public class OwnerEmailService : Emailer.EmailerBase
    {
        private readonly ILogger<OwnerEmailService> _logger;

        public OwnerEmailService(ILogger<OwnerEmailService> logger)
        {
            _logger = logger;
        }

        public override Task<EmailReply> GetEmail(EmailRequest request, ServerCallContext context)
        {
            return Task.FromResult(new EmailReply() { Email = "fdfd@mail.ru" });
        }
    }
}
