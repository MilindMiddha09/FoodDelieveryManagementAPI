using FoodDelieveryManagementAPI.Enum;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelieveryManagementAPI.Models
{
    public class AppUser
    {
        public int ID { get; set; }
        public string Name { get; set; }
      
        public string Address { get; set; }
      
        public long ContactNo { get; set; }
        public int TotalOrders { get; set; }

        public UserType UserRole { get; set; }

        public int Discount { get; set; }
        public ICollection<OrderProducts> OrderHistory { get; set; }
        public ICollection<MenuProduct> Menu { get; set; }

        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }

        public IdentityUser IdentityUser { get; set; }

    }
}
