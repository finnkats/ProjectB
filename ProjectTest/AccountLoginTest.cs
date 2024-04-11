using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;

namespace Logic.Tests
{
    [TestClass]
    public class AccountLogicTests
    {   

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            // Load account data before each test
            App.Start();
        }

        [TestInitialize]
        public void Reset(){
            AccountLogic.Logout();
        }


        [TestMethod]
        public void CheckLoginTest()
        {
            // Arrange
            string loginName = "Admin123";
            string loginPassword = "Password123";

            Account wrongAdmin = new("Wrong", "Wrong", true);
            Assert.IsFalse(AccountLogic.CheckLogin(loginName, loginPassword, wrongAdmin), "Login should not be successful");

            Account correctAdmin = new(loginName, loginPassword, true);
            Assert.IsTrue(AccountLogic.CheckLogin(loginName, loginPassword, correctAdmin), "Login should be successful");

            Account customer = new("Customer", "Password", false);
            Assert.IsTrue(AccountLogic.CheckLogin("Customer", "Password", customer), "Login should be successful");
        }

        [TestMethod]
        public void AdminLogin(){
            string loginName = "Admin123";
            string loginPassword = "Password123";
            Account Admin = new(loginName, loginPassword, true);
            AccountLogic.Login(loginName, loginPassword);

            Assert.AreNotEqual("Unknown", App.LoggedInUsername);

            string menuString = App.HomePage.MenuString();
            Assert.IsTrue(menuString.Contains("Admin Features"), menuString);
            Assert.IsFalse(menuString.Contains("View Tickets"), menuString);
        }

        [TestMethod]
        public void CustomerLogin(){
            string loginName = "Soufiane";
            string loginPassword = "password";
            Account Customer = new(loginName, loginPassword, false);
            AccountLogic.Login(loginName, loginPassword);

            Assert.AreNotEqual("Unknown", App.LoggedInUsername);

            string menuString = App.HomePage.MenuString();
            Assert.IsTrue(menuString.Contains("View Tickets"), menuString);
            Assert.IsFalse(menuString.Contains("Admin Features"), menuString);
        }
    }
}
