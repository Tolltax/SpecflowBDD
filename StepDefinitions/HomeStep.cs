using Microsoft.VisualBasic;
using OpenQA.Selenium;
using SpecflowProject.Pages;
using System.Runtime.CompilerServices;
using TechTalk.SpecFlow;

namespace SpecflowProject.StepDefinitions
{
    [Binding]
    public class HomeStep : BasePage
    {

        //private readonly IWebDriver _driver;
        private readonly ScenarioSettings _scenarioSettings;

        public HomeStep(IWebDriver driver, ScenarioSettings scenarioSettings) : base(driver)
        {   
            this._driver = driver;
            _scenarioSettings = scenarioSettings;
        }

        [Given(@"I have navigated to the (desktop|mobile) home page")]
        public void GivenIHaveNavigatedToTheDesktopHomePage(string platformType)
        {
            CheckPageHasLoadedCorrectly();
        }

        [Then(@"I am redirected to the home page")]
        public void ThenIAmRedirectedToTheHomePage()
        {
            HomePage.VerifyHomePageLoaded();
        }
    }
}
