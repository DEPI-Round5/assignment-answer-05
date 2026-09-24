using System;
using System.Collections;
using System.Collections.Generic;

namespace AdvancedCSharpAssignment1
{
    // =========================================================================
    // Question 2: Generic Range<T> Class
    // =========================================================================
    public class Range<T> where T : IComparable<T>
    {
        public T Min { get; set; }
        public T Max { get; set; }

        public Range(T min, T max)
        {
            Min = min;
            Max = max;
        }

        // Checks if a value is inside the range
        public bool IsInRange(T value)
        {
            return value.CompareTo(Min) >= 0 && value.CompareTo(Max) <= 0;
        }

        // Calculates length between Max and Min (using dynamic for numeric types)
        public dynamic Length()
        {
            dynamic maxVal = Max;
            dynamic minVal = Min;
            return maxVal - minVal;
        }
    }


    // =========================================================================
    // Question 5: Generic FixedSizeList<T> Class
    // =========================================================================
    public class FixedSizeList<T>
    {
        private T[] items;
        private int count;

        public int Capacity { get; }

        public FixedSizeList(int capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentException("Capacity must be greater than zero.");
            }

            Capacity = capacity;
            items = new T[capacity];
            count = 0;
        }

        // Adds item if there is empty space
        public void Add(T item)
        {
            if (count >= Capacity)
            {
                throw new InvalidOperationException("List is full. Cannot add more elements.");
            }

            items[count] = item;
            count++;
        }

        // Gets item by index
        public T Get(int index)
        {
            if (index < 0 || index >= count)
            {
                throw new IndexOutOfRangeException("Invalid index.");
            }

            return items[index];
        }

        public int Count => count;
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            // =========================================================================
            // Question 1: Optimized Bubble Sort
            // Optimization Explanation:
            // Standard Bubble Sort always runs O(n^2) loops even if the array becomes sorted early.
            // We can optimize it by adding a boolean flag (isSwapped). If no elements were swapped 
            // in an entire inner loop pass, the array is already sorted, so we can stop early.
            // =========================================================================
            Console.WriteLine("----- Question 1: Optimized Bubble Sort -----");
            int[] numbers = { 64, 34, 25, 12, 22, 11, 90 };
            
            Console.WriteLine("Original Array: " + string.Join(", ", numbers));
            OptimizedBubbleSort(numbers);
            Console.WriteLine("Sorted Array:   " + string.Join(", ", numbers));

            Console.WriteLine("\n-------------------------------------------------------------------------\n");

            // =========================================================================
            // Question 2: Testing Generic Range<T> Class
            // =========================================================================
            Console.WriteLine("----- Question 2: Generic Range<T> -----");
            Range<int> intRange = new Range<int>(10, 50);
            
            Console.WriteLine("Is 25 in range (10-50)? " + intRange.IsInRange(25));
            Console.WriteLine("Is 5 in range (10-50)?  " + intRange.IsInRange(5));
            Console.WriteLine("Range Length: " + intRange.Length());

            Console.WriteLine("\n-------------------------------------------------------------------------\n");

            // =========================================================================
            // Question 3: Reverse ArrayList In-Place
            // =========================================================================
            Console.WriteLine("----- Question 3: Reverse ArrayList In-Place -----");
            ArrayList list = new ArrayList() { 1, 2, 3, 4, 5, 6 };

            Console.Write("Original ArrayList: ");
            PrintArrayList(list);

            ReverseArrayListInPlace(list);

            Console.Write("Reversed ArrayList: ");
            PrintArrayList(list);

            Console.WriteLine("\n-------------------------------------------------------------------------\n");

            // =========================================================================
            // Question 4: Get Even Numbers from Integer List
            // =========================================================================
            Console.WriteLine("----- Question 4: Get Even Numbers -----");
            List<int> origList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            List<int> evenList = GetEvenNumbers(origList);

            Console.WriteLine("Original List: " + string.Join(", ", origList));
            Console.WriteLine("Even Numbers:  " + string.Join(", ", evenList));

            Console.WriteLine("\n-------------------------------------------------------------------------\n");

            // =========================================================================
            // Question 5: Testing FixedSizeList<T> Class
            // =========================================================================
            Console.WriteLine("----- Question 5: FixedSizeList<T> -----");
            try
            {
                FixedSizeList<string> fixedList = new FixedSizeList<string>(3);
                fixedList.Add("Ahmed");
                fixedList.Add("Mohamed");
                fixedList.Add("Saeed");

                Console.WriteLine("Element at index 0: " + fixedList.Get(0));
                Console.WriteLine("Element at index 1: " + fixedList.Get(1));

                // Trying to add 4th item to full list (will throw exception)
                Console.WriteLine("Trying to add a 4th element...");
                fixedList.Add("Extra");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Caught: " + ex.Message);
            }

            Console.WriteLine("\n-------------------------------------------------------------------------\n");

            // =========================================================================
            // Question 6: First Non-Repeated Character Index using Dictionary
            // =========================================================================
            Console.WriteLine("----- Question 6: First Non-Repeated Character -----");
            string testStr = "swiss";
            int index = FirstNonRepeatedCharIndex(testStr);

            Console.WriteLine($"String: \"{testStr}\"");
            if (index != -1)
            {
                Console.WriteLine($"First non-repeated character is '{testStr[index]}' at index {index}.");
            }
            else
            {
                Console.WriteLine("No non-repeated character found.");
            }

            Console.WriteLine("\n=========================================================================");
            Console.WriteLine("End of Assignment 1 Advanced C#");
            Console.WriteLine("=========================================================================");

            Console.ReadLine(); // Keeps console open
        }

        // =========================================================================
        // Question 1 Helper Method
        // =========================================================================
        static void OptimizedBubbleSort(int[] arr)
        {
            int n = arr.Length;
            bool isSwapped;

            for (int i = 0; i < n - 1; i++)
            {
                isSwapped = false;

                for (int j = 0; j < n - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        // Swap elements
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;

                        isSwapped = true;
                    }
                }

                // If no two elements were swapped, array is sorted
                if (!isSwapped)
                {
                    break;
                }
            }
        }

        // =========================================================================
        // Question 3 Helper Methods
        // =========================================================================
        static void ReverseArrayListInPlace(ArrayList list)
        {
            int left = 0;
            int right = list.Count - 1;

            while (left < right)
            {
                // Swap elements at left and right pointers
                object temp = list[left];
                list[left] = list[right];
                list[right] = temp;

                left++;
                right--;
            }
        }

        static void PrintArrayList(ArrayList list)
        {
            foreach (var item in list)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }

        // =========================================================================
        // Question 4 Helper Method
        // =========================================================================
        static List<int> GetEvenNumbers(List<int> numbers)
        {
            List<int> result = new List<int>();

            foreach (int num in numbers)
            {
                if (num % 2 == 0)
                {
                    result.Add(num);
                }
            }

            return result;
        }

        // =========================================================================
        // Question 6 Helper Method
        // =========================================================================
        static int FirstNonRepeatedCharIndex(string str)
        {
            Dictionary<char, int> charCounts = new Dictionary<char, int>();

            // Count occurrences of each character
            foreach (char c in str)
            {
                if (charCounts.ContainsKey(c))
                {
                    charCounts[c]++;
                }
                else
                {
                    charCounts[c] = 1;
                }
            }

            // Find index of the first character with count 1
            for (int i = 0; i < str.Length; i++)
            {
                if (charCounts[str[i]] == 1)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}