using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BankAPI;
using System.Linq;

namespace BankAccountProject
{
    [TestClass]
    public class BankAccountTest
    {
        [TestMethod]
        public void TestDeposit()
        {

            // 1 . Arrange 
            // Set values for the data that will be passed to the method under test
            var currentBalance = 10M;
            var deposit = 20M;
            var expectedBalance = 30M;

            // Initialize the object : Let's say the starting balance of the account is 10 
            BankAccount account = new BankAccount(currentBalance);

            // 2 . Act 
            // Just call the tested method with the arranged parameters
            account.Deposit(deposit);
            var actualBalance = account.balance;

            // 3. Assert 
            // Verifify that you get what you expect to get 
            Assert.AreEqual(actualBalance, expectedBalance);

        }

        [TestMethod]
        public void TestWithdraw()
        {
            // 1 . Arrange 
            var currentBalance = 30M;
            var debit = 20M;
            var expectedBalance = 10M;

            BankAccount account = new BankAccount(currentBalance);

            // 2 . Act 
            account.Withdraw(debit);
            var actualBalance = account.balance;

            // 3. Assert 
            Assert.AreEqual(actualBalance, expectedBalance);
        }


        [TestMethod]
        public void TestTransfer()
        {
            // 1 . Arrange 

            var sourceAccountCurrent = 30M;
            var destinationAccountCurrent = 50M;
            var transferredAmount = 20M;

            var sourceAccountExpectedBalance = sourceAccountCurrent - transferredAmount;
            var destinationAccountExpectedBalance = destinationAccountCurrent + transferredAmount;

            BankAccount destinationAccount = new BankAccount(destinationAccountCurrent);
            BankAccount sourceAccount = new BankAccount(sourceAccountCurrent);


            // 2 . Act 

            sourceAccount.Transfer(transferredAmount, destinationAccount);
            var sourceAccountActualBalance = sourceAccount.balance;
            var destinationAccountActualBalance = destinationAccount.balance;

            // 3. Assert 

            Assert.AreEqual(sourceAccountActualBalance, sourceAccountExpectedBalance);
            Assert.AreEqual(destinationAccountActualBalance, destinationAccountExpectedBalance);

        }


        [TestMethod]
        public void TestSaveTransferHistory()
        {
            // Arrange

            BankAccount sourceAccount = new BankAccount(40M);
            BankAccount destinationAccount = new BankAccount(30M);
            var transferredAmount = 10M;

            // Act 		

            sourceAccount.SaveTransfer(transferredAmount, destinationAccount);

            // Assert 

            Assert.IsTrue(sourceAccount.history.IsAccountInHistory(destinationAccount));
            Assert.IsTrue(destinationAccount.history.IsAccountInHistory(sourceAccount));
        }


        [TestMethod]
        public void TestQueryTransfertHistory()
        {
            // Arrange

            BankAccount sourceAccount = new BankAccount(40);
            BankAccount destinationAccount = new BankAccount(30);

            var transferredAmount = 10M;
            var sourceAccountExpectedTransactions = new Stack<decimal>();
            sourceAccountExpectedTransactions.Push(-10);

            var destinationAccountExpectedTransactions = new Stack<decimal>() { };
            destinationAccountExpectedTransactions.Push(10);

            // Act 		

            sourceAccount.Transfer(transferredAmount, destinationAccount);
            var sourceActualHistoryTransaction = sourceAccount.QueryTransfertHistory(destinationAccount);
            var destActualHistoryTransaction = destinationAccount.QueryTransfertHistory(sourceAccount);

            // Assert 

            Assert.IsTrue(sourceAccountExpectedTransactions.SequenceEqual(sourceActualHistoryTransaction));
            Assert.IsTrue(destinationAccountExpectedTransactions.SequenceEqual(destActualHistoryTransaction));
        }
    }
}
