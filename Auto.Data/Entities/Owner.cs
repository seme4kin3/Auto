using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto.Data.Entities
{
    public partial class Owner
    {
        public string NumberAd { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }  
        public string VehicleCode { get; set; }

        [JsonIgnore]
        public virtual Vehicle OwnerVehicle { get; set; }
    }
}
