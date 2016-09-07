using PowerShellAPIFramework.Core.Extensions;
using PowerShellAPIFramework.Core.Models;
using System.Threading.Tasks;
using System.Web.Http;

namespace PowerShellAPIFramework.Web.Controllers
{
    public class WmiApiController : ApiController
    {
        [Route("api/wmi/queryWmi")]
        [HttpPost]
        public async Task<QueryWmiModel> QueryWmi(QueryWmiModel model)
        {
            return await model.QueryWmi();
        }
    }
}
