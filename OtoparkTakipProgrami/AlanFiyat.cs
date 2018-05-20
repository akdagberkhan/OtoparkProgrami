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
    public partial class AlanFiyat : Form
    {
        anamenu ana;
        public AlanFiyat(Form gelen)
        {
            ana = (gelen as anamenu);
            InitializeComponent();
        }
        public AlanFiyat()
        {
            InitializeComponent();
        }


        //Veritabanı Bağlantısı
        /*Veri tabanı yolunu belirterek baglan degişkenime veri tabanımı tanıttım.*/
        OleDbConnection baglan = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Otopark_db.mdb");
        //Veritabanı *SON

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ana.yenileme = 1;
            this.Close();

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.Checked = false;
            panel4.Enabled = false;
            panel3.Enabled = true;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            panel4.Enabled = true;
            panel3.Enabled = false;
        }

        private void AlanFiyat_Load(object sender, EventArgs e)
        {
            panel4.Enabled = false;
            panel3.Enabled = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // ALAN SAYISI VE PARK ALANLARI AYARLARI
            /*
             - "ekle" metodumu çağırdım bu metotda : park_alanlari tablomdaki park alanlarını tabloya attım ve tabloda kaç tane park alanı olduğunu "top" değişkenine attım
             - "ustunekele" metodumu çağırdım bu metotda : alan ekledikten sonra oluşan güncel alan sayımı aldım "guncelalan" değişkenine attım
             - oluşan güncel alanımı fiyat_alan tablomdaki alan stunuma ekledim
             - en son park_alanlari tabloma güncel sayım kadar alan ekeldim
             */
            try
            {
                ekle();
                ustunekele();
                baglan.Open();
                OleDbCommand cmd = new OleDbCommand("update fiyat_alan set alan='" + guncelalan.ToString() + "'", baglan);
                cmd.ExecuteNonQuery();
                baglan.Close();
                for (int i = 0; i < Convert.ToInt16(textBox3.Text); i++)
                {
                    top += 1;
                    baglan.Open();
                    OleDbCommand cmd2 = new OleDbCommand("insert into park_alanlari(alan,kontrol) values('" + top + "','0')", baglan);
                    cmd2.ExecuteNonQuery();
                    baglan.Close();
                }
                MessageBox.Show("İşlem Başarılı");
            }
            catch (Exception)
            {
                MessageBox.Show("Bir Hata Oluştu");

            }
            // ALAN SAYISI VE PARK ALANLARI AYARLARI  *SON

        }
        int guncelalan;
        void ustunekele()
        {
            baglan.Open();
            DataTable tb = new DataTable();
            OleDbDataAdapter adp = new OleDbDataAdapter("select alan from fiyat_alan",baglan);
            adp.Fill(tb);
            baglan.Close();
            guncelalan =Convert.ToInt16(tb.Rows[0][0])+ Convert.ToInt16(textBox3.Text);

        }
        int top;
        void ekle()
        {
            baglan.Open();
            DataTable tb = new DataTable();
            OleDbDataAdapter adp = new OleDbDataAdapter("select alan from park_alanlari", baglan);
            adp.Fill(tb);
            baglan.Close();
            top = tb.Rows.Count;
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // YENİ ALAN SAYISI
            /*
             girilen yeni alan sayımı tablomdaki alan sayıma ekledim
             park_alanlari tablomu temizledim
             park_alanlari tabloma yeni girilen alan sayımı ekledim
             */
            try
            {
                baglan.Open();
                OleDbCommand cmd = new OleDbCommand("update fiyat_alan set alan='" + textBox2.Text + "'", baglan);
                cmd.ExecuteNonQuery();
                OleDbCommand cmd2 = new OleDbCommand("DELETE FROM park_alanlari", baglan);
                cmd2.ExecuteNonQuery();
                OleDbCommand cmd4 = new OleDbCommand("DELETE FROM giris_yapanlar", baglan);
                cmd4.ExecuteNonQuery();
                baglan.Close();
                
                for (int i = 0; i < Convert.ToInt16(textBox2.Text); i++)
                {
                    
                    baglan.Open();
                    OleDbCommand cmd3 = new OleDbCommand("insert into park_alanlari(alan,kontrol) values('" +(i+1)+"','0')", baglan);
                    cmd3.ExecuteNonQuery();
                    baglan.Close();
                }
                MessageBox.Show("İşlem Başarılı");
            }
            catch (Exception)
            {
                MessageBox.Show("Bir Hata Oluştu");

            }
            // YENİ ALAN SAYISI  *SON

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // FİYAT
            /*
             girilen fiyatımı tablomdaki ilgili hücreme ekledim
             */
            try
            {
                baglan.Open();
                OleDbCommand cmd = new OleDbCommand("update fiyat_alan set fiyat='" + textBox1.Text + "'", baglan);
                cmd.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("İşlem Başarılı");
            }
            catch (Exception)
            {
                MessageBox.Show("Bir Hata Oluştu");

            }
            // FİYAT  *SON

        }

    }
}
