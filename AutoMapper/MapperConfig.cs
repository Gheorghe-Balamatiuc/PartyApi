using AutoMapper;
using PartyApi.DTOs;
using PartyApi.Models;

namespace PartyApi.Mapper;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<Member, MemberDTO>();
        CreateMap<Member, MemberDTO>().ReverseMap();
        CreateMap<Member, MemberNoIdDTO>().ReverseMap();

        CreateMap<Party, PartyDTO>();
        CreateMap<Party, PartyWithMembersDTO>();
        CreateMap<Party, PartyNoIdDTO>().ReverseMap();
    }
}