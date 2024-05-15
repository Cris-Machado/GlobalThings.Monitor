using AutoMapper;
using GlobalThings.Domain.Entities;
using GlobalThings.Domain.Models;

namespace GlobalThings.Repository.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SensorModel, SensorEntity>().ReverseMap();
            CreateMap<EquipamentModel, EquipamentEntity>().ReverseMap();
            CreateMap<MeasurementModel, MeasurementEntity>().ReverseMap();
        }
    }
}
