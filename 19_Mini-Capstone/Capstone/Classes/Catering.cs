using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Catering
    {
        // This class should contain all the "work" for catering

        private List<CateringItem> items = new List<CateringItem>();

        public Catering()
        {
            FileAccess fileAccess = new FileAccess();
            items = fileAccess.ReadFromFile();
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
        

   
    }
}
