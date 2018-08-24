using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Classes
{
    public class PointOfPurchase
    {

        public string Name { get; set; }

        public string Description {get; set;}

        public List<StockItem> Items { get; set; }

        private Guid ID;

        public string GetID()
        {
            if (ID == Guid.Empty)
            {
                ID = Guid.NewGuid();
            }

            return ID.ToString();
        }

        public PointOfPurchase(string Name)
        {
            this.Name = Name;
            Items = new List<StockItem>();
            GetID();
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
