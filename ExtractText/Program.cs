using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace ExtractText
{
    class Program
    {
        static void Main(string[] args)
        {
            string directory = @"D:\data_all_cleaned_v2\";

            Cleaner cleaner = new Cleaner();
            cleaner.cleanEmptyLineData(directory);

            //Crawler.crawlingOneFile(directory);

            //string URL = "http://www.thejakartapost.com/news/2015/02/14/kadin-chairman-arrested-newspaper-attack.html";

            //string content = "";
            //using (var client = new WebClient())
            //{
            //    // get HTML contents from URL
            //    content = client.DownloadString(URL);
            //}

            //// get raw document
            //string document = "";
            //Regex regex = new Regex("(?<=<div\\s*class=\"span-13 last\">\\s*)(.|\\n)*?(?=<div\\s*style)");
            ////Regex regex = new Regex("(?<=<div\\s*class=\"span-13 last\">\\s*)(.|\\n)*?(?=<div\\s*style)");
            //Match match = regex.Match(content);
            //if (match.Success)
            //{
            //    document = match.Value;
            //}

            //Console.WriteLine(document);

            // cleaned data location Indonesia
            //string directory = @"D:/data_all_cleaned/";
            //string line;
            //int counter = 0;
            //List<int> lineNumbers = new List<int>();

            //System.IO.StreamReader file = new System.IO.StreamReader(directory + "data_location.txt");
            //while ((line = file.ReadLine()) != null)
            //{
            //    counter++;
            //    if (line.ToLower().Equals("indonesia"))
            //    {
            //        //Console.WriteLine(counter);
            //        lineNumbers.Add(counter);
            //    }
            ////    //else
            ////    //{
            ////    //    // add text document to document cleaned
            ////    //    string outputFileDocument = directory + "cleaned_data_document.txt";
            ////    //    File.AppendAllText(outputFileDocument, line + Environment.NewLine);

            ////    //    // add text location to location cleaned
            ////    //    string outputFileLocation = directory + "cleaned_data_location.txt";
            ////    //    string location = File.ReadLines(directory + "data_location.txt").Skip(counter - 1).Take(1).First();
            ////    //    File.AppendAllText(outputFileLocation, location + Environment.NewLine);

            ////    //    // add text link to link cleaned
            ////    //    string outputFileLink = directory + "cleaned_links.txt";
            ////    //    string link = File.ReadLines(directory + "links.txt").Skip(counter - 1).Take(1).First();
            ////    //    File.AppendAllText(outputFileLink, link + Environment.NewLine);
            ////    //}
            //}

            //int iterator = 0;
            //System.IO.StreamReader file1 = new System.IO.StreamReader(directory + "data_location_odd_raw.txt");
            //while ((line = file1.ReadLine()) != null)
            //{
            //    // get location from line
            //    string text = "";
            //    Regex regex1 = new Regex(@"(?<=.*, ).*?(?=, Indonesia \|)");
            //    //Regex regex1 = new Regex(@"(?<=.*, ((The )?Associated Press, )?).*?(?=, Indonesia \|)");
            //    //Regex regex1 = new Regex(@"(?<=.*, ((The )?(Associated Press, ))?).*?(?=, Indonesia \|)");
            //    //Regex regex1 = new Regex(@"(?<=.*, (The )?(Associated Press, )?).*?(?=, Indonesia \|)");
            //    Match match1 = regex1.Match(line);
            //    if (match1.Success)
            //    {
            //        text = match1.Value;
            //    }

            //    // check if text still have "Associated Press"
            //    if (text.Contains("Associated"))
            //    {
            //        //Regex regex2 = new Regex(@"(?<=(The )?Associated Press, ).*?");
            //        Regex regex2 = new Regex(@"(?<=(The )?Associated Press, ).*");
            //        Match match2 = regex2.Match(text);
            //        if (match2.Success)
            //        {
            //            text = match2.Value;
            //        }
            //    }

            //    File.AppendAllText(directory + "data_location_odd_cleaned_ii.txt", lineNumbers[iterator] + " " + text + Environment.NewLine);
            //    iterator++;
            //}

            // add list stopwords
            //StopwordTool.AddDictionaryFromText(@"D:\stopwords.txt");

            //Cleaner cleaner = new Cleaner();
            //string directory = @"D:\output\2015\";
            //cleaner.cleanEmptyLineData(directory);

            // inisiasi variabel tanggal
            //string tahun = "2016";
            //string bulan = "01";
            //string hari = "31";

            // crawling link news article
            //Crawler.crawlingOneDate(tahun, bulan, hari);
            //Crawler.crawlingOneMonth(tahun, bulan);
            //Crawler.crawlingOneYear(tahun);

            Console.WriteLine("Selesai!");
            Console.ReadLine();
        }
    }
}
