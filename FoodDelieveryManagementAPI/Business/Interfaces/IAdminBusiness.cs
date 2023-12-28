using FoodDelieveryManagementAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodDelieveryManagementAPI.Business.Interfaces
{
    public interface IAdminBusiness
    {
        List<AppUser> GetAdminList();
        Task<bool> Register(Register registerDetails, string role);
        Task<bool> DeleteAdmin(int id);
        void Update(JsonPatchDocument<AppUser> updates, string userId);
    }
}