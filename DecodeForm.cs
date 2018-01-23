using System;
using System.Drawing;
using System.Windows.Forms;

namespace StegAES
{
    public partial class DecodeForm : Form
    {
        private Bitmap bmp = null;
        private string hiddenText = string.Empty;

        public DecodeForm()
        {
            InitializeComponent();
            btnProses2.Enabled = false;
        }

        private void btnImage2_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_dialog = new OpenFileDialog();
            open_dialog.Filter = "Image Files (*.jpeg; *.png; *.bmp)|*.jpg; *.png; *.bmp";

            if (open_dialog.ShowDialog() == DialogResult.OK)
            {
                imagePictureBox2.Image = Image.FromFile(open_dialog.FileName);
                this.imagePictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                btnProses2.Enabled = true;
            }
        }

        private void btnProses2_Click(object sender, EventArgs e)
        {
            bmp = (Bitmap)imagePictureBox2.Image;

            //System.IO.StreamReader myFile = new System.IO.StreamReader

            string hiddenText = SteganoLSB.extractText(bmp);

            if (decryptCheckBox.Checked)
            {
                try
                {
                    hiddenText = CryptoAES.DecryptStringAES(hiddenText, passwordTextBox2.Text);
                }
                catch
                {
                    MessageBox.Show("Wrong password", "Error");

                    return;
                }
            }

            secretTextBox2.Text = hiddenText;
        }

        private void btnCancel2_Click(object sender, EventArgs e)
        {
            this.Close();
            MainForm MF = new MainForm();
            MF.Show();
        }
    }
}
