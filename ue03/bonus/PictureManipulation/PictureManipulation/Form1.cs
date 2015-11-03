using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureManipulation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }
        static public Bitmap clamp(Bitmap img)
        {
            Color c;
            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    Byte r = img.GetPixel(x, y).R;
                    if (r < 75)
                    {
                        r = 75;
                    }
                    if (r > 180)
                    {
                        r = 180;
                    }
                    c = Color.FromArgb(r, img.GetPixel(x, y).G, img.GetPixel(x, y).B);
                    img.SetPixel(x, y, c);
                }
            }
            return img;
        }

        static public Bitmap invert(Bitmap img)
        {
            Color c;
            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    Byte r = img.GetPixel(x, y).R;
                    c = Color.FromArgb(255 - r, img.GetPixel(x, y).G, img.GetPixel(x, y).B);
                    img.SetPixel(x, y, c);
                }
            }
            return img;
        }

        static Bitmap multiply(Bitmap img)
        {
            Color c;
            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    int r = img.GetPixel(x, y).R * 4;
                    if (r < 2)
                    {
                        r = 75;
                    }
                    if (r > 253)
                    {
                        r = 180;
                    }
                    c = Color.FromArgb(r, img.GetPixel(x, y).G, img.GetPixel(x, y).B);
                    img.SetPixel(x, y, c);
                }
            }
            return img;
        }

        static Bitmap uniquant(Bitmap img)
        {
            Color c;
            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    int r = img.GetPixel(x, y).R / 32;
                    r *= 16;
                    int g = img.GetPixel(x, y).G / 32;
                    g *= 16;
                    int b = img.GetPixel(x, y).B / 32;
                    b *= 16;
                    c = Color.FromArgb(r, g, b);
                    img.SetPixel(x, y, c);
                }
            }
            return img;
        }

        static public Bitmap threshold(Bitmap img)
        {
            Color c;
            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    Byte r = img.GetPixel(x, y).R;
                    if (r < 128) r = 0; else r = 255;
                    c = Color.FromArgb(r, img.GetPixel(x, y).G, img.GetPixel(x, y).B);
                    img.SetPixel(x, y, c);
                }
            }
            return img;
        }

        static public Bitmap greyScale(Bitmap img)
        {
            Color c;
            int greyval;
            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    greyval = (img.GetPixel(x, y).G + img.GetPixel(x, y).G + img.GetPixel(x, y).B) / 3;
                    c = Color.FromArgb(greyval, greyval, greyval);
                    img.SetPixel(x, y, c);
                }
            }
            return img;
        }

        static public Bitmap swapColor(Bitmap img)
        {
            Color c;
            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    c = Color.FromArgb(img.GetPixel(x, y).G, img.GetPixel(x, y).R, img.GetPixel(x, y).B);
                    img.SetPixel(x, y, c);
                }
            }
            return img;
        }

        static public Bitmap takeBlue(Bitmap img)
        {
            Color c;
            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    c = Color.FromArgb(img.GetPixel(x, y).R, img.GetPixel(x, y).G, 0);
                    img.SetPixel(x, y, c);
                }
            }
            return img;
        }

        static Bitmap sobelx(Bitmap img)
        {
            Bitmap newimg = new Bitmap(img.Width, img.Height);
            int r;
            Color c;
            int[,] sobelmatrix = {
				{(-1),0,1},
				{(-2),0,2},
				{(-1),0,1}};
            for (int x = 1; x < img.Width - 1; x++)
            {
                for (int y = 1; y < img.Height - 1; y++)
                {
                    r = 0;
                    for (int i = -1; i < 2; i++)
                    {
                        for (int j = -1; j < 2; j++)
                        {
                            r += (img.GetPixel(x + i, y + j).R * sobelmatrix[i + 1, j + 1]);
                        }
                    }
                    r = (int)Math.Sqrt(r * r);
                    if (r > 230) r = 255;
                    if (r < 50) r = 0;
                    c = Color.FromArgb(r, r, r);
                    newimg.SetPixel(x, y, c);
                }
            }
            return newimg;
        }

        static Bitmap sobely(Bitmap img)
        {
            Bitmap newimg = new Bitmap(img.Width, img.Height);
            int r;
            Color c;
            int[,] sobelmatrix = {
				{(-1),(-2),(-1)},
				{0,0,0},
				{1,2,1}};
            for (int x = 1; x < img.Width - 1; x++)
            {
                for (int y = 1; y < img.Height - 1; y++)
                {
                    r = 0;
                    for (int i = -1; i < 2; i++)
                    {
                        for (int j = -1; j < 2; j++)
                        {
                            r += (img.GetPixel(x + i, y + j).R * sobelmatrix[i + 1, j + 1]);
                        }
                    }
                    r = (int)Math.Sqrt(r * r);
                    if (r > 230) r = 255;
                    if (r < 50) r = 0;
                    c = Color.FromArgb(r, r, r);
                    newimg.SetPixel(x, y, c);
                }
            }
            return newimg;
        }

        static Bitmap blur(Bitmap img)
        {

            Bitmap newimg = new Bitmap(img.Width, img.Height);
            int r, g, b, innerx, innerX, innery, innerY;
            Color c;

            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    innerx = x - 3;
                    innerX = x + 3;
                    innery = y - 3;
                    innerY = y + 3;
                    if (x < 3)
                    {
                        innerx = 0;
                    }
                    else if (x > img.Width - 3)
                    {
                        innerX = img.Width;
                    }
                    if (y < 3)
                    {
                        innery = 0;
                    }
                    else if (y > img.Height - 3)
                    {
                        innerY = img.Height;
                    }
                    r = 0;
                    g = 0;
                    b = 0;
                    for (int i = innerx; i < innerX; i++)
                    {
                        for (int j = innery; j < innerY; j++)
                        {
                            r += img.GetPixel(i, j).R;
                            g += img.GetPixel(i, j).G;
                            b += img.GetPixel(i, j).B;
                        }
                    }
                    c = Color.FromArgb(r / 49, g / 49, b / 49);
                    newimg.SetPixel(x, y, c);

                }
            }
            return newimg;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            Image a = Image.FromFile(openFileDialog1.FileName);
            pictureBox1.Image = a;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int what = comboBox1.SelectedIndex;
            Bitmap toBeProcessed = new Bitmap(pictureBox1.Image);
            
            switch (what)
            {
                case 0: pictureBox1.Image = invert(toBeProcessed);
                    break;
                case 1: pictureBox1.Image = clamp(toBeProcessed);
                    break;
                case 2: pictureBox1.Image = multiply(toBeProcessed);
                    break;
                case 3: pictureBox1.Image = uniquant(toBeProcessed);
                    break;
                case 4: pictureBox1.Image = threshold(toBeProcessed);
                    break;
                case 5: pictureBox1.Image = takeBlue(toBeProcessed);
                    break;
                case 6: pictureBox1.Image = greyScale(toBeProcessed);
                    break;
                case 7: pictureBox1.Image = swapColor(toBeProcessed);
                    break;
                case 8: pictureBox1.Image = blur(toBeProcessed);
                    break;
                case 9: pictureBox1.Image = sobelx(toBeProcessed);
                    break;
                case 10: pictureBox1.Image = sobely(toBeProcessed);
                    break;
                default: Console.Write("nothing");
                    break;
            }
           
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            pictureBox1.Image.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }
    }
}
