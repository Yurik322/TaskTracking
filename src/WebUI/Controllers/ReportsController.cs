using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.EtitiesDTO.Report;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace WebUI.Controllers
{
    [Route("api/reports")]
    [Authorize]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly ILoggerManager _logger;

        public ReportsController(IReportService reportService, ILoggerManager logger)
        {
            _reportService = reportService;
            _logger = logger;
        }

        // GET: /reports
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetReports()
        {
            try
            {
                var claims = User.Claims;
                var reportsDto = await _reportService.GetAllReports();

                return Ok(reportsDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetReports)} action {ex}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }

        // GET: /reports/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReportById(int id)
        {
            try
            {
                var reportResult = await _reportService.GetReportById(id);
                return Ok(reportResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetReportsById action: {ex.Message}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }

        // POST: /reports/create
        [HttpPost]
        public async Task<IActionResult> CreateReport([FromBody] ReportForCreationDto report)
        {
            try
            {
                if (report == null)
                {
                    _logger.LogError("Reports object sent from client is null.");
                    return BadRequest("Reports object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid report object sent from client.");
                    return BadRequest("Invalid model object");
                }

                await _reportService.CreateReport(report);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateReports action: {ex.Message}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }

        // PUT: /reports/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReport(int id, [FromBody] ReportForCreationDto report)
        {
            try
            {
                if (report == null)
                {
                    _logger.LogError("Reports object sent from client is null.");
                    return BadRequest("Reports object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid report object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var reportEntity = _reportService.GetReportById(id);
                if (reportEntity == null)
                {
                    _logger.LogError($"Reports with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                await _reportService.UpdateReport(id, report);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateReports action: {ex.Message}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }

        // DELETE: /reports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReport(int id)
        {
            try
            {
                var report = _reportService.GetReportById(id);
                if (report == null)
                {
                    _logger.LogError($"Report with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                await _reportService.DeleteReport(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteReport action: {ex.Message}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }
    }
}
