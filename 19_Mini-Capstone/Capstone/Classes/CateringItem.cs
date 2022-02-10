using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class CateringItem
    {
        // This class should contain the definition for one catering item
        public CateringItem(string productCode, string description, int qty, decimal price)
        {
            ProductCode = productCode;
            Description = description;
            Qty = qty;
            Price = price;

        }
     
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
    }
}
