using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Auto.Website.Models
{
    public class OwnerDto
    {
        public string NumberAd { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string VehicleCode { get; set; }
    }
}
