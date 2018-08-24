using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Classes
{
    public class StockItem
    {

        public string Name { get; set; }

        public string ItemCode { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        private Guid ID;

        public StockItem(string Name, string ItemCode, double Price, int Quantity)
        {
            this.Name = Name;
            this.ItemCode = ItemCode;
            this.Price = Price;
            this.Quantity = Quantity;

            GetID();
        }

        public string GetID()
        {
            if(ID == Guid.Empty)
            {
                ID = Guid.NewGuid();
            }

            return ID.ToString();
        }

        public override string ToString()
        {
            return Name + " has " + Quantity + " items";
        }
    }
}
