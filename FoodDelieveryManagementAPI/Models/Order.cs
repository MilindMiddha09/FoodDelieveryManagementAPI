using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FoodDelieveryManagementAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int TotalAmount { get; set; }
        public DateTime OrderTime { get; set; }

        [JsonIgnore]
        public int CustomerId { get; set; }
        [JsonIgnore]
        public int RestaurantId { get; set; }

        public ICollection<OrderProducts> OrderedItems { get; set; }
    }
}
