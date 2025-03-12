using KTANE_Diffusal_Assistant.Modules;

namespace KTANE_Diffusal_Assistant.Solvers;

public class _3dMazeSolver {
    private string solve3dMaze(string speech)
        {
            _3DMaze module = (_3DMaze)expert.currentModule;
            switch (module.stage)
            {
                case 1:
                    string mazeText = Program.fromNatoPhonetic(speech);
                    if (
                        !(mazeText.Contains('A') && mazeText.Contains('B') && mazeText.Contains('C'))
                        && !(mazeText.Contains('A') && mazeText.Contains('B') && mazeText.Contains('D'))
                        && !(mazeText.Contains('A') && mazeText.Contains('B') && mazeText.Contains('H'))
                        && !(mazeText.Contains('A') && mazeText.Contains('C') && mazeText.Contains('D'))
                        && !(mazeText.Contains('A') && mazeText.Contains('C') && mazeText.Contains('H'))
                        && !(mazeText.Contains('A') && mazeText.Contains('D') && mazeText.Contains('H'))
                        && !(mazeText.Contains('B') && mazeText.Contains('C') && mazeText.Contains('D'))
                        && !(mazeText.Contains('B') && mazeText.Contains('C') && mazeText.Contains('H'))
                        && !(mazeText.Contains('B') && mazeText.Contains('D') && mazeText.Contains('H'))
                        && !(mazeText.Contains('C') && mazeText.Contains('D') && mazeText.Contains('H'))
                    )
                    {
                        return "Invalid Maze";
                    }

                    module.SetMaze(mazeText);
                    module.stage = 2;
                    expert.loadGrammar("3d Maze 2");
                    return "Now find a straight path in the maze";
                case 2:
                    string pathText = Program.fromNatoPhonetic(speech.Replace("Path ", string.Empty));
                    pathText = pathText.Replace("Cardinal", "*");
                    pathText = pathText.Replace("Blank", "?");
                    pathText = pathText.Replace(" ", string.Empty);

                    char[] mazeLetters = module.mazeLetters;
                    foreach (char c in mazeLetters)
                    {
                        if (
                            c != '*'
                            && c != '?'
                            && c != 'N'
                            && c != 'E'
                            && c != 'S'
                            && c != 'W'
                            && c != mazeLetters[0]
                            && c != mazeLetters[1]
                            && c != mazeLetters[2]
                        )
                        {
                            return "Path has at least one invalid character: " + c;
                        }
                    }

                    module.stage = 3;
                    module.pathText = pathText;
                    expert.loadGrammar("3d Maze 3");
                    return "Are you facing a wall?";
                case 3:
                    bool facingWall = speech == "Yes";
                    List<int[]> possiblePaths = module.ValidPathText(module.pathText, facingWall);

                    if (possiblePaths.Count == 0)
                    {
                        expert.loadGrammar("3d Maze 2");
                        module.stage = 2;
                        return "No paths found";
                    }

                    if (possiblePaths.Count != 1)
                    {
                        expert.loadGrammar("3d Maze 2");
                        module.stage = 2;
                        return "Found Multiple paths";
                    }

                    int[] playerPosition = possiblePaths[0];

                    module.PlayerPosition = module.Maze[playerPosition[0], playerPosition[1]];
                    module.PlayerDirection = module.ConvertPlayerDirection(playerPosition[2]);
                    expert.loadGrammar("3d Maze 4");
                    module.stage = 4;
                    return "/msgbox:Go " + module.FindCardinal() + "and say cardinal";
                case 4:
                    string goalCardinal = speech.ToUpper();
                    module.MainCardinalGoal = goalCardinal;

                    int row = module.FindRow();
                    int column = module.FindColumn();
                    module.MainGoal = module.Maze[row, column];
                    module.UpdateGoal();
                    module.onSolve();
                    string solution = "Go " + module.Solve();
                    MessageBox.Show(solution);
                    return solution;
                default:
                    return expert.Bug();
            }
        }
}