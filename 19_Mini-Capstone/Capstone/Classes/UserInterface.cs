using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class UserInterface
    {
        // This class provides all user communications, but not much else.
        // All the "work" of the application should be done elsewhere

        // ALL instances of Console.ReadLine and Console.WriteLine should 
        // be in this class.
        // NO instances of Console.ReadLine or Console.WriteLIne should be
        // in any other class.

        private Catering catering = new Catering();
       
        public void RunInterface()
        {
            bool done = false;
            while (!done)
            {
                Console.WriteLine("(1) Display Catering Items");

                Console.WriteLine("(2) Order");

                Console.WriteLine("(3) Quit");
                string mainMenuOption = Console.ReadLine();

                if (mainMenuOption == "1")
                {
                    
                    DisplayMenu();

                }
                else if (mainMenuOption == "2")
                {
                    //while loop?? until transaction completed
                    bool completedTransaction = false;
                    while (!completedTransaction)
                    {
                        OrderMenu();
                        string orderOptions = Console.ReadLine();
                        if (orderOptions == "1")
                        {
                            AddMoney();
                        }
                        else if (orderOptions == "2")
                        {
                            SelectProducts();
                        }


                        else if (orderOptions == "3")
                        {
                            //decimal total = catering.GetTotalCost();
                            PrintReciept();
                            
                            completedTransaction = true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid user input.");
                        }
                    }


                }
                else if (mainMenuOption == "3")
                {
                    done = true;
                }

            }
        }
        public void DisplayMenu()
        {

            Console.WriteLine($"Product Code  Description          Qty  Price");
            CateringItem[] items = catering.GetItems();

            for (int i = 0; i < items.Length; i++)
            {
                Console.WriteLine($" {items[i].ProductCode.PadRight(12)} {items[i].Description.PadRight(20)} {items[i].Qty}   ${items[i].Price}");
            }
        }

        public void OrderMenu()
        {
            Console.WriteLine("(1) Add money");
            Console.WriteLine("(2) Select products");
            Console.WriteLine("(3) Complete transaction");
            Console.WriteLine($"Current account balance: {catering.Balance}");
        }

        public void AddMoney()
        {
            Console.WriteLine("Please enter whole dollar amount up to $100. (e.g. $1, $5, $10,etc.)");
            decimal depositAmount = decimal.Parse(Console.ReadLine());
            catering.AddBalance(depositAmount);
            
        }

        public void SelectProducts()
        {
            DisplayMenu();

            Console.WriteLine("Please select a product:");
            string productChoice = Console.ReadLine();
            Console.WriteLine("Please select quantity");
            int productQty = int.Parse(Console.ReadLine());
            Console.WriteLine(catering.AddToShoppingCart(productChoice, productQty));

        }

        public void PrintReciept()
        {
            Dictionary<string, int> cart = catering.GetCart();
            CateringItem[] items = catering.GetItems();
            string itemType = "";
            string message = "";
            foreach(CateringItem item in items)
            {
                if (cart.ContainsKey(item.ProductCode))
                {
                    if (item.ProductCode.Substring(0,1) == "A")
                    {
                        itemType = "Appetizer";
                        message = "You might need extra plates.";
                    }
                    else if (item.ProductCode.Substring(0, 1) == "B")
                    {
                        itemType = "Beverage";
                        message = "Don't forget ice.";
                    }
                    else if (item.ProductCode.Substring(0, 1) == "D")
                    {
                        itemType = "Dessert";
                        message = "Coffee goes with dessert";
                    }
                    else if (item.ProductCode.Substring(0, 1) == "E")
                    {
                        itemType = "Entree";
                        message = "Did you remember dessert?";
                    }
                    Console.WriteLine($"{cart[item.ProductCode].ToString().PadRight(5)} {itemType.PadRight(10)} {item.Description.PadRight(20)} ${item.Price.ToString().PadRight(5)} ${(item.Price * cart[item.ProductCode]).ToString().PadRight(5)} {message.PadRight(5)}");

                    
                    
                }
                
            }
            Console.WriteLine();
            Console.WriteLine($"Total: ${catering.GetTotalCost()}");
            Console.WriteLine();
            Console.WriteLine($"You recieved {catering.GetChangeReturned()}");
            
        }
}
}