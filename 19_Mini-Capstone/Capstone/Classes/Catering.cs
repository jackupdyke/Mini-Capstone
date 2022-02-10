﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Catering
    {
        // This class should contain all the "work" for catering

        private List<CateringItem> items = new List<CateringItem>();
        private Dictionary<string, int> shoppingCart = new Dictionary<string, int>();


        public Catering()
        {
            FileAccess fileAccess = new FileAccess();
            items = fileAccess.ReadFromFile();
            //items = SortProductCode(items);
            
        }

        //public List<CateringItem> SortProductCode(List<CateringItem> items)
        //{
        //    List<string> productCode = new List<string>();
        //    List<CateringItem> sortedMenu = new List<CateringItem>();
        //    foreach(CateringItem item in items)
        //    {
        //        productCode.Add(item.ProductCode);
        //    }
        //    productCode.Sort();
        //    //for(int i = 0; i < productCode.Count; i++)
        //    //{
        //    //    int index = items[i].ProductCode.IndexOf(productCode[i]);
        //    //    sortedMenu.Add(productCode[i], items.);

        //    //}
        //    return sortedMenu;
        //}
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
                            return "added";
                        }
                        else
                        {
                            shoppingCart.Add(productCode, qty);
                            if (int.Parse(item.Qty) == 0)
                            {
                                item.Qty = "SOLD OUT";
                            }
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

        public decimal GetChangeReturned()
        {
            decimal changeDue = 0.00M;

            changeDue = Balance - GetTotalCost();
            return changeDue;
        }
    }
}
