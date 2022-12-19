
namespace Auto.Messages
{
    public class NewOwnerMessage
    {
        public string NumberAd { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string VehicleCode { get; set; }
        public DateTime ListedAtUtc { get; set; }
    }
}
