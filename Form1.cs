using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace fitness2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        string connectionString = @"Server=localhost;Database=FitnessDB;Trusted_Connection=True;";

        // Kullanıcı bilgilerini veritabanına kaydetme
        private void KaydetButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Kullanici (Yas, Boy, Kilo, Hedef, HareketSeviyesi) VALUES (@Yas, @Boy, @Kilo, @Hedef, @HareketSeviyesi)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Yas", int.Parse(YasTextBox.Text));
                    command.Parameters.AddWithValue("@Boy", float.Parse(BoyTextBox.Text));
                    command.Parameters.AddWithValue("@Kilo", float.Parse(KiloTextBox.Text));
                    command.Parameters.AddWithValue("@Hedef", HedefComboBox.SelectedItem.ToString());
                    command.Parameters.AddWithValue("@HareketSeviyesi", HareketSeviyesiComboBox.SelectedItem.ToString());

                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Kullanıcı bilgileri başarıyla kaydedildi.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        // Haftalık değişimleri veritabanına kaydetme
        private void HaftalikDegisimKaydetButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO HaftalikDegisim (KullaniciID, Gun1, Gun2, Gun3, Gun4, Gun5, Gun6, Gun7) " +
                                   "VALUES (@KullaniciID, @Gun1, @Gun2, @Gun3, @Gun4, @Gun5, @Gun6, @Gun7)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@KullaniciID", int.Parse(KullaniciIDTextBox.Text)); // Kullanıcı ID'sini al
                    command.Parameters.AddWithValue("@Gun1", float.Parse(Gun1TextBox.Text));
                    command.Parameters.AddWithValue("@Gun2", float.Parse(Gun2TextBox.Text));
                    command.Parameters.AddWithValue("@Gun3", float.Parse(Gun3TextBox.Text));
                    command.Parameters.AddWithValue("@Gun4", float.Parse(Gun4TextBox.Text));
                    command.Parameters.AddWithValue("@Gun5", float.Parse(Gun5TextBox.Text));
                    command.Parameters.AddWithValue("@Gun6", float.Parse(Gun6TextBox.Text));
                    command.Parameters.AddWithValue("@Gun7", float.Parse(Gun7TextBox.Text));

                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Haftalık değişimler başarıyla kaydedildi.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        // Haftalık kilo değişimlerini analiz etme
        private void HaftalikAnalizButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM HaftalikDegisim WHERE KullaniciID = @KullaniciID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@KullaniciID", int.Parse(KullaniciIDTextBox.Text));

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Haftalık değişimleri DataGridView'de görüntüle
                    HaftalikDegisimDataGridView.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
    }
}
    
