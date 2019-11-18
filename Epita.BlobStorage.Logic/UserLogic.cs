using System.Collections.Generic;
using System.Threading.Tasks;
using Epita.BlobStorage.Logic.Contracts;
using Epita.BlobStorage.Model;
using Epita.BlobStorage.Services.Contracts;

namespace Epita.BlobStorage.Logic
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserService userService;
        
        public UserLogic(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<User> LoginAsync(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            string userId = await userService.LoginAsync(login, password).ConfigureAwait(false);

            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }

            return await userService.GetByIdAsync(userId).ConfigureAwait(false);
        }

        public Task<IEnumerable<User>> GetAsync(Role? role = null) => userService.GetAsync(role);

        public Task<User> GetByIdAsync(string userId) => userService.GetByIdAsync(userId);
    }
}