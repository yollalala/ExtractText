using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractText
{
    /// <summary>
    /// Reference: http://www.dotnetperls.com/stopword-dictionary 
    /// </summary>
    class StopwordTool
    {
        /// <summary>
        /// Words we want to remove.
        /// </summary>
        static Dictionary<string, bool> _stops = new Dictionary<string, bool>();

        public static void AddDictionaryFromText(string location)
        {
            List<string> words = new List<string>();
            using (StreamReader reader = new StreamReader(location))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    words.Add(line); // Add to list.
                }
            }
            foreach (string word in words)
            {
                _stops.Add(word, true);
            }
        }

        /// <summary>
        /// Remove stopwords from string.
        /// </summary>
        public static string RemoveStopwords(string input)
        {
            // Split parameter into words
            var words = input.Split(' ');

            // Store results in this StringBuilder
            StringBuilder builder = new StringBuilder();

            // Loop through all words
            foreach (string currentWord in words)
            {
                // Convert to lowercase
                string lowerWord = currentWord.ToLower();

                // If this is a usable word, add it
                if (!_stops.ContainsKey(lowerWord))
                {
                    builder.Append(currentWord).Append(' ');
                }
            }
            
            return builder.ToString().Trim();
        }
    }
}
