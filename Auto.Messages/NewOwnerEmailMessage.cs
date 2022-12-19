using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto.Messages
{
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
            Country = country;
        }
    }
}
