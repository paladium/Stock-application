using StockApp.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockApp
{
    public partial class PromptAddStockItem : Form
    {

        public delegate void AddItemDelegate(StockItem item);

        public AddItemDelegate FinishEditing { get; set; }

        public PromptAddStockItem()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FinishEditing(new StockItem(itemName.Text, itemCode.Text, double.Parse(itemPrice.Text), int.Parse(itemQuantity.Text)));

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
