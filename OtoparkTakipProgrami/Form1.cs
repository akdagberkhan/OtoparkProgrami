using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace OtoparkTakipProgrami
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Veritabanı Bağlantısı
        /*Veri tabanı yolunu belirterek baglan degişkenime veri tabanımı tanıttım.*/
        OleDbConnection baglan = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Otopark_db.mdb");
        //Veritabanı *SON

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public static string kullanici;
        private void button1_Click(object sender, EventArgs e)
        {
            //GİRİŞ KONTROL
            /*
             veritabanıma kayıtlı olan kullanıcılar arasında textboxlara girilen akullanıcıadı ve şifre ile uyuşan kullanıcım olup olmadığını kontrol ettirdim
             ve eğer varsa girişini sağlayıp anamenu ye yönlendirdim.
             */
            try
            {
                baglan.Open();
                DataTable tablo = new DataTable();
                OleDbDataAdapter adp = new OleDbDataAdapter("select * from admin where kul_ad='" + textBox1.Text + "' and sifre='" + textBox2.Text + "'", baglan);
                adp.Fill(tablo);
                baglan.Close();
                kullanici = tablo.Rows[0][2].ToString();
                if (tablo.Rows[0][2].ToString() != "")
                {
                    anamenu ana = new anamenu();
                    ana.Show();
                    this.Hide();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Hatalı Giriş");
            }
            //GİRİŞ KONTROL *SON
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }
    }
}
