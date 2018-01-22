using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.OleDb;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace commonClasses
{
	//Generic logging of events throughout the program
	public class customLogging
	{
		/// <summary>
		/// Logs the creation of a new session
		/// </summary>
		public static void newSession()
		{
			string result;
			string dividerASCII = "------------------------------------------";
			result = dividerASCII + generateTimestamp() + dividerASCII;
			writeEntry("");
			writeEntry(result);
		}

		/// <summary>
		/// Writes a timestamped entry to the logfile
		/// </summary>
		/// <param name="entryText">The text that will be written to the logfile</param>
		public static void newEntry(string entryText)
		{
			string result = generateTimestamp() + " " + entryText;
			writeEntry(result);
		}

		/// <summary>
		/// Writes a timestamped entry to the logfile with the exception message
		/// </summary>
		/// <param name="except">The exception thrown</param>
		public static void newException(Exception except)
		{
			string result = generateTimestamp() + " An error occurred in " + except.Source + " with message " + except.Message;
			writeEntry(result);
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

	/// <summary>
	/// A small class containing some tools for sanitizing string inputs so SQL injection can't happen
	/// </summary>
	public class customSecurity
	{
		/// <summary>
		/// A constant error message for easy consistency
		/// </summary>
		public const string sanitizeErrorMessage = "The(, ), +, -, = and ' characters are not valid in ANY fields";

		/// <summary>
		/// Check if the code contains (, ), +, -, = or '
		/// </summary>
		/// <param name="input">An array containing all strings to be checked</param>
		/// <returns>True if clean, false if dirty (contains SQL characters)</returns>
		public static bool sanitizeCheck(string[] input)
		{
			bool isClean = true;
			//Goes through all strings in the array
			foreach (string i in input)
			{
				//If any strings contain illegal characters, sets the isclean variable to false and skips the rest of the loops
				if (i.Contains("(") || i.Contains(")") || i.Contains("'") || i.Contains("=") || i.Contains("-") || i.Contains("+"))
				{
					isClean = false;
					break;
				}

			}
			return isClean;
		}

		public static string generateMD5(string input)
		{
			var md5 = System.Security.Cryptography.MD5.Create();
			byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
			byte[] hash = md5.ComputeHash(inputBytes);

			var sb = new System.Text.StringBuilder();
			for (int i = 0; i < hash.Length; i++)
			{
				sb.Append(hash[i].ToString("X2"));
			}
			return sb.ToString();
		}
	}
}