using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace GalleryLibrary
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        SqlConnection cn;
        SqlCommand cmd;

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Title = "Select Image";
            d.Filter = " (*.jpg;*.png;*.jpeg) | *.jpg;*.png;*.jpeg";
            DialogResult dr = new DialogResult();
            dr = d.ShowDialog();
            if (dr == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(d.FileName);
            }
        }
        public byte[] savePhoto(PictureBox pb)
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pb.Image.RawFormat);
            return ms.GetBuffer();
        }

        //display Image  
        public Image displayImage(byte[] photo)
        {
            MemoryStream ms = new MemoryStream(photo);
            return Image.FromStream(ms);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("insert into Images values(@name,@image)", cn);
            cmd.Parameters.AddWithValue("name", textBox1.Text);
            cmd.Parameters.AddWithValue("image", savePhoto(pictureBox1));
            cmd.ExecuteNonQuery();
            MessageBox.Show("veri tabanına kayıt başarılı", "kaydedildi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Aniventi\source\repos\GalleryLibrary\GalleryLibrary\picture.mdf;Integrated Security=True");
            cn.Open();

        }
    }
}
