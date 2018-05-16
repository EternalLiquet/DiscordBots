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
        Dictionary<string, string> c = Support.GetConfigFile();

        [OneTimeSetUp]
        public void SetUpTestAccount()
        {
            Support.driver = new ChromeDriver();
            Support.LogInToAdminAccount();
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
