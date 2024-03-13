using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MySqlConnector;

//поправить граммы, разобраться с галочками.
namespace SpisokPokupok
{
    public partial class Form1 : Form
    {
        MySqlConnection conn;
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {//добавить в список
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            string nazvanieProdukta = textBox1.Text.Trim();
            if (nazvanieProdukta.Length > 2)
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO `spisokpokupok`(`Name`, `quantity`, `mera`, `price`) VALUES (@name,@quantity,@mera,@price);";
                cmd.Parameters.AddWithValue("name", nazvanieProdukta);
                cmd.Parameters.AddWithValue("quantity", numericUpDown1.Value);
                cmd.Parameters.AddWithValue("mera", comboBox1.Text);
                cmd.Parameters.AddWithValue("price", numericUpDown2.Value);
                cmd.ExecuteNonQuery();

                DBOutput(conn);
                textBox1.Clear();
                numericUpDown1.Value = 1;
                numericUpDown2.Value = 10;
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Название продукта слишком короткое");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {//удалить из списка

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection("Server=localhost;User ID=pk31;Password=pk31;Database=pk31spisokpokupok");
                //connection.Open();
                Console.WriteLine(connection.State);
                conn = connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                //отловить код ошибки подключения к БД
            }
            comboBox1.SelectedIndex = 0;
            DBOutput(conn);
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
        void DBOutput(MySqlConnection _conn)
        {//метод для получения всей таблицы и вывода в листбокс
            if (_conn.State != ConnectionState.Open)
            {
                _conn.Open();
            }

            MySqlCommand command = new MySqlCommand("SELECT `Name`, `quantity`, `mera`, `price` FROM `spisokpokupok`;", _conn);
            MySqlDataReader reader = command.ExecuteReader();
            checkedListBox1.Items.Clear();
            while (reader.Read())
            {
                string sql = $"{reader.GetString(0)} {reader.GetInt32(1)}{reader.GetString(2)} {reader.GetInt32(3)}руб.";
                checkedListBox1.Items.Add(sql);
            }
            _conn.Close();
        }
    }
}
