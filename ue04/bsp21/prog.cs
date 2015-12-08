/*
fhs37227 - Gabriel Alexander
fhs37229 - Huber Sebastian
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ue04
{
    class bsp21
    {
        static void Main(string[] args)
        {
        	
            Image i = Image.FromFile(args[0]);
			Bitmap img = new Bitmap(i);

			dilate(img);			
        }

        static int[] findPivot(int[,] piv){
        	int[] result = new int[2];
        	for(int i = 0; i < 3; i++){
				for(int j = 0; j < 3; j++){
					if(piv[i,j] == 2){
						result[0] = i;
						result[1] = j;
					}
				}
			}
			return result;
        }

        public static Bitmap toBW(Bitmap img) {
			Bitmap outputImg = new Bitmap (img.Width, img.Height);
			int temp;
			int color;
			for (int i = 0; i < img.Width; i++) {
				for (int j = 0; j < img.Height; j++) {
					Color temPixel = img.GetPixel (i,j);
					temp = (temPixel.R + temPixel.G + temPixel.B) / 3;
					if (temp > 128) {
						color = 255;
					} else {
						color = 0;
					}
					outputImg.SetPixel (i,j, Color.FromArgb(255, color, color, color));
				}
			}
			return outputImg;
		}

		static void dilate(Bitmap img){
			img = toBW(img);
			img.Save("bw.png", System.Drawing.Imaging.ImageFormat.Png);
			int temp;
			int[,] helper = new int[img.Width,img.Height];
			int[,] pivotMatrix = {
				
				{2,1,0},
				{1,0,0},
				{0,0,0}

			};
			int[] piv = findPivot(pivotMatrix);

			Color black = Color.FromArgb(0,0,0);
			Color white = Color.FromArgb(255,255,255);

			Bitmap newimg = new Bitmap(img.Width,img.Height);

			for(int y = 2; y < img.Width-2; y++){
				for(int x = 2; x < img.Height-2; x++){
					if(img.GetPixel(x,y).R == 0){
						for(int a = 0; a < 3; a++){
							for(int b = 0; b < 3; b++){
								temp = img.GetPixel(x-piv[0]+a,y-piv[1]+b).R + pivotMatrix[a,b];
								helper[x-piv[0]+a,y-piv[1]+b] += temp;
							}

						}

						
					}
					
				}

			}
			for(int y = 0; y < helper.GetLength(0)-1; y++){
				for(int x = 1; x < helper.GetLength(1)-1; x++){
					if(helper[x,y] == 0){
						newimg.SetPixel(x, y, white);
					}
					else{
						newimg.SetPixel(x, y, black);
					}
				}
			}
			newimg.Save("dilate.png", System.Drawing.Imaging.ImageFormat.Png);
		}
    }
}