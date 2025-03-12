namespace KTANE_Diffusal_Assistant
{
    public class PortPlate
    {
        public bool DVID;
        public bool Parallel;
        public bool PS2;
        public bool RJ45;
        public bool Serial;
        public bool RCA;

        public PortPlate(bool DVID, bool Parallel, bool PS2, bool RJ45, bool Serial, bool RCA)
        {
            this.DVID = DVID;
            this.Parallel = Parallel;
            this.PS2 = PS2;
            this.RJ45 = RJ45;
            this.Serial = Serial;
            this.RCA = RCA;
        }
    }
}