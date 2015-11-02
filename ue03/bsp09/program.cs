using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace mse
{
    class program
    {
        static void Main(string[] args)
        {


            Image i = Image.FromFile(args[0]);
            Bitmap pic = new Bitmap(i);

            if(createCSV(pic)){
                Console.WriteLine("DONE");
            }
            else{
                Console.WriteLine("FAIL");
            }
        }


        public static bool createCSV(Bitmap pic){
        	SortedDictionary<int, int> histogram = new SortedDictionary<int, int>();
            int test = 0;
            for (int x = 0; x < pic.Width; x++)
            {
                for (int y = 0; y < pic.Height; y++)
                {
                    if(!histogram.ContainsKey(pic.GetPixel(x,y).R)){
                        histogram.Add(pic.GetPixel(x,y).R,1);
                        test++;
                    }
                    else{
                       histogram[pic.GetPixel(x,y).R]++;
                    }
                    
                }
            }
            if(WriteFile(histogram)){
                return true;
            }
            else return false;
        }
        public static bool WriteFile(SortedDictionary<int,int> histogram){
            try{
                using(StreamWriter writer = new StreamWriter("out.csv")){
                    foreach(KeyValuePair<int, int> item in histogram){
                        writer.WriteLine("{0}\t",item.Value);
                    }
                }
            }
            catch{
                return false;   
            }
            return true;
        }
    }
}