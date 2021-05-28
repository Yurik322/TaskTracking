using AutoMapper;
using BLL.EtitiesDTO;
using BLL.EtitiesDTO.Attachment;
using BLL.EtitiesDTO.Company;
using BLL.EtitiesDTO.Employee;
using BLL.EtitiesDTO.Issue;
using BLL.EtitiesDTO.Project;
using BLL.EtitiesDTO.Report;
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

            CreateMap<Issue, IssueDto>();
            CreateMap<IssueForCreationDto, Issue>();

            CreateMap<Attachment, AttachmentDto>();
            CreateMap<AttachmentForCreationDto, Attachment>();

            CreateMap<Project, ProjectDto>();
            CreateMap<ProjectForCreationDto, Project>();

            CreateMap<Report, ReportDto>();
            CreateMap<ReportForCreationDto, Report>();

            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeForCreationDto, Employee>();

            CreateMap<UserForRegistrationDto, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
        }
    }
}
