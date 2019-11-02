using System;
using System.Drawing;

namespace Pixelizator
{
    static class Color256
    {
        static int Rgrades = 8;
        static int Ggrades = 8;
        static int Bgrades = 4;


        static Color[] Palette;

        static int[,] Pattern = { { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                           { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
                           { 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0 },
                           { 1, 0, 1, 0, 0, 1, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1 },
                           { 1, 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 0, 0, 1, 0, 1 },
                           { 1, 0, 1, 1, 0, 1, 0, 1, 1, 1, 1, 0, 0, 1, 0, 1 },
                           { 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1 },
                           { 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1 },
                           { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } };

        static Random RND = new Random();

        static int PixelisationMode;

        public static int Mode
        {
            set { PixelisationMode = value; }
        }

        public static byte FromColor(int x, int y, Color c)
        {
            //return (byte)(bin(x, y, c.R, Rgrades) + bin(x, y, c.G, Ggrades) * 6 + bin(x, y, c.B, Bgrades) * 36);
            return (byte)(bin(x, y, c.R, Rgrades) + bin(x, y, c.G, Ggrades) * Rgrades + bin(x, y, c.B, Bgrades) * Rgrades * Ggrades);
        }

        static byte bin(int x, int y, int Bright, int grades)
        {
            int max = (256 / grades) * grades; //на 6 градаций - 249
            int bound = (256 - max) / 2;
            max = 256 - bound;
            int subGrades = 255 / (grades - 1);
            Bright -= bound;
            if (Bright < 0) Bright = 0;
            if (Bright > max) Bright = max;
            int subBright = Bright % subGrades;
            byte b = (byte)(Bright / subGrades);
            int patStep = subGrades / 8;
            //if (patStep > 8) patStep = 8;
            if (PixelisationMode == 0)
                b += (byte)(subBright > subGrades / 2 ? 1 : 0);
            if (PixelisationMode == 1)
                b += (byte)(Pattern[(int)(subBright / patStep), x % 4 + y % 4 * 4] == 1 ? 1 : 0);
            if (PixelisationMode == 2)
                b += (byte)(subBright > RND.Next(subGrades - 1) ? 1 : 0);
            return b;
        }

        public static Color ColorRGB(int Color256)
        {
            if (Palette == null)
            {
                Palette = new Color[256];
                int i = 0;
                int RsubGrades = 255 / (Rgrades - 1);
                int GsubGrades = 255 / (Ggrades - 1);
                int BsubGrades = 255 / (Bgrades - 1);
                for (int b = 0; b < Bgrades; b++)
                    for (int g = 0; g < Ggrades; g++)
                        for (int r = 0; r < Rgrades; r++)
                            Palette[i++] = Color.FromArgb(r * RsubGrades, g * GsubGrades, b * BsubGrades);
            }
            return Palette[Color256];
        }
    }
}
