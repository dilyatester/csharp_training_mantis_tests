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
    public class RegistrationHelper: HelperBase
    {
        public RegistrationHelper(ApplicationManager manager) : base(manager) { }

        public void Register(AccountData account)
        {
            OpenMainPage();
            OpenRegistrationForm();
            FillRegistrationForm(account);
            SubmitRegistration();
        }

        private void OpenRegistrationForm()
        {
            //manager.Navigation.GoToLoginPage();
            //driver.FindElement(By.XPath("//a[contains(@href,'signup_page.php')]")).Click();
            manager.Navigation.GoToRegistrationPage();
        }

        private void SubmitRegistration()
        {
            driver.FindElement(By.ClassName("btn-success")).Click();
        }

        private void FillRegistrationForm(AccountData account)
        {
            driver.FindElement(By.Id("username")).SendKeys(account.Name);
            driver.FindElement(By.Id("email-field")).SendKeys(account.Email);

            //driver.FindElement(By.ClassName("back-to-login-link")).Click();
        }

        private void OpenMainPage()
        {
            manager.Navigation.OpenHomePage();
        }
    }
}
