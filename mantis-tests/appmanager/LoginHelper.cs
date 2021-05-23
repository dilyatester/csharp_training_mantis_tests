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
    public class LoginHelper:HelperBase
    {
        public LoginHelper(ApplicationManager manager)
            : base(manager)
        {
        }

        public void Login(AccountData account)
        {
            if (IsLoggedIn())
            {
                if (IsLoggedIn(account)) 
                {
                    return;
                }

                Logout();
            }
            manager.Navigation.GoToLoginPage();
            Tipe(By.Id("username"), account.Name);
            driver.FindElement(By.ClassName("btn-success")).Click();

            Tipe(By.Name("password"), account.Password);
            driver.FindElement(By.ClassName("btn-success")).Click();
        }

        public bool IsLoggedIn()
        {
            return IsElementPresent(By.ClassName("user-info"));
        }

        public bool IsLoggedIn(AccountData account)
        {
            return IsLoggedIn()
            && GetLoggedUserName() == account.Name;
            
        }

        public string GetLoggedUserName()
        {
            string text = driver.FindElement(By.ClassName("user-info")).Text;
            return text;


        }

        public void Logout()
        {
            if (IsLoggedIn())
            {
                driver.FindElement(By.XPath("//a[contains(@href,'logout_page.php')]")).Click();
            }
            
        }


    }
}
