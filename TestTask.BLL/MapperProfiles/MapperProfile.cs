using AutoMapper;
using TestTask.BLL.Models;
using TestTask.DAL.Entities;

namespace TestTask.BLL.MapperProfiles;

public class MapperProfile: Profile
{
    public MapperProfile()
    {
        CreateMap<MessageEntity, MessageModel>().ReverseMap();
    }
}
