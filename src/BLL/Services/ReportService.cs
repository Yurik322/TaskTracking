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
    /// <summary>
    /// Class for report services.
    /// </summary>
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public ReportService(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Method for get all ReportDto objects.
        /// </summary>
        /// <returns>collection of ReportDto.</returns>
        public async Task<IEnumerable<ReportDto>> GetAllReports()
        {
            var companies =
                _mapper.Map<IEnumerable<Report>, IEnumerable<ReportDto>>(await _repository.Report.GetAllReports(trackChanges: false));

            return _mapper.Map<IEnumerable<ReportDto>>(companies);
        }

        /// <summary>
        /// Method for get ReportDto object by id.
        /// </summary>
        /// <param name="id">id of ReportDto.</param>
        /// <returns>object of ReportDto</returns>
        public async Task<ReportDto> GetReportById(int id)
        {
            var report = await _repository.Report.GetReportById(id);

            return _mapper.Map<ReportDto>(report);
        }

        /// <summary>
        /// Method for create ReportForCreationDto.
        /// </summary>
        /// <param name="report">new report.</param>
        /// <returns>new object.</returns>
        public async Task CreateReport(ReportForCreationDto report)
        {
            var reportEntity = _mapper.Map<Report>(report);

            _repository.Report.CreateReport(reportEntity);
            await _repository.SaveAsync();
        }

        /// <summary>
        /// Method for update ReportForCreationDto.
        /// </summary>
        /// <param name="id">id of updated report.</param>
        /// <param name="report">updated report.</param>
        /// <returns>updated object.</returns>
        public async Task UpdateReport(int id, ReportForCreationDto report)
        {
            var reportEntity = await _repository.Report.GetReportById(id);

            _mapper.Map(report, reportEntity);
            _repository.Report.UpdateReport(reportEntity);
            await _repository.SaveAsync();
        }

        /// <summary>
        /// Method for deleting ReportDto.
        /// </summary>
        /// <param name="id">id of report.</param>
        /// <returns>deleted object.</returns>
        public async Task DeleteReport(int id)
        {
            var reportEntity = await _repository.Report.GetReportById(id);

            _repository.Report.DeleteReport(reportEntity);
            await _repository.SaveAsync();
        }
    }
}
