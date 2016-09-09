using System.Collections.Generic;
using System.Security;

namespace PowerShellAPIFramework.Core.Models
{
    public class QueryWmiModel
    {
        public string query { get; set; }
        public string[] properties { get; set; }
        public string computername { get; set; }
        public string wmiNamespace { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public bool isRemoteConnection { get; set; }
        public SecureString securePassword
        {
            get
            {
                var value = new SecureString();

                foreach (var c in password)
                {
                    value.AppendChar(c);
                }

                return value;
            }
        }
    }
}
