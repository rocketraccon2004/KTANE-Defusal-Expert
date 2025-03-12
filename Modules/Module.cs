using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTANE_Diffusal_Assistant.Solvers;

namespace KTANE_Diffusal_Assistant.Modules
{
    public abstract class Module
    {
        public string name;
        public Bomb bomb;
        public bool isSolved;
        public Solver solver;
        public Expert expert;
        
        public void onSolve()
        {
            isSolved = true;
        }
    }
}
