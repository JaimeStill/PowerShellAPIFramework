using PowerShellAPIFramework.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;

namespace PowerShellAPIFramework.Core.Extensions
{
    public static class WmiExtensions
    {
        public static async Task<ReturnModel> QueryWmi(this QueryWmiModel model)
        {
            try
            {
                ReturnModel returnModel = new ReturnModel();
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
                                var exceptions = new StringBuilder();

                                foreach (var error in ps.Streams.Error)
                                {
                                    exceptions.AppendLine(error.Exception.GetExceptionMessageChain());
                                }

                                throw new Exception(exceptions.ToString());
                            }
                        }
                        else
                        {
                            foreach (var result in psResults)
                            {
                                if (psResults.IndexOf(result) == 0)
                                {
                                    returnModel.properties = result.Properties.Select(x => x.Name).ToList();
                                }

                                var resultModel = new ResultModel
                                {
                                    propertyValues = result.Properties.Select(x => new PropertyValueModel
                                    {
                                        property = x.Name,
                                        value = x.Value
                                    }).AsEnumerable()
                                };

                                returnModel.results.Add(resultModel);
                            }
                        }
                    }
                }

                return returnModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.GetExceptionMessageChain());
            }
        }
    }
}
