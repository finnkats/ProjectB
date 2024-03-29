using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;

namespace Logic.Tests
{
    [TestClass]
    public class AccountLogicTests
    {   

        private static Dictionary<string, AccountDataModel> accountData = new();

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            // Load account data before each test
            Dictionary<string, AccountDataModel>? accountDataTemp = AccountDataAccess.LoadAll("../../../Sources/AccountData.json");
            if (accountData != null) accountData = accountDataTemp;
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

            AccountDataModel wrongAdmin = new("Wrong", "Wrong", true);
            Assert.IsFalse(AccountLogic.CheckLogin(loginName, loginPassword, wrongAdmin), "Login should not be successful");

            AccountDataModel correctAdmin = new(loginName, loginPassword, true);
            Assert.IsTrue(AccountLogic.CheckLogin(loginName, loginPassword, correctAdmin), "Login should be successful");

            AccountDataModel customer = new("Customer", "Password", false);
            Assert.IsTrue(AccountLogic.CheckLogin("Customer", "Password", customer), "Login should be successful");
        }

        [TestMethod]
        public void AdminLogin(){
            string loginName = "Admin123";
            string loginPassword = "Password123";
            AccountDataModel Admin = new(loginName, loginPassword, true);
            AccountLogic.Login(accountData, loginName, loginPassword);

            Assert.AreNotEqual("Unknown", App.LoggedInUsername);

            string menuString = App.HomePage.MenuString();
            Assert.IsTrue(menuString.Contains("Admin Features"), menuString);
            Assert.IsFalse(menuString.Contains("View Tickets"), menuString);
        }

        [TestMethod]
        public void CustomerLogin(){
            string loginName = "Soufiane";
            string loginPassword = "password";
            AccountDataModel Customer = new(loginName, loginPassword, false);
            AccountLogic.Login(accountData, loginName, loginPassword);

            Assert.AreNotEqual("Unknown", App.LoggedInUsername);

            string menuString = App.HomePage.MenuString();
            Assert.IsTrue(menuString.Contains("View Tickets"), menuString);
            Assert.IsFalse(menuString.Contains("Admin Features"), menuString);
        }
    }
}
