using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAPI
{
    public class TransactionHistory
    {
        private Dictionary<BankAccount, Stack<decimal>> _history { get; set; }

        public TransactionHistory()
        {
            this._history = new Dictionary<BankAccount, Stack<decimal>>();
        }

        public bool IsAccountInHistory(BankAccount account)
        {
            return _history.ContainsKey(account);
        }

        public void SaveTransfer(BankAccount recipient, decimal amount)
        {
            // on vérifie s'il y a déja eu une transaction vers le compte spécifié à la méthode (recipient)
            if (_history.ContainsKey(recipient))
            {
                // la pile nous permet d'avoir les transferts stockées dans l'ordre chronologique
                _history[recipient].Push(amount);
            }
            // en cas du premier transfert vers le compte bancaire (recipient)
            else
            {
                _history[recipient] = new Stack<decimal>(new[] { amount });
            }
        }

        public Stack<decimal> QueryHistory(BankAccount account)
        {
            return _history.ContainsKey(account) ? _history[account] : null;
        }
    }
}