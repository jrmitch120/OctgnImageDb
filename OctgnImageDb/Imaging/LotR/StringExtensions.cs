using System;
using System.Globalization;
using System.Linq;

public static class StringExtensions
{
	public static bool CustomContains(this string source, string toCheck)
	{
		if (source.ToLower().EndsWith("- nightmare") && !toCheck.ToLower().Contains("nightmare")) {
			//Check for duplicate nightmare pack
			return false;
		}
		string simpleSource = string.Join("", source.Where(char.IsLetterOrDigit)).ToLower();
		if (simpleSource.StartsWith("the "))
		{
			int index = simpleSource.IndexOf("the ");
			simpleSource = (index < 0)
				? simpleSource
				: simpleSource.Remove(index, "the ".Length);
		}
		if (simpleSource.StartsWith("a "))
		{
			int index = simpleSource.IndexOf("a ");
			simpleSource = (index < 0)
				? simpleSource
				: simpleSource.Remove(index, "a ".Length);
		}
		if (simpleSource.StartsWith("an "))
		{
			int index = simpleSource.IndexOf("an ");
			simpleSource = (index < 0)
				? simpleSource
				: simpleSource.Remove(index, "an ".Length);
		}

		string simpleToCheck = string.Join("", toCheck.Where(char.IsLetterOrDigit)).ToLower();
		if (simpleToCheck.StartsWith("the "))
		{
			int index = simpleToCheck.IndexOf("the ");
			simpleToCheck = (index < 0)
				? simpleToCheck
				: simpleToCheck.Remove(index, "the ".Length);
		}
		if (simpleToCheck.StartsWith("a "))
		{
			int index = simpleToCheck.IndexOf("a ");
			simpleToCheck = (index < 0)
				? simpleToCheck
				: simpleToCheck.Remove(index, "a ".Length);
		}
		if (simpleToCheck.StartsWith("an "))
		{
			int index = simpleToCheck.IndexOf("an ");
			simpleToCheck = (index < 0)
				? simpleToCheck
				: simpleToCheck.Remove(index, "an ".Length);
		}
		CompareInfo ci = new CultureInfo("en-US").CompareInfo;
		CompareOptions co = CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace;
		return ci.IndexOf(simpleSource, simpleToCheck, co) != -1;
	}
}
