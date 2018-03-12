using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.OleDb;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using mainCoursework;

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
		public const string sanitizeErrorMessage = "All fields must be full. The (, ), +, -, = and ' characters are not allowed in ANY fields";

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
				if (i == "")
				{
					isClean = false;
					break;
				}

			}
			return isClean;
		}

		/// <summary>
		/// MD5 Hasher
		/// </summary>
		/// <param name="input">The string to be hashed</param>
		/// <returns>A hexadecimal value (with lowercase letters)</returns>
		public static string generateMD5(string input)
		{
			//Make a new md5 object
			var md5 = System.Security.Cryptography.MD5.Create();
			byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
			byte[] hash = md5.ComputeHash(inputBytes);
			//Make a new string and add all the new bytes to it
			string output = "";
			for (int i = 0; i < hash.Length; i++)
			{
				output = output + hash[i].ToString("x2");
			}
			return output;
		}
	}

	static class common
	{
		public static T[] appendArray<T>(T[] baseArray, T[] addition)
		{
			T[] result = new T[baseArray.Length + addition.Length];
			for (int i = 0; i < baseArray.Length; i++)
			{
				result[i] = baseArray[i];
			}
			for (int i = baseArray.Length; i < result.Length; i++)
			{
				result[i] = addition[i - baseArray.Length];
			}
			return result;
		}
		public static T[] appendArray<T>(T[] baseArray, T addition)
		{
			T[] result = new T[baseArray.Length + 1];
			for (int i = 0; i < baseArray.Length; i++)
			{
				result[i] = baseArray[i];
			}
			result[baseArray.Length] = addition;
			return result;
		}

		public static string formatPrice(decimal input)
		{
			string output = Convert.ToString(input);
			try
			{
				if (output.Substring(output.IndexOf('.') + 1).Length == 0)
				{
					output = output + "00";
				}
				else if (output.Substring(output.IndexOf('.') + 1).Length == 1)
				{
					output = output + "0";
				}
			}
			catch
			{
				output = output + ".00";
			}
			return output;
		}
	}
}