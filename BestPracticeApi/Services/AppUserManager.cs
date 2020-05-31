using BestPracticeApi.Interfaces;
using BestPracticeApi.Models;
using System;
using System.Threading.Tasks;

namespace BestPracticeApi.Services
{
    public class AppUserManager : IAppUserManager
    {
        public AppUser GetUser()
        {
            var rnd = new Random();
            return new AppUser
            {
                Id = Guid.NewGuid(),
                UserName = "un",
                Name = "Daniel",
                LastName = "Smith",
                Age = rnd.Next(5, 30)
            };
        }

        public async Task<AppUser> GetUserAsync()
        {
            return await Task.Run(() => GetUser());
        }
    }
}
