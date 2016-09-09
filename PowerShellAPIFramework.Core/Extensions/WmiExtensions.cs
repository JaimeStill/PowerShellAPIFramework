using PowerShellAPIFramework.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Threading.Tasks;

namespace PowerShellAPIFramework.Core.Extensions
{
    public static class WmiExtensions
    {
        public static async Task<IEnumerable<ResultModel>> QueryWmi(this QueryWmiModel model)
        {
            try
            {
                List<ResultModel> results = new List<ResultModel>();
                InitialSessionState iss = InitialSessionState.CreateDefault();
                iss.ExecutionPolicy = Microsoft.PowerShell.ExecutionPolicy.Unrestricted;

                using (Runspace rs = RunspaceFactory.CreateRunspace(iss))
                {
                    rs.Open();

                    var script = string.Empty;

                    if (model.isRemoteConnection)
                        script = await ("PowerShellAPIFramework.Core.Scripts.query-wmi-remote.ps1").GetTextFromEmbeddedResource();
                    else
                        script = await ("PowerShellAPIFramework.Core.Scripts.query-wmi.ps1").GetTextFromEmbeddedResource();

                    Command queryWmi = new Command(script, true);                    
                    queryWmi.Parameters.Add("query", model.query);
                    queryWmi.Parameters.Add("properties", model.properties);
                    queryWmi.Parameters.Add("computername", model.computername);
                    queryWmi.Parameters.Add("wmiNamespace", model.wmiNamespace);

                    if (model.isRemoteConnection)
                        queryWmi.Parameters.Add("credential", new PSCredential(model.username, model.securePassword));

                    using (PowerShell ps = PowerShell.Create())
                    {
                        ps.Runspace = rs;
                        ps.Commands.AddCommand(queryWmi);
                        var psResults = ps.Invoke();

                        if (ps.HadErrors)
                        {
                            if (ps.Streams.Error.Count > 0)
                            {
                                foreach (var error in ps.Streams.Error)
                                {
                                    throw new Exception(error.Exception.GetExceptionMessageChain());
                                }
                            }
                        }
                        else
                        {
                            foreach (var result in psResults)
                            {
                                var resultModel = new ResultModel
                                {
                                    propertyValues = result.Properties.Select(x => new PropertyValueModel
                                    {
                                        property = x.Name,
                                        value = x.Value
                                    }).AsEnumerable()
                                };

                                results.Add(resultModel);
                            }
                        }
                    }
                }

                return results.AsEnumerable();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.GetExceptionMessageChain());
            }
        }
    }
}
