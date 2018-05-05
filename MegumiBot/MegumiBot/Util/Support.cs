using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using System.Collections.Specialized;

namespace MegumiBot
{
    public static class Support
    {
        public static readonly NameValueCollection config = ConfigurationManager.AppSettings;
        public static string botToken;

        public static void StartupOperations()
        {
            if (!Directory.Exists(config.Get("botFilesPath")))
            {
                Directory.CreateDirectory(config.Get("botFilesPath"));
            }

            if (!Directory.Exists(config.Get("botFilesPath") + config.Get("botAnnouncementPath")))
            {
                Directory.CreateDirectory(config.Get("botFilesPath") + config.Get("botAnnouncementPath"));
            }

            if (!Directory.Exists(config.Get("botFilesPath") + config.Get("botAnnouncementPath") + config.Get("botWhereIsLiquetPath")))
            {
                Directory.CreateDirectory(config.Get("botFilesPath") + config.Get("botAnnouncementPath") + config.Get("botWhereIsLiquetPath"));
            }

            if (!Directory.Exists(config.Get("botFilesPath") + config.Get("botIntroductionFilesPath")))
            {
                Directory.CreateDirectory(config.Get("botFilesPath") + config.Get("botIntroductionFilesPath"));
            }

            switch (config.Get("botRunEnvironment"))
            {
                case "Prod":
                    botToken = config.Get("botTokenProd");
                    break;
                case "Test":
                    botToken = config.Get("botTokenTest");
                    break;
                default:
                    Console.WriteLine("WARNING: App.config bot run environment value not configured correctly, please change the value to \"Test\" for the Megumi Bot Function Tester or \"Prod\" for Megumi Bot");
                    Console.WriteLine("Defaulting to Test Environment!");
                    botToken = config.Get("botTokenTest");
                    break;
            }
        }
    }
}
