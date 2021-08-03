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

namespace Seçim_istatiktik_ve_Grafik_Sistemi
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection baglantı = new SqlConnection(@"Data Source=DESKTOP-M4Q4SMD\SQLEXPRESS;Initial Catalog=secimsonuc;Integrated Security=True");

        private void Form2_Load(object sender, EventArgs e)
        {
            baglantı.Open();
            //COMBOBOXA İLÇELERİ GETİRME
            SqlCommand komut = new SqlCommand("Select ILCEAD from tbl_ılce",baglantı);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                comboBox1.Items.Add(dr[0]);
            }
            baglantı.Close();
            //GRAFİĞE  TOPLAM SONUÇLARI GETİRME 
            baglantı.Open();
            SqlCommand komut2 = new SqlCommand("Select sum(APARTI),sum(BPARTI),sum(CPARTI),sum(DPARTI),sum(EPARTI) from tbl_ılce",baglantı);
            SqlDataReader dr2 = komut2.ExecuteReader();
            while(dr2.Read())
            {
                chart1.Series["PARTİLER"].Points.AddXY("A PARTİ", dr2[0]);
                chart1.Series["PARTİLER"].Points.AddXY("B PARTİ", dr2[1]);
                chart1.Series["PARTİLER"].Points.AddXY("C PARTİ", dr2[2]);
                chart1.Series["PARTİLER"].Points.AddXY("D PARTİ", dr2[3]);
                chart1.Series["PARTİLER"].Points.AddXY("E PARTİ", dr2[4]);
                
            }
            baglantı.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand komut3 = new SqlCommand("Select * From tbl_ılce where ILCEAD=@p1",baglantı);
            komut3.Parameters.AddWithValue("@p1",comboBox1.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                progressBar1.Value = int.Parse(dr3[2].ToString());
                progressBar2.Value = int.Parse(dr3[3].ToString());

                progressBar3.Value = int.Parse(dr3[4].ToString());

                progressBar4.Value = int.Parse(dr3[5].ToString());

                progressBar5.Value = int.Parse(dr3[6].ToString());
                label7.Text = dr3[2].ToString();
                label8.Text = dr3[3].ToString();
                label9.Text = dr3[4].ToString();
                label10.Text = dr3[5].ToString();
                label11.Text = dr3[6].ToString();



            }
            baglantı.Close();
        }
    }
}
