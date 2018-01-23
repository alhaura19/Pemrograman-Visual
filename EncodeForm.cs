using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace StegAES
{
    public partial class EncodeForm : Form
    {
        private Bitmap bmp = null;
        private Bitmap stego = null;
        private string hiddenText = string.Empty;

        public EncodeForm()
        {
            InitializeComponent();
            //btnProses1.Enabled = false;
            btnSimpan1.Enabled = false;
        }

        private void btnImage1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_dialog = new OpenFileDialog();
            open_dialog.Filter = "Image Files (*.jpeg; *.png; *.bmp)|*.jpg; *.png; *.bmp";

            if (open_dialog.ShowDialog() == DialogResult.OK)
            {
                bmp = (Bitmap)Image.FromFile(open_dialog.FileName);
                imagePictureBox.Image = bmp;
                this.imagePictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                btnProses1.Enabled = true;
            }
        }

        private void btnProses1_Click(object sender, EventArgs e)
        {

            string text = secretTextBox.Text;

            if (text.Equals(""))
            {
                MessageBox.Show("The text you want to hide can't be empty", "Warning");

                return;
            }

            if (encryptCheckBox.Checked)
            {
                if (passwordTextBox.Text.Length < 6)
                {
                    MessageBox.Show("Please enter a password with at least 6 characters", "Warning");

                    return;
                }
                else
                {
                    //MessageBox.Show("Password Accepted");
                    text = CryptoAES.EncryptStringAES(text, passwordTextBox.Text);
                }
            }
            stego = SteganoLSB.embedText(text, bmp);
            pictureBox1.Image = stego;

            MessageBox.Show("Message has been hidden successfully!", "Success");
            btnSimpan1.Enabled = true;
        }

        private void btnSimpan1_Click(object sender, EventArgs e)
        {
            SaveFileDialog save_dialog = new SaveFileDialog();
            save_dialog.Filter = "Png Image|*.png|Bitmap Image|*.bmp";

            if (save_dialog.ShowDialog() == DialogResult.OK)
            {
                switch (save_dialog.FilterIndex)
                {
                    case 0:
                        {
                            bmp.Save(save_dialog.FileName, ImageFormat.Png);
                        }
                        break;
                    case 1:
                        {
                            bmp.Save(save_dialog.FileName, ImageFormat.Bmp);
                        }
                        break;
                }
                MessageBox.Show("File has been saved!", "Done");
            }
        }

        private void btnCancel1_Click(object sender, EventArgs e)
        {
            this.Close();
            MainForm MF = new MainForm();
            MF.Show();
        }
    }
}
