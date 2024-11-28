using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using SpecflowProject.Utilities;

namespace SpecflowProject.Pages
{
    public class LoginPage : BaseClass
    {
        private readonly By _userName = By.Id("user-name");
        private readonly By _password = By.XPath("//input[@id = 'password']");
        private readonly By _submitBtn = By.Name("login-button");


        public LoginPage(IWebDriver driver) : base(driver) 
        { 
          this._driver = driver;
        }


        public void EnterLoginDetails(string emailAddress, string password)
        {
            ClearField(_userName);
            _driver.FindElement(_userName).SendKeys(emailAddress);
            ClearField(_userName);
            _driver.FindElement(_userName).SendKeys(password);
            _driver.FindElement(_submitBtn).Click();
        }
    }
}
