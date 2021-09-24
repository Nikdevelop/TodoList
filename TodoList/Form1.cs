using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace TodoList
{
    public partial class Form1 : Form
    {
        public List<Item> Items;
        private readonly string _config;

        public Form1()
        {
            Items = new List<Item>();
            _config = Application.StartupPath + "\\config.json";
            if (!File.Exists(_config))
            {
                File.Create(_config).Close();
            }
            InitializeComponent();
            LoadItems(_config);
            UpdateItems();
        }

        private void SaveChanges(string execPath)
        {
            using (StreamWriter sw = new StreamWriter(execPath))
            {                
                sw.WriteLine(JsonConvert.SerializeObject(Items));
            }
        }

        private void LoadItems(string execPath)
        {
            using (StreamReader sr = new StreamReader(execPath))
            {
                List<Item> items = JsonConvert.DeserializeObject<List<Item>>(sr.ReadToEnd());
                if (items != null)
                {
                    foreach (var item in items)
                    {
                        Items.Add(item);
                    }
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveChanges(_config);
        }


        private void RemoveItem() // now clears full list
        {
            //var newList = Items.Except(items).ToList();
            //Items = newList;
            Items.Clear();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveChanges(_config);
        }

        public void UpdateItems()
        {
            listBox1.Items.Clear();
            if (Items != null)
            {
                foreach (var item in Items)
                {
                    listBox1.Items.Add(item.Name + "  " + (item.IsDone ? "[Done]" : "[Work In Progress]"));
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Items.Add(new Item(Guid.NewGuid(), textBox1.Text, textBox2.Text, false));
            textBox1.Text = textBox2.Text = "";

            UpdateItems();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // delete
            //var items = Items.Where(i => i.Name == listBox1.SelectedItem.ToString()).ToList();
            RemoveItem(); // now clears full list
            UpdateItems();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            //Enabled = false;
            if (listBox1.SelectedIndex != -1)
            {
                try
                {
                    var item = Items[listBox1.SelectedIndex];
                    var details = new Form2(item);
                    details.Owner = this;
                    details.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Critical error!\n Try to reload the App.\n" + ex.Message);
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form3().Show();
        }
    }
}
