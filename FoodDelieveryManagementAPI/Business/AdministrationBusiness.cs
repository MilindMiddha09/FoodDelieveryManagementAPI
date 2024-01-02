using FoodDelieveryManagementAPI.Business.Interfaces;
using FoodDelieveryManagementAPI.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace FoodDelieveryManagementAPI.Business
{
    public class AdministrationBusiness : IAdministrationBusiness
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdministrationBusiness(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task CreateRole(Usertype role)
        {
            IdentityRole identityRole = new IdentityRole
            {
                Name = role.RoleName
            };
            IdentityResult result = await _roleManager.CreateAsync(identityRole);

            if (result.Succeeded)
            {
                return;
            }
        }
    }
}
