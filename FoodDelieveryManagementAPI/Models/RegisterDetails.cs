using System.ComponentModel.DataAnnotations;

namespace FoodDelieveryManagementAPI.Models
{
    public class RegisterDetails
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public long ContactNo { get; set; }
        public string Address { get; set; }

    }
}
