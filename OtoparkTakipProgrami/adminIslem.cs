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
    public partial class adminIslem : Form
    {
        public adminIslem()
        {
            InitializeComponent();
        }

        //Veritabanı Bağlantısı
        /*Veri tabanı yolunu belirterek baglan degişkenime veri tabanımı tanıttım.*/
        OleDbConnection baglan = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Otopark_db.mdb");
        //Veritabanı *SON

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
            adminIslem admı = new adminIslem();
            admı.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            //VERİTABANINA ADMİN EKLEME
            /*-Veritabanıma sql komut kullanarak textboxlara girdiğim değerleri kaydettim
              -try,catch kullanarak herhangi bir hata mesajının kullanıcıya yansımamasını sağladım
              -OleDbCommand ile veritabanıma komut gönderdim*/
            try
            {
                baglan.Open();
                OleDbCommand cmd = new OleDbCommand("insert into admin(ad_soyad,kul_ad,sifre,tel,posta,adres) values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "')", baglan);
                cmd.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("İşlem Başarılı");
            }
            catch (Exception)
            {
                MessageBox.Show("Bir Hata Oluştu");

            }
            //VERİTABANINA ADMİN EKLEME *SON

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //VERİTABANINDAN ADMİN SİLME
            /*-Veritabanıma sql komut kullanarak textboxa girdiğim değer adındaki kullanıcıyı sildim
              -try,catch kullanarak herhangi bir hata mesajının kullanıcıya yansımamasını sağladım
              -OleDbCommand ile veritabanıma komut gönderdim*/
            try
            {
                baglan.Open();
                OleDbCommand cmd = new OleDbCommand("delete from admin where kul_ad='"+textBox14.Text+"'", baglan);
                cmd.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("İşlem Başarılı");
            }
            catch (Exception)
            {
                MessageBox.Show("Bir Hata Oluştu");

            }
            //VERİTABANINDAN ADMİN SİLME *SON
        }
    }
}
