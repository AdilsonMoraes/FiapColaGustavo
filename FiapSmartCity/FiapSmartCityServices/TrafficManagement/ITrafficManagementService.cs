using FiapSmartCityDomain.Authentication;
using FiapSmartCityDomain.Authentication.ViewModel;
using FiapSmartCityDomain.Entities;
using FiapSmartCityDomain.TrafficManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiapSmartCityServices.TrafficManagement
{
    public interface ITrafficManagementService
    {
        Task<ICollection<AccidentsViewModel>> GetAccidents();
        Task<AccidentsViewModel> CreateAccidents(AccidentsViewModel accidentsViewModel);
    }
}
