using System.Drawing;

namespace Pixelizator
{
    class Bitmap256
    {
        public int Width;
        public int Height;

        byte[,] M;

        /// <summary>
        /// Конструктор пустого битмапа по размерам
        /// </summary>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        public Bitmap256(int Width, int Height)
        {
            this.Width = Width;
            this.Height = Height;
            M = new byte[Width, Height];
        }

        /// <summary>
        /// Конструктор битмапа, сконвертированного из РГБ-битмапа
        /// </summary>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        public Bitmap256(Bitmap BMP)
        {
            FromBitMap(BMP);
        }

        public void SetPixel(int x, int y, Color color)
        {
            M[x, y] = Color256.FromColor(x, y, color);
        }

        public Bitmap ToBitmap()
        {
            Bitmap BMP = new Bitmap(Width, Height);
            for (int i = 0; i < Width; i++)
                for (int j = 0; j < Height; j++)
                    BMP.SetPixel(i, j, Color256.ColorRGB(M[i, j]));
            return BMP;
        }

        public void FromBitMap(Bitmap BMP)
        {
            Width = BMP.Width;
            Height = BMP.Height;
            M = new byte[Width, Height];
            for (int i = 0; i < Width; i++)
                for (int j = 0; j < Height; j++)
                    M[i, j] = Color256.FromColor(i, j, BMP.GetPixel(i, j));
        }
    }
}
