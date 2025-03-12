using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;

namespace KTANE_Diffusal_Assistant
{
    public class Grammars
    {
        public static DictationGrammar dictationGrammar => getDictationGrammar();
        public static Grammar mainMenuGrammar => getMainMenuGrammar();
        public static Grammar mainBombCheckGrammar => getMainBombCheckGrammar();
        public static Grammar portCheckGrammar => getPortCheckGrammar();
        public static Grammar _3dMazeStage1Grammar => get3dMazeStage1Grammar();
        public static Grammar _3dMazeStage2Grammar => get3dMazeStage2Grammar();

        public static Grammar _3dMazeStage3Grammar => get3dMazeStage3Grammar();
        public static Grammar _3dMazeStage4Grammar => get3dMazeStage4Grammar();
        public static Grammar _3dMazeStage5Grammar => get3dMazeStage5Grammar();

        private static DictationGrammar getDictationGrammar()
        {
            DictationGrammar toReturn = new DictationGrammar("grammar:dictation#pronunciation");
            toReturn.Name = "DictationGrammar";
            return toReturn;
        }

        private static Grammar getMainMenuGrammar()
        {
            List<string> phrases = ["Load Bomb", "Bomb Check", "Save Bomb"];
            foreach (string module in modules)
            {
                phrases.Add($"Defuse {module}");
            }

            return new Grammar(new Choices(phrases.ToArray()));
        }

        private static Grammar getMainBombCheckGrammar()
        {
            List<string> lettersAndNumbers = [.. nato, .. numbers];

            List<string> phrases = new List<string>();

            for (int i = 1; i <= 250; i++)
            {
                if (i == 1)
                {
                    phrases.Add("1 Battery");
                    phrases.Add("1 Port Plate");
                    phrases.Add("1 Holder");
                    phrases.Add("1 Module");
                    continue;
                }
                phrases.Add($"{i} Batteries");
                phrases.Add($"{i} Port Plates");
                phrases.Add($"{i} Holders");
                phrases.Add($"{i} Modules");
            }

            foreach (string indicator in indicators)
            {
                string nameAsPhonetic = string.Empty;
                foreach (char c in indicator)
                {
                    nameAsPhonetic += Program.toNatoPhonetic(c.ToString());
                }
                phrases.Add($"Indicator Unlit {nameAsPhonetic}");
                phrases.Add($"Indicator Lit {nameAsPhonetic}");
            }
            phrases.Add("Done");

            GrammarBuilder serialChoices = new GrammarBuilder(new Choices(lettersAndNumbers.ToArray()), 6, 6);
            GrammarBuilder serial = new GrammarBuilder("Serial");
            serial.Append(serialChoices);

            GrammarBuilder main = new GrammarBuilder(new Choices(phrases.ToArray()));

            Choices both = new Choices(new GrammarBuilder[] { main, serial });

            return new Grammar(both);
        }

        private static Grammar getPortCheckGrammar()
        {
            List<string> phrases = [
                    "DVI-D",
                    "Parallel",
                    "PS2",
                    "RJ45",
                    "Serial",
                    "RCA"
                ];
            Choices ports = new Choices(phrases.ToArray());
            GrammarBuilder gb = new GrammarBuilder(ports, 1, 10);
            return new Grammar(gb);
        }

        private static Grammar get3dMazeStage1Grammar()
        {
            List<string> mazeIdPhrases = [
                "Alpha",
                "Bravo",
                "Charlie",
                "Delta",
                "Hotel"
                ];
            Choices c = new Choices(mazeIdPhrases.ToArray());
            GrammarBuilder MazeID = new GrammarBuilder(c, 3, 3);
            return new Grammar(MazeID);
        }

        private static Grammar get3dMazeStage2Grammar()
        {
            List<string> pathPhrases = [
                "Blank", 
                "Cardinal",
                "Alpha",
                "Bravo",
                "Charlie",
                "Delta",
                "Hotel"];
            Choices c = new Choices(pathPhrases.ToArray());
            GrammarBuilder paths = new GrammarBuilder(c, 3, 10);
            return new Grammar(paths);
        }

        private static Grammar get3dMazeStage3Grammar()
        {
            List<string> yesNo = ["Yes", "No"];
            GrammarBuilder gbYesNo = new GrammarBuilder(new Choices(yesNo.ToArray()));
            return new Grammar(gbYesNo);
        }

        private static Grammar get3dMazeStage4Grammar()
        {
            List<string> cardinals = ["North", "South", "East", "West"];

            GrammarBuilder Cardinals = new GrammarBuilder(new Choices(cardinals.ToArray()));
            return new Grammar(Cardinals);
        }

        private static Grammar get3dMazeStage5Grammar()
        {
            throw new NotImplementedException();
        }


        private static List<string> modules = [
            "3d Maze",
            ];

        private static List<string> numbers = [
            "One",
            "Two",
            "Three",
            "Four",
            "Five",
            "Six",
            "Seven",
            "Eight",
            "Nine",
            "Zero"
            ];

        private static List<string> nato = [
                "Alpha",
                "Bravo",
                "Charlie",
                "Delta",
                "Echo",
                "Foxtrot",
                "Golf",
                "Hotel",
                "India",
                "Juliet",
                "Kilo",
                "Lima",
                "Mike",
                "November",
                "Oscar",
                "Papa",
                "Quebec",
                "Romeo",
                "Sierra",
                "Tango",
                "Uniform",
                "Victor",
                "Wiskey",
                "Xray",
                "Yankee",
                "Zulu"
            ];

        static List<string> indicators = [
            "SND",
            "CLR",
            "CAR",
            "IND",
            "FRQ",
            "SIG",
            "NSA",
            "MSA",
            "TRN",
            "BOB",
            "FRK"
            ];
    }
}
