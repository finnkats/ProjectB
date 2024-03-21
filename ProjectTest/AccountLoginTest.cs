using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;

namespace Logic.Tests
{
    [TestClass]
    public class AccountLogicTests
    {   

        private List<AccountDataModel> accountDataList = new();

        [TestInitialize]
        public void Setup()
        {
            // Load account data before each test
            List<AccountDataModel>? accountData = AccountDataAccess.LoadAll("../../../Sources/AccountData.json");
            if (accountData != null) accountDataList = accountData;
        }


        [TestMethod]
        public void Login_Admin()
        {
            // Arrange
            string loginName = "Admin123";
            string loginPassword = "Password123";

            AccountDataModel wrongAdmin = new();
            AccountDataModel.AdminAccount wrongAdminInfo = new();
            wrongAdminInfo.AdminName = "Wrong";
            wrongAdminInfo.AdminPassword = "Wrong";
            wrongAdmin.Admin = wrongAdminInfo;
            Assert.IsFalse(AccountLogic.CheckAdmin(loginName, loginPassword, wrongAdmin), "Login should not be successful");

            AccountDataModel correctAdmin = new();
            AccountDataModel.AdminAccount correctAdminInfo = new();
            correctAdminInfo.AdminName = loginName;
            correctAdminInfo.AdminPassword = loginPassword;
            correctAdmin.Admin = correctAdminInfo;
            Assert.IsTrue(AccountLogic.CheckAdmin(loginName, loginPassword, correctAdmin), "Login should be successful");

            AccountDataModel customer = new();
            AccountDataModel.CustomerAccount customerInfo = new();
            Assert.IsFalse(AccountLogic.CheckAdmin(loginName, loginPassword, customer), "Login should not be successful");
        }

        [TestMethod]
        public void Login_Customer()
        {
            // Arrange
            string loginName = "Soufiane";
            string loginPassword = "password";

            AccountDataModel.CustomerAccount wrongCustomer = new();
            wrongCustomer.Name = "Wrong";
            wrongCustomer.Password = "Wrong";
            Assert.IsFalse(AccountLogic.CheckCustomer(loginName, loginPassword, wrongCustomer), "Login should not be successful");

            AccountDataModel.CustomerAccount correctCustomer = new();
            correctCustomer.Name = loginName;
            correctCustomer.Password = loginPassword;
            Assert.IsTrue(AccountLogic.CheckCustomer(loginName, loginPassword, correctCustomer), "Login should be successful");
        }
    }
}
