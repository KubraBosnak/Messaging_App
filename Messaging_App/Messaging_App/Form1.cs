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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=KUBRA-BOSNAK;Initial Catalog=DbMessagingApp;Integrated Security=True");
        private void btnGiris_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT * FROM TBLKISILER WHERE NUMARA=@P1 AND SIFRE=@P2", baglanti);
            komut.Parameters.AddWithValue("@P1", mskNo.Text);
            komut.Parameters.AddWithValue("@P2",txtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                Form2 frm = new Form2();
                frm.numara = mskNo.Text;
                frm.Show();
            }
            else
            {
                MessageBox.Show("Hatalı Bilgi");
            }
            baglanti.Close();
        }
    }
}
