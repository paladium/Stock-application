using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Classes
{
    public class StockDocument
    {

        public string DocumentName { get; set; }


        public string Content { get; set; }

        public string Date { get; set; }

        public void SaveDocument()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + (DocumentName + Date) + ".txt";
            File.WriteAllText(path, Content);
        }
    }
}
