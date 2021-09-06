using Accounts.Grpc.Protos;
using AutoMapper;
using Domain;

namespace Accounts.Grpc
{
    public class ActivitiesProfile : Profile
    {
        public ActivitiesProfile()
        {
            CreateMap<Activity, ActivitiesModel>().ReverseMap();
        }
    }
}