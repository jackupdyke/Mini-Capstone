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
                    Console.WriteLine($"Product Code Description Qty Price");
                    CateringItem[] items = catering.GetItems();

                    for (int i = 0; i < items.Length; i++)
                    {
                        Console.WriteLine($"{items[i].ProductCode} {items[i].Description} {items[i].Qty} {items[i].Price}");
                    }

                }
                else if (mainMenuOption == "2")
                {
                    //while loop?? until transaction completed
                    bool completedTransaction = false;
                    while (!completedTransaction)
                    {
                        Console.WriteLine("(1) Add money");
                        Console.WriteLine("(2) Select products");
                        Console.WriteLine("(3) Complete transaction");
                        Console.WriteLine($"Current account balance: {catering.Balance}");
                        string orderOptions = Console.ReadLine();
                        if (orderOptions == "1")
                        {
                            Console.WriteLine("Please enter whole dollar amount up to $500. (e.g. $1, $5, $10,etc.)");
                            decimal depositAmount = decimal.Parse(Console.ReadLine());
                            catering.AddBalance(depositAmount);
                        }
                        else if (orderOptions == "2")
                        {
                            CateringItem[] items = catering.GetItems();

                            for (int i = 0; i < items.Length; i++)
                            {
                                Console.WriteLine($"{items[i].ProductCode} {items[i].Description} {items[i].Qty} {items[i].Price}");
                            }

                            Console.WriteLine("Please select a product:");
                            string productChoice = Console.ReadLine();
                            Console.WriteLine("Please select quantity");
                            int productQty = int.Parse(Console.ReadLine());
                            Console.WriteLine(catering.AddToShoppingCart(productChoice, productQty));
                        }


                        else if (orderOptions == "3")
                        {
                            Console.WriteLine("Transaction complete");
                            completedTransaction = true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid user input.");
                        }
                    }


                }

            }
        }
    }
}