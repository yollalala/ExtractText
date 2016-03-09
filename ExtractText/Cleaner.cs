using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExtractText
{
    class Cleaner
    {
        public void cleanEmptyLineData(string directory)
        {
            int counter = 0;
            string line;

            // Read the file line by line.
            System.IO.StreamReader file = new System.IO.StreamReader(directory + "data_document_v2-5.txt");
            while ((line = file.ReadLine()) != null)
            {
                counter++;
                if(line == "")
                {
                    Console.WriteLine(counter);
                }
                else
                {
                    // add text document to document cleaned
                    string outputFileDocument = directory + "data_document_v2-5_cleaned.txt";
                    File.AppendAllText(outputFileDocument, line + Environment.NewLine);

                    // add text location to location cleaned
                    string outputFileLocation = directory + "data_location_v2_cleaned.txt";
                    string location = File.ReadLines(directory + "data_location_v2.txt").Skip(counter - 1).Take(1).First();
                    File.AppendAllText(outputFileLocation, location + Environment.NewLine);

                    // add text link to link cleaned
                    string outputFileLink = directory + "links_cleaned.txt";
                    string link = File.ReadLines(directory + "links.txt").Skip(counter - 1).Take(1).First();
                    File.AppendAllText(outputFileLink, link + Environment.NewLine);
                }
            }

            file.Close();
        }

        public string getCleanLocation(string location)
        {
            string text = getErasedTimeLocation(location);     // erased time
            text = getCheckedLocation(text);

            return text;
        }

        private string getErasedTimeLocation(string location)
        {
            // get location from line
            string text = "";
            Regex regex = new Regex(@"(?<=.*, ).*?(?= \|)");
            Match match = regex.Match(location);
            if (match.Success)
            {
                text = match.Value;
            }

            return text;
        }

        private string getCheckedLocation(string location)
        {
            string text = location;

            // check if text still have ','
            if (text.Contains(','))
            {
                text = getErasedOtherFrontStringLocation(text);
            }

            if (text.Contains('/') || text.Contains('.') || text.ToLower().Contains("the jakarta post"))         
            {
                // check if has '/' (ambiguity location) or  location is "the jakarta post" or has "." (not a location)
                text = "";
            }

            return text;
        }

        private string getErasedOtherFrontStringLocation(string location)
        {
            // remove other text in location
            string text = Regex.Replace(location, @".*,\s+", string.Empty);

            return text;
        }

        public string getCleanDocument(string document)
        {
            string text = "";

            List<string> tags = getTags(document);
            text = getErasedTagDocument(document, tags);            // erase tags, author at the end document
            text = getErasedPunctuationDocument(text);              // erase punctuation
            text = getErasedStopwordDocument(text);                 // erase stopwords
            text = getErasedNumbersAndOneTwoCharDocument(text);     // erase numbers, one or two character(s), white space at the end
            //text = getErasedNewLineDocument(text);                  // erase new line
            text = text.ToLower();                                  // change to lower case

            return text;
        }

        private List<string> getTags(string document)
        {
            // get tags in document
            List<string> tags = new List<string>();

            Regex regex = new Regex("</?.*?>");
            foreach (var match in regex.Matches(document))
            {
                if (!tags.Contains(match.ToString()))
                    tags.Add(match.ToString());
            }

            return tags;
        }

        public string getErasedTagDocument(string document, List<string> tags)
        {
            string text = document;

            // erase tags in document
            foreach (string tag in tags)
            {
                text = text.Replace(tag, "");
            }

            // remove author in the end document
            text = Regex.Replace(text, @"\s+\(\w*/?\w*\)(\(\++\))*\s*$", string.Empty);

            return text;
        }

        private string getErasedNumbersAndOneTwoCharDocument(string document)
        {
            // remove numbers in document
            string content = Regex.Replace(document, @"[0-9]+( |$)", string.Empty);

            // remove one or two character(s) in document
            content = Regex.Replace(content, @"(?<= )\w\w? ", string.Empty);

            // remove white space in the end document
            content = Regex.Replace(content, @"\s+$", string.Empty);

            //// remove white space in the start document
            //content = Regex.Replace(content, @"^\s+", string.Empty);

            return content;
        }

        private string getErasedNewLineDocument(string document)
        {
            // remove new line in document
            string content = Regex.Replace(document, @"\n ", string.Empty);

            return content;
        }

        private string getErasedStopwordDocument(string document)
        {
            // remove stopwords
            string result = StopwordTool.RemoveStopwords(document);

            return result;
        }

        private string getErasedPunctuationDocument(string document)
        {
            // split document into words
            string[] words = document.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            string result = string.Join(" ", words);

            return result;
        }

        private char[] delimiters = new char[]
        {
	        ' ',
	        ',',
	        ';',
	        '.',
            '-',
            '\n',
            '\'',
            ':',
            '(',
            ')',
            '/',
            '?',
            '\t',
            '[',
            ']',
            '"',
            '%',
            '=',
            '>',
            '<',
            '*',
            '#',
            '@',
            '!',
            '$',
            '{',
            '}',
            '^',
            '&',
            '+',
            'â',
            '€',
            '™',
            '”',
            '“',
            '’',
            'œ',
            ''     // odd character
        };

        public string getRealLocationFromIndonesiaWord(string line)
        {
            // get location from line
            string text = "";
            Regex regex1 = new Regex(@"(?<=.*, ).*?(?=, Indonesia \|)");
            Match match1 = regex1.Match(line);
            if (match1.Success)
            {
                text = match1.Value;
            }

            // check if text still have "Associated Press"
            if (text.Contains("Associated"))
            {
                Regex regex2 = new Regex(@"(?<=.*, (The )?Associated Press, ).*");
                Match match2 = regex2.Match(text);
                if (match2.Success)
                {
                    text = match2.Value;
                }
            }

            return text;
        }
    }
}
