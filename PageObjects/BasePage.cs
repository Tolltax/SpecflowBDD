using NUnit.Framework;
using OpenQA.Selenium;
using SpecflowProject.PageObjects;
using SpecflowProject.StepDefinitions;
using SpecflowProject.Utilities;

namespace SpecflowProject.Pages
{
    public class BasePage : BaseClass
    {

        public LoginPage LoginPage;
        public HomePage HomePage;
        public ConfigData ConfigData;
        public FooterPage FooterPage;


        public BasePage(IWebDriver driver) : base(driver)
        {
            ConfigData = Hooks.GetApplicationConfiguration();
            LoginPage = new LoginPage(driver);
            HomePage = new HomePage(driver);
            FooterPage = new FooterPage(driver);
        }

        public void CheckPageHasLoadedCorrectly()
        {
            WaitForXSecond(9);
            BasseWebDriverWait(Constants.DefaultWaitTime).Until(x => GetPageSourceLength() > 150);
            Assert.IsTrue(GetPageSourceLength() > 150, "page has not loaded correctly. (A Build may have in progress)");
        }

        public void VerifyBannerLoadedProperly()
        {

        }
    }
}
