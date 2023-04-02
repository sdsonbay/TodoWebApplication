using AutoMapper;
using TodoWebApplication.Dtos;
using TodoWebApplication.Models;

namespace TodoWebApplication.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
        CreateMap<Todo, TodoDto>();
        CreateMap<TodoDto, Todo>();
        CreateMap<Subtodo, SubtodoDto>();
        CreateMap<SubtodoDto, Subtodo>();
    }
}