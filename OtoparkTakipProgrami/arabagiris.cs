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
    public partial class arabagiris : Form
    {
        anamenu amenu;
        public arabagiris(Form gelen)
        {
            amenu = (gelen as anamenu);
            InitializeComponent();
        }
        public arabagiris()
        {
            InitializeComponent();
        }

        //Veritabanı Bağlantısı
        /*Veri tabanı yolunu belirterek baglan degişkenime veri tabanımı tanıttım.*/
        OleDbConnection baglan = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Otopark_db.mdb");
        //Veritabanı *SON

        private void arabagiris_Load(object sender, EventArgs e)
        {
            label2.Text = amenu.k;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            amenu.yenileme = 1;
            this.Close();
          
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
                OleDbCommand cmd = new OleDbCommand("insert into giris_yapanlar(ad,soyad,tel,posta,plaka,model,renk,parkalani,tarih_saat) values('" + textBox1.Text + "','" + textBox2.Text + "','" + maskedTextBox1.Text + "','" + textBox4.Text + "','" + textBox3.Text + "','" + textBox6.Text + "','" + comboBox1.Text + "','"+ amenu.k + "','"+DateTime.Now.ToString()+"')", baglan);
                cmd.ExecuteNonQuery();
                baglan.Close();
                amenu.yenileme = 1;
                alankontrol();
                this.Close();
               
            }
            catch (Exception)
            {


            }
            //VERİTABANINA ADMİN EKLEME *SON
        }
        public void alankontrol()
        {
          /*
          giriş yaptıktan sonra park alanımda ilgili bölgenin dolu olduğunu göstermek için kontrol hücremi güncelleyerek 0 yaptım.
           
          */
            baglan.Open();
            OleDbCommand cmd = new OleDbCommand("update park_alanlari set kontrol='1' where alan='"+ amenu.k + "' ",baglan);
            cmd.ExecuteNonQuery();
            baglan.Close();
        }
        
    }
}
