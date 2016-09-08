using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerShellAPIFramework.Core.Models
{
    public class ResultModel
    {
        public IEnumerable<PropertyValueModel> propertyValues { get; set; }
    }
}
