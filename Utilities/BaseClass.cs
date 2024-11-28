using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using ExpectedCondition = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace SpecflowProject.Utilities
{
    public class BaseClass
    {
        protected IWebDriver _driver;
        public ConfigData configuration;

        public BaseClass(IWebDriver driver)
        {
            _driver = driver;
        }

        public WebDriverWait BasseWebDriverWait(double time)
        {
            var WebDriverWait = new WebDriverWait(_driver, TimeSpan.FromMilliseconds(time));
            return WebDriverWait;
        }

        public void ClearField(By locator)
        {
          try
          {
                var element  = _driver.FindElement(locator); 
                if(element.Enabled ==  true)
                {
                    element.Clear();                   
                }
            }
            catch(WebDriverException e)
            {
                throw new WebDriverException("Error when clearing the field with locator " + locator + "\n" +e);
            }      
        }

        public int GetPageSourceLength()
        {
            try
            {
                return _driver.PageSource.Length;
            }
            catch (WebDriverException e)
            {
                throw new WebDriverException("Unable to laod the page souce \n" + e);
            }
        }

        public void SwitchToFirstWindow()
        {
            _driver.SwitchTo().Window(_driver.WindowHandles.FirstOrDefault());
        }

        public void DoubleClickUsingActions(IWebElement ele)
        {
            IWebElement element = new WebDriverWait(_driver, TimeSpan.FromMilliseconds(5000)).Until(ExpectedCondition.ElementToBeClickable(ele));
            Actions action = new Actions(_driver);
            action.MoveToElement(element).DoubleClick().Build().Perform();
        }

        public void SwitchToLastWindow()
        {
            try
            {
                _driver.SwitchTo().Window(_driver.WindowHandles.Last());

            }
            catch (WebDriverException e)
            {
                throw new NoSuchWindowException("Unable to switch to last window \n" + e);
            }
        }

        public void ScrollToElementToViewInMid(IWebElement element)
        {
            if (element == null)
            {
                throw new NoSuchWindowException($"Cannot find the element to with locator {element}");
            }

            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("argument[0].scrollIntoView({block: 'center, inline: 'nearest'})", element);
        }

        public void ClickUsinfAction(IWebElement ele)
        {
            try
            {
                IWebElement element = new WebDriverWait(_driver, TimeSpan.FromMilliseconds(5000)).Until(ExpectedCondition.ElementToBeClickable(ele));
                Actions action = new Actions(_driver);
                action.MoveToElement(element).Click().Build().Perform();

            }
            catch (WebDriverException e)
            {
                throw new WebDriverException("Error in trying to click the element \n" + e.StackTrace);
            }
        }

        public bool ElementExists(IWebElement ele)
        {
            return ele != null;

        }

        public void SwitchToNewTab(IEnumerable<string> prior , IWebDriver _driver)
        {
            WaitUntil(() => _driver.WindowHandles.Count > 1, "There is no second tab available",waitMillisecond: 500, 
                maxRetryCount: 10);
            _driver.SwitchTo().Window(_driver.WindowHandles[1]);
            
        }

        public static void WaitUntil(Func<bool> function, string assertionFailureMessage, int waitMillisecond = 500, int maxRetryCount = 10)
        {
            while (maxRetryCount --> 0)
            {
                try
                {
                    if(function() == false)
                    {
                        Thread.Sleep(waitMillisecond);
                        continue;
                    }
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"The exception was thrown on {maxRetryCount}" + e.StackTrace);

                }

            }
        }

        public IWebElement GetParent(IWebElement e)
        {
            return e.FindElement(By.XPath(".."));
        }

        public void BrowserRefresh()
        {
            try
            {
                WaitForPageToLoad(() => _driver.Navigate().Refresh());

            }
            catch (WebDriverException e)
            {
                throw new WebDriverException("Error when trying to click on browser back button \n" + e.StackTrace);
            }
        }

        public void WaitForPageToLoad(Action action)
        {
            IWebElement oldPage = _driver.FindElement(By.TagName("html"));
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromMilliseconds(100000));
            action();
            wait = new WebDriverWait(_driver, TimeSpan.FromMilliseconds(90000));
            try
            {
                wait.Until(driver => (IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete");
            }
            catch (Exception pageLoadWaitError)
            {
                throw new Exception("Timeout during page load", pageLoadWaitError);

            }
        }

        public void ClickBrowserBackButton()
        {
            try
            {
                _driver.Navigate().Back();
            }
            catch (WebDriverException e)
            {
                throw new WebDriverException("Error while clicking browser back button \n"+ e.StackTrace);
            }
        }

        public void ScrollVerticallyXLines(IWebDriver driver, int linesToScroll)
        {
            if (driver is IJavaScriptExecutor jsExecutor)
            {
                for (var i = 0; i <= linesToScroll; i++)
                {
                    jsExecutor.ExecuteScript("window.scrollBy(0,40);");
                }                 
            }
            else
            {
                throw new InvalidOperationException("The driver does not support JavaScript execution.");
            }

        }

        public bool ScrollToElementAndClick(IWebElement element)
        {
            var clickSuccessful = false;
            var scrollCount = 0;
            var MaxLineScroll = 20;

            while (clickSuccessful == false && scrollCount < MaxLineScroll)
            {
                try
                {
                    element.Click();
                    clickSuccessful = true;
                }
                catch (ElementNotInteractableException e)
                {
                    ScrollVerticallyXLines(_driver, 1);
                    scrollCount++;
                }

            }
            return clickSuccessful;

        }

        public void WaitForXSecond(int waitTime)
        {
            int second = waitTime * 1000;
            Thread.Sleep(second);
        }

        public void SwitchToFrame(IWebElement element)
        {
            try
            {
                _driver.SwitchTo().Frame(element);
            }
            catch (WebDriverException e)
            {
                throw new WebDriverException("Unable to switch to frame \n" + e);
            }
        }

        public void ScrollToFooter()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
        }

        public void ScrollToBottom()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight || document.documentElement.scrollHeight);");
        }

        public bool CheckExpectedUrl(string expectedURL, double timeOut = Constants.DefaultWaitTime)
        {
            try
            {
                BasseWebDriverWait(timeOut).Until(ExpectedCondition.UrlContains(expectedURL));
                return true;
            }
            catch (WebDriverException e)
            {
                var actualURL = _driver.Url;
                Console.WriteLine($"waited {timeOut/1000} second: URL is " +actualURL +
                    " and does not contain expected " +expectedURL);
                return false;
            }
            catch (NullReferenceException)
            {
                var actualURL = _driver.Url;
                if (actualURL.Contains(expectedURL, StringComparison.OrdinalIgnoreCase))
                    return true;
                else
                    return false;

            }
        }

        public string GetCurrentURL()
        {
            try
            {
                return _driver.Url;
            }
            catch (WebDriverException e)
            {
                throw new WebDriverException("unable to fetch the current url \n" + e.StackTrace);
            }
        }

    }
}
         