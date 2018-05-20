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
    public partial class arabacikis : Form
    {
        anamenu amenu;
        public arabacikis(Form gelen)
        {
            amenu = (gelen as anamenu);
            InitializeComponent();
        }
        public arabacikis()
        {
            InitializeComponent();
        }

        //Veritabanı Bağlantısı
        /*Veri tabanı yolunu belirterek baglan degişkenime veri tabanımı tanıttım.*/
        OleDbConnection baglan = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Otopark_db.mdb");
        //Veritabanı *SON

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            amenu.yenileme = 1;
            this.Close();
            
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //EKLE BUTONU
            /*
             butonuma basılınca gerekli olan metodları sırası ile çalıştırdım
             */
            try
            {
                gecmis();
                alankontrol();
                giriskontrol();
                amenu.yenileme = 1;
                this.Close();

            }
            catch (Exception)
            {

                MessageBox.Show("Bir Hata Oluştu");
            }
            //EKLE BUTONU *SON

        }
        public void alankontrol()
        {
            /*
             araba giriş yaptığında tablomdaki ilgili hücreyi 1 olarak ayarlandığı için dolu gözüküyordu ve bende o alanı boşalmak için gelen alanı boşaltmak için
             kontrol hücremi tekrardan 0 yaptım
             */
            baglan.Open();
            OleDbCommand cmd = new OleDbCommand("update park_alanlari set kontrol='0' where alan='" + amenu.k + "' ", baglan);
            cmd.ExecuteNonQuery();
            baglan.Close();
        }
        public void giriskontrol()
        {
            /*
             park alanında(giris_yapanlar tablosu) gözüken aracı çıkış yaptıktan sonra park alanından (giris_yapanlar tablosu) sildim.
             */
            baglan.Open();
            OleDbCommand cmd = new OleDbCommand("delete from giris_yapanlar where parkalani='" + amenu.k + "' ", baglan);
            cmd.ExecuteNonQuery();
            baglan.Close();
        }
        public void gecmis()
        {
            /*
             çıkış yapan aracın sahibinin adı,soyadı,araç plakası ve ne kadar ücret aldığımı gecmis_islemler tabloma aktardım
             */
            baglan.Open();
            OleDbCommand cmd = new OleDbCommand("insert into gecmis_islemler(ad,soyad,plaka,fiyat) values('"+ tablo.Rows[0][0].ToString() + "','" + tablo.Rows[0][1].ToString() + "','" + textBox1.Text + "','" + textBox2.Text + "') ", baglan);
            cmd.ExecuteNonQuery();
            baglan.Close();
        }
        DataTable tablo = new DataTable();
        public void parkalan()
        {
            /*
             park alanımda olan araçların tüm bilgilerini tablo adlı veritabloma aktardım
             */
            baglan.Open();
            tablo.Rows.Clear();
            OleDbDataAdapter adp = new OleDbDataAdapter("select * from giris_yapanlar where parkalani='"+ amenu.k +"'", baglan);
            adp.Fill(tablo);
            baglan.Close();

        }

        private void arabacikis_Load(object sender, EventArgs e)
        {
            /*
             veritablomdaki bilgileri yazması gereken label ve textboxlara yazdırdım
             */
            try
            {
                fiyatal();
                parkalan();
                label10.Text = tablo.Rows[0][0].ToString() + " " + tablo.Rows[0][1].ToString();
                label11.Text = tablo.Rows[0][3].ToString();
                label12.Text = tablo.Rows[0][2].ToString();
                label14.Text = tablo.Rows[0][6].ToString();
                label15.Text = tablo.Rows[0][7].ToString();
                textBox1.Text = tablo.Rows[0][5].ToString();
                textBox1.Enabled = false;
                ucret();
                textBox3.Text = topsaat.ToString();
                textBox3.Enabled = false;
                textBox2.Text = fiyat.ToString();
                textBox2.Enabled = false;
                
            }
            catch (Exception)
            {
                MessageBox.Show("Bir Hata Oluştu");
            }
            
            
        }


        public static int fiyat, topsaat;
       
        public void ucret()
        {
            /*
             ücreti hesaplamak için aracın giriş saati ve şuanki çıkış saatini alarak 
             hersaat başı 10 tl olarak bir fiyat belirlettim
             -eğer çıkış yapan aracın aboneliği varsa fiyatı yarıya düşürdüm.
             */
            try
            {
                abone();
                DateTime dtpcikis = DateTime.Now;
                DateTime dtpgiris = Convert.ToDateTime(tablo.Rows[0][9]);

                System.TimeSpan zaman;
                zaman = dtpcikis.Subtract(dtpgiris);
                topsaat = Convert.ToInt32(zaman.TotalHours);
                if (abonek.Rows.Count <= 0)
                {
                    fiyat = topsaat * guncelfiyat;
                    label13.Text = "YOK";
                    label17.Text = "İndirim Yapılamdı.";
                }
                if (abonek.Rows.Count > 0)
                {
                    fiyat = (topsaat * guncelfiyat) / 2;
                    label13.Text = "VAR";
                    label17.Text = "İndirim Yapıldı.";
                }
            }
            catch (Exception)
            {

                
            }
            
        }
        DataTable abonek = new DataTable();
        public void abone()
        {
            /*
             çıkış yapan aracın plakasında göre bir abonelik varsa tablomdan abone bilgilerini veritabloma attım.
             */
            try
            {
                baglan.Open();
                OleDbDataAdapter adp = new OleDbDataAdapter("select * from aboneler where plaka='" + tablo.Rows[0][5].ToString() + "'", baglan);
                adp.Fill(abonek);
                baglan.Close();
            }
            catch (Exception)
            {

                
            }
            
        }
        int guncelfiyat;
        void fiyatal()
        {
            /*
             Admin in belirlediği güncel ücreti veri tabanımdan alıp değişkenime attım
             */
            try
            {
                DataTable tb = new DataTable();
                baglan.Open();
                OleDbDataAdapter adp = new OleDbDataAdapter("select fiyat from fiyat_alan", baglan);
                adp.Fill(tb);
                baglan.Close();
                guncelfiyat =Convert.ToInt16(tb.Rows[0][0]);
            }
            catch (Exception)
            {


            }
        }
    }
}
