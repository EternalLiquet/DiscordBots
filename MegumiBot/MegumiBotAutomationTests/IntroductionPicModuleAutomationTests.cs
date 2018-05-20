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

        [Test, Order(0)]
        public void IntroSetupCommandTest()
        {
            Support.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Support.driver.FindElement(By.CssSelector("textarea")).SendKeys("~intro setup");
            Support.driver.FindElement(By.CssSelector("textarea")).SendKeys(Keys.Enter);
            Assert.True(Support.driver.FindElement(By.XPath("//*[contains(., 'successfully set the introduction channel for Megumi Bot Test Server to \"#general\"!')]")).Displayed);
        }

        [Test, Order(1)]
        public void IntroCreateCommandTest()
        {
            Support.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Support.driver.FindElement(By.CssSelector("textarea")).SendKeys("~intro create");
            Support.driver.FindElement(By.CssSelector("textarea")).SendKeys(Keys.Enter);
            Support.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Assert.True(Support.driver.FindElement(By.XPath("//*[contains(., 'Welcome to')]")).Displayed);
        }

        [Test, Order(2)]
        public void IntroUnsetCommandTest()
        {
            Support.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Support.driver.FindElement(By.CssSelector("textarea")).SendKeys("~intro unset");
            Support.driver.FindElement(By.CssSelector("textarea")).SendKeys(Keys.Enter);
            Support.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Assert.True(Support.driver.FindElement(By.XPath("//*[contains(., 'successfully unset')]")).Displayed);
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
