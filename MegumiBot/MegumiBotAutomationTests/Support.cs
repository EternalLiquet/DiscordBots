﻿using OpenQA.Selenium;
using System.Collections;
using System.IO;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MegumiBotAutomationTests
{
    public static class Support
    {
        public static IWebDriver driver;
        public static Dictionary<string, string> c;
        private static string config;

        public static Dictionary<string,string> LoadConfigFile()
        {
            AddConfigFileToString();
            c = config.Split('\n')
                .Select(value => value.Split(':'))
                .ToDictionary(pair => pair[0].Trim(), pair => pair[1].Trim().Trim('\''));
            return c;
        }

        private static void AddConfigFileToString()
        {
            Environment.CurrentDirectory = Environment.GetEnvironmentVariable("homepath");
            StreamReader sr = new StreamReader("MegumiBotTestProperties.kato");
            config += sr.ReadToEnd();
            sr.Close();
        }
    }
}