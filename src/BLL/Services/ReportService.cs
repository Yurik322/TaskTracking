using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.EtitiesDTO.Report;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System.Linq;

namespace BLL.Services
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public ReportService(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReportDto>> GetAllReports()
        {
            var companies =
                _mapper.Map<IEnumerable<Report>, IEnumerable<ReportDto>>(await _repository.Report.GetAllReports(trackChanges: false));

            return _mapper.Map<IEnumerable<ReportDto>>(companies);
        }

        public async Task<ReportDto> GetReportById(int id)
        {
            var report = await _repository.Report.GetReportById(id);

            return _mapper.Map<ReportDto>(report);
        }

        public async Task CreateReport(ReportForCreationDto report)
        {
            var reportEntity = _mapper.Map<Report>(report);

            _repository.Report.CreateReport(reportEntity);
            await _repository.SaveAsync();
        }

        public async Task UpdateReport(int id, ReportForCreationDto report)
        {
            var reportEntity = await _repository.Report.GetReportById(id);

            _mapper.Map(report, reportEntity);
            _repository.Report.UpdateReport(reportEntity);
            await _repository.SaveAsync();
        }

        public async Task DeleteReport(int id)
        {
            var reportEntity = await _repository.Report.GetReportById(id);

            _repository.Report.DeleteReport(reportEntity);
            await _repository.SaveAsync();
        }
    }
}
