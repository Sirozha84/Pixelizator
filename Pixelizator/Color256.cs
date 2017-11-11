using System;
using System.Drawing;

namespace Pixelizator
{
    static class Color256
    {
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
            return (byte)(bin(x, y, c.R) + bin(x, y, c.G) * 6 + bin(x, y, c.B) * 36);
        }

        static byte bin(int x, int y, int Bright)
        {
            Bright -= 2;
            if (Bright < 0) Bright = 0;
            if (Bright > 249) Bright = 249;
            int subBright = Bright % 50;
            byte b = (byte)(Bright / 50);
            if (PixelisationMode == 0)
                b += (byte)(subBright > 24 ? 1 : 0);
            if (PixelisationMode == 1)
                b += (byte)(Pattern[(int)(subBright / 5.55), x % 4 + y % 4 * 4] == 1 ? 1 : 0);
            if (PixelisationMode == 2)
                b += (byte)(subBright > RND.Next(49) ? 1 : 0);
            return b;
        }

        public static Color ColorRGB(int Color256)
        {
            if (Palette == null)
            {
                Palette = new Color[256];
                int i = 0;
                for (int b = 0; b < 6; b++)
                    for (int g = 0; g < 6; g++)
                        for (int r = 0; r < 6; r++)
                            Palette[i++] = Color.FromArgb(r * 50, g * 50, b * 50);
            }
            return Palette[Color256];
        }
    }
}
