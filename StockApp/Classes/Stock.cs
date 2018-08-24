using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Classes
{
    public class Stock
    {
        public List<StockItem> AvailableItems {get; set;}

        private static Stock instance;

        private Stock()
        {
            AvailableItems = new List<StockItem>();
        }

        public void AddItem(StockItem item)
        {
            StockItem find = null;
            if (AvailableItems.Count() > 0)
                find = AvailableItems.Find(x => x.ItemCode.ToLower() == item.ItemCode.ToLower());

            if(find != null)
            {
                find.Quantity += item.Quantity;
            }
            else
            {
                AvailableItems.Add(item);
            }
        }

        public static Stock GlobalStock
        {
            get
            {
                if(instance == null)
                {
                    instance = new Stock();
                }
                return instance;
            }
        }
    }
}
