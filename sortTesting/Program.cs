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
			foreach (int current in toSort)
			{
				Console.Write(current + ", ");
			}

			sorted = quickSort(toSort);

			Console.WriteLine();
			Console.WriteLine("The algorithm gave the following sorted list:");
			foreach (int i in sorted)
			{
				Console.Write(i + ", ");
			}
			Console.Read();
		}

		static int[] quickSort(int[] input)
		{
			if (input.Length == 1)
			{
				return input;
			}

			int[] subArray;
			List<int> subList = new List<int>();
			int[] superArray;
			List<int> superList = new List<int>();
			int pivotIndex = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(input.Length) / 2));

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
			int[] result = new int[finalLength];

			for (int i = 0; i < finalLength; i++)
			{
				if (i < subArray.Length)
				{
					result[i] = subArray[i];
				}
				else if (i == subArray.Length)
				{
					result[i] = input[pivotIndex];
				}
				else
				{
					result[i] = superArray[i - subArray.Length];
				}
			}

			return result;
		}
	}
}
