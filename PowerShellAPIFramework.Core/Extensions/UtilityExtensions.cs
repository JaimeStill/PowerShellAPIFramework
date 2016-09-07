using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerShellAPIFramework.Core.Extensions
{
    public static class UtilityExtensions
    {
        public static string GetExceptionMessageChain(this Exception ex)
        {
            var message = new StringBuilder(ex.Message);
            message.AppendLine();

            if (ex.InnerException != null)
            {
                message.AppendLine(GetExceptionMessageChain(ex.InnerException));
            }

            return message.ToString();
        }
    }
}
