using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MySqlConnector;

namespace SpisokPokupok
{
    public partial class Form1 : Form
    {
        MySqlConnection conn;
        MySqlCommand cmdGlobal = new MySqlCommand();
        List<int> idList = new List<int>();
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
                //MySqlCommand cmd = new MySqlCommand();
                cmdGlobal.Connection = conn;
                cmdGlobal.CommandText = "INSERT INTO `spisokpokupok`(`Name`, `quantity`, `mera`, `price`) VALUES (@name,@quantity,@mera,@price);";
                cmdGlobal.Parameters.Clear();
                cmdGlobal.Parameters.AddWithValue("name", nazvanieProdukta);
                cmdGlobal.Parameters.AddWithValue("quantity", numericUpDown1.Value);
                cmdGlobal.Parameters.AddWithValue("mera", comboBox1.Text);
                if (comboBox1.Text.Equals("г."))
                {
                    cmdGlobal.Parameters.AddWithValue("price", numericUpDown2.Value / 1000);
                }
                else
                {
                    cmdGlobal.Parameters.AddWithValue("price", numericUpDown2.Value);
                }
                cmdGlobal.ExecuteNonQuery();

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
            //DELETE FROM `spisokpokupok` WHERE `id`=2
            if (checkedListBox1.SelectedIndex > -1)
            {
                string sql = "DELETE FROM `spisokpokupok` WHERE `id`=@zombie";
                cmdGlobal.CommandText = sql;
                cmdGlobal.Connection = conn;
                cmdGlobal.Parameters.Clear();
                cmdGlobal.Parameters.AddWithValue("zombie", idList[checkedListBox1.SelectedIndex]);
                conn.Open();
                cmdGlobal.ExecuteNonQuery();
                conn.Close();
                DBOutput(conn);
            }
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
        void DBOutput(MySqlConnection _conn)
        {//метод для получения всей таблицы и вывода в листбокс
            if (_conn.State != ConnectionState.Open)
            {
                _conn.Open();
            }

            //MySqlCommand command = new MySqlCommand("SELECT `Name`, `quantity`, `mera`, `price`, `id` FROM `spisokpokupok`;", _conn);
            cmdGlobal.CommandText = "SELECT `Name`, `quantity`, `mera`, `price`, `id` FROM `spisokpokupok`;";
            cmdGlobal.Connection = _conn;
            MySqlDataReader reader = cmdGlobal.ExecuteReader();
            checkedListBox1.Items.Clear();
            idList.Clear();
            while (reader.Read())
            {
                string sql = $"{reader.GetString(0)} {reader.GetInt32(1)}{reader.GetString(2)} {reader.GetFloat(3)}руб.";
                checkedListBox1.Items.Add(sql);
                idList.Add(reader.GetInt32(4));
            }
            reader.Close();
            //SELECT SUM(`quantity`*`price`) FROM `spisokpokupok` 
            cmdGlobal.CommandText = "SELECT SUM(`quantity`*`price`) FROM `spisokpokupok`;";
            cmdGlobal.ExecuteNonQuery();
            MySqlDataReader sumReader = cmdGlobal.ExecuteReader();
            while (sumReader.Read())
            {
                label1.Text = $"ИТОГО: {sumReader.GetFloat(0)}руб.";
            }
            _conn.Close();
        }
    }
}
