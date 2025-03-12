namespace KTANE_Diffusal_Assistant
{
    public class Indicator
    {
        public string name;
        public bool lit;
        public bool visible;

        public Indicator(string name)
        {
            this.name = name;
            lit = false;
            visible = false;
        }
    }
}