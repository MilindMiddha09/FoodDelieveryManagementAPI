using FoodDelieveryManagementAPI.Data;
using FoodDelieveryManagementAPI.DataRepositories.Interfaces;
using FoodDelieveryManagementAPI.Enum;
using FoodDelieveryManagementAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Linq;

namespace FoodDelieveryManagementAPI.DataRepositories
{
    public class CustomerDataRepo : ICustomerDataRepo
    {
        private readonly ApiDbContext _dbContext;

        public CustomerDataRepo(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<AppUser> GetAllCustomers()
        {
            return _dbContext.UserDetails.ToList().FindAll(user => user.UserRole == UserRole.Customer);
        }

        public void AddCustomer(AppUser user)
        {
            _dbContext.UserDetails.Add(user);
            _dbContext.SaveChanges();
        }

        public void DeleteCustomer(AppUser user)
        {
            _dbContext.UserDetails.Remove(user);
            _dbContext.SaveChanges();
        }

        public void UpdateCustomer(JsonPatchDocument<AppUser> updates, string identityUserId)
        {
            var reqUser = _dbContext.UserDetails.FirstOrDefault(user => user.IdentityUserId.Equals(identityUserId));
            updates.ApplyTo(reqUser);
            _dbContext.SaveChanges();
        }
    }
}
