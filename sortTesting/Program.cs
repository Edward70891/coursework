using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sortTesting
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Please input all the numbers you want sorting (separated by pressing enter), followed by any non numberic input to terminate the list.");

			List<int> inputs = new List<int>();
			int[] toSort;
			int[] sorted;
			while (true)
			{
				int inputInt;
				if (int.TryParse(Console.ReadLine(), out inputInt))
				{
					inputs.Add(inputInt);
				}
				else
				{
					toSort = inputs.ToArray();
					inputs = null;
					break;
				}
			}

			Console.Clear();
			Console.WriteLine("You've given the following list to be sorted:");
			printArray(toSort);

			sorted = quickSort(toSort);

			Console.WriteLine();
			Console.WriteLine("The algorithm gave the following sorted list:");
			printArray(sorted);

			Console.Read();
		}

		static int[] quickSort(int[] input)
		{
			if (input.Length == 1)
			{
				return input;
			}
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

			int[] subArray;
			List<int> subList = new List<int>();
			int[] superArray;
			List<int> superList = new List<int>();
			int pivotIndex = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(input.Length) / 2)) - 1;

			for (int i = 0; i < input.Length; i++)
			{
				if (i == pivotIndex)
				{
					continue;
				}
				else if (input[i] <= input[pivotIndex])
				{
					subList.Add(input[i]);
				}
				else if (input[i] > input[pivotIndex])
				{
					superList.Add(input[i]);
				}
			}
			subArray = quickSort(subList.ToArray());
			superArray = quickSort(superList.ToArray());

			int finalLength = subArray.Length + superArray.Length + 1;
			int[] result = subArray;
			int[] pivotArray = new int[1];
			pivotArray[0] = input[pivotIndex];
			appendArray(ref result, ref pivotArray);
			appendArray(ref result, ref superArray);

			printArray(result);
			return result;
		}

		static void appendArray<T>(ref T[] baseArray, ref T[] addition)
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
			baseArray = result;
		}

		static void printArray<T>(T[] input)
		{
			foreach (T current in input)
			{
				Console.WriteLine(Convert.ToString(current) + ", ");
			}
		}
	}
}
