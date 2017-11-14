using System;
using System.IO;

namespace commonClasses
{
	public class customLogging
	{
		string getTimestamp()
		{
			string timestamp;
			timestamp = "[" + Convert.ToString(DateTime.Now) + "] ";
			return timestamp;
		}

		/// <summary>
		/// <para>Write a timestamped session divider to the logfile</para>
		/// </summary>
		public static void newSession()
		{
			string result;
			string dividerASCII = "------------------------------------------";
			result = dividerASCII + generateTimestamp() + dividerASCII;
			writeEntry("");
			writeEntry(result);
		}

		public static void endSession()
		{
			string entryText = generateTimestamp() + "Session ended";
		}

		/// <summary> 
		/// <para>Write a timestamped entry to the logfile with the provided text</para>
		/// <para>Type must be "system", "user" or "other" so far</para>
		/// </summary>
		public static void newEntry(string entryText, string type)
		{
			string result;
			switch (type)
			{
				case "user":
					result = "USR";
					break;
				case "other":
					result = "OTR";
					break;
				case "system":
					result = "SYS";
					break;
				default:
					throw new ArgumentException("Type must be 'system', 'user' or 'other'");
			}
			result += generateTimestamp() + " " + entryText;
			writeEntry(entryText);
		}

		//Fix me better!
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


		private static string generateTimestamp()
		{
			string output = "[" + Convert.ToString(DateTime.Now) + "]";
			return output;
		}
	}
}