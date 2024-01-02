using FoodDelieveryManagementAPI.Models;
using System.Threading.Tasks;

namespace FoodDelieveryManagementAPI.Business.Interfaces
{
    public interface IAdministrationBusiness
    {
        Task CreateRole(Usertype role);
    }
}