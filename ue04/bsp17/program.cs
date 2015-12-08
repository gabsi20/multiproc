/*
fhs37227 - Gabriel Alexander
fhs37229 - Huber Sebastian
*/

using System;
using System.Drawing;
using System.Collections.Generic;

namespace ue04
{
	class bsp17
	{
		static void Main(string[] args)
		{
			Image input = Image.FromFile(args[0]);
			Bitmap img = new Bitmap(input);

			double[] analow = new double[]{(-1.0/8.0),(2.0/8.0),(6.0/8.0),(2.0/8.0),(-1.0/8.0)};
			double[] anahigh = new double[]{(-1.0/2.0),(1.0),(-1.0/2.0)};
			double[] syntlow = new double[]{(1.0/2.0),(1.0),(1.0/2.0)};
			double[] synthigh = new double[]{(-1.0/8.0),(-2.0/8.0),(6.0/8.0),(-2.0/8.0),(-1.0/8.0)};

			// starting lowpass //
			double[,] red1 = getred(img);
			double[,] green1 = getgreen(img);
			double[,] blue1 = getblue(img);

			red1 = filter(red1, analow); 
			red1 = downsample(red1,1);
			red1 = upsample(red1,1);
			red1 = filter(red1, syntlow);

			blue1 = filter(blue1, analow);
			blue1 = downsample(blue1,1);
			blue1 = upsample(blue1,1);
			blue1 = filter(blue1, syntlow);


			green1 = filter(green1, analow);
			green1 = downsample(green1,1);
			green1 = upsample(green1,1);
			green1 = filter(green1, syntlow);

			// starting highpass //

			double[,] red2 = getred(img);
			double[,] green2 = getgreen(img);
			double[,] blue2 = getblue(img);

			red2 = filter(red2, anahigh);
			red2 = downsample(red2,0);
			red2 = upsample(red2,0);
			red2 = filter(red2, synthigh);


			blue2 = filter(blue2, anahigh);
			blue2 = downsample(blue2,0);
			blue2 = upsample(blue2,0);
			blue2 = filter(blue2, synthigh);


			green2 = filter(green2, anahigh);
			green2 = downsample(green2,0);
			green2 = upsample(green2,0);
			green2 = filter(green2, synthigh);

			combine(red1,red2,green1,green2,blue1,blue2);

		}

		static double[,] getred(Bitmap img)
		{
			double[,] returncolor = new double[img.Width,img.Height];
			for(int x = 0; x < img.Width; x++)
			{
				for(int y = 0; y < img.Height; y++)
				{
					returncolor[x,y] = img.GetPixel(x,y).R;
				}
			}
			return returncolor;
		}
		static double[,] getblue(Bitmap img)
		{
			double[,] returncolor = new double[img.Width,img.Height];
			for(int x = 0; x < img.Width; x++)
			{
				for(int y = 0; y < img.Height; y++)
				{
					returncolor[x,y] = img.GetPixel(x,y).B;
				}
			}
			return returncolor;
		}
		static double[,] getgreen(Bitmap img)
		{
			double[,] returncolor = new double[img.Width,img.Height];
			for(int x = 0; x < img.Width; x++)
			{
				for(int y = 0; y < img.Height; y++)
				{
					returncolor[x,y] = img.GetPixel(x,y).G;
				}
			}
			return returncolor;
		}

		static public void combine(double[,] red1, double[,] red2, double[,] green1, double[,] green2, double[,] blue1, double[,] blue2)
		{
			Bitmap returnimg = new Bitmap(red1.GetLength(0), red1.GetLength(1));
			double r,g,b;
			for(int x = 0; x < red1.GetLength(0); x++)
			{
				for(int y = 0; y < red1.GetLength(1); y++)
				{
					r = red1[x,y] + red2[x,y];
					g = green1[x,y] + green2[x,y];
					b = blue1[x,y] + blue2[x,y];
					returnimg.SetPixel(x,y,Color.FromArgb(Convert.ToInt32(r),Convert.ToInt32(g),Convert.ToInt32(b)));

				}
				
			}			
			returnimg.Save("final.png",System.Drawing.Imaging.ImageFormat.Png);
		}

		static public double[,] upsample(double[,] img, int evenodd)
		{
			double[,] returnimg = new double[img.GetLength(0)*2,img.GetLength(1)];
			for(int x = 0; x < returnimg.GetLength(0); x++)
			{
				for(int y = 0; y < returnimg.GetLength(1); y++)
				{
					if(x % 2 == evenodd)
					{
						returnimg[x,y] = img[Convert.ToInt32(x/2),y];
					}
					else
					{
						returnimg[x,y] = 0;
					}
				}
			}
			return returnimg;
		}

		static public double[,] downsample(double[,] img, int evenodd)
		{
			double[,] returnimg = new double[img.GetLength(0)/2,img.GetLength(1)];
			for(int x = 0; x < returnimg.GetLength(0); x++)
			{
				for(int y = 0; y < returnimg.GetLength(1); y++)
				{
					
						returnimg[x,y] = img[x*2+evenodd,y];
					
				}	
			}
			return returnimg;
		}

		static public double[,] extend(double[,] img)
		{
			double[,] returnimg = new double[img.GetLength(0)+4, img.GetLength(1)];
			for(int y = 0; y < img.GetLength(1); y++)
			{
				returnimg[0,y] = img[2,y];
				returnimg[1,y] = img[1,y];
				returnimg[returnimg.GetLength(0)-2,y] = img[img.GetLength(0)-2,y];
				returnimg[returnimg.GetLength(0)-1,y] = img[img.GetLength(0)-3,y];
			}
			for(int x = 2; x < returnimg.GetLength(0)-2; x++){
				for(int y = 0; y < returnimg.GetLength(1); y++){
					returnimg[x,y] = img[x-2,y];
				}
			}
			return returnimg;
		}

		static public double[,] filter(double[,] img, double[] filter)
		{
			img = extend(img);

			double col, newcol;
			int filtersize = filter.Length / 2;
			double[,] returnimg = new double[img.GetLength(0)-4, img.GetLength(1)];

			for(int x = 2; x < img.GetLength(0)-2; x++)
			{
				for(int y = 0; y < img.GetLength(1); y++)
				{
					newcol = 0;
					for(int j = -filtersize; j <= filtersize; j++)
					{	
			
						col = img[x+j,y] * filter[j+filtersize];
						newcol += col;
					}
			
					returnimg[x-2,y] = newcol;
				}
			}

			return returnimg;
		}

	}
}