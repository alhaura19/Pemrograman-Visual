using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StegAES
{
    public partial class MainForm : Form
    {
        private EncodeForm EF;
        private DecodeForm DF;
        private Tes Tes;

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnEncode_Click(object sender, EventArgs e)
        {
            EF = new EncodeForm();
            EF.Show();
            this.Hide();
        }

        private void btnDecode_Click(object sender, EventArgs e)
        {
            DF = new DecodeForm();
            DF.Show();
            this.Hide();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Tes = new Tes();
            Tes.Show();
            this.Hide();
        }

        private void aboutMenu_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Developed By:" + Environment.NewLine + Environment.NewLine + "Latifah Alhaura" + Environment.NewLine + "Meila Kusuma P" + Environment.NewLine + "Ryan Fadholi" + Environment.NewLine + "Tri Kur Aprilianta" + Environment.NewLine + Environment.NewLine + "Teknik Informatika © 2016", "About");
        }

        private void helpMenu_Click(object sender, EventArgs e)
        {
            string[] text = new string[]
         { "StegAES merupakan aplikasi untuk mengamankan pesan rahasia pada gambar digital. "
         , ""
         , "1. Encode"
         , "Pada menu ini akan terjadi proses pengenkripsian data berupa teks "
         , "dan penyembunyian hasil enkripsi teks ke dalam citra digital berformat JPEG,PNG, BMP."
         , ""
         , "2. Decode"
         , "Pada menu ini akan terjadi proses pendekripsian data berupa gambar yang telah memiliki pesan rahasia."
         , ""
         , "3. Help"
         , "Pada menu ini berisi penjelasan singkat mengenai Steg-AES."
         , ""
         , "4. About Us"
         , "Pada menu ini berisi informasi mengenai pengembang dari Steg-AES. "
         };

            show_in_MessageBox_the_lines_of(text);
        }

        private void show_in_MessageBox_the_lines_of(string[] text)
        {
            StringBuilder msg_Builder = new StringBuilder(" " + text[0] + " ");
            for (int i_line = 1; i_line < text.Length; i_line++)
            {
                msg_Builder.Append(Environment.NewLine + " " + text[i_line] + " ");
            }
            MessageBox.Show(msg_Builder.ToString(), "Help");
        }

    }
}