using FoodDelieveryManagementAPI.Enum;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FoodDelieveryManagementAPI.Models
{
    
    public class AppUser
    {
        public int ID { get; set; }
        public string Name { get; set; }
      
        public string Address { get; set; }
      
        public long ContactNo { get; set; }

        public UserRole UserRole { get; set; }

        [JsonIgnore]
        public int Discount { get; set; }
        [JsonIgnore]
        public ICollection<OrderProducts> OrderHistory { get; set; }
        [JsonIgnore]
        public ICollection<MenuProduct> Menu { get; set; }

        [ForeignKey("IdentityUser")]
        [JsonIgnore]
        public string IdentityUserId { get; set; }
        [JsonIgnore]
        public IdentityUser IdentityUser { get; set; }

    }
}
