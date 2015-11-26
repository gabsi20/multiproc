using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;

namespace lowpass
{
    class program
    {
        static void Main(string[] args)
        {
            
            

            Image i = Image.FromFile(args[0]);
			Bitmap img = new Bitmap(i);

			lowpass(img);
			highpass(img);

			Console.WriteLine("DONE");

			Console.ReadLine();
        }

        static void lowpass(Bitmap img){
        	Bitmap newimg = new Bitmap(img.Width, img.Height);
         	double r,g,b;
         	double valr;
					double valg; 
					double valb;
        	Color c;
			double[] h = new double[]{(-1.0)/8.0,(2.0/8.0),(6.0/8.0),(2.0/8.0),(-1.0)/8.0};
			
			for(int x = 0; x < img.Width; x++){
				for(int y = 0; y < img.Height; y++){
					r = 0;
					g = 0;
					b = 0;

					for(int i = -2; i < 3; i++){
						if((x < 2)||(x > img.Width-3)){
							valr = 0;
							valg = 0;
							valb = 0;
						}
						else{
							valr = (img.GetPixel(x+i,y).R);
							valg = (img.GetPixel(x+i,y).G);
							valb = (img.GetPixel(x+i,y).B);
						}
							r += (valr * h[i+2]);
							g += (valg * h[i+2]);
							b += (valb * h[i+2]);

					}

					if(r < 0) r = 0;
					if(r > 255) r = 255;
					if(g < 0) g = 0;
					if(g > 255) g = 255;
					if(b < 0) b = 0;
					if(b > 255) b = 255;
					c = Color.FromArgb((int)r,(int)g,(int)b);
					newimg.SetPixel(x,y,c);
				}
			}
			newimg.Save("lowpass.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
         	
    	}

    	static void highpass(Bitmap img){
        	Bitmap newimg = new Bitmap(img.Width, img.Height);
         	double r,g,b;
         	double valr;
					double valg; 
					double valb;
        	Color c;
			double[] t = new double[]{(-1.0/2.0),1.0,(-1.0/2.0)};
			foreach(double a in t) Console.WriteLine(a);
			for(int x = 0; x < img.Width; x++){
				for(int y = 0; y < img.Height; y++){
					r = 0;
					g = 0;
					b = 0;

					for(int i = -1; i < 2; i++){
						if((x < 1)||(x > img.Width-2)){
							valr = 0;
							valg = 0;
							valb = 0;
						}
						else{
							valr = (img.GetPixel(x+i,y).R);
							valg = (img.GetPixel(x+i,y).G);
							valb = (img.GetPixel(x+i,y).B);
						}
							r += (valr * t[i+1]);
							g += (valg * t[i+1]);
							b += (valb * t[i+1]);

					}
					r *= r;
					g *= g;
					b *= b;
					if(r < 0) r = 0;
					if(r > 255) r = 255;
					if(g < 0) g = 0;
					if(g > 255) g = 255;
					if(b < 0) b = 0;
					if(b > 255) b = 255;
					c = Color.FromArgb((int)r,(int)g,(int)b);
					newimg.SetPixel(x,y,c);
				}
			}
			newimg.Save("highpass.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
         	
    	}
    }
}