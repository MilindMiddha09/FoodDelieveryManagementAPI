using FoodDelieveryManagementAPI.Business.Interfaces;
using FoodDelieveryManagementAPI.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace FoodDelieveryManagementAPI.Business
{
    public class AuthBusiness : IAuthBusiness
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        public AuthBusiness(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task Login(LoginDetails loginDetails)
        {
            if (loginDetails == null)
            {
                throw new ArgumentException();
            }

            var result = await _signInManager.PasswordSignInAsync(loginDetails.Email, loginDetails.Password, loginDetails.RememberMe, false);

            if (result.Succeeded)
            {
                return;
            }
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
            return;
        }
    }
}
