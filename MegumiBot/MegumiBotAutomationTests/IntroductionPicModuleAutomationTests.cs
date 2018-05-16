using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MegumiBotAutomationTests
{
    [TestFixture, Order(0)]
    [NonParallelizable]
    public class IntroductionPicModuleAutomationTests
    {
        string baseDiscordURL = "https://discordapp.com/";
        string baseInviteURL = "https://discord.gg/";
        Dictionary<string, string> c = Support.GetConfigFile();

        [OneTimeSetUp]
        public void SetUpTestAccount()
        {
            c = Support.GetConfigFile();
            Support.driver = new ChromeDriver();
            Support.LogInToAdminAccount();
        }

        private void LogInToTestAccountAndAcceptInvite()
        {
            Support.driver.Navigate().GoToUrl(baseInviteURL + c["discordtestinv"]);
            Support.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            Support.driver.FindElement(By.XPath("//*[contains(., 'Already have an account?')]")).Click();
            Support.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            Support.driver.FindElements(By.TagName("input")).First(attr => attr.GetAttribute("type").Equals("email")).SendKeys(c["discordtestemail"]);
            Support.driver.FindElements(By.TagName("input")).First(attr => attr.GetAttribute("type").Equals("password")).SendKeys(c["discordpass"]);
            Support.driver.FindElements(By.TagName("input")).First(attr => attr.GetAttribute("type").Equals("password")).SendKeys(Keys.Enter);
        }

        [Test, Order(0)]
        public void IntroSetupCommandTest()
        {
            Support.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Support.driver.FindElement(By.CssSelector("textarea")).SendKeys("~intro setup");
            Support.driver.FindElement(By.CssSelector("textarea")).SendKeys(Keys.Enter);
            Assert.True(Support.driver.FindElement(By.XPath("//*[contains(., 'successfully set the introduction channel for Megumi Bot Test Server to \"#general\"!')]")).Displayed);
        }

        [Test, Order(1)]
        public void NewUserEventTest()
        {
            Support.driver.Close();
            Support.driver = new ChromeDriver();
            LogInToTestAccountAndAcceptInvite();
            Support.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Assert.True(Support.driver.FindElement(By.XPath("//*[contains(., 'Welcome to Megumi Bot Test Server')]")).Displayed);
        }

        [Test, Order(2)]
        public void IntroCreateCommandTest()
        {
            
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
