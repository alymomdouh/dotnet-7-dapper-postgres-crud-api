using dotnet_7_dapper_postgres_crud_api.Entities;
using dotnet_7_dapper_postgres_crud_api.Models.Users;

namespace dotnet_7_dapper_postgres_crud_api.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(int id);
        Task Create(CreateRequest model);
        Task Update(int id, UpdateRequest model);
        Task Delete(int id);
    }
}
