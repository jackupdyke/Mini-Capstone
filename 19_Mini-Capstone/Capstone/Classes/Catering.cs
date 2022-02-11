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
            //gets list of menu items and calls method to sort them
            items = fileAccess.ReadFromFile();
            items = SortProductCode(items);
            
        }

        //method sorts items in list by product code
        public List<CateringItem> SortProductCode(List<CateringItem> items)
        {
            //list to hold only product codes and one to hold new sorted menu
            List<string> productCode = new List<string>();
            List<CateringItem> sortedMenu = new List<CateringItem>();

            //makes list of just product codes
            foreach (CateringItem item in items)
            {
                productCode.Add(item.ProductCode);
            }
            //sorts alphabetically
            productCode.Sort();

            //added items menu items in alaphabetical order using product list
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

            //returns sorted list
            return sortedMenu;
        }

        //gives an array of all menu items
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

        //adds money to account 
        public decimal AddBalance(decimal addAmount)
        {
            //checks if it's a valid dollar amount and if it's below the $1,500 max limit
            
            if (addAmount <= 100 && Balance + addAmount <= 1500)
            {
                if (addAmount == 1 || addAmount == 5 || addAmount == 10 || addAmount == 20 ||
                    addAmount == 50 || addAmount == 100)
                {
                    
                    //adds money
                    Balance += addAmount;

                    //writes amount added transaction to log file
                    string addMoneyInfo = $"{DateTime.Now} ADD MONEY: {addAmount} {Balance}";
                    fileAccess.WriteToFile(addMoneyInfo);

                    //returns new balance
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


        //adds selected items to shopping cart
        public string AddToShoppingCart(string productCode, int qty)
        {
           //runs through each item in menu
            foreach(CateringItem item in items)
            {

                //checks if current item has needed product code
                if(item.ProductCode == productCode)
                {
                    if(item.Qty == "SOLD OUT")
                    {
                        return "Item sold out";
                    }
                    
                    //checks if quantity wanted greater or equal to inventory and if balance
                    //is sufficient for transaction
                   else if ((int.Parse(item.Qty) >= qty) && (Balance >= (qty * item.Price)))
                    {

                        //gets integer of current quatity;
                        int tempQty = int.Parse(item.Qty);

                        //updates item quantity
                        item.Qty = (tempQty - qty).ToString();

                        //check if cart already has this item
                        if (shoppingCart.ContainsKey(productCode))
                        {shoppingCart[productCode] = qty + shoppingCart[productCode];
                            Balance -= item.Price * qty;
                            if (int.Parse(item.Qty) == 0)
                            {
                                item.Qty = "SOLD OUT";
                            } 
                            return "added";
                        }
                        else
                        {
                            shoppingCart.Add(productCode, qty);
                            Balance -= item.Price * qty;

                            string shoppingCartTransaction = $"{DateTime.Now} {shoppingCart[productCode]} {item.Description} {productCode} {item.Price * shoppingCart[productCode]} {Balance}";
                            fileAccess.WriteToFile(shoppingCartTransaction);
                            if (int.Parse(item.Qty) == 0)
                            {
                                item.Qty = "SOLD OUT";
                            }
                            
                            return "added";
                        }
 
                    }
                    
                    else if(Balance < item.Price * qty)
                    {
                        return "Insufficient Funds";
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
            changeDue = Balance;
            decimal changeDueConstant = changeDue;

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

            string getChangeTransaction = "";
            getChangeTransaction = $"{DateTime.Now} GIVE CHANGE {changeDueConstant} {Balance}";
            fileAccess.WriteToFile(getChangeTransaction);
            return returnString;
        }


    }
}
