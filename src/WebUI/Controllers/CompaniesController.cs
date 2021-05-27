using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.EtitiesDTO;
using BLL.EtitiesDTO.Company;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Route("api/company")]
    [Authorize]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        //TODO
        private readonly IUnitOfWork _repository;


        public CompaniesController(ICompanyService companyService, ILoggerManager logger, IMapper mapper, IUnitOfWork repository)
        {
            _companyService = companyService;
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }

        // GET: /company/companies
        //[Authorize]
        [HttpGet]
        public IActionResult GetCompanies()
        {
            try
            {
                var claims = User.Claims;
                var companiesDto = _companyService.GetAllCompanies(); 

                return Ok(companiesDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetCompanies)} action {ex}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }

        //TODO
        [HttpGet("{id}", Name = "CompanyById")]
        public IActionResult GetCompanyById(int id)
        {
            try
            {
                var company = _repository.Company.GetCompanyById(id);
                if (company == null)
                {
                    _logger.LogError($"Company with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned company with id: {id}");

                    var companyResult = _mapper.Map<CompanyDto>(company);
                    return Ok(companyResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetCompanyById action: {ex.Message}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }

        //TODO
        // GET: /company/5/employee
        [HttpGet("{id}/employee")]
        public IActionResult GetCompanyWithDetails(int id)
        {
            try
            {
                var company = _repository.Company.GetCompanyWithDetails(id);
                if (company == null)
                {
                    _logger.LogError($"Company with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned company with details for id: {id}");

                    var companyResult = _mapper.Map<CompanyDto>(company);
                    return Ok(companyResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetCompanyWithDetails action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: /company/create
        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyForCreationDto company)
        {
            try
            {
                if (company == null)
                {
                    _logger.LogError("Company object sent from client is null.");
                    return BadRequest("Company object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid company object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var companyEntity = _mapper.Map<Company>(company);

                _repository.Company.CreateCompany(companyEntity);
                await _repository.SaveAsync();

                var createdCompany = _mapper.Map<CompanyDto>(companyEntity);

                return CreatedAtRoute("CompanyById", new { id = createdCompany.Id }, createdCompany);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateCompany action: {ex.Message}");
                return StatusCode(500, "Internal server error"+ ex);
            }
        }

        // PUT: /company/5/edit
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, [FromBody] CompanyForCreationDto company)
        {
            try
            {
                if (company == null)
                {
                    _logger.LogError("Company object sent from client is null.");
                    return BadRequest("Company object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid company object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var companyEntity = _repository.Company.GetCompanyById(id);
                if (companyEntity == null)
                {
                    _logger.LogError($"Company with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(company, companyEntity);

                _repository.Company.UpdateCompany(companyEntity);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateCompany action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: /company/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
            //var issue = _context.Issues.Where(i => i.IssueId == id).FirstOrDefault();
            //if (issue == null)
            //    return NotFound();

            //try
            //{
            //    _context.Issues.Remove(issue);
            //    _context.SaveChanges();
            //    return Ok();
            //}
            //catch (DbUpdateException)
            //{
            //    return BadRequest();
            //}
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




//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using AutoMapper;
//using BLL.EtitiesDTO;
//using BLL.Interfaces;
//using DAL.Entities;
//using DAL.Interfaces;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace WebUI.Controllers
//{
//    [Route("api/company")]
//    [Authorize]
//    [ApiController]
//    public class CompaniesController : ControllerBase
//    {
//        private readonly ICompanyService _companyService;
//        private readonly ILoggerManager _logger;
//        private readonly IMapper _mapper;

//        //TODO
//        private readonly IUnitOfWork _repository;


//        public CompaniesController(ICompanyService companyService, ILoggerManager logger, IMapper mapper, IUnitOfWork repository)
//        {
//            _companyService = companyService;
//            _logger = logger;
//            _mapper = mapper;
//            _repository = repository;
//        }

//        // GET: /company/companies
//        //[Authorize]
//        [HttpGet]
//        public IActionResult GetCompanies()
//        {
//            try
//            {
//                var claims = User.Claims;
//                var companiesDto = _companyService.GetAllCompanies(trackChanges: false);

//                return Ok(companiesDto);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError($"Something went wrong in the {nameof(GetCompanies)} action {ex}");
//                return StatusCode(500, "Internal server error");
//            }
//        }

//        //TODO
//        [HttpGet("{id}", Name = "CompanyById")]
//        public IActionResult GetCompanyById(int id)
//        {
//            try
//            {
//                var company = _repository.Company.GetCompanyById(id);
//                if (company == null)
//                {
//                    _logger.LogError($"Company with id: {id}, hasn't been found in db.");
//                    return NotFound();
//                }
//                else
//                {
//                    _logger.LogInfo($"Returned company with id: {id}");

//                    var companyResult = _mapper.Map<CompanyDto>(company);
//                    return Ok(companyResult);
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError($"Something went wrong inside GetCompanyById action: {ex.Message}");
//                return StatusCode(500, "Internal server error");
//            }
//        }

//        // GET: /company/companies/5
//        [HttpGet("{id}")]
//        public IActionResult Details(int id)
//        {
//            throw new NotImplementedException();
//            //var issue = _context.Issues.Where(i => i.IssueId == id).FirstOrDefault();
//            //if (issue == null)
//            //    return NotFound();

//            //return Ok(issue);
//        }

//        //TODO
//        // POST: /company/create
//        [HttpPost]
//        public async Task<IActionResult> CreateCompany([FromForm] CompanyForCreationDto company)
//        {
//            try
//            {
//                if (company == null)
//                {
//                    _logger.LogError("Company object sent from client is null.");
//                    return BadRequest("Company object is null");
//                }

//                if (!ModelState.IsValid)
//                {
//                    _logger.LogError("Invalid company object sent from client.");
//                    return BadRequest("Invalid model object");
//                }

//                var companyEntity = _mapper.Map<Company>(company);

//                _repository.Company.Create(companyEntity);
//                await _repository.SaveAsync();

//                var createdCompany = _mapper.Map<CompanyDto>(companyEntity);

//                return CreatedAtRoute("CompanyById", new { id = createdCompany.Id }, createdCompany);
//                //return CreatedAtRoute("CompanyById", new { id = createdCompany.Id }, createdCompany);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError($"Something went wrong inside CreateCompany action: {ex.Message}");
//                return StatusCode(500, "Internal server error");
//            }
//        }


//        //[HttpPost]
//        //public IActionResult Create([FromBody] CompanyDto issue)
//        //{
//        //    throw new NotImplementedException();
//        //    //if (!ModelState.IsValid)
//        //    //    return BadRequest();

//        //    //// Validate enums
//        //    //if (!Enum.IsDefined(typeof(Priority), issue.Priority) ||
//        //    //    !Enum.IsDefined(typeof(IssueType), issue.IssueType) ||
//        //    //    !Enum.IsDefined(typeof(Status), issue.StatusType))
//        //    //{
//        //    //    return BadRequest();
//        //    //}

//        //    //// A small trick. Do not tell anyone... :)
//        //    //var projectId = issue.IssueId;
//        //    //var project = _context.Projects.Where(p => p.ProjectId == projectId).FirstOrDefault();
//        //    //if (project == null)
//        //    //    return NotFound();

//        //    //var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
//        //    //var user = _context.Users.Find(userId);
//        //    //try
//        //    //{
//        //    //    // Create new issue
//        //    //    var newIssue = new Issue()
//        //    //    {
//        //    //        Title = issue.Title,
//        //    //        Description = issue.Description,
//        //    //        Priority = issue.Priority,
//        //    //        IssueType = issue.IssueType,
//        //    //        StatusType = issue.StatusType,
//        //    //        CreatedAt = DateTime.Now,
//        //    //        UpdatedAt = DateTime.Now,
//        //    //        Creator = user.UserName,
//        //    //        Project = project
//        //    //    };

//        //    //    _context.Issues.Add(newIssue);
//        //    //    _context.SaveChanges();

//        //    //    // Fix for net::ERR_INCOMPLETE_CHUNKED_ENCODING
//        //    //    newIssue.Project = null;

//        //    //    return Ok(newIssue);
//        //    //}
//        //    //catch (DbUpdateException)
//        //    //{
//        //    //    return BadRequest();
//        //    //}
//        //}

//        // PUT: /company/companies/5/edit
//        [HttpPut("{id}")]
//        public IActionResult Edit(int id, [FromBody] CompanyDto company)
//        {
//            throw new NotImplementedException();
//            //if (!ModelState.IsValid)
//            //    return BadRequest();

//            //var _issue = _context.Issues.Where(i => i.IssueId == id).FirstOrDefault();
//            //if (_issue == null)
//            //    return NotFound();

//            //// Validate enums
//            //if (!Enum.IsDefined(typeof(Priority), issue.Priority) ||
//            //    !Enum.IsDefined(typeof(IssueType), issue.IssueType) ||
//            //    !Enum.IsDefined(typeof(Status), issue.StatusType))
//            //{
//            //    return BadRequest();
//            //}

//            //// Only the fields we want to update
//            //_issue.Title = issue.Title;
//            //_issue.Priority = issue.Priority;
//            //_issue.IssueType = issue.IssueType;
//            //_issue.StatusType = issue.StatusType;
//            //_issue.Description = issue.Description;

//            //_context.Issues.Update(_issue);
//            //_context.SaveChanges();

//            //return Ok();
//        }

//        // DELETE: /company/companies/5
//        [HttpDelete("{id}")]
//        public IActionResult Delete(int id)
//        {
//            throw new NotImplementedException();
//            //var issue = _context.Issues.Where(i => i.IssueId == id).FirstOrDefault();
//            //if (issue == null)
//            //    return NotFound();

//            //try
//            //{
//            //    _context.Issues.Remove(issue);
//            //    _context.SaveChanges();
//            //    return Ok();
//            //}
//            //catch (DbUpdateException)
//            //{
//            //    return BadRequest();
//            //}
//        }


//        [HttpGet("Privacy")]
//        [Authorize(Roles = "Administrator")]
//        public IActionResult Privacy()
//        {
//            var claims = User.Claims
//                .Select(c => new { c.Type, c.Value })
//                .ToList();

//            return Ok(claims);
//        }
//    }
//}

