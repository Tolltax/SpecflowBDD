using OpenQA.Selenium;
using SpecflowProject.Pages;
using TechTalk.SpecFlow;

namespace SpecflowProject.StepDefinitions
{
    [Binding]
    public class LoginSteps : BasePage
    {

        private readonly IWebDriver _driver;


        public LoginSteps(IWebDriver driver) : base(driver)
        {

        }

    }
}
