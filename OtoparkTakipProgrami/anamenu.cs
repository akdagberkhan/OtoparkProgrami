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
    public partial class anamenu : Form
    {
        public anamenu()
        {
            InitializeComponent();
        }

        //Veritabanı Bağlantısı
        /*Veri tabanı yolunu belirterek baglan degişkenime veri tabanımı tanıttım.*/
        OleDbConnection baglan = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Otopark_db.mdb");
        //Veritabanı *SON

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Close();
            
        }

        public int alansayisi;
        DataTable alanstablo = new DataTable();
        public void parkalan()
        {
            //park_alanlari ADLI TABLODAN VERİ ÇEKME
            /* -park_alanlari tablomdaki verileri "alanstablo" adındaki veri tabloma attım.
             * -kaç alanım olduğunu hesaplamak için alansayisi adlı int türndeki değişkenime tablomdaki değerlerin kaç tane olduğunu yazdırdım.
             */
            baglan.Open();
            alanstablo.Rows.Clear();
            OleDbDataAdapter adp = new OleDbDataAdapter("select * from park_alanlari", baglan);
            adp.Fill(alanstablo);
            alansayisi = alanstablo.Rows.Count;
            baglan.Close();
            //park_alanlari ADLI TABLODAN VERİ ÇEKME  *SON

        }

        public int yenileme;
        
        private void menuSizeKontrol_Tick(object sender, EventArgs e)
        {
            //TİMER İLE ALANLARIMIN OLUŞUMUNU KONTROL ETME
            /*
              -Yenileme değişkenim 1 olduğunda veri tabanımdaki kayıtlı olan alan kadar park alanı için picturebox  alan sayısını belirtmek içinde label ürettim.
              -picturebox ların hangi fotoğraf geleceğini park_alanalri tablomdan dönen değer ile kontrol ettim 

             */
            if (yenileme == 1)
            {
                
                flowLayoutPanel1.Controls.Clear();
                
                parkalan();
                for (int i = 0; i < alansayisi; i++)
                {
                    PictureBox pb = new PictureBox();
                    pb.Size = new Size(197, 260);
                    pb.Tag = i + 1;
                    if (alanstablo.Rows[i][1].ToString()=="0") //tablomun belirttiğim hücresindeki değer 0 ise veya 1 ise belirttiğim fotoğrafları ekledim
                    {
                        pb.ImageLocation = "resim/P_yapilabilir.png";

                    }
                    if (alanstablo.Rows[i][1].ToString() == "1")
                    {
                        pb.ImageLocation = "resim/P_yapilamaz.png";

                    }
                    pb.BackColor = Color.Gainsboro;
                    pb.BorderStyle = (BorderStyle.FixedSingle);
                    flowLayoutPanel1.Controls.Add(pb); //flowLayoutPanel1 ' imin içine oluşan pictureboxları ekledim
                    pb.Click += Pb_Click; //Pictureboxlara tıklama eventi verdim

                        Label lbl = new Label();
                        lbl.Font = new Font("Vertical", 12);
                        lbl.Text = "PARK :" + pb.Tag.ToString();
                        pb.Controls.Add(lbl);//oluşan picturebox'larımın içine oluşan labelleri ekledim

                } 
             yenileme = 0; //pictureboxlaar ve labeller oluştuktan sonra birdaha oluşmaması için değişkenimi 0 yaptım
                
                
            }
            //TİMER İLE ALANLARIMIN OLUŞUMUNU KONTROL ETME *SON
        }


        private void anamenu_Load(object sender, EventArgs e)
        {
            yenileme = 1;

        }

        public string k;
        private void Pb_Click(object sender, EventArgs e)
        {
            //Pictureboxlarımın tıklama eventi
            /*
             
             */
            PictureBox gelen = (sender as PictureBox); //bu metod bütün picureboxların event'i olduğu için bana sadece tıklanan nesnein bilgileri lazımdı ve bende 
                                                       //bir tane picturebox değişken oluşturdum ve tıklanan picturebox u o değğişkenin içine attım
            k = gelen.Tag.ToString();  //tıklanan nesnemin tag ' ını k değişkenime attım

            if (gelen.ImageLocation == "resim/P_yapilabilir.png") // park yapılır veya yapılamaz resimlerini nesnemde kontrol ederek ilgili formlara erişim açtım
            {
                arabagiris arbg = new arabagiris(this);
                arbg.ShowDialog();
            }
            if (gelen.ImageLocation == "resim/P_yapilamaz.png")
            {
                arabacikis arbc = new arabacikis(this);
                arbc.ShowDialog();
            }
            //Pictureboxlarımın tıklama eventi *SON

        }

        private void asdasdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            adminIslem admı = new adminIslem();
            admı.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gecmisIslem gı = new gecmisIslem();
            gı.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            aboneislem abni = new aboneislem();
            abni.ShowDialog();
        }

        int topkazanc;
        private void button3_Click(object sender, EventArgs e)
        {
            baglan.Open();
            DataTable kazanctablo = new DataTable();
            kazanctablo.Rows.Clear();
            topkazanc = 0;
            OleDbDataAdapter adp = new OleDbDataAdapter("select * from gecmis_islemler", baglan);
            adp.Fill(kazanctablo);
            baglan.Close();
            for (int i = 0; i < kazanctablo.Rows.Count; i++)
            {
                topkazanc +=Convert.ToInt32(kazanctablo.Rows[i][3]);
            }
            MessageBox.Show("Toplam Kazancınız :"+topkazanc.ToString()+" TL");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AlanFiyat aln = new AlanFiyat(this);
            aln.ShowDialog();
        }
    }
}
