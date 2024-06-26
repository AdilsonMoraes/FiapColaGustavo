using FiapSmartCityDomain.Enums;

namespace FiapSmartCityDomain.TrafficManagement.ViewModel
{
    public class AccidentsViewModel
    {
        public CarViewModel Cars { get; set; }
        public AddressViewModel Address { get; set; }
        public GravityType Gravity { get; set; }
    }
}
