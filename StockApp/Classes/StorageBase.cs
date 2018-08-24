using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Classes
{
    public class StorageBase
    {
        public string Name { get; set; }

        public List<StockItem> Items { get; set; }

        private Guid ID;

        public StorageBase(string Name)
        {
            this.Name = Name;
            Items = new List<StockItem>();
            GetID();
        }

        public void AddItem(StockItem item)
        {
            Items.Add(item);
        }

        public void RemoveItem(string ID)
        {
            Items.RemoveAll(x => x.GetID() == ID);
        }

        public string GetID()
        {
            if (ID == Guid.Empty)
            {
                ID = Guid.NewGuid();
            }

            return ID.ToString();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
