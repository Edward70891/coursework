using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sortTesting
{
	static class sort
	{
		public static int[] quickSortInteger(int[] input)
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
			subArray = quickSortInteger(subArray);
			superArray = quickSortInteger(superArray);

			//Assembles the final array
			int[] result;
			result = common.appendArray(subArray, input[pivotIndex]);
			result = common.appendArray(result, superArray);

			//Returns the final array
			return result;
		}
	}
}
