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

namespace Messaging_App
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public string numara;

        SqlConnection baglanti = new SqlConnection(@"Data Source=KUBRA-BOSNAK;Initial Catalog=DbMessagingApp;Integrated Security=True");

        void GelenKutusu()
        {
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM TBLMESAJLAR WHERE ALICI=" + numara, baglanti);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;
        }
        void GidenKutusu()
        {
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT * FROM TBLMESAJLAR WHERE GONDEREN=" + numara, baglanti);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            lblNumara.Text = numara;
            GelenKutusu();
            GidenKutusu();

            //ADI VE SOYADI LABEL'E ÇEKME
            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT AD, SOYAD FROM TBLKISILER WHERE NUMARA=" + numara, baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblAdSoyad.Text = dr[0] + " " + dr[1];
            }
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("INSERT INTO TBLMESAJLAR (GONDEREN,ALICI,BASLIK,ICERIK) VALUES (@P1,@P2,@P3,@P4)", baglanti);
            komut.Parameters.AddWithValue("@P1", numara);
            komut.Parameters.AddWithValue("@P2", mskAlici.Text);
            komut.Parameters.AddWithValue("@P3", txtBaslik.Text);
            komut.Parameters.AddWithValue("@P4", richIcerik.Text);
            komut.ExecuteNonQuery(); // İşlemi gerçekleştir.
            baglanti.Close();
            MessageBox.Show("Mesajınız İletildi");
            GidenKutusu();


        }
    }
}
