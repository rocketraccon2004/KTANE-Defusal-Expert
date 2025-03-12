namespace KTANE_Diffusal_Assistant
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }

        public static string toNatoPhonetic(string phrase)
        {
            string[] split = phrase.Split(' ');
            List<char> charsInPhrase = new List<char>();
            foreach (string s in split)
            {
                charsInPhrase.Add(char.Parse(s));
            }

            string converted = string.Empty;
            foreach (char c in charsInPhrase)
            {
                converted += natoPairs[c].ToString() + " ";
            }
            return converted;
        }

        public static string fromNatoPhonetic(string phrase)
        {
            string[] wordsInPhrase = phrase.Split(' ');
            string converted = string.Empty;
            foreach (string word in wordsInPhrase)
            {
                bool isWordInPrase = false;
                foreach (var kvp in natoPairs)
                {
                    if (kvp.Value == word)
                    {
                        converted += kvp.Key.ToString() + ' ';
                        isWordInPrase = true;
                    }
                }
                if (!isWordInPrase)
                {
                    converted += word + ' ';
                }
            }
            return converted;
        }

        public static bool isNatoPhonetic(string word)
        {
            foreach (string s in natoPairs.Values)
            {
                if (s == word)
                {
                    return true;
                }
            }
            return false;
        }


        static Dictionary<char, string> natoPairs = new Dictionary<char, string>
        {
            {'A', "Alpha"},
            {'B', "Bravo"},
            {'C', "Charlie"},
            {'D', "Delta"},
            {'E', "Echo"},
            {'F', "Foxtrot"},
            {'G', "Golf"},
            {'H', "Hotel"},
            {'I', "India"},
            {'J', "Juliet"},
            {'K', "Kilo"},
            {'L', "Lima"},
            {'M', "Mike"},
            {'N', "November"},
            {'O', "Oscar"},
            {'P', "Papa"},
            {'Q', "Quebec"},
            {'R', "Romeo"},
            {'S', "Sierra"},
            {'T', "Tango"},
            {'U', "Uniform"},
            {'V', "Victor"},
            {'W', "Wiskey"},
            {'X', "Xray"},
            {'Y', "Yankee"},
            {'Z', "Zulu"}
        };
    }
}