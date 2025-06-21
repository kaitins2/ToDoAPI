using AutoMapper;
using ToDoAPI.Model;
using ToDoAPI.Model.DTO;

namespace ToDoAPI.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<RegisterUserDto, UserDto>();
            
            CreateMap<ToDoItem, ToDoDto>();
            CreateMap<CreateToDoDto, ToDoDto>();

            CreateMap<RegisterUserDto, User>();

        }
    }
}
