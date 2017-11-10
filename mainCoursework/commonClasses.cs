using System;

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
			result += "[" + Convert.ToString(DateTime.Now) + "] " + entryText;
			using (System.IO.StreamWriter logFile = new System.IO.StreamWriter(@"\\albert\2011\R04637\Computer Science\coursework\mainCoursework\App_Data\log.txt", true)) //Look this up; it won't work on different computers
			{
				logFile.WriteLine(result);
			}
		}
	}
}