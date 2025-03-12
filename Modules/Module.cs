using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTANE_Diffusal_Assistant.Modules
{
    public class Module
    {
        public string name;
        public Bomb bomb;
        public bool isSolved;
        
        public void onSolve()
        {
            isSolved = true;
        }
    }
}
