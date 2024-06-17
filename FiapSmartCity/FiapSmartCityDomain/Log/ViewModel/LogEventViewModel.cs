using FiapSmartCityDomain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiapSmartCityDomain.Log.ViewModel
{
    public class LogEventViewModel
    {
        public LogType Type { get; set; }

        public object Content { get; set; }

        public Guid SessionGuid { get; set; }

        public string ApplicationName { get; set; }

        public DateTime Time { get; set; }
    }
}
