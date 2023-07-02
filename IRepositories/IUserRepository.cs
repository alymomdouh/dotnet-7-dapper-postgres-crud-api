using dotnet_7_dapper_postgres_crud_api.Entities;

namespace dotnet_7_dapper_postgres_crud_api.IRepositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(int id);
        Task<User> GetByEmail(string email);
        Task Create(User user);
        Task Update(User user);
        Task Delete(int id);
    }
}
