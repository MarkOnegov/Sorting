using System;
using System.Collections.Generic;

namespace Sorting
{
	class Program
	{
		static readonly Random random = new Random();
		enum ArrSort
		{
			Up, Down, Random
		}
		static List<int> GenerateArr(int n, ArrSort arrSort)
		{
			var l = new List<int>();
			for (int i = 0; i < n; i++)
			{
				switch (arrSort)
				{
					case ArrSort.Up:
						l.Add(i);
						break;
					case ArrSort.Down:
						l.Add(n - i);
						break;
					case ArrSort.Random:
						l.Add(random.Next());
						break;
					default:
						throw new ArgumentException("Not Available Sort");
				}
			}
			return l;
		}
		static List<T> Swap<T>(List<T> list, int i, int j)
		{
			T value = list[i];
			list[i] = list[j];
			list[j] = value;
			return list;
		}
		static List<T> SelectSort<T>(List<T> list)
		{
			var n = list.Count;
			for (var i = 0; i < n - 1; i++)
			{
				var min = i;
				for (var j = i + 1; j < n; j++)
					if (Comparer<T>.Default.Compare(list[j], list[min]) < 0)
						min = j;
				if (min != i) Swap(list, min, i);
			}
			return list;
		}
		static List<T> BubbleSort<T>(List<T> list)
		{
			var n = list.Count;
			for (var i = 0; i < n - 1; i++)
			{
				var swaped = false;
				for (var j = 1; j < n - i; j++)
					if (Comparer<T>.Default.Compare(list[j - 1], list[j]) > 0)
					{
						swaped = true;
						Swap(list, j - 1, j);
					}
				if (!swaped) return list;
			}
			return list;
		}
		static List<T> InsertSort<T>(List<T> list)
		{
			var n = list.Count;
			for (var i = 1; i < n; i++)
			{
				T k = list[i];
				int j;
				for (j = i - 1; j >= 0 && Comparer<T>.Default.Compare(list[j], k) > 0; j--) list[j + 1] = list[j];
				list[j + 1] = k;
			}
			return list;
		}
		static bool Check<T>(List<T> list)
		{
			var n = list.Count;
			for (var i = 1; i < n; i++)
				if (Comparer<T>.Default.Compare(list[i - 1], list[i]) > 0) return false;
			return true;
		}

		static void Main()
		{
			Console.BufferWidth = 140;
			Console.WindowWidth = 140;
			Console.WriteLine("{0,7}|{1,32}|{2,32}|{3,32}|{4,32}", "n", "SelectSort", "BubbleSort", "InsertSort", "Array.Sort");
			Console.Write("       ");
			for (var i = 0; i <= 3; i++)
				Console.Write("|        Up|      Down|    Random");
			Console.WriteLine();
			for (var i = 1; i < 140; i++)
				Console.Write("-");
			Console.WriteLine();
			//var n = 100;
			for (var n = 50000; n <= 200000; n += 25000)
			{
				Console.Write("{0,7}", n);
				var lists = new List<List<int>>()
				{
					GenerateArr(n,ArrSort.Up),
					GenerateArr(n,ArrSort.Down),
					GenerateArr(n,ArrSort.Random)
				};
				for (var i = 0; i < lists.Count; i++)
				{
					var t = new List<int>(lists[i]);
					DateTime start = DateTime.Now;
					SelectSort(t);
					DateTime end = DateTime.Now;
					Console.Write("|{0,10}", (int)(end - start).TotalMilliseconds);
				}
				for (var i = 0; i < lists.Count; i++)
				{
					var t = new List<int>(lists[i]);
					DateTime start = DateTime.Now;
					BubbleSort(t);
					DateTime end = DateTime.Now;
					Console.Write("|{0,10}", (int)(end - start).TotalMilliseconds);
				}
				for (var i = 0; i < lists.Count; i++)
				{
					var t = new List<int>(lists[i]);
					DateTime start = DateTime.Now;
					InsertSort(t);
					DateTime end = DateTime.Now;
					Console.Write("|{0,10}", (int)(end - start).TotalMilliseconds);
				}
				for (var i = 0; i < lists.Count; i++)
				{
					var t = new List<int>(lists[i]).ToArray();
					DateTime start = DateTime.Now;
					Array.Sort(t);
					DateTime end = DateTime.Now;
					Console.Write("|{0,10}", (int)(end - start).TotalMilliseconds);
				}
				Console.Beep();
				Console.WriteLine();
			}
			Console.ReadKey();
		}
	}
}
