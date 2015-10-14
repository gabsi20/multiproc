using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Program
{
    class Program
    {
        static void Main()
        {
            Console.Write("Bitte Zahl eingeben: ");
            string number = Console.ReadLine();
            
            Console.Write("Bitte die Basis der Zahl eingeben: ");
            string oldbase = Console.ReadLine();
            
            Console.Write("Bitte die neue Basis eingeben: ");
            string newbase = Console.ReadLine();
            
            Console.WriteLine("Das Ergebnis: {0}!",baseCalc(number,oldbase,newbase));
        }
        
        
        public static string baseCalc(string number, string oldbase, string newbase){
            
            double dezi = 0;
            string final = "";
            

            for (int i = 0; i < number.Length; i++){
                char cur = number[number.Length-1-i];
                dezi += Char.GetNumericValue(cur)*Math.Pow(int.Parse(oldbase),i);
            }
            Console.WriteLine(dezi);
            while((int)dezi != 0){
                
                final = (int)dezi % int.Parse(newbase) + final;
                dezi = (int)dezi / int.Parse(newbase);
            }
            
            return final;
        }
    }        
}