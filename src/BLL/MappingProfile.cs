using AutoMapper;
using BLL.EtitiesDTO;
using DAL.Entities;

namespace BLL
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDto>()
                .ForMember(c => 
                        c.FullAddress,
                    opt => opt.MapFrom(
                        x => string.Join(' ', x.Address, x.Country)));




            CreateMap<CompanyForCreationDto, Company>()
                .ForMember(b =>
                        b.Address,
                    opt => 
                        opt.MapFrom(x=>x.FullAddress))
                .ForMember(b=>b.Country, opt=>opt.MapFrom(x=>x.FullAddress)
                );




            CreateMap<UserForRegistrationDto, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
        }
    }
}
