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
    public partial class aboneislem : Form
    {
        public aboneislem()
        {
            InitializeComponent();
        }

        //Veritabanı Bağlantısı
        /*Veri tabanı yolunu belirterek baglan degişkenime veri tabanımı tanıttım.*/
        OleDbConnection baglan = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Otopark_db.mdb");
        //Veritabanı *SON

        private void button7_Click(object sender, EventArgs e)
        {

            try
            {
                baglan.Open();
                OleDbCommand cmd = new OleDbCommand("insert into aboneler(ad_soyad,plaka,tel,posta,adres) values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "')", baglan);
                cmd.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Abone Eklendi.");
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Bir Hata Ouştu.");
                this.Close();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                baglan.Open();
                OleDbCommand cmd = new OleDbCommand("delete from aboneler where plaka='"+textBox2.Text+"'",baglan);
                cmd.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Abone Silindi.");
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Bir Hata Ouştu.");
                this.Close();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
