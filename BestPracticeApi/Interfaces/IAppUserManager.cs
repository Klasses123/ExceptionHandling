using BestPracticeApi.Models;
using System.Threading.Tasks;

namespace BestPracticeApi.Interfaces
{
    public interface IAppUserManager
    {
        AppUser GetUser();
        Task<AppUser> GetUserAsync();
    }
}
