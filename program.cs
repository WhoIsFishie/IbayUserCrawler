using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DebugConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            HtmlWeb web = new HtmlWeb();
            string Username;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 100000; i++)
            {
                var Url = $"https://ibay.com.mv/index.php?page=profile&id={i}"; //construct url
                var htmlDoc = web.Load(Url); //get html string
                if (htmlDoc.Text != "Bad User ID") //check to see if user exists
                {
                    Username = htmlDoc.DocumentNode.Descendants("span").Where(
                        node => node.GetAttributeValue("style", "").Equals("margin: 0px;font-size:1.5em;font-weight:bold;")).FirstOrDefault()?.InnerText; //get the username text
                    Username = Regex.Replace(Username, @"\t|\n|\r| ", ""); //clean up text
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Username = "Invalid";//set user as invalid
                    Console.ForegroundColor = ConsoleColor.Red;
                }

                sb.AppendFormat("\n{0},{1}", Url, Username); //convert to csv format
                Console.WriteLine("Url: " + Url + " | User: " + Username); //output to console
                File.AppendAllText("data.csv", sb.ToString()); //write to file
                sb.Clear(); //clear string for next loop
            }
        }
    }
}
