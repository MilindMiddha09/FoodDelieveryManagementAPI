using FoodDelieveryManagementAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace FoodDelieveryManagementAPI.Business.Interfaces
{
    public interface IMenuBusiness
    {
        bool UpdateMenu(MenuProduct product, string userName);
    }
}