using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{

    [TestFixture]
    public class ProjectRemovalTest : TestBase
    {
        [SetUp]
        public void LogIn()
        {
            app.Login.Login(new AccountData() { Name = "administrator", Password = "root" });
        }

        [Test]
        public void RemoveProject()
        {

            AccountData accountAPI = new AccountData()
            {
                Name = "administrator",
                Password = "root"
            };

            List<ProjectData> oldProjects = app.API.GetProjectList(accountAPI);//app.Project.GetProjectList();

            //Если проектов нет, то создадим один
            if (oldProjects.Count < 1)
            {
                ProjectData project = new ProjectData() { Name = GenerateRandomString(7), Description = "My description" };

                app.API.CreateNewProject(accountAPI, project);

                oldProjects = app.API.GetProjectList(accountAPI);
            }

            //Удаляем первый проект в списке
            ProjectData toBeRemoved = oldProjects.First();
            app.Project.RemoveByName(toBeRemoved.Name);

            List<ProjectData> newProjects = app.API.GetProjectList(accountAPI);

            oldProjects.Remove(toBeRemoved);

            oldProjects.Sort();
            newProjects.Sort();
            Assert.AreEqual(oldProjects, newProjects);


        }
    }
}
