using FiapSmartCityDomain.TrafficManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiapSmartCityInfrastructure.TrafficManagement
{
    public interface ITrafficManagementRepository
    {
        Task<ICollection<AccidentsViewModel>> GetAccidents();
        Task<AccidentsViewModel> CreateAccidents(AccidentsViewModel accidentsViewModel);
    }
}
