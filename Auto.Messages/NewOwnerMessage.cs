
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

    public class NewOwnerEmailMessage : NewOwnerMessage
    {
        public string Email { get; set; }
        public string Country { get; set; }

        public NewOwnerEmailMessage() { }

        public NewOwnerEmailMessage(NewOwnerMessage owner, string email, string country)
        {
            this.NumberAd = owner.NumberAd;
            this.FirstName = owner.FirstName;
            this.LastName = owner.LastName;
            this.Email = email;
            this.Country = country;
        }
    }
}
