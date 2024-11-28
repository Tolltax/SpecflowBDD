using BoDi;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System.Configuration;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Configuration;

namespace SpecflowProject.Utilities
{
    [Binding]
    public class Hooks : Steps
    {
        public static ConfigData Configuration;
        private IWebDriver _driver;
        private bool _driverStartedSucessfully;
        private readonly IObjectContainer _objectContainer;
        private readonly ScenarioSettings _scenarioSettings;


        public Hooks(IObjectContainer objectContainer, ScenarioSettings scenarioSettings)
        {
            _objectContainer = objectContainer;
            _scenarioSettings = scenarioSettings;
        }


        [BeforeScenario]
        public void BeforeScenario(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
             Configuration = GetApplicationConfiguration();
            _driverStartedSucessfully = StartDriver();
            _objectContainer.RegisterInstanceAs(_driver);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _driver?.Quit(); 
        }

        private bool StartDriver()
        {
            try
            {
                StartLocalDriver();
                if (Configuration.implicit_wait > 0)
                    _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Configuration.implicit_wait);

                if (Configuration.page_load_timeout > 0)
                    _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(Configuration.implicit_wait);
            }
            catch (WebDriverException ex)
            {
                throw new WebDriverException(ex.Message + "\n");

            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "\n");
            }

            return true;
        }

        private void StartLocalDriver()
        {
            switch (Configuration.browser.ToLower())
            {
                case "chrome":
                    ChromeOptions option = new ChromeOptions();
                    if (Configuration.incognitoMode)
                    {
                        option.AddArgument("--incognito");
                    }
                    if (Configuration.deviceName != string.Empty)
                    {
                        option.EnableMobileEmulation(Configuration.deviceName);
                    }
                    _driver = new ChromeDriver(option);
                    _driver.Manage().Window.Maximize();
                    _driver.Navigate().GoToUrl(Configuration.url);                   
                    break;

                case "edge":
                    EdgeOptions edgeoption = new EdgeOptions();
                    if (Configuration.incognitoMode)
                    {
                        edgeoption.AddArgument("--InPrivate");
                    }
                    if (Configuration.deviceName != string.Empty)
                    {
                        edgeoption.EnableMobileEmulation(Configuration.deviceName);
                    }
                    _driver = new EdgeDriver(edgeoption);
                    _driver.Navigate().GoToUrl(Configuration.url);
                    break;

                default:
                    throw new Exception($"Browser Name {Configuration.browser} not recognized");
            }
        }

        public static ConfigData GetApplicationConfiguration()
        {
            var configuration = new ConfigData();
            var iConfig = GetConfig();
            iConfig.Bind(configuration);

            if (string.IsNullOrEmpty(configuration.browser))
            {
                throw new Exception("Browser is not defined in configuration.json.");
            }

            return configuration;
        }

        private static IConfiguration GetConfig()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("configuration.json")
                .Build();

            return builder;
        }       
    }
}
