using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;


namespace image
{
    class Program
    {
        static void Main(string[] args)
        {
            Image i = Image.FromFile(args[1]);
            Bitmap img = new Bitmap(i);
            switch(args[0]){
                case "1": takeBlue(img);
                    break;
                case "2": greyScale(img);
                    break;
                case "3": swapColor(img);
                    break;
            }

            Console.Write("DONE");
            Console.ReadLine();
            
        }

        static public void greyScale(Bitmap img){
            Color c;
            int greyval;
            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    greyval = (img.GetPixel(x, y).G + img.GetPixel(x, y).G + img.GetPixel(x, y).B) / 3;
                    c = Color.FromArgb(greyval,greyval,greyval);
                    img.SetPixel(x, y, c);
                }
            }
            img.Save("grey.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        static public void swapColor(Bitmap img){
            Color c;
            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    c = Color.FromArgb(img.GetPixel(x, y).G,img.GetPixel(x, y).R,img.GetPixel(x, y).B);
                    img.SetPixel(x, y, c);
                }
            }
            img.Save("swap.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        static public void takeBlue(Bitmap img){
            Color c;
            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    c = Color.FromArgb(img.GetPixel(x, y).R,img.GetPixel(x, y).G,0);
                    img.SetPixel(x, y, c);
                }
            }
            img.Save("takeblue.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
        }
        
        
    }
}