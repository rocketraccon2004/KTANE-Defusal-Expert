using System.Globalization;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using KTANE_Diffusal_Assistant.Modules;
using KTANE_Diffusal_Assistant.Solvers;
using Newtonsoft.Json;

namespace KTANE_Diffusal_Assistant
{
    public class Expert
    {
        public Bomb? bomb;
        public bool isListening = false;
        public int remainingModules;
        public Module? currentModule;
        public Solver currentSolver;

        private State currentState;
        private MainForm? mainForm;
        private SpeechRecognitionEngine? sre;
        private static SpeechSynthesizer? synth;
        private bool hasInitialised;
        private int portPlateNum;
        private int addedPlates = 0;

        private Dictionary<string, Grammar> grammars = new Dictionary<string, Grammar>()
        {
            { "MainMenu", Grammars.mainMenuGrammar },
            { "Bomb Check", Grammars.mainBombCheckGrammar },
            { "Ports", Grammars.portCheckGrammar },
            { "3d Maze ", Grammars._3dMazeStage1Grammar },
            { "3d Maze 2", Grammars._3dMazeStage2Grammar },
            { "3d Maze 3", Grammars._3dMazeStage3Grammar },
            { "3d Maze 4", Grammars._3dMazeStage4Grammar },
        };

        private Dictionary<string, Module> modules = new()
        {
            { "3dMaze", new _3DMaze() }
        };

        private Dictionary<string, int> numberWordPairs = new Dictionary<string, int>()
        {
            { "One", 1 },
            { "Two", 2 },
            { "Three", 3 },
            { "Four", 4 },
            { "Five", 5 },
            { "Six", 6 },
            { "Seven", 7 },
            { "Eight", 8 },
            { "Nine", 9 },
            { "Zero", 0 }
        };


        public Expert(MainForm mainForm)
        {
            this.mainForm = mainForm;
            sre = new SpeechRecognitionEngine(new CultureInfo("en-GB"));
            synth = new SpeechSynthesizer();
            sre.SpeechRecognized += onSpeechRecognised;
            sre.SetInputToDefaultAudioDevice();
            loadGrammar("MainMenu");
            bomb = new Bomb();
        }


        /// <summary>
        /// Called when the <see cref="SpeechRecognitionEngine"/> recognises a phrase
        /// </summary>
        private void onSpeechRecognised(object? sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Grammar.Name != "DictationGrammar")
            {
                Say(ProcessSpeech(e.Result.Text), shouldFixSpaces());
                if (currentModule != null && currentModule.isSolved)
                {
                    currentState = State.Idle;
                    currentModule = null;
                    loadGrammar("MainMenu");
                }
            }
        }

        private bool shouldFixSpaces()
        {
            return currentState == State.Checking;
        }

        /// <summary>
        /// Processes the recognised speech
        /// </summary>
        /// <param name="speech">The recognised speech</param>
        private string ProcessSpeech(string speech)
        {
            mainForm.setSpeech(speech);

            switch (currentState)
            {
                case State.Idle:
                    if (speech == "Load Bomb")
                    {
                        return loadBombFromJSON();
                    }

                    if (speech == "Save Bomb")
                    {
                        return saveBombToJSON();
                    }

                    if (speech.Contains("Defuse"))
                    {
                        if (!bomb.isInitialized)
                        {
                            return "Please do a bomb check first";
                        }

                        string[] split = speech.Split(' ');
                        string moduleName = string.Empty;
                        for (int i = 1; i < split.Length; i++)
                        {
                            moduleName += split[i] + ' ';
                        }

                        string moduleNameFixed = moduleName.Replace(" ", string.Empty);
                        currentModule = modules[moduleNameFixed];
                        currentModule.name = moduleName;
                        currentModule.bomb = bomb;
                        currentState = State.Diffusing;
                        loadGrammar(moduleName);
                        return moduleName + getModuleStartText();
                    }

                    currentState = State.Checking;
                    loadGrammar("Bomb Check");
                    return "Bomb Check";

                case State.Checking:
                    string response = string.Empty;
                    if (!bomb.isInitialized)
                    {
                        bomb.isInitialized = true;
                    }

                    if (speech.Contains("Batteries") || speech.Contains("Battery"))
                    {
                        bomb.batteries = int.Parse(speech.Split(' ')[0]);
                        response = speech;
                    }

                    else if (speech.Contains("Holders") || speech.Contains("Holder"))
                    {
                        bomb.batteryHolders = int.Parse(speech.Split(' ')[0]);
                        response = speech;
                    }

                    else if (speech.Contains("Port Plates") || speech.Contains("Port Plate"))
                    {
                        portPlateNum = int.Parse(speech.Split(' ')[0]);
                        loadGrammar("Ports");
                        currentState = State.Ports;
                        response = $"{speech}; Say ports on plate 1";
                    }

                    else if (speech.Contains("Indicator"))
                    {
                        string indicatorNameAsNato =
                            $"{speech.Split(' ')[2]} {speech.Split(' ')[3]} {speech.Split(' ')[4]}";
                        string[] indicatorNameSplit = indicatorNameAsNato.Split(' ');
                        string indicatorName = string.Empty;
                        bool lit = speech.Contains("Lit");

                        foreach (string s in indicatorNameSplit)
                        {
                            indicatorName += Program.fromNatoPhonetic(s);
                        }

                        foreach (Indicator indicator in bomb.indicators)
                        {
                            if (indicator.name == indicatorName)
                            {
                                indicator.visible = true;
                                indicator.lit = lit;
                                break;
                            }
                        }

                        response = speech;
                    }

                    else if (speech.Contains("Serial"))
                    {
                        string[] split = speech.Split(' ');
                        string serialNato = $"{split[1]} {split[2]} {split[3]} {split[4]} {split[5]} {split[6]}";
                        string serial = Program.fromNatoPhonetic(serialNato);

                        foreach (string s in numberWordPairs.Keys)
                        {
                            serial = serial.Replace(s, numberWordPairs[s].ToString());
                        }

                        serial = serial.Replace(" ", string.Empty);

                        bomb.serial = serial;
                        response =
                            $"Serial {serial[0]}, {serial[1]}, {serial[2]}, {serial[3]}, {serial[4]}, {serial[5]}";
                    }

                    else if (speech.Contains("Module") || speech.Contains("Modules"))
                    {
                        bomb.moduleCount = int.Parse(speech.Split(' ')[0]);
                        response = speech;
                    }

                    else
                    {
                        currentState = State.Idle;
                        response = "Bomb Check Complete";
                        remainingModules = bomb.moduleCount;
                        loadGrammar("MainMenu");
                    }

                    return response;

                case State.Ports:
                    if (addedPlates != portPlateNum)
                    {
                        bomb.addPortPlate(speech);
                        if (addedPlates + 1 != portPlateNum)
                        {
                            addedPlates++;
                            return $"{speech}; Now say ports on plate {addedPlates + 1}";
                        }

                        addedPlates++;
                        if (addedPlates != portPlateNum)
                        {
                            return speech;
                        }
                    }

                    currentState = State.Checking;
                    loadGrammar("Bomb Check");
                    return $"{speech}; Completed Port Check";

                case State.Diffusing:
                    switch (currentModule.name)
                    {
                        case "3d Maze ":
                            return solve3dMaze(speech);
                    }

                    goto default;
                default:
                    return Bug();
            }
        }

        private string getModuleStartText()
        {
            switch (currentModule.name)
            {
                case "3d Maze ":
                    return "Say Letters in maze";
            }

            return Bug();
        }

        private string loadBombFromJSON()
        {
            if (!File.Exists("Bomb.json"))
            {
                return "Could not find Bomb.jason";
            }

            StreamReader sr = new StreamReader("Bomb.json");
            string json = sr.ReadToEnd();
            bomb = JsonConvert.DeserializeObject<Bomb>(json);
            bomb.isInitialized = true;
            return "Loaded bomb from jason";
        }

        private string saveBombToJSON()
        {
            string json = JsonConvert.SerializeObject(bomb);
            File.WriteAllText("Bomb.json", json);
            return "Saved bomb to jason";
        }

        public void Say(string toSay, bool fixSpaces = false)
        {
            string fixedSpeech = string.Empty;
            bool msgBox = false;
            if (toSay.Contains("/msgbox:"))
            {
                msgBox = true;
                fixedSpeech = toSay.Replace("/msgbox:", String.Empty);
            }

            string fixedText = toSay.Replace("jason", "json");
            if (fixSpaces)
                fixedText = fixedText.Replace(", ", string.Empty);
            // synth.Rate = getSpeechSpeed();
            mainForm.setResponse(fixedText);
            if (!msgBox)
                synth.Speak(fixedSpeech);
            else
                MessageBox.Show(fixedSpeech);
        }

        // private int getSpeechSpeed()
        // {
        //     if (sre.Grammars.Any(x => x == grammars["3d Maze 4"]))
        //     {
        //         return -5;
        //     }
        //
        //     return 0;
        // }

        public string[] GetAllVoices()
        {
            return (from voice in synth.GetInstalledVoices() select voice.VoiceInfo.Name).ToArray();
        }

        public void SetVoice(string voice)
        {
            synth.SelectVoice(voice);
        }

        public void loadGrammar(string grammar)
        {
            sre.UnloadAllGrammars();
            sre.LoadGrammar(grammars[grammar]);
            sre.LoadGrammar(Grammars.dictationGrammar);
        }

        public void startListening()
        {
            isListening = true;
            sre.RecognizeAsync(RecognizeMode.Multiple);
        }

        public void stopListening()
        {
            isListening = false;
            sre.RecognizeAsyncCancel();
        }

        public string Bug()
        {
            return "This is a bug";
        }
    }

    public enum State
    {
        Idle,
        Checking,
        Ports,
        Diffusing
    }
}