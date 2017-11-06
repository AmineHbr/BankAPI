using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAPI
{
   public class BankAccount
   {
       #region Properties and Constructors

       public decimal balance{ get; set; }

       public TransactionHistory history { get; set; }
       //Constructeur
        public BankAccount()
        {
            this.history = new TransactionHistory();
        }

        public BankAccount(decimal balance)
        {
            this.balance = balance;
            this.history = new TransactionHistory();
        }

       #endregion

        #region Methods
        //methode qui permet de deposer une somme au compte 
        public void Deposit(decimal amount)
        {
            if (amount >= 0)
                this.balance += amount;
            else
                throw new ArgumentException("Trying to deposit negative amount !");
        }

       //methode qui permet de retirer une somme du compte 
        public void Withdraw(decimal amount)
        {

            if (amount <= balance)
                this.balance -= amount;
            else
                throw new ArgumentException("You can't Withdraw this amount , it's greater than balance !");
            
        }

       //methode qui permet de transferer une somme à un autre compte
        public void Transfer(decimal transferredAmount, BankAccount destinationAccount)
        {
            if (this.balance >= transferredAmount)
            {
                destinationAccount.Deposit(transferredAmount);
                this.Withdraw(transferredAmount);
                SaveTransfer(transferredAmount, destinationAccount);
            }

            else
                throw new ArgumentException("You can't Withdraw this amount , it's greater than balance !");
        }

        //methode qui permet de créer un historique de transfert
        public void SaveTransfer(decimal transferredAmount, BankAccount destinationAccount)
        {
            this.history.SaveTransfer(destinationAccount, -transferredAmount);
            destinationAccount.history.SaveTransfer(this, transferredAmount);
        }


        public Stack<decimal> QueryTransfertHistory(BankAccount account)
        {
            return this.history.QueryHistory(account);
        }
        #endregion
   }
}