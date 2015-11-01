using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Program
{
    class Program
    {
        static void Main(){
            string input = "0100001011011011001110100100010001101010";
            Dictionary<string, string> zeichen = new Dictionary<string, string>();
            zeichen.Add("01001","A");
            zeichen.Add("10","F");
            zeichen.Add("01000","I");
            zeichen.Add("0111","M");
            zeichen.Add("0101","N");
            zeichen.Add("11","O");
            zeichen.Add("0110","R");
            zeichen.Add("00","T");
            try{
                Console.WriteLine(decode(input,zeichen));
                 
            }catch(System.ArgumentException e){
                Console.WriteLine(e);
            }
        }
        
        public static string decode(string input,Dictionary<string, string> zeichen){
            string check = "";
            string final = "";
            foreach(Char a in input){
                check += a;
                if(check.Length > 5){
                    throw new System.ArgumentException("The string you entered contains unknown values");
                }
                if(((int)a > 49)||((int)a < 48)){
                    throw new System.ArgumentException("The string you entered contains invalid characters!");   
                }
                if(zeichen.ContainsKey(check)){
                    final += zeichen[check];
                    check = "";
                }
            }
            return final;   
        }
    }
}