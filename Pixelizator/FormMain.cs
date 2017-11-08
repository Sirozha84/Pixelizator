using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pixelizator
{
    public partial class FormMain : Form
    {
        Random RND = new Random();
        int[,] Pattern = { { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                           { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
                           { 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0 },
                           { 1, 0, 1, 0, 0, 1, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1 },
                           { 1, 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 0, 0, 1, 0, 1 },
                           { 1, 0, 1, 1, 0, 1, 0, 1, 1, 1, 1, 0, 0, 1, 0, 1 },
                           { 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1 },
                           { 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1 },//
                           { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } };

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
            Bitmap bmp = new Bitmap(100, 100);
            for (int i = 0; i < 100; i++)
                for (int j = 0; j < 100; j++)
                {
                    if (j < 50)
                        bmp.SetPixel(i, j, color(i, j, i, 1));
                    else
                        bmp.SetPixel(i, j, color(i, j, trackBar1.Value, 1));
                }
            pictureBox1.Image = bmp;
        }

        Color color(int x, int y, int c, int t)
        {
            switch (t)
            {
                case 1:
                    return Pattern[(int)(c / 11.12), x % 4 + y % 4 * 4] == 1 ? Color.White : Color.Black;
                case 2:
                    return c > RND.Next(100) ? Color.White : Color.Black;
                default:
                    return c > 50 ? Color.White : Color.Black;
            }
        }
    }
}
