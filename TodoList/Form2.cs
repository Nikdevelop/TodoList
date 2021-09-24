using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TodoList
{
    public partial class Form2 : Form
    {
        private readonly Item _item;

        public Form2(Item item)
        {
            _item = item;
            InitializeComponent();
        }
        
        private void Form2_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = _item.IsDone;
            textBox1.Text = _item.Name;
            textBox2.Text = _item.Description;
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 form = (Form1)this.Owner;
            if (checkBox1.Checked != _item.IsDone ||
                textBox1.Text != _item.Name ||
                textBox2.Text != _item.Description)
            {
                var newItems = new List<Item>();
                for(int i = 0; i < form.Items.Count; i++)
                {
                    if (form.Items[i].Id == _item.Id)
                    {
                        newItems.Add(new Item(Guid.NewGuid(), textBox1.Text, textBox2.Text, checkBox1.Checked));
                    }
                    else
                    {
                        newItems.Add(form.Items[i]);
                    }
                }

                form.Items = newItems;
                form.UpdateItems();
            }
        }
    }
}
