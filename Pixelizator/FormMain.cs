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
                           { 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1 },
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
            int mode = 1;
            Bitmap bmp = new Bitmap(128, 128);
            for (int i = 0; i < 128; i++)
                for (int j = 0; j < 128; j++)
                {
                    if (j < 64)
                        bmp.SetPixel(i, j, color(i, j, Color.FromArgb(i*2, i*2, i*2), mode));
                    else
                        bmp.SetPixel(i, j, color(i, j, Color.FromArgb(trackBar1.Value, trackBar1.Value, trackBar1.Value), mode));
                }
            pictureBox1.Image = bmp;
        }

        Color color(int x, int y, Color c, int Mode)
        {
            return Color.FromArgb(bin(x, y, c.R, Mode) * 50,
                                  bin(x, y, c.G, Mode) * 50,
                                  bin(x, y, c.B, Mode) * 50);
        }

        int bin(int x, int y, int Bright, int Mode)
        {
            Bright -= 2;
            if (Bright < 0) Bright = 0;
            if (Bright > 249) Bright = 249;
            int subBright = Bright % 50;
            if (subBright > 49) subBright = 49;
            int b = Bright / 50;
            //Console.WriteLine(Bright + " - " + subBright + " - " + b);
            if (Mode == 0)
                b += subBright > 24 ? 1 : 0;
            if (Mode == 1)
                b += Pattern[(int)(subBright / 5.55), x % 4 + y % 4 * 4] == 1 ? 1 : 0;
            if (Mode == 2)
                b += subBright > RND.Next(50) ? 1 : 0;
            //Console.WriteLine("            " + b);
            return b;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //for (int i = 0; i < 256; i++)
              //  bin(i, 0, i, 0);
                //Console.WriteLine(i + " - " + bin(i, 0, i, 1));
        }
    }
}
