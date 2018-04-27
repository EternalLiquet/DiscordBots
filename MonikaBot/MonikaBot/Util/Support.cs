using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using System.Collections.Specialized;

namespace MonikaBot
{
    public static class Support
    {
        public static readonly NameValueCollection config = ConfigurationManager.AppSettings;

        public static void StartupOperations()
        {
            if (!Directory.Exists(config.Get("botFilesPath")))
            {
                Directory.CreateDirectory(config.Get("botFilesPath"));
            }
        }
    }
}
