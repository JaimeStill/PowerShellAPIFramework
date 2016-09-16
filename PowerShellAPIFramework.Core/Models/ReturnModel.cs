using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerShellAPIFramework.Core.Models
{
    public class ReturnModel
    {
        public ReturnModel()
        {
            properties = new List<string>();
            results = new List<ResultModel>();
        }

        public List<string> properties { get; set; }
        public List<ResultModel> results { get; set; }
    }
}
