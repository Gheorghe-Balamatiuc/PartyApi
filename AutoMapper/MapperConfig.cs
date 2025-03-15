using AutoMapper;
using PartyApi.DTOs;
using PartyApi.Models;

namespace PartyApi.Mapper;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<Party, PartyDTO>();
        CreateMap<Party, PartyWithMembersDTO>();
        CreateMap<Party, PartyNoIdDTO>().ReverseMap();

        CreateMap<User, UserDTO>();
        CreateMap<User, UserWithPartiesDTO>();
        CreateMap<User, UserNoIdDTO>().ReverseMap();
    }
}