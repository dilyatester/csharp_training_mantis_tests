using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI;
using SimpleBrowser.WebDriver;

namespace mantis_tests
{
    public class AdminHelper : HelperBase
    {
        private string baseUrl;

        public AdminHelper(ApplicationManager manager, String baseUrl) : base(manager)
        {
            this.baseUrl = baseUrl;

        }

        public List<AccountData> GetAllAccounts() 
        {
            List<AccountData> accounts = new List<AccountData>();
            IWebDriver driver = OpenAppAndLogin();
            driver.Url = baseUrl + "/manage_user_page.php";


            IList<IWebElement> rows = driver.FindElements(By.XPath("//body/div/div/div/div/div/div/div/div/div/table[@class='table table-striped table-bordered table-condensed table-hover']/tbody/tr"));
            foreach (IWebElement row in rows)
            {
                IWebElement link =  row.FindElement(By.TagName("a"));
                string name = link.Text;
                string href = link.GetAttribute("href");
                Match m = Regex.Match(href, @"\d+$");
                string id = m.Value;
                accounts.Add(new AccountData()
                {
                    Name = name,
                    Id = id,
                });

            }

            return accounts;
        }

        public void DeleteAccount(AccountData account) 
        {
            IWebDriver driver = OpenAppAndLogin();
            driver.Url = baseUrl + "/manage_user_edit_page.php?user_id=" + account.Id;
            driver.FindElement(By.XPath("//*[@id='manage-user-delete-form']/fieldset/span/input"));
            new WebDriverWait(driver, TimeSpan.FromSeconds(8)).Until(d => d.FindElements(By.XPath("//div[@class='alert alert-warning center']")) != null);
            driver.FindElement(By.XPath("//input[@class='btn btn-primary btn-white btn-round']")).Click();
        }

        private IWebDriver OpenAppAndLogin()
        {
            IWebDriver driver = new SimpleBrowserDriver();
            manager.Login.Login(new AccountData()
            {
                Name = "administrator",
                Password = "root"
            });
            /*driver.Url = baseUrl + "/login_page.php";

            driver.FindElement(By.Id("username")).SendKeys("");
            driver.FindElement(By.ClassName("btn-success")).Click();
            driver.FindElement(By.Name("password")).SendKeys("root");
            driver.FindElement(By.ClassName("btn-success")).Click();*/
            return driver;
        }
    }
}
