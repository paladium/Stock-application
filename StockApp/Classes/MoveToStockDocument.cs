using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Classes
{
    public class MoveToStockDocument : StockDocument
    {

        public MoveToStockDocument()
        {
            DocumentName = "MoveToStock_";

            Date = DateTime.UtcNow.Ticks.ToString();
        }

        public void AddItemMovement(StockItem item, string baseName)
        {
            Content += String.Format("Item with name {0}, code {1} and price {2} from {3} was added in the quantity of {4}", 
                item.Name, item.ItemCode, item.Price, baseName, item.Quantity);
        }
    }
}
