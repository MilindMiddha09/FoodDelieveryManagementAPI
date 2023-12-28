using FoodDelieveryManagementAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace FoodDelieveryManagementAPI.DataRepositories.Interfaces
{
    public interface ICustomerDataRepo
    {
        void AddCustomer(AppUser user);
        void DeleteCustomer(AppUser user);
        List<AppUser> GetAllCustomers();
        void UpdateCustomer(JsonPatchDocument<AppUser> updates, string identityUserId);
    }
}