using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;

var accountDataList = AccountDataAccess.LoadAll();

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
            accountDataList = AccountDataAccess.LoadAll();
        }

        [TestMethod]
        public void Login_AdminLogin_Success()
        {
            // Arrange
            string input = "Admin123\nPassword123\n";
            using (StringReader stringReader = new StringReader(input))
            {
                // Redirect Console input
                Console.SetIn(stringReader);

                // Captures the console output
                using (StringWriter stringWriter = new StringWriter())
                {
                    Console.SetOut(stringWriter);

                    // Act
                    AccountLogic.Login();

                    foreach (var accountData in accountDataList)
                    {
                        // Assert
                        string consoleOutput = stringWriter.ToString();
                        Assert.IsTrue(consoleOutput.Contains("Login successful!"), "Login should be successful");
                        Assert.IsTrue(consoleOutput.Contains($"Logged in as administrator {accountData.Admin.AdminName}"), "Logged in as administrator should be printed");
                    }
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
                // Redirect Console input
                Console.SetIn(stringReader);

                // Captures the console output
                using (StringWriter stringWriter = new StringWriter())
                {
                    Console.SetOut(stringWriter);

                    // Act
                    AccountLogic.Login();

                    // Assert
                    foreach (var accountData in accountDataList)
                    {
                        foreach (var customer in accountData.Customers)
                        {
                            string consoleOutput = stringWriter.ToString();
                            Assert.IsTrue(consoleOutput.Contains("Login successful!"), "Login should be successful");
                            Assert.IsTrue(consoleOutput.Contains($"Welcome back {customer.Name}"), "Logged in as administrator should be printed");
                            break;
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void Login_CustomerLogin_Fail()
        {
            // Arrange
            string input = "A\nA\n";
            using (StringReader stringReader = new StringReader(input))
            {
                // Redirect Console input
                Console.SetIn(stringReader);

                // Captures the console output
                using (StringWriter stringWriter = new StringWriter())
                {
                    Console.SetOut(stringWriter);

                    // Act
                    AccountLogic.Login();

                    // Assert
                    foreach (var accountData in accountDataList)
                    {

                        foreach (var customer in accountData.Customers)
                        {
                            string consoleOutput = stringWriter.ToString();
                            Assert.IsTrue(consoleOutput.Contains("Invalid name or password. Please try again.\n"), "Login should be successful");
                            break;
                        }
                    }
                }
            }
        }
    }
}
