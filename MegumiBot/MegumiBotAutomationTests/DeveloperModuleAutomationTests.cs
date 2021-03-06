﻿using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using System.Collections.Generic;

namespace MegumiBotAutomationTests
{
    [TestFixture]
    [NonParallelizable]
    public class DeveloperModuleAutomationTests
    {
        
        Dictionary<string, string> c = Support.GetConfigFile();

        [OneTimeSetUp]
        public void SetUpTestAccount()
        {
            c = Support.GetConfigFile();
            Support.driver = new ChromeDriver();
            DiscordHelpers.LogInToDiscord(c);
        }

        

        [Test]
        public void DevCommandTest()
        {
            c = Support.GetConfigFile();
            Support.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Support.driver.FindElement(By.CssSelector("textarea")).SendKeys("~dev");
            Support.driver.FindElement(By.CssSelector("textarea")).SendKeys(Keys.Enter);
            Assert.True(Support.driver.FindElement(By.XPath("//*[contains(., ' is my developer')]")).Displayed);
        }

        [Test]
        public void SocialMediaCommandTest()
        {
            c = Support.GetConfigFile();
            Support.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Support.driver.FindElement(By.CssSelector("textarea")).SendKeys("~socialmedia");
            Support.driver.FindElement(By.CssSelector("textarea")).SendKeys(Keys.Enter);
            Assert.True(Support.driver.FindElement(By.XPath("//*[contains(., 'Feel free to follow my developer')]")).Displayed);
        }

        [Test]
        public void SupportCommandTest()
        {
            c = Support.GetConfigFile();
            Support.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Support.driver.FindElement(By.CssSelector("textarea")).SendKeys("~support");
            Support.driver.FindElement(By.CssSelector("textarea")).SendKeys(Keys.Enter);
            Assert.True(Support.driver.FindElement(By.XPath("//*[contains(., 'The official support server for Megumi Bot is Cherry Blessing!')]")).Displayed);
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
