using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sortTesting
{
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

		public static void outputArray<T>(T[] input)
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
