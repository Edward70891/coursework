using System.IO;
using System;

public class logging
{
	public static void newUserEntry(string user, string entryText)
	{
		
	}

	public static void newOtherEntry(string entryText)
	{

	}

	private string getTimestamp()
	{
		string timestamp;
		timestamp = "[" + DateTime.Now + "] ";
		return timestamp;
	}
}