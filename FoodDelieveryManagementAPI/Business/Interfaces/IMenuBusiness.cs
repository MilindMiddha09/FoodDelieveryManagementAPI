﻿using FoodDelieveryManagementAPI.Models;

namespace FoodDelieveryManagementAPI.Business.Interfaces
{
    public interface IMenuBusiness
    {
        void UpdateMenu(MenuProduct product, string userName);
    }
}