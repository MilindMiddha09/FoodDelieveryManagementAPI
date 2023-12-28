using FoodDelieveryManagementAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace FoodDelieveryManagementAPI.DataRepositories.Interfaces
{
    public interface IAdminDataRepo
    {
        List<AppUser> GetAllAdminList();
        void RemoveAdmin(AppUser user);
        void AddAdmin(AppUser user);
        void UpdateAdmin(JsonPatchDocument<AppUser> updates, string userId);
    }
}