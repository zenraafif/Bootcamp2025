namespace UserApi.Services
{
    public interface IUserService
    {
        Task<string> GetUserNameAsync(int id);
    }
}
