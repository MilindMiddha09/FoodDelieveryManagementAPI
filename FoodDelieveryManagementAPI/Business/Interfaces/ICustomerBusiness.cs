using FoodDelieveryManagementAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodDelieveryManagementAPI.Business.Interfaces
{
    public interface ICustomerBusiness
    {
        List<AppUser> GetCustomerList();
        Task Register(RegisterDetails registerDetails, string role);
        Task DeleteCustomer(int id);
        void Update(JsonPatchDocument<AppUser> updates, string identityUserId);


    }
}