using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using System.Collections.Generic;

namespace MegumiBotAutomationTests
{
    [TestFixture, Order(0)]
    [NonParallelizable]
    public class IntroductionPicModuleAutomationTests
    {
        string baseDiscordURL = "https://discordapp.com/";
        Dictionary<string, string> c = Support.GetConfigFile();

        [OneTimeSetUp]
        public void SetUpTestAccount()
        {
            c = Support.GetConfigFile();
            Support.driver = new ChromeDriver();
            LogInToAdminAccount();
        }

        private void LogInToTestAccount()
        {
            Support.driver.Navigate().GoToUrl(baseDiscordURL + c["discordtestserverid"]);
            Support.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            Support.driver.FindElement(By.Id("register-email")).SendKeys(c["discordtestemail"]);
            Support.driver.FindElement(By.Id("register-password")).SendKeys(c["discordtestpass"]);
            Support.driver.FindElement(By.Id("register-password")).SendKeys(Keys.Enter);
        }

        private void LogInToAdminAccount()
        {
            Support.driver.Navigate().GoToUrl(baseDiscordURL + c["discordtestserverid"]);
            Support.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            Support.driver.FindElement(By.Id("register-email")).SendKeys(c["discordemail"]);
            Support.driver.FindElement(By.Id("register-password")).SendKeys(c["discordpass"]);
            Support.driver.FindElement(By.Id("register-password")).SendKeys(Keys.Enter);
        }

        [Test]
        public void IntroSetupCommandTest()
        {
            Support.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Support.driver.FindElement(By.CssSelector("textarea")).SendKeys("~intro setup");
            Support.driver.FindElement(By.CssSelector("textarea")).SendKeys(Keys.Enter);
            Assert.True(Support.driver.FindElement(By.XPath("//*[contains(., 'I've successfully set the introduction channel for Megumi Bot Test Server to \"#general\"!')]")).Displayed);
        }

        [Test]
        public void NewUserEventTest()
        {
            LogInToTestAccount();
            Support.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Support.driver.FindElement(By.CssSelector("textarea")).SendKeys("~intro setup");
            Support.driver.FindElement(By.CssSelector("textarea")).SendKeys(Keys.Enter);
            Assert.True(Support.driver.FindElement(By.XPath("//*[contains(., 'I've successfully set the introduction channel for Megumi Bot Test Server to \"#general\"!')]")).Displayed);
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
