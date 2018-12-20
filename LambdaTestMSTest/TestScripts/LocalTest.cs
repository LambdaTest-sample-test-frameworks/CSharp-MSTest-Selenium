using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambdaTestMSTest.TestScripts
{
    [TestClass]
    public class AdvanceBoyTest 
	{
        private IWebDriver driver;

        [TestInitialize]
        public void Open()
        {
            String LTUser = ConfigurationSettings.AppSettings["LTUser"];
            String LTAccessKey = ConfigurationSettings.AppSettings["LTAccessKey"];

            DesiredCapabilities capsLT = new DesiredCapabilities();
            capsLT.SetCapability("platform", ConfigurationSettings.AppSettings["OS"]);
            capsLT.SetCapability("browserName", ConfigurationSettings.AppSettings["Browser"]);
            capsLT.SetCapability("version", ConfigurationSettings.AppSettings["BrowserVersion"]);
            capsLT.SetCapability("user", LTUser);
            capsLT.SetCapability("accessKey", LTAccessKey);
            capsLT.SetCapability("build", "LambdaTestSampleApp");
            capsLT.SetCapability("name", "LambdaTestJavaSample");
            capsLT.SetCapability("network", true); // To enable network logs
            capsLT.SetCapability("visual", true); // To enable step by step screenshot
            capsLT.SetCapability("video", true); // To enable video recording
            capsLT.SetCapability("console", true); // To capture console logs
            driver = new RemoteWebDriver(new Uri(ConfigurationSettings.AppSettings["LTUrl"]), capsLT, TimeSpan.FromSeconds(600)); ;

            driver.Manage().Window.Maximize();
            driver.Url = "https://4dvanceboy.github.io/lambdatest/lambdasampleapp.html";
            Assert.AreEqual("Sample page - lambdatest.com", driver.Title);
        }


        [TestMethod]
        public void TestLocal()
		{				
			String itemName = "Yey, Let's add it to list";

			// Click on First Check box 
			IWebElement firstCheckBox = driver.FindElement(By.Name("li1"));
			firstCheckBox.Click();

			// Click on Second Check box 
			IWebElement secondCheckBox = driver.FindElement(By.Name("li2"));
			secondCheckBox.Click();

			// Enter Item name	
			IWebElement textfield = driver.FindElement(By.Id("sampletodotext"));
			textfield.SendKeys(itemName);

			// Click on Add button 
			IWebElement addButton = driver.FindElement(By.Id("addbutton"));
			addButton.Click();

			// Verified Added Item name
			IWebElement itemtext = driver.FindElement(By.XPath("/html/body/div/div/div/ul/li[6]/span"));
			String getText = itemtext.Text;
			Assert.IsTrue(itemName.Contains(getText));
		}

        [TestCleanup]
        public void Close()
		{
			driver.Quit();
		}
	}
}
