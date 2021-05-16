using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class LoginTests : TestBase
    {
        [Test]
        public void LoginWithValidCredentials()
        {
            //prepare
            app.Login.Logout();

            //action
            AccountData account = new AccountData() { Name = "administrator", Password = "root"};
            app.Login.Login(account);

            //verification
            Assert.IsTrue(app.Login.IsLoggedIn());
        }

        [Test]
        public void LoginWithInvalidCredentials()
        {
            //prepare
            app.Login.Logout();

            //action
            AccountData account = new AccountData() { Name = "administrator", Password = "ro1ot" };
            app.Login.Login(account);

            //verification
            Assert.IsFalse(app.Login.IsLoggedIn());
        }

    }
}
