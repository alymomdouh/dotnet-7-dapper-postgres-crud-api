using AutoMapper;
using dotnet_7_dapper_postgres_crud_api.Entities;
using dotnet_7_dapper_postgres_crud_api.Models.Users;

namespace dotnet_7_dapper_postgres_crud_api.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // CreateRequest -> User
            CreateMap<CreateRequest, User>();

            // UpdateRequest -> User
            CreateMap<UpdateRequest, User>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        // ignore both null & empty string properties
                        if (prop == null) return false;
                        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                        // ignore null role
                        if (x.DestinationMember.Name == "Role" && src.Role == null) return false;

                        return true;
                    }
                ));
        }
    }
}
