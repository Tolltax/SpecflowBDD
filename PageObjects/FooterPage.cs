using NUnit.Framework;
using OpenQA.Selenium;
using SpecflowProject.Utilities;

namespace SpecflowProject.PageObjects
{
    public class FooterPage : BaseClass
    {
        private HomePage homePage;

        public FooterPage(IWebDriver driver) : base(driver) 
        {
            this._driver = driver;
            homePage = new HomePage(driver);
        }

        public void ClickFooterLink(string link)
        {
            IWebElement links = _driver.FindElement(By.XPath($"//a[contains(text(), '{link}')]"));

            ScrollToBottom();
            _driver.FindElement(By.XPath($"//a[contains(text(), '{link}')]")).Click();
        }

        public void CheckExpectedUrl(string expectedURl, string tabType)
        {
            switch (tabType.ToLower())
            {
                case "internal":
                    CheckExpectedUrl(expectedURl, 5000);
                    var currentURL = GetCurrentURL();
                    Assert.True(currentURL.ToLower().Contains(currentURL));
                    break;    
            }     
        }
    }
}
