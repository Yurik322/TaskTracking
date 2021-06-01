using AutoMapper;
using BLL.EtitiesDTO;
using BLL.EtitiesDTO.Attachment;
using BLL.EtitiesDTO.Employee;
using BLL.EtitiesDTO.Issue;
using BLL.EtitiesDTO.Project;
using BLL.EtitiesDTO.Report;
using DAL.Entities;

namespace BLL
{
    /// <summary>
    /// Class for mapping profile.
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
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
