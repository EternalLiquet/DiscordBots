using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using System.Collections.Generic;

namespace MegumiBotAutomationTests
{
    [TestFixture]
    [NonParallelizable]
    public class AnnouncementModuleAutomationTests
    {
        string baseDiscordURL = "https://discordapp.com/";
        Dictionary<string, string> c = Support.GetConfigFile();

        [OneTimeSetUp]
        public void SetUpTestAccount()
        {
            Support.driver = new ChromeDriver();
            Support.driver.Navigate().GoToUrl(baseDiscordURL + c["discordtestserverid"]);
            Support.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            Support.driver.FindElement(By.Id("register-email")).SendKeys(c["discordemail"]);
            Support.driver.FindElement(By.Id("register-password")).SendKeys(c["discordpass"]);
            Support.driver.FindElement(By.Id("register-password")).SendKeys(Keys.Enter);
        }

        [Test, Order(0)]
        public void IamCommandTest()
        {
            Support.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Support.driver.FindElement(By.CssSelector("textarea")).SendKeys("~Iam currently automation testing Megumi Bot!");
            Support.driver.FindElement(By.CssSelector("textarea")).SendKeys(Keys.Enter);
            Assert.True(Support.driver.FindElement(By.XPath("//*[contains(., 'successfully updated your status')]")).Displayed);
        }

        [Test, Order(1)]
        public void WhereIsDevCommandTest()
        {
            Support.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Support.driver.FindElement(By.CssSelector("textarea")).SendKeys("~WhereIsLiquet?");
            Support.driver.FindElement(By.CssSelector("textarea")).SendKeys(Keys.Enter);
            Assert.True(Support.driver.FindElement(By.XPath("//*[contains(., 'currently automation testing Megumi Bot!')]")).Displayed);
        }

        [OneTimeTearDown]
        public void CleanUpServerAndExit()
        {
            Support.driver.FindElement(By.CssSelector("textarea")).SendKeys("~purge 99");
            Support.driver.FindElement(By.CssSelector("textarea")).SendKeys(Keys.Enter);
            Support.driver.Close();
        }
    }
}
