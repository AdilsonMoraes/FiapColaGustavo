using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiapSmartCityDomain.Entities
{
    public class AuditableEntity : BaseEntity
    {
        public string CreatedBy { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDateTime { get; set; }
    }
}
