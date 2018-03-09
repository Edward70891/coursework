using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace sortTesting
{
	class Program
	{
		static void Main(string[] args)
		{
			while (true)
			{
				Console.Clear();
				Console.WriteLine("Input 1 to sort integers, 2 to sort decimals, 3 to sort strings or 0 to exit");
				string choice = Console.ReadLine();
				if (choice == "1")
				{
					sortInts();
				}
				else if (choice == "2")
				{
					sortDecimals();
				}
				else if (choice == "3")
				{
					sortStrings();
				}
				else if (choice == "0")
				{
					break;
				}
				else
				{
					Console.WriteLine("Invalid input");
				}
			}
		}

		//----------------------------------------------------------------------------------------------------------------------------------------------------------------

		static void sortInts()
		{
			while (true)
			{
				Console.Clear();
				Console.WriteLine("Input 1 to enter your own integers, 2 to randomly generate ones or 0 to go back");
				string choice = Console.ReadLine();
				if (choice == "1")
				{
					sortUserInputInts();
				}
				else if (choice == "2")
				{
					sortRandomInts();
				}
				else if (choice == "0")
				{
					break;
				}
				else
				{
					Console.WriteLine("Invalid input");
				}
			}
		}

		static void sortRandomInts()
		{
			Console.Clear();
			Console.WriteLine("How many numbers do you want to generate?");
			int count = Convert.ToInt32(Console.ReadLine());
			Console.WriteLine("What do you want the upper limit to be?");
			int limit = Convert.ToInt32(Console.ReadLine());
			Random generator = new Random();
			List<int> inputs = new List<int>();
			for (int i = 0; i <= count; i++)
			{
				inputs.Add(Convert.ToInt32(Math.Ceiling(generator.NextDouble() * limit)));
			}
			intSortContainer(inputs.ToArray());
		}

		static void sortUserInputInts()
		{
			Console.Clear();
			Console.WriteLine("Please input all the numbers you want sorting (separated by pressing enter), followed by any non numberic input to terminate the list.");

			List<int> userInputs = new List<int>();
			int[] elements;
			bool firstInput = true;
			while (true)
			{
				if (int.TryParse(Console.ReadLine(), out int inputInt))
				{
					userInputs.Add(inputInt);
					firstInput = false;
				}
				else if (firstInput == true)
				{
					Console.Clear();
					Console.WriteLine("You must enter at least one number!");
					continue;
				}
				else
				{
					elements = userInputs.ToArray();
					userInputs = null;
					break;
				}
			}

			intSortContainer(elements);
		}

		static void intSortContainer(int[] elements)
		{
			bool ascending;
			while (true)
			{
				Console.WriteLine("Input 1 for ascending order, 2 for descending order");
				string ascendingChoice = Console.ReadLine();
				if (ascendingChoice == "1")
				{
					ascending = true;
					break;
				}
				else if (ascendingChoice == "2")
				{
					ascending = false;
					break;
				}
				else
				{
					Console.WriteLine("Invalid input.");
				}
			}

			Console.Clear();
			Console.WriteLine("You've given the following list to be sorted:");
			common.outputArray(elements);

			Stopwatch timer = new Stopwatch();
			timer.Start();
			elements = sort.quickSortInteger(elements, ascending);
			timer.Stop();

			Console.WriteLine();
			Console.WriteLine("The algorithm gave the following sorted list in " + timer.ElapsedMilliseconds + " ms or " + timer.ElapsedTicks + "ticks:");
			common.outputArray(elements);

			Console.Read();
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------------------------

		static void sortDecimals()
		{
			while (true)
			{
				Console.Clear();
				Console.WriteLine("How many numbers do you want to generate? (Enter a non-numeric input to go back)");
				string input = Console.ReadLine();
				if (int.TryParse(input, out int count) != true)
				{
					break;
				}
				Console.WriteLine("What do you want the upper limit to be?");
				int limit = Convert.ToInt32(Console.ReadLine());
				Random generator = new Random();
				List<decimal> inputs = new List<decimal>();
				for (int i = 0; i <= count; i++)
				{
					inputs.Add(Convert.ToDecimal(generator.NextDouble() * limit));
				}
				decimalSortContainer(inputs.ToArray());
			}
		}

		static void decimalSortContainer(decimal[] elements)
		{
			bool ascending;
			while (true)
			{
				Console.WriteLine("Input 1 for ascending order, 2 for descending order");
				string ascendingChoice = Console.ReadLine();
				if (ascendingChoice == "1")
				{
					ascending = true;
					break;
				}
				else if (ascendingChoice == "2")
				{
					ascending = false;
					break;
				}
				else
				{
					Console.WriteLine("Invalid input.");
				}
			}

			Console.Clear();
			Console.WriteLine("You've given the following list to be sorted:");
			common.outputArray(elements);

			Stopwatch timer = new Stopwatch();
			timer.Start();
			elements = sort.quickSortDecimal(elements, ascending);
			timer.Stop();

			Console.WriteLine();
			Console.WriteLine("The algorithm gave the following sorted list in " + timer.ElapsedMilliseconds + " ms or " + timer.ElapsedTicks + "ticks:");
			common.outputArray(elements);

			Console.Read();
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------------------------

		static void sortStrings()
		{
			while (true)
			{
				Console.Clear();
				Console.WriteLine("Input 1 to enter your own strings, 2 to randomly generate ones or 0 to go back");
				string choice = Console.ReadLine();
				if (choice == "1")
				{
					sortUserInputStrings();
				}
				else if (choice == "2")
				{
					sortRandomStrings();
				}
				else if (choice == "0")
				{
					break;
				}
				else
				{
					Console.WriteLine("Invalid input");
				}
			}
		}

		static void sortUserInputStrings()
		{
			Console.Clear();
			Console.WriteLine("Please input all the strings you want sorting (separated by pressing enter), followed by any numberic input to terminate the list.");

			List<string> userInputs = new List<string>();
			string[] elements;
			bool firstInput = true;
			while (true)
			{
				string inputString = Console.ReadLine();
				if (int.TryParse(inputString, out int stuff) == false)
				{
					userInputs.Add(inputString);
					firstInput = false;
				}
				else if (firstInput == true)
				{
					Console.Clear();
					Console.WriteLine("You must enter at least one number!");
					continue;
				}
				else
				{
					elements = userInputs.ToArray();
					userInputs = null;
					break;
				}
			}

			stringSortContainer(elements);
		}

		static void sortRandomStrings()
		{
			Console.Clear();
			Console.WriteLine("How many strings do you want to generate?");
			int count = Convert.ToInt32(Console.ReadLine());
			Console.WriteLine("How many letters per string?");
			int length = Convert.ToInt32(Console.ReadLine());
			Random generator = new Random();
			List<string> inputs = new List<string>();
			for (int i = 0; i <= count; i++)
			{
				char[] characters = new char[length];
				for (int o = 0; o < length; o++)
				{
					characters[o] = (char)Convert.ToInt32(Math.Ceiling((generator.NextDouble() * 26) + 64));
				}
				string element = new string(characters);
				inputs.Add(element);
			}
			stringSortContainer(inputs.ToArray());
		}

		static void stringSortContainer(string[] elements)
		{
			bool ascending;
			while (true)
			{
				Console.WriteLine("Input 1 for ascending order, 2 for descending order");
				string ascendingChoice = Console.ReadLine();
				if (ascendingChoice == "1")
				{
					ascending = true;
					break;
				}
				else if (ascendingChoice == "2")
				{
					ascending = false;
					break;
				}
				else
				{
					Console.WriteLine("Invalid input.");
				}
			}

			Console.Clear();
			Console.WriteLine("You've given the following list to be sorted:");
			common.outputArray(elements);

			Stopwatch timer = new Stopwatch();
			timer.Start();
			elements = sort.quickSortString(elements, ascending);
			timer.Stop();

			Console.WriteLine();
			Console.WriteLine("The algorithm gave the following sorted list in " + timer.ElapsedMilliseconds + " ms or " + timer.ElapsedTicks + "ticks:");
			common.outputArray(elements);

			Console.Read();
		}
	}
}
