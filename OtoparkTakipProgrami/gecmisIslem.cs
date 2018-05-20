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
    public partial class gecmisIslem : Form
    {
        public gecmisIslem()
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
            
        }

        private void gecmisIslem_Load(object sender, EventArgs e)
        {
            baglan.Open();
            DataTable tb = new DataTable();
            OleDbDataAdapter adp = new OleDbDataAdapter("select * from gecmis_islemler",baglan);
            adp.Fill(tb);
            baglan.Close();
            dataGridView1.DataSource = tb;
            dataGridView1.Columns["ad"].HeaderText = "Ad";
            dataGridView1.Columns["soyad"].HeaderText = "Soyad";
            dataGridView1.Columns["plaka"].HeaderText = "Araç Plaka";
            dataGridView1.Columns["fiyat"].HeaderText = "Ücret";
        }
    }
}
