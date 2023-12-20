using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FoodDelieveryManagementAPI.Models
{
    public class MenuProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }


        [ForeignKey("UserDetails")]
        public int RestaurantId { get; set; }

        [JsonIgnore]
        public AppUser Restaurant { get; set; }
    }
}
