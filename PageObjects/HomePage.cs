using NUnit.Framework;
using OpenQA.Selenium;
using SpecflowProject.Utilities;

namespace SpecflowProject.PageObjects
{
    public class HomePage : BaseClass
    {

        private By _homePageBannerLeft = By.CssSelector(".banner-left");
        private By _homePageBannerRight = By.CssSelector(".banner-right");

        public HomePage(IWebDriver driver) : base(driver) 
        { 
            this._driver = driver;
        }

        public void VerifyHomePageLoaded()
        {
            bool homepagebannerLeft = _driver.FindElement(_homePageBannerLeft).Displayed;
            bool homepagebannerRight = _driver.FindElement(_homePageBannerRight).Displayed;
            if (homepagebannerLeft || homepagebannerRight)
            {
                Assert.IsTrue((homepagebannerLeft || homepagebannerRight), "Home page not loaded");
            }

        }

    }
}
