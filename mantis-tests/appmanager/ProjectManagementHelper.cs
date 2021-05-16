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
    class ProjectManagementHelper : HelperBase
    {
        public ProjectManagementHelper(ApplicationManager manager)
    : base(manager)
        {

        }
        public void Create(ProjectData project)
        {
            manager.Navigation.GoToProjectManagement();
            CrateNewProject();
            FillProjectData(project);
            SubmitProjectData();
        }

        public void RemoveByName(string name)
        {
            manager.Navigation.GoToProjectManagement();
            OpenProjectByText(name);
            RemoveProject();
        }

        private void RemoveProject()
        {
            driver.FindElement(By.XPath("//input[@class='btn btn-primary btn-sm btn-white btn-round']")).Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(8)).Until(d => d.FindElements(By.XPath("//div[@class='alert alert-warning center']")) != null);
            driver.FindElement(By.XPath("//input[@class='btn btn-primary btn-white btn-round']")).Click();
        }

        private void OpenProjectByText(string name)
        {
            driver.FindElement(By.LinkText(name)).Click();
        }

        public List<ProjectData> GetProjectList()
        {

            List<ProjectData> projList = new List<ProjectData>();
            manager.Navigation.GoToProjectManagement();
            ICollection<IWebElement> elements = driver.FindElements(By.XPath("//body/div/div/div/div/div/div/div/div/div/table[@class='table table-striped table-bordered table-condensed table-hover']/tbody/tr"));
                foreach (IWebElement element in elements)
                {
                    ICollection<IWebElement> td = element.FindElements(By.CssSelector("td"));
                    projList.Add(new ProjectData()
                    {
                        Name = td.ElementAt(0).Text,
                        Description = td.ElementAt(4).Text
                    });
                }


            return new List<ProjectData>(projList);
        }


        public void CrateNewProject()
        {
            driver.FindElement(By.XPath("//button[@class='btn btn-primary btn-white btn-round']")).Click();
        }

        public void FillProjectData(ProjectData project)
        {
            Tipe(By.Id("project-name"), project.Name);
            Tipe(By.Id("project-description"), project.Description);
        }

        public void SubmitProjectData()
        {
            driver.FindElement(By.XPath("//input[@class='btn btn-primary btn-white btn-round']")).Click();
        }
    }
}
