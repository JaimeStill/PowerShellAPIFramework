using PowerShellAPIFramework.Core.Models;
using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Threading.Tasks;

namespace PowerShellAPIFramework.Core.Extensions
{
    public static class WmiExtensions
    {
        public static Task<QueryWmiModel> QueryWmi(this QueryWmiModel model)
        {
            return Task.Run(() =>
            {
                try
                {
                    InitialSessionState iss = InitialSessionState.CreateDefault();
                    iss.ExecutionPolicy = Microsoft.PowerShell.ExecutionPolicy.Unrestricted;

                    using (Runspace rs = RunspaceFactory.CreateRunspace(iss))
                    {
                        rs.Open();
                        
                        Command queryWmi = new Command("PowerShellAPIFramework.Core.Scripts.QueryWmi.ps1");
                        queryWmi.Parameters.Add("query", model.query);
                        queryWmi.Parameters.Add("properties", model.properties);
                        queryWmi.Parameters.Add("computername", model.computername);
                        queryWmi.Parameters.Add("wmiNamespace", model.wmiNamespace);

                        using (PowerShell ps = PowerShell.Create())
                        {
                            ps.Runspace = rs;
                            ps.Commands.AddCommand(queryWmi);
                            var results = ps.Invoke();

                            if (ps.HadErrors)
                            {
                                if (ps.Streams.Error.Count > 0)
                                {
                                    foreach (var error in ps.Streams.Error)
                                    {
                                        Console.WriteLine(error.Exception.GetExceptionMessageChain());
                                    }
                                }
                            }
                            else
                            {
                                foreach (var result in results)
                                {
                                    Console.WriteLine(result.ToString());
                                }
                            }
                        }
                    }

                    return model;
                }
                catch (Exception ex)
                {
                    model.results = ex.GetExceptionMessageChain();
                    return model;
                }
            });
        }
    }
}
