using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace MegumiBotAutomationTests
{
    public static class DiscordHelpers
    {
        static string baseDiscordURL = "https://discordapp.com/";

        public static void LogInToDiscord(Dictionary<string, string> configLibrary)
        {
            Support.driver.Navigate().GoToUrl(baseDiscordURL + configLibrary["discordtestserverid"]);
            Support.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            Support.driver.FindElements(By.CssSelector("input"))[0].SendKeys(configLibrary["discordemail"]);
            Support.driver.FindElements(By.CssSelector("input"))[1].SendKeys(configLibrary["discordpass"]);
            Support.driver.FindElements(By.CssSelector("input"))[1].SendKeys(Keys.Enter);
        }
    }
}
