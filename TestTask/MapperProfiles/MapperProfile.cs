using AutoMapper;
using TestTask.API.DTO;
using TestTask.BLL.Models;

namespace TestTask.API.MapperProfiles;

public class MapperProfile: Profile
{
    public MapperProfile()
    {
        CreateMap<MessageModel, MessageDto>().ReverseMap();
        CreateMap<MessageModel, CreateMessageDto>().ReverseMap();
    }
}
