using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Catering
    {
        // This class should contain all the "work" for catering

        private List<CateringItem> items = new List<CateringItem>();
        private Dictionary<string, int> shoppingCart = new Dictionary<string, int>();
        private FileAccess fileAccess = new FileAccess();

        public Catering()
        {
            
            items = fileAccess.ReadFromFile();
            items = SortProductCode(items);
            
        }

        public List<CateringItem> SortProductCode(List<CateringItem> items)
        {
            List<string> productCode = new List<string>();
            List<CateringItem> sortedMenu = new List<CateringItem>();
            foreach (CateringItem item in items)
            {
                productCode.Add(item.ProductCode);
            }
            productCode.Sort();
            for (int i = 0; i < productCode.Count; i++)
            {
                foreach (CateringItem item in items)
                {
                    if (productCode[i] == item.ProductCode)
                    {
                        sortedMenu.Add(item);
                    }

                }

            }
            return sortedMenu;
        }
        public CateringItem[] GetItems()
        {
            CateringItem[] arrayItems = new CateringItem[items.Count];
            for (int i = 0; i < items.Count; i++) 
            {
                arrayItems[i] = items[i];
            }
            return arrayItems;
        }

        public decimal Balance { get; private set; }
        public decimal AddBalance(decimal addAmount)
        {
            
            if (addAmount <= 100 && Balance + addAmount <= 1500)
            {
                if (addAmount == 1 || addAmount == 5 || addAmount == 10 || addAmount == 20 ||
                    addAmount == 50 || addAmount == 100)
                {
                    string addMoneyInfo = $"{DateTime.Now} ADD MONEY: {addAmount} {Balance}";
                    fileAccess.WriteToFile(addMoneyInfo);
                    Balance += addAmount;
                    return Balance;
                }
                else
                {
                    return Balance;
                }
            }
            else
            {
                return Balance;
            }
        }

        public string AddToShoppingCart(string productCode, int qty)
        {
           
            foreach(CateringItem item in items)
            {
                if(item.ProductCode == productCode)
                {
                    if (int.Parse(item.Qty) >= qty)
                    {
                        int tempQty = int.Parse(item.Qty);
                        item.Qty = (tempQty - qty).ToString();


                        if (shoppingCart.ContainsKey(productCode))
                        {
                            shoppingCart[productCode] = qty + shoppingCart[productCode];
                            if (int.Parse(item.Qty) == 0)
                            {
                                item.Qty = "SOLD OUT";
                            }

                            Balance -= item.Price * int.Parse(item.Qty);
                            return "added";
                        }
                        else
                        {
                            shoppingCart.Add(productCode, qty);
                            
                            if (int.Parse(item.Qty) == 0)
                            {
                                item.Qty = "SOLD OUT";
                            }
                            Balance -= item.Price * int.Parse(item.Qty);
                            return "added";
                        }
 
                    }
                    else if (int.Parse(item.Qty) == 0)
                    {
                        return "Item Sold Out";
                    }
                    else
                    {
                        return "Insufficient stock";
                    }
                }
                
            }
            return "Product code selected does not exist.";
            
        }

       public Dictionary<string, int> GetCart()
       {
            return shoppingCart;
       }
        public decimal GetTotalCost()
        {
            decimal total = 0.00M;
            foreach(CateringItem item in items)
            {
                foreach(KeyValuePair<string, int> kvp in shoppingCart)
                {
                    if(kvp.Key == item.ProductCode)
                    {
                         total += kvp.Value * item.Price;
                    }
                }
            }
            return total;
        }

        public string GetChangeReturned()
        {
            decimal changeDue = 0.00M;
            changeDue = Balance - GetTotalCost();

            int fifties = 0;
            int twenties = 0;
            int tens = 0;
            int fives = 0;
            int ones = 0;
            int quarters = 0;
            int dimes = 0;
            int nickels = 0;
            string changeString = "";

            if (changeDue - 50 > 0)
            {
                fifties = (int)(changeDue / 50);
                changeDue = changeDue % 50;
                changeString += "(" + fifties + ") fifties, ";
            }
            if (changeDue - 20 > 0)
            {
                twenties = (int)(changeDue / 20);
                changeDue = changeDue % 20;
                changeString += "(" + twenties + ") twenties, ";
            }
            if (changeDue - 10 > 0)
            {
                tens = (int)(changeDue / 10);
                changeDue = changeDue % 10;
                changeString += "(" + tens + ") tens, ";
            }
            if (changeDue - 5 > 0)
            {
                fives = (int)(changeDue / 5);
                changeDue = changeDue % 5;
                changeString += "(" + fives + ") fives, ";
            }
            if (changeDue - 1 > 0)
            {
                ones = (int)(changeDue / 1);
                changeDue = changeDue % 1;
                changeString += "(" + ones + ") ones, ";
            }
            if (changeDue - 0.25M > 0)
            {
                quarters = (int)(changeDue / 0.25M);
                changeDue = changeDue % 0.25M;
                changeString += "(" + quarters + ") quarters, ";
            }
            if (changeDue - 0.10M > 0)
            {
                dimes = (int)(changeDue / 0.10M);
                changeDue = changeDue % 0.10M;
                changeString += "(" + dimes + ") dimes, ";
            }
            if (changeDue - 0.05M >= 0)
            {
                nickels = (int)(changeDue / 0.05M);
                changeString += "(" + nickels + ") nickels, ";
            }
            string returnString = changeString.Substring(0, changeString.Length - 2);
            Balance = 0.00M;
            return returnString;
        }


    }
}
