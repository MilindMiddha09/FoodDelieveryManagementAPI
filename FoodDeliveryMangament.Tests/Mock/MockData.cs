using FoodDelieveryManagementAPI.Enum;
using FoodDelieveryManagementAPI.Models;

namespace FoodDeliveryMangament.Tests.MockData
{
    public static class MockData
    {
        public static List<AppUser> admins = new List<AppUser>()
        {
            new AppUser()
            {
                Name = "NewAdmin",
                UserRole = UserRole.Admin
            },
            new AppUser()
            {
                Name = "NewAdmin2",
                UserRole = UserRole.Admin
            }
        };

        public static List<AppUser> customers = new List<AppUser>() { 
            new AppUser()
            {
                Name = "Customer1",
                UserRole = UserRole.Customer,
                ContactNo = 2143532,
                Address = "Customer1 Address"
            },
            new AppUser()
            {
                Name = "Customer2",
                UserRole = UserRole.Customer,
                ContactNo = 2143213123,
                Address = "Customer2 Address"
            }
        };

        public static List<AppUser> restaurants = new List<AppUser>() {
            new AppUser()
            {
                Name = "Restaurant1",
                UserRole = UserRole.Restaurant,
                ContactNo = 6789435,
                Address = "Restaurant1Address"
            },
            new AppUser()
            {
                Name = "Restaurant2",
                UserRole= UserRole.Restaurant,
                ContactNo = 9798242934,
                Address = "NewRestaurant2Address"
            }
            };

        public static List<MenuProduct> menu = new List<MenuProduct>()
        {
            new MenuProduct()
            {
                Name = "Noodles",
                Price = 100
            },
            new MenuProduct()
            {
                Name = "Pav Bhaji",
                Price = 100
            }
        };

        public static AppUser mockRestaurantData = new AppUser()
        {
            Name = "New Restaurant",
            UserRole = UserRole.Restaurant,
            ContactNo = 213123123,
            Address = "RestaurantAddress"
        };
    }
}
