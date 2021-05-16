using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class NavigationHelper : HelperBase
    {
        private string baseURL;
        public NavigationHelper(ApplicationManager manager, string baseURL)
            : base(manager)
        {
            this.baseURL = baseURL;
        }
        public void OpenHomePage()
        {
            if (driver.Url == baseURL + "/my_view_page.php")
            {
                return;
            }
            driver.Navigate().GoToUrl(baseURL + "/my_view_page.php");
        }

        public void GoToLoginPage()
        {
            if (driver.Url == baseURL + "/login_page.php")
            {
                return;
            }
            driver.Navigate().GoToUrl(baseURL + "/login_page.php");
        }

        public void GoToManagementPage()
        {
            if (driver.Url == baseURL + "/manage_overview_page.php"
                && IsElementPresent(By.ClassName("nav-tabs")))
            {
                return;
            }
            driver.FindElement(By.XPath("//*[@id='sidebar']/ul/li/a/i[@class='fa fa-gears menu-icon']")).Click();
        }

        public void GoToProjectManagement()
        {
            GoToManagementPage();
            driver.FindElement(By.XPath("//a[@href='/mantisbt-2.25.1/manage_proj_page.php']")).Click();

        }
    }

}
