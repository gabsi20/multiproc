using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace program
{
    class program 
    {
        static void Main(){
            Console.Clear();
            Console.Write("Bitte gib den Input-Filename an: ");
            Console.WriteLine("\nEntropy: "+ReadnCompute(Console.ReadLine())); 
        }
        
        public static double ReadnCompute(string filename){
            double number = 0;
            double final = 0;
            try{    
                using(StreamReader reader = new StreamReader(filename)){
                    while (true){
                        string line = reader.ReadLine();
                        if (line == null)
                        {
                        break;
                        }
                        number = Convert.ToDouble(line.Split('\t')[1]); 
                        final += number*Math.Log(1/number,2);
                    }
                }
            }
            catch{
                Console.WriteLine("beim Lesen des Files {0} ist ein fehler aufgetreten", filename);
            }
            return final;
        }
        
    }
    
}