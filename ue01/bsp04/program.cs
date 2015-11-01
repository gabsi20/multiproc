using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Program
{
    class Program
    {
        static void Main(){
            Dictionary<char, int> charCount = new Dictionary<char, int>();
            Console.Write("Bitte gib den zu lesenden Dateinamen ein: ");
            string input = ReadFile(Console.ReadLine());
            int totalCount = ComputeDict(charCount, input);
            if(WriteFile(charCount, totalCount)){
                Console.Clear();
                Console.WriteLine("file written!");
            }
            else{
                Console.WriteLine("fehler");   
            }
        }
        
        public static string ReadFile(string filename){
            string input = "";
            try{    
                using(StreamReader reader = new StreamReader(filename)){
                    while (true){
                        string line = reader.ReadLine();
                        if (line == null)
                        {
                        break;
                        }
                        input += line; 
                    }
                }
            }
            catch{
                Console.WriteLine("beim Lesen des Files {0} ist ein fehler aufgetreten");
            }
            return input;
        }
        
        public static bool WriteFile(Dictionary<char, int> charCount, int totalCount){
            try{
                using(StreamWriter writer = new StreamWriter("result.csv")){
                    foreach(KeyValuePair<char, int> item in charCount){
                        writer.WriteLine("{0}\t{1}",(int)item.Key,(float)item.Value/(float)totalCount);
                    }
                }
            }
            catch{
                return false;   
            }
            return true;
        }
        
        public static int ComputeDict(Dictionary<char, int> charCount, string input){
            int totalCount = 0;
            foreach(char a in input){
                if(!charCount.ContainsKey(a)){
                    charCount.Add(a,1);
                    totalCount++;
                }
                else{
                    charCount[a]++;
                    totalCount++;
                }
            }
            return totalCount;
        }
        
    }
}