using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pixelizator
{
    public partial class FormMain : Form
    {
        

        public FormMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            Bitmap BMP = (Bitmap)Image.FromFile(@"d:\Документы\Света\Фото\9.jpg");
            //Bitmap BMP = (Bitmap)Image.FromFile(@"d:\Документы\Света\Фото\DSC_0407-2.jpg");
            Bitmap256 BMP256 = new Bitmap256(BMP);
            pictureBox1.Image = BMP256.ToBitmap();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Draw();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (!timer1.Enabled) Draw();
        }

        void Draw()
        {
            int mode = 1;
            Bitmap256 bmp = new Bitmap256(128, 128);

            for (int i = 0; i < 128; i++)
                for (int j = 0; j < 128; j++)
                {
                    if (j < 64)
                        bmp.SetPixel(i, j, Color.FromArgb(i*2, i*2, i*2));
                    else
                        bmp.SetPixel(i, j, Color.FromArgb(trackBar1.Value, trackBar2.Value, trackBar3.Value));
                }
            pictureBox1.Image = bmp.ToBitmap();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Color256.Mode = 1;
        }
    }
}
