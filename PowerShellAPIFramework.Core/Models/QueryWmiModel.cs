using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerShellAPIFramework.Core.Models
{
    public class QueryWmiModel
    {
        public string query { get; set; }
        public string[] properties { get; set; }
        public string computername { get; set; }
        public string wmiNamespace { get; set; }
        public string results { get; set; }
    }
}
