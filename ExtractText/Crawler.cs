using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExtractText
{
    class Crawler
    {
        public static void crawlingOneFile(string directory)
        {
            // link or document number
            int i = 0;  

            // Read the file links line by line
            string line = "";
            System.IO.StreamReader file = new System.IO.StreamReader(directory);
            while ((line = file.ReadLine()) != null)
            {
                i++;    // increase number of document
                string content = "";
                using (var client = new WebClient())
                {
                    // get HTML contents from line (link)
                    content = client.DownloadString(line);
                }

                //string raw = getDocumentRaw(content);
                string raw = getLocationRaw(content);
                addToFile(@"E:\output_real\data_location_raw\data_location_raw.txt", raw);
            }
        }

        public static void crawlingOneYear(string year)
        {
            int numMonths = 12;

            for (int i = 1; i <= numMonths; i++)
            {
                Console.WriteLine("month " + i);
                string month = i.ToString("00");
                crawlingOneMonth(year, month);
            }
        }

        public static void crawlingOneMonth(string year, string month)
        {
            int numDays = Convert.ToInt32(getNumDaysInOneMonth(year, month));

            for (int i = 1; i <= numDays; i++)
            {
                Console.WriteLine("day " + i);
                string day = i.ToString("00");
                crawlingOneDate(year, month, day);
            }
        }

        public static void crawlingOneDate(string year, string month, string day) 
        {
            string URL = "http://www.thejakartapost.com/archive/archipelago/" + year + "-" + month + "-" + day;
            string homeURL = "http://www.thejakartapost.com";

            string contents = "";
            using (var client = new WebClient())
            {
                // get HTML contents from URL
                contents = client.DownloadString(URL);
            }

            // get links (news article link) from HTML content 
            List<string> links = getNewLinks(contents);

            int i = 0;
            foreach (string link in links)
            {
                RawFeature rf = new RawFeature();
                rf = getContentRaw(homeURL + link);

                // clean document
                Cleaner cleaner = new Cleaner();
                string cleanedLocation = cleaner.getCleanLocation(rf.location);

                // check if location null, if null then pass
                if(cleanedLocation != "")
                {
                    string cleanedDocument = cleaner.getCleanDocument(rf.document);
                    addToFile("D:/output/" + year + "/data_document.txt", cleanedDocument);
                    addToFile("D:/output/" + year + "/data_location.txt", cleanedLocation);
                    addToFile("D:/output/" + year + "/links.txt", homeURL + link);
                    i++;
                    Console.WriteLine("link " + i);
                }
            }
        }

        private static List<string> getNewLinks(string content)
        {
            // regex for general link
            //Regex regexLink = new Regex("(?<=<a\\s*?href=(?:'|\"))[^'\"]*?(?=(?:'|\"))");

            // regex for link news article thejakartapost.com
            Regex regexLink = new Regex("(?<=<a\\s*href=['\"])/news/.*?(?=['\"])");

            List<string> newLinks = new List<string>();
            foreach (var match in regexLink.Matches(content))
            {
                if (!newLinks.Contains(match.ToString()))
                {
                    string link = getCheckedLink(match.ToString());
                    newLinks.Add(link);
                }
            }

            return newLinks;
        }

        private static RawFeature getContentRaw(string link)
        {
            string content = "";
            using (var client = new WebClient())
            {
                // get HTML contents from link
                content = client.DownloadString(link);
            }

            // get document and location raw from HTML text
            RawFeature rf = new RawFeature();
            rf.document = getDocumentRaw(content);
            rf.location = getLocationRaw(content);

            return rf;
        }

        private static string getDocumentRaw(string content)
        {
            // get raw document
            string document = "";
            //Regex regex = new Regex("(?<=<div\\s*class=\"span-13 last\">\\s*).*(?=<div\\s*style)");
            Regex regex = new Regex("(?<=<div\\s*class=\"span-13 last\">\\s*)(.|\\n)*?(?=<div\\s*style)");
            Match match = regex.Match(content);
            if (match.Success)
            {
                document = match.Value;
            }

            return document;
        }

        private static string getLocationRaw(string content)
        {
            // get raw location
            string location = "";
            Regex regex = new Regex("(?<=<div\\s*class=\"byline\">).*(?=</div>)");
            Match match = regex.Match(content);
            if (match.Success)
            {
                location = match.Value;
            }

            return location;
        }

        private static void printLinksToFile(string outputFile, List<string> links)
        {
            using (StreamWriter writer = new StreamWriter(outputFile))
            {
                foreach (var link in links)
                {
                    writer.WriteLine(link);
                }
            }
        }

        private static void addToFile(string outputFile, string text)
        {
            // add text to outputFile
            File.AppendAllText(@outputFile, text + Environment.NewLine);
        }

        private static string getNumDaysInOneMonth(string year, string month)
        {
            string days = "0";
            switch (month)
            {
                case "01": days = "31"; break;
                case "02":
                    if (((Convert.ToInt32(year) % 4) == 0) && ((Convert.ToInt32(year) % 100) != 0))
                    { days = "29"; }
                    else 
                    { days = "28"; }
                    break;
                case "03": days = "31"; break;
                case "04": days = "30"; break;
                case "05": days = "31"; break;
                case "06": days = "30"; break;
                case "07": days = "31"; break;
                case "08": days = "31"; break;
                case "09": days = "30"; break;
                case "10": days = "31"; break;
                case "11": days = "30"; break;
                case "12": days = "31"; break;
            }

            return days;
        }

        private static string getCheckedLink(string text)
        {
            string link = text;
            // check if link contains â€™ or â€˜
            if (link.Contains("â€™"))
            {
                link = link.Replace("â€™", "’");
            }

            if (link.Contains("â€˜"))
            {
                link = link.Replace("â€˜", "‘");
            }

            if (link.Contains("Ã©"))
            {
                link = link.Replace("Ã©", "é");
            }

            if (link.Contains("â€œ"))
            {
                link = link.Replace("â€œ", "“");
            }

            if (link.Contains("â€"))
            {
                link = link.Replace("â€", "”");
            }

            if (link.Contains("Â"))
            {
                link = link.Replace("Â", "");
            }

            return link;
        }
    }
}
