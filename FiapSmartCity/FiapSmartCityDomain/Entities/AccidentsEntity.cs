using FiapSmartCityDomain.Enums;
using FiapSmartCityDomain.TrafficManagement.ViewModel;

namespace FiapSmartCityDomain.Entities
{
    public class AccidentsEntity : BaseEntity
    {
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public string CarYear { get; set; }
        public string AddressDescription { get; set; }
        public string AddressComplement { get; set; }
        public int ZipCode { get; set; }
        public GravityType Gravity { get; set; }
    }
}
