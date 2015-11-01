using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace mse
{
    class program
    {
        static void Main(string[] args)
        {
        	double mse = 0;

            Image i = Image.FromFile(args[0]);
            Bitmap pic1 = new Bitmap(i);
            i = Image.FromFile(args[1]);
            Bitmap pic2 = new Bitmap(i);

            Console.WriteLine(computeMSE(pic1,pic2));
        }
        
        public static int squareDifference(int x1, int x2){
        	return (int)Math.Pow((x2-x1),2);

        }

        public static double computeMSE(Bitmap pic1, Bitmap pic2){
        	int count = 0;
        	int sum = 0;
        	for (int x = 0; x < pic1.Width; x++)
            {
                for (int y = 0; y < pic1.Height; y++)
                {
                	count++;
                	sum += squareDifference(pic2.GetPixel(x,y).R,pic1.GetPixel(x,y).R);
                }
            }
            return sum/count;
        }
    }
}