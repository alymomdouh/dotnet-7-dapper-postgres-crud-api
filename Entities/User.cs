using System.Text.Json.Serialization;
using dotnet_7_dapper_postgres_crud_api.Enums;

namespace dotnet_7_dapper_postgres_crud_api.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public Role Role { get; set; }

        [JsonIgnore]
        public string? PasswordHash { get; set; }
    }
}
