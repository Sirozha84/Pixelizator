using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pixelizator
{
    public partial class FormMain : Form
    {
        Bitmap BMP;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Color256.Mode = 1;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Color256.Mode = 0;
            Convert();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Color256.Mode = 1;
            Convert();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Color256.Mode = 2;
            Convert();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                BMP = (Bitmap)Image.FromFile(dialog.FileName);
                Convert();
            }
        }

        void Convert()
        {
            Bitmap256 BMP256 = new Bitmap256(BMP);
            pictureBox1.Image = BMP256.ToBitmap();
        }
    }
}
