using AutoMapper;
using FiapSmartCityDomain.Authentication;
using FiapSmartCityDomain.Authentication.ViewModel;
using FiapSmartCityDomain.Entities;
using FiapSmartCityDomain.TrafficManagement.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiapSmartCityInfrastructure.TrafficManagement
{
    public class TrafficManagementRepository : ITrafficManagementRepository
    {
        private readonly SmartCitiesContext _context;

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;


        public TrafficManagementRepository(SmartCitiesContext context,
            IConfiguration configuration, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;

            _configuration = configuration;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AccidentsViewModel> CreateAccidents(AccidentsViewModel accidentsViewModel)
        {

            var entity = new AccidentsEntity()
            {
                AddressComplement = accidentsViewModel.Address.Complement,
                AddressDescription = accidentsViewModel.Address.Description,
                CarBrand = accidentsViewModel.Cars.Brand,
                CarModel = accidentsViewModel.Cars.Model,
                CarYear = accidentsViewModel.Cars.Year,
                Gravity = accidentsViewModel.Gravity,
                ZipCode = accidentsViewModel.Address.ZipCode
            };

            var resultAdd = await _context.Accidents.AddAsync(entity);
            await _context.SaveChangesAsync();

            return accidentsViewModel;
        }

        public async Task<ICollection<AccidentsViewModel>> GetAccidents()
        {
            var result = new List<AccidentsViewModel>();
            var accidentsEntities = await _context.Accidents.ToListAsync();
            foreach (var accident in accidentsEntities)
            {
                result.Add(new AccidentsViewModel()
                {
                    Address = new AddressViewModel()
                    {
                        Complement = accident.AddressComplement,
                        Description = accident.AddressDescription,
                        ZipCode = accident.ZipCode
                    },
                    Cars = new CarViewModel()
                    {
                        Brand = accident.CarBrand,
                        Model = accident.CarModel,
                        Year = accident.CarYear
                    },
                    Gravity = accident.Gravity
                });
            }
            return result;
        }
    }
}
