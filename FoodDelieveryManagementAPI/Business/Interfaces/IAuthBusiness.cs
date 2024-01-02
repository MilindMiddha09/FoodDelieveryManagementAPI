using FoodDelieveryManagementAPI.Models;
using System.Threading.Tasks;

namespace FoodDelieveryManagementAPI.Business.Interfaces
{
    public interface IAuthBusiness
    {
        Task Login(LoginDetails loginDetails);
        Task Logout();
    }
}