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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            LoadItems();
        }

        private List<StorageBase> bases;

        private List<PointOfPurchase> points;

        private void LoadItems()
        {
            bases = new List<StorageBase>();

            points = new List<PointOfPurchase>();

            StorageBase base1 = new StorageBase("Base1");

            base1.AddItem(new StockItem("Phone", "0001", 45, 10));

            bases.Add(base1);

            StorageBase base2 = new StorageBase("Base2");

            base2.AddItem(new StockItem("Car", "0002", 45, 10));
            base2.AddItem(new StockItem("Plane", "0003", 56, 4));
            base2.AddItem(new StockItem("Bottle", "0004", 13, 150));

            bases.Add(base2);
            LoadItemsComboBox();
            
            
        }

        private void UpdateGrid()
        {
            dataGridView1.DataSource = new List<StockItem>();
            dataGridView1.Refresh();

            if((comboBox1.SelectedItem as StorageBase).Items.Count > 0)
            {
                dataGridView1.DataSource = (comboBox1.SelectedItem as StorageBase).Items;
                dataGridView1.Refresh();

            }
        }

        private void UpdatePoints()
        {
            dataGridView3.DataSource = new List<StockItem>();
            dataGridView3.Refresh();

            if ((comboBox2.SelectedItem as PointOfPurchase).Items.Count > 0)
            {
                dataGridView3.DataSource = (comboBox2.SelectedItem as PointOfPurchase).Items;
                dataGridView3.Refresh();

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateGrid();
        }

        private void LoadItemsComboBox()
        {
            comboBox1.Items.Clear();

            foreach (var item in bases)
            {
                comboBox1.Items.Add(item);
            }

            if(comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
        }

        private void LoadPointsComboBox()
        {
            comboBox2.Items.Clear();

            foreach (var item in points)
            {
                comboBox2.Items.Add(item);
            }

            if (comboBox2.Items.Count > 0)
                comboBox2.SelectedIndex = 0;
        }

        //delete base
        private void button2_Click(object sender, EventArgs e)
        {
            var current = comboBox1.SelectedItem as StorageBase;

            bases.RemoveAll(x => x.GetID() == current.GetID());

            LoadItemsComboBox();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string baseName = Prompt.ShowDialog("Enter base name: ", "Adding new base");

            if(baseName != "")
            {
                bases.Add(new StorageBase(baseName));
                LoadItemsComboBox();
            }
        }

        //add item
        private void button3_Click(object sender, EventArgs e)
        {
            PromptAddStockItem add = new PromptAddStockItem();

            add.FinishEditing = x =>
            {
                bases[comboBox1.SelectedIndex].AddItem(x);
                UpdateGrid();
            };

            add.ShowDialog();

            
        }

        //delete item
        private void button4_Click(object sender, EventArgs e)
        {
            bases[comboBox1.SelectedIndex].RemoveItem((dataGridView1.SelectedRows[0].DataBoundItem as StockItem).GetID());
            UpdateGrid();
        }

        //item to stock
        private void button5_Click(object sender, EventArgs e)
        {
            MovementForm move = new MovementForm();

            move.SetItems(bases[comboBox1.SelectedIndex].Items);

            move.AddedItemsToStock = (x, y) => {
                foreach (var item in y)
                {
                    Stock.GlobalStock.AddItem(item);
                    x.Where(k => k.ItemCode == item.ItemCode).First().Quantity -= item.Quantity;
                }

                dataGridView2.DataSource = new List<StockItem>();
                dataGridView2.Refresh();

                if (Stock.GlobalStock.AvailableItems.Count > 0)
                {
                    dataGridView2.DataSource = Stock.GlobalStock.AvailableItems;
                    dataGridView2.Refresh();

                }

                

                //MoveToStockDocument document = new MoveToStockDocument();

                //foreach(var item in y)
                //{
                //    document.AddItemMovement(item, bases[comboBox1.SelectedIndex].Name);
                //}
                //document.SaveDocument();
                
            };

            move.Show();
        }

        //points of purchase

        //add point
        private void button8_Click(object sender, EventArgs e)
        {
            string pointName = Prompt.ShowDialog("Enter point name: ", "Adding new point");

            if (pointName != "")
            {
                points.Add(new PointOfPurchase(pointName));
                LoadPointsComboBox();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePoints();
        }

        //delete point
        private void button7_Click(object sender, EventArgs e)
        {
            var current = comboBox2.SelectedItem as PointOfPurchase;

            points.RemoveAll(x => x.GetID() == current.GetID());

            LoadPointsComboBox();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MovementForm move = new MovementForm();

            move.Text = "Move from Stock to " + comboBox2.SelectedValue;

            move.SetItems(Stock.GlobalStock.AvailableItems);

            move.AddedItemsToStock = (x, y) =>
            {
                foreach (var item in y)
                {
                    points[comboBox2.SelectedIndex].Items.Add(item);
                    x.Where(k => k.ItemCode == item.ItemCode).First().Quantity -= item.Quantity;
                }

                UpdatePoints();

            };

            move.Show();
        }
    }
}
