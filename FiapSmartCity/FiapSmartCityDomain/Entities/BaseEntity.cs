using System.ComponentModel.DataAnnotations;

namespace FiapSmartCityDomain.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
