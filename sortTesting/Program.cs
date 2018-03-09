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
				Console.WriteLine("Input 1 for entering your own values, 2 for randomly generated ones or 0 for exit");
				string choice = Console.ReadLine();
				if (choice == "1")
				{
					sortUserInput();
				}
				else if (choice == "2")
				{
					sortRandomNumbers();
				}
				else if (choice == "9")
				{
					break;
				}
				else
				{
					Console.WriteLine("Invalid input");
				}
			}
		}

		static void sortRandomNumbers()
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
			sortContainer(inputs.ToArray());
		}

		static void sortUserInput()
		{
			Console.Clear();
			Console.WriteLine("Please input all the numbers you want sorting (separated by pressing enter), followed by any non numberic input to terminate the list.");

			List<int> userInputs = new List<int>();
			int[] elements;
			bool firstInput = true;
			while (true)
			{
				int inputInt;
				if (int.TryParse(Console.ReadLine(), out inputInt))
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

			sortContainer(elements);
		}

		static void sortContainer(int[] elements)
		{
			Console.Clear();
			Console.WriteLine("You've given the following list to be sorted:");
			outputArray(elements);

			Stopwatch timer = new Stopwatch();
			timer.Start();
			elements = quickSortNumeric(elements);
			timer.Stop();

			Console.WriteLine();
			Console.WriteLine("The algorithm gave the following sorted list in " + timer.ElapsedMilliseconds + " ms or " + timer.ElapsedTicks + "ticks:");
			outputArray(elements);

			Console.Read();
		}

		static int[] quickSortNumeric(int[] input)
		{
			//All the base cases
			//If it is passed an array with a single (or no) element, return just that
			if (input.Length <= 1)
			{
				return input;
			}
			//If it is passed an array with two elements, check if they need swapping and do so if necessary, then return them
			else if (input.Length == 2)
			{
				if (input[0] > input[1])
				{
					int temp = input[0];
					input[0] = input[1];
					input[1] = temp;
				}
				return input;
			}

			//Initialize the needed variables; the two lists to add the numbers to, the arrays to add those to, and the index of the pivot
			int[] subArray;
			List<int> subList = new List<int>();
			int[] superArray;
			List<int> superList = new List<int>();
			int pivotIndex = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(input.Length) / 2)) - 1;

			//The actual sort, cycle through all the elements in the array
			for (int i = 0; i < input.Length; i++)
			{
				//If it's looking at the pivot, don't sort it
				if (i == pivotIndex)
				{
					continue;
				}
				//If the current element is smaller than or equal to the pivot, add it to the sublist
				else if (input[i] <= input[pivotIndex])
				{
					subList.Add(input[i]);
				}
				//If the current element is larger than the picot, add it to the superlist
				else if (input[i] > input[pivotIndex])
				{
					superList.Add(input[i]);
				}
			}

			//Calls itself on the two arrays we produce from the lists
			subArray = subList.ToArray();
			superArray = superList.ToArray();
			subArray = quickSortNumeric(subArray);
			superArray = quickSortNumeric(superArray);

			//Assembles the final array
			int[] result;
			result = appendArray(subArray, input[pivotIndex]);
			result = appendArray(result, superArray);
			
			//Returns the final array
			return result;
		}

		static T[] appendArray<T>(T[] baseArray, T[] addition)
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
		static T[] appendArray<T>(T[] baseArray, T addition)
		{
			T[] result = new T[baseArray.Length + 1];
			for (int i = 0; i < baseArray.Length; i++)
			{
				result[i] = baseArray[i];
			}
			result[baseArray.Length] = addition;
			return result;
		}

		static void outputArray<T>(T[] input)
		{
			for (int i = 0; i < input.Length; i++)
			{
				if (i == input.Length - 1)
				{
					Console.WriteLine(Convert.ToString(input[i]));
				}
				else
				{
					Console.Write(Convert.ToString(input[i]) + ", ");
				}
			}
		}
	}
}
