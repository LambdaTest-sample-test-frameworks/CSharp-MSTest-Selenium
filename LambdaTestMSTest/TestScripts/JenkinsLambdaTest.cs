using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Configuration;

namespace LambdaTestMSTest.TestScripts
{

    [TestClass]
    public class JenkinsLambdaTest
    {
        private IWebDriver driver;
        protected String ltUserName;
        protected String ltAppKey;
        protected String plateform;
        protected String browser;
        protected String browserVersion;

        
        [TestInitialize]
        public void setUp()
        {
            this.init();
             DesiredCapabilities caps1 = new DesiredCapabilities();
            caps1.SetCapability("platform", plateform);
            caps1.SetCapability("browserName", browser);
            caps1.SetCapability("version", browserVersion);
            caps1.SetCapability("user", ltUserName);
            caps1.SetCapability("accessKey", ltAppKey);
            caps1.SetCapability("network", true); // To enable network logs
            caps1.SetCapability("visual", true); // To enable step by step screenshot
            caps1.SetCapability("video", true); // To enable video recording
            caps1.SetCapability("console", true); // To capture console logs
            driver = new RemoteWebDriver(new Uri(ConfigurationSettings.AppSettings["LTUrl"]), caps1, TimeSpan.FromSeconds(600));

            driver.Manage().Window.Maximize();
            driver.Url = "https://4dvanceboy.github.io/lambdatest/lambdasampleapp.html";
            Assert.AreEqual("Sample page - lambdatest.com", driver.Title);
        }

        [TestMethod]
        public void TestJenkins()
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

        public void init()
        {
            if (String.IsNullOrEmpty(Environment.GetEnvironmentVariable("LT_USERNAME"))){
                this.ltUserName = ConfigurationSettings.AppSettings["LTUser"];
            }

            if (String.IsNullOrEmpty(Environment.GetEnvironmentVariable("LT_APPKEY")))
                  this.ltAppKey = ConfigurationSettings.AppSettings["LTAccessKey"];

            if (String.IsNullOrEmpty(Environment.GetEnvironmentVariable("LT_OPERATING_SYSTEM")))
                this.plateform = ConfigurationSettings.AppSettings["OS"];

            if (String.IsNullOrEmpty(Environment.GetEnvironmentVariable("LT_BROWSER")))
                this.browser = ConfigurationSettings.AppSettings["Browser"];

            if (String.IsNullOrEmpty(Environment.GetEnvironmentVariable("LT_BROWSER_VERSION")))
                this.browserVersion = ConfigurationSettings.AppSettings["BrowserVersion"];


        }

    }
}
