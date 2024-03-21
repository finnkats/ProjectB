using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;

namespace Logic.Tests
{
    [TestClass]
    public class AccountLogicTests
    {   

        private List<AccountDataModel> accountDataList;

        [TestInitialize]
        public void Setup()
        {
            // Load account data before each test
            accountDataList = AccountDataAccess.LoadAll("../../../Sources/AccountData.json");
        }


        [TestMethod]
        public void Login_AdminLogin_Success()
        {
            // Arrange
            string input = "Admin123\nPassword123\n";
            using (StringReader stringReader = new StringReader(input))
            {
                using (StringWriter stringWriter = new StringWriter())
                {
                    Console.SetIn(stringReader);
                    Console.SetOut(stringWriter);

                    // Act
                    AccountLogic.Login(accountDataList);

                    // Assert
                    string consoleOutput = stringWriter.ToString();
                    Assert.IsTrue(consoleOutput.Contains("Login successful!"), "Admin login should be successful");
                    Assert.IsTrue(consoleOutput.Contains("Logged in as administrator Admin123"), "Logged in as administrator should be printed");
                }
            }
        }

        [TestMethod]
        public void Login_CustomerLogin_Success()
        {
            // Arrange
            string input = "Soufiane\npassword\n";
            using (StringReader stringReader = new StringReader(input))
            {
                using (StringWriter stringWriter = new StringWriter())
                {
                    Console.SetIn(stringReader);
                    Console.SetOut(stringWriter);

                    // Act
                    AccountLogic.Login(accountDataList);

                    // Assert
                    string consoleOutput = stringWriter.ToString();
                    Assert.IsTrue(consoleOutput.Contains("Login successful!"), "Customer login should be successful");
                    Assert.IsTrue(consoleOutput.Contains("Welcome back Soufiane"), "Welcome message should be printed");
                }
            }
        }

        [TestMethod]
        public void Login_InvalidCredentials_Fail()
        {
            // Arrange
            string input = "InvalidName\nInvalidPassword\n";
            using (StringReader stringReader = new StringReader(input))
            {
                using (StringWriter stringWriter = new StringWriter())
                {
                    Console.SetIn(stringReader);
                    Console.SetOut(stringWriter);

                    // Act
                    AccountLogic.Login(accountDataList);

                    // Assert
                    string consoleOutput = stringWriter.ToString();
                    Assert.IsTrue(consoleOutput.Contains("Invalid name or password. Please try again."), "Login with invalid credentials should fail");
                }
            }
        }
    }
}
