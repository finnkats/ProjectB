using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;

namespace Logic.Tests
{
    [TestClass]
    public class AccountLogicTests
    {   

        private Dictionary<string, AccountDataModel> accountData = new();

        [TestInitialize]
        public void Setup()
        {
            // Load account data before each test
            Dictionary<string, AccountDataModel>? accountDataTemp = AccountDataAccess.LoadAll("../../../Sources/AccountData.json");
            if (accountData != null) accountData = accountDataTemp;
        }


        [TestMethod]
        public void CheckLogin()
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
    }
}
