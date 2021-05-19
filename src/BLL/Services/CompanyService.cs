using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BLL.EtitiesDTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public CompanyService(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges)
        {
            var companies = _mapper.Map<IEnumerable<Company>, IEnumerable<CompanyDto>>(_repository.Company.GetAllCompanies(trackChanges: false));
            
            return _mapper.Map<IEnumerable<CompanyDto>>(companies);
        }
    }
}
