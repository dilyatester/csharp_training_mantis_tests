using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{

    [TestFixture]
    public class ProjectCreationTest : TestBase
    {
        [SetUp]
        public void LogIn()
        {
            app.Login.Login(new AccountData() { Name = "administrator", Password = "root" });
        }

        [Test]
        public void CreateProject()
        {
            AccountData accountAPI = new AccountData()
            {
                Name = "administrator",
                Password = "root"
            };

            ProjectData project = new ProjectData() { Name = "NewProject", Description = "My description" };

            List<ProjectData> oldProjects = app.API.GetProjectList(accountAPI);//app.Project.GetProjectList();

            //Если такой проект уже существует, то сгенерируемслучайное имя
            if (oldProjects.Contains(project))
            {
                project.Name = GenerateRandomString(7);
            }

            app.Project.Create(project);

            List<ProjectData> newProjects = app.API.GetProjectList(accountAPI);//app.Project.GetProjectList();
            oldProjects.Add(project);
            oldProjects.Sort();
            newProjects.Sort();
            Assert.AreEqual(oldProjects, newProjects);


        }
    }
}
