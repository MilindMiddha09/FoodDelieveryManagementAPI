using FoodDelieveryManagementAPI.Data;
using FoodDelieveryManagementAPI.DataRepositories.Interfaces;
using FoodDelieveryManagementAPI.Enum;
using FoodDelieveryManagementAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Linq;

namespace FoodDelieveryManagementAPI.DataRepositories
{
    public class AdminDataRepo : IAdminDataRepo
    {
        private ApiDbContext _dbContext;
        public AdminDataRepo(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<AppUser> GetAllAdminList()
        {
            return _dbContext.UserDetails.ToList().FindAll(user => user.UserRole == UserRole.Admin);
        }
        
        public void RemoveAdmin(AppUser user)
        {
            _dbContext.UserDetails.Remove(user);
            _dbContext.SaveChanges();
        }

        public void AddAdmin(AppUser user)
        {
            _dbContext.UserDetails.Add(user);
            _dbContext.SaveChanges();
        }

        public void UpdateAdmin(JsonPatchDocument<AppUser> updates, string userId)
        {
            var reqUser = _dbContext.UserDetails.FirstOrDefault(user => user.IdentityUserId == userId);
            updates.ApplyTo(reqUser);
            _dbContext.SaveChanges();
        }

    }
}
