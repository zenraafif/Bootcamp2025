using UserApi.Models;
using UserApi.Repositories;

namespace UserApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        public UserService(IUserRepository repo) => _repo = repo;

        public async Task<string> GetUserNameAsync(int id)
        {
            var user = await _repo.GetByIdAsync(id);
            return user?.Name ?? "Unknown";
        }
    }
}
