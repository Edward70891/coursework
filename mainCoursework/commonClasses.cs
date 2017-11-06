using System;

namespace commonClasses
{
	public class customLogging
	{
		public string getTimestamp()
		{
			string timestamp;
			timestamp = "[" + Convert.ToString(DateTime.Now) + "] ";
			return timestamp;
		}

		public static void newUserEntry(string entryText)
		{
			string result = "USR [" + Convert.ToString(DateTime.Now) + "] " + entryText;
			using (System.IO.StreamWriter logFile = new System.IO.StreamWriter(@"\\albert\2011\R04637\Computer Science\coursework\mainCoursework\App_Data\log.txt", true)) //Look this up; it won't work on different computers
			{
				logFile.WriteLine(result);
			}
		}

		public static void newOtherEntry(string entryText)
		{

		}
	}
}