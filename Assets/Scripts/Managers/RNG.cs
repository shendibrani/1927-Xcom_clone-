using System;

public class RNG
{

	private static Random RandomNumberGenerator = new Random();

	public static int RNGCount { get; private set; }

	private RNG () {}

	public static float NextFloat()
	{
		RNGCount++;
		return (float)RandomNumberGenerator.NextDouble ();
	}

	public static float NextFloat(int min, int max)
	{	
		RNGCount += 2;
		return (float)RandomNumberGenerator.NextDouble() + RandomNumberGenerator.Next(min, max-1);
	}

	public static int Next()
	{
		RNGCount++;
		return RandomNumberGenerator.Next();
	}

	public static int Next(int min, int max)
	{
		RNGCount++;
		return RandomNumberGenerator.Next(min, max);
	}

	public static double NextDouble()
	{
		RNGCount++;
		return RandomNumberGenerator.NextDouble();
	}

	public static bool NextBoolean()
	{
		RNGCount++;
		if ((RandomNumberGenerator.Next () % 2) == 0) {
			return false;
		}
		return true;
	}

	public static void SetSeed(int seed)
	{
		RandomNumberGenerator = new Random (seed);
		RNGCount = 0;
	}

	public static void Reset(int seed, int useCount)
	{
		RandomNumberGenerator = new Random (seed);
		RNGCount = 0;

		for (int counter = 0; counter <= useCount; counter++) {
			RNG.Next();
		}
	}
}