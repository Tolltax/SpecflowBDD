using OpenQA.Selenium;
using SpecflowProject.PageObjects;
using SpecflowProject.Pages;
using TechTalk.SpecFlow;

namespace SpecflowProject.StepDefinitions
{
    [Binding]
    public class FooterStep : BasePage
    {


        public FooterStep(IWebDriver driver) : base(driver)
        {

        }


        [When(@"I click on footer (.*) link")]
        public void WhenIClickOnFooterAboutUsLink(string footerLink)
        {
            FooterPage.ClickFooterLink(footerLink);
        }

        [Then(@"I check the URL (.*) is correct - (.*) Link")]
        public void ThenIVerifyLinkOpenWithCorrectUrl(string expectedUrl, string tabType)
        {
            FooterPage.CheckExpectedUrl(expectedUrl, tabType);
        }
    }
}
