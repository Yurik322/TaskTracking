using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.EtitiesDTO;
using BLL.Interfaces;
using DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Route("api/companies")]
    [Authorize]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IUnitOfWork _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CompaniesController(IUnitOfWork repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetCompanies()
        {
            try
            {
                var claims = User.Claims;

                //TODO ERROR
                var companies = _repository.Company.GetAllCompanies(trackChanges: false);

                var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);

                return Ok(companiesDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetCompanies)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("Privacy")]
        [Authorize(Roles = "Administrator")]
        public IActionResult Privacy()
        {
            var claims = User.Claims
                .Select(c => new { c.Type, c.Value })
                .ToList();

            return Ok(claims);
        }
    }
}
