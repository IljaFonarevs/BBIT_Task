using AutoMapper;
using Uzd2.Datatypes;
using Uzd2.DTOs;

namespace Uzd2
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Maja, MajaDTO>();
            CreateMap<Dzivoklis, DzivDTO>();
            CreateMap<Iedzivotajs, IedzDTO>();

            CreateMap<MajaDTO, Maja>();
            CreateMap<DzivDTO, Dzivoklis>();
            CreateMap<IedzDTO, Iedzivotajs>();
        }
    }
}
