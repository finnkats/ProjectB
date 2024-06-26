using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectTest;
using System;
using System.IO;
using System.Text;

namespace Logic.Tests.AccountTest
{
    [TestClass]
    public class AccountLoginTests
    {   

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            // Load account data before each test
            App.Start();
        }

        [TestInitialize]
        public void Reset(){
            TestDataFiller.FillApp();
            AccountLogic.Logout();
        }


        [TestMethod]
        public void CheckLoginTest()
        {
            // Arrange
            string loginName = "Admin1";
            string loginPassword = "Password1";

            Account wrongAdmin = new("Wrong", "Wrong", true);
            Assert.IsFalse(AccountLogic.CheckLogin(loginName, loginPassword, wrongAdmin), "Login should not be successful");

            Account correctAdmin = new(loginName, loginPassword, true);
            Assert.IsTrue(AccountLogic.CheckLogin(loginName, loginPassword, correctAdmin), "Login should be successful");

            Account customer = new("Customer", "Password", false);
            Assert.IsTrue(AccountLogic.CheckLogin("Customer", "Password", customer), "Login should be successful");
        }

        [TestMethod]
        public void AdminLogin(){
            string loginName = "Admin1";
            string loginPassword = "Password1";
            AccountLogic.Login(loginName, loginPassword);

            Assert.AreNotEqual("Unknown", App.LoggedInUsername);
            Assert.AreEqual("Admin1", App.LoggedInUsername);

            string menuString = App.FrontPage.MenuString();
            Assert.IsTrue(menuString.Contains("Admin Features"), menuString);
        }

        [TestMethod]
        public void CustomerLogin(){
            string loginName = "User1";
            string loginPassword = "Password2";
            AccountLogic.Login(loginName, loginPassword);

            Assert.AreNotEqual("Unknown", App.LoggedInUsername);
            Assert.AreEqual("User1", App.LoggedInUsername);

            string menuString = App.FrontPage.MenuString();
            Assert.IsFalse(menuString.Contains("Admin Features"), menuString);
        }
    }
}
