using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySqlConnector;

//поправить граммы, разобраться с галочками.
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
                Pokupki pokupka = new Pokupki(nazvanieProdukta, (int)numericUpDown1.Value, comboBox1.Text, (int)numericUpDown2.Value);
                SpisokPokupok.Add(pokupka);
                checkedListBox1.Items.Add(pokupka.ShowInfo());
                label1.Text = $"ИТОГО: {Itogo(SpisokPokupok)}руб.";
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
                SpisokPokupok.RemoveAt(index);
            }
            label1.Text = $"ИТОГО: {Itogo(SpisokPokupok)}руб.";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try {
                MySqlConnection connection = new MySqlConnection("Server=localhost;User ID=pk31;Password=pk31;Database=pk31spisokpokupok");
                connection.Open();
                Console.WriteLine(connection.State);
            } catch(Exception ex ) { 
                Console.WriteLine("pup"+ex.ToString());
                //отловить код ошибки подключения к БД
            }
            comboBox1.SelectedIndex = 0;
            //label6.Font = new Font(label6.Font, label6.Font.Style ^ FontStyle.Strikeout); 
        }
        int Itogo(List<Pokupki> _mySpisok)
        {
            int summa = 0;
            foreach (var item in _mySpisok)
            {
                summa += item.SummaPokupki();
            }
            return summa;
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            
        }
    }
}
