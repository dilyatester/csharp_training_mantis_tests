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
    public class APIHelper : HelperBase
    {
        
        public APIHelper(ApplicationManager manager) : base(manager) { }

        public void CreateNewIssue(AccountData account, ProjectData project, IssueData issueData)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.IssueData issue = new Mantis.IssueData();
            issue.summary = issueData.Summary;
            issue.description = issueData.Description;
            issue.category = issueData.Category;
            issue.project = new Mantis.ObjectRef();
            issue.project.id = project.Id;
            client.mc_issue_add(account.Name, account.Password, issue);
        }

        public List<ProjectData> GetProjectList(AccountData account)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData[] list = client.mc_projects_get_user_accessible(account.Name, account.Password);
            
            List<ProjectData> projList = new List<ProjectData>();
            
            foreach(Mantis.ProjectData obj in list)
            {
                projList.Add(new ProjectData()
                {
                    Name = obj.name,
                    Description = obj.description,
                    Id = obj.id
                });
                    
            }


            return new List<ProjectData>(projList);
        }

        public void CreateNewProject(AccountData account, ProjectData project)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData pd = new Mantis.ProjectData()
            {
                id = project.Id,
                name = project.Name,
                description = project.Description
            };
            client.mc_project_add(account.Name, account.Password, pd);
        }
    }
}
