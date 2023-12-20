using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FoodDelieveryManagementAPI.Models
{
    public class OrderProducts
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }

        [JsonIgnore]
        public Order order { get; set; }
    }
}
