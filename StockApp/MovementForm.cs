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

    public delegate void AddedItemsToStock(List<StockItem> left, List<StockItem> right);

    public partial class MovementForm : Form
    {
        public MovementForm()
        {
            InitializeComponent();

            leftList = new List<StockItem>();
            rightList = new List<StockItem>();
        }

        public List<StockItem> leftList;
        public List<StockItem> rightList;

        public AddedItemsToStock AddedItemsToStock { get; set; }

        public void SetItems(List<StockItem> items)
        {
            leftList = items;
            listBox1.DataSource = leftList;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var item = listBox1.SelectedItem as StockItem;
            int quantity = (int)numericUpDown1.Value;
            
            
            if(quantity != 0 && quantity <= item.Quantity)
            {
                listBox2.Items.Add(new StockItem(item.Name, item.ItemCode, item.Price, quantity));
            }
            else
            {
                MessageBox.Show("Enter correct quantity");
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var item = listBox2.SelectedItem;

            listBox2.Items.Remove(item);
        }

        public void MoveToGlobalStock()
        {
            foreach(var item in rightList)
            {
                Stock.GlobalStock.AddItem(item);
                leftList.Where(x => x.ItemCode == item.ItemCode).First().Quantity -= item.Quantity;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            rightList = listBox2.Items.Cast<StockItem>().ToList();

            //MoveToGlobalStock();

            AddedItemsToStock(leftList, rightList);

            this.Close();
        }
    }
}
