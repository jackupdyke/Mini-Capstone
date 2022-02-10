using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Account
    {
        public decimal Balance { get; set;  }

        public Account()
        {
            Balance = 0.00M;
        }

        public decimal AddBalance(decimal addAmount)
        {
            Balance += addAmount;
            return Balance;
        }
    }
}
