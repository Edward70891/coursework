using System;
using System.Web;
using System.Web.UI;
using System.IO;

namespace commonClasses
{
	//Generic logging of events throughout the program
	public class customLogging
	{
		//Logs the creation of a new session when the program starts
		public static void newSession()
		{
			string result;
			string dividerASCII = "------------------------------------------";
			result = dividerASCII + generateTimestamp() + dividerASCII;
			writeEntry("");
			writeEntry(result);
		}

		//Writes a timestamped new entry to the logfile
		public static void newEntry(string entryText)
		{
			string result;
			if (HttpContext.Current.User.Identity.Name == "")
			{
				result = generateTimestamp() + " " + entryText;
			}
			else
			{
				result = generateTimestamp() + " " + entryText + "; Logged user is " + HttpContext.Current.User.Identity.Name;
			}
			writeEntry(entryText);
		}

		//Checks both possible locations for the logfile (this needs a better solution) then writes the given text to them
		private static void writeEntry(string entryText)
		{
			try
			{
				using (StreamWriter logFile = new StreamWriter(@"\\albert \2011\R04637\Computer Science\coursework\mainCoursework\App_Data\log.txt", true))
				{
					logFile.WriteLine(entryText);
				}
			}
			catch (IOException)
			{
				using (StreamWriter logFile = new StreamWriter(@"C:\Users\Edward\Source\Repos\coursework\mainCoursework\App_Data\log.txt", true))
				{
					logFile.WriteLine(entryText);
				}
			}
		}

		//Generates a timestamp string in square bracketss
		private static string generateTimestamp()
		{
			string output = "[" + Convert.ToString(DateTime.Now) + "]";
			return output;
		}
	}

	public class SQLSanitization
	{
		//Returns true if any inputs are detected to have brackets, apostrophes, equals signs or hyphons in them
		public static bool sanitizeCheck(string[] input)
		{
			bool isClean = true;
			//Goes through all strings in the array
			foreach (string i in input)
			{
				//If any strings contain illegal characters, sets the isclean variable to false
				if (i.Contains("(") || i.Contains(")") || i.Contains("'") || i.Contains("=") || i.Contains("-"))
				{
					isClean = false;
				}
			}
			return isClean;
		}
	}
}