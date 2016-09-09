using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PowerShellAPIFramework.Core.Extensions
{
    public static class UtilityExtensions
    {
        public static string GetExceptionMessageChain(this Exception ex)
        {
            var message = new StringBuilder(ex.Message);

            if (ex.InnerException != null)
            {
                message.AppendLine(GetExceptionMessageChain(ex.InnerException));
            }

            return message.ToString();
        }

        // Thanks to PetSerAl and Jaime Macias on Stackoverflow
        // http://stackoverflow.com/questions/39359587/load-c-sharp-embedded-resource-path-into-powershell-command-class
        public static async Task<string> GetTextFromEmbeddedResource(this string resourceName)
        {
            string text = string.Empty;

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    text = await reader.ReadToEndAsync();
                }
            }

            return text;
        }
    }
}
