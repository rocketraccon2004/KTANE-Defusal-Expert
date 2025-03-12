namespace KTANE_Diffusal_Assistant;

public class Bomb
{
	public bool isInitialized = false;
	public int batteries;
	public int batteryHolders;
	public List<PortPlate> portPlates = new List<PortPlate>();
	public int moduleCount;
	public List<Indicator> indicators = [
		new Indicator("SND"),
		new Indicator("CLR"),
		new Indicator("CAR"),
		new Indicator("IND"),
		new Indicator("FRQ"),
		new Indicator("SIG"),
		new Indicator("NSA"),
		new Indicator("MSA"),
		new Indicator("TRN"),
		new Indicator("BOB"),
		new Indicator("FRK"),
		];

	public string serial = string.Empty;

    public void addPortPlate(string ports)
    {
        bool DVID = ports.Contains("DVID");
        bool Parallel = ports.Contains("Parallel");
        bool PS2 = ports.Contains("PS2");
        bool RJ45 = ports.Contains("RJ45");
        bool Serial = ports.Contains("Serial");
        bool RCA = ports.Contains("RCA");

		portPlates.Add(new PortPlate(DVID, Parallel, PS2, RJ45, Serial, RCA));
    }

    public bool vowelInSerial()
	{
		return serial.Contains('A') ||
			serial.Contains('E') ||
			serial.Contains('I') ||
			serial.Contains('O') ||
			serial.Contains('U');
	}

	public int firstDigitInSerial()
	{
		for (int i = 0; i < 6; i++)
		{
			char c = serial[i];
			if (char.IsDigit(c))
			{
				return (int) char.GetNumericValue(c);
			}
		}
		throw new Exception("Error: No digit in serial number");
	}

	public int LastDigitInSerial()
	{
		for (int i = 5; i >= 0; i--)
		{
			char c = serial[i];
			if (char.IsDigit(c))
			{
				return (int)char.GetNumericValue(c);
			}
		}
		throw new Exception("Error: No digit in serial number");
	}

	public bool isOdd(int num)
	{
		return num % 2 != 0;
	}
}
