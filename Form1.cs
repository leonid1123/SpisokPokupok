using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpisokPokupok
{
    public partial class Form1 : Form
    {
        List<Pokupki> SpisokPokupok = new List<Pokupki>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {//добавить в список
            string nazvanieProdukta = textBox1.Text.Trim();
            if (nazvanieProdukta.Length > 2)
            {
                Pokupki pokupka = new Pokupki(nazvanieProdukta,(int)numericUpDown1.Value,(int)numericUpDown2.Value);
                SpisokPokupok.Add(pokupka);
                checkedListBox1.Items.Add(pokupka.ShowInfo());
            }
            else
            {
                MessageBox.Show("Название продукта слишком короткое");
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {//удалить из списка
            int index = checkedListBox1.SelectedIndex;
            if (index > -1)
            {
                checkedListBox1.Items.RemoveAt(index);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
