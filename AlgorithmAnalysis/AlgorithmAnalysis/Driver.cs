using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmAnalysis
{
    class Driver
    {
        /// <summary>
        /// Mains the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            //Create the random object
            Random r = new Random();

            //Create a default list to popluate the other lists
            List<int> BaseList = new List<int>();

            for (int i = 0; i < 10; i++)
            {
                //Adds a random integer value
                BaseList.Add(r.Next());
            }

            //Create the other lists and set the list items to the default list
            List<int> Sink = new List<int>(BaseList);
            List<int> Selection = new List<int>(BaseList);
            List<int> Insertion = new List<int>(BaseList);
            List<int> Mergesort = new List<int>(BaseList);
            List<int> Quicksort = new List<int>(BaseList);
            List<int> QuicksortMedian = new List<int>(BaseList);
            List<int> Shell = new List<int>(BaseList);

            //Perform the sorting methods on the correct lists
            SinkSort(Sink);
            SelectionSort(Selection, Selection.Count);
            InsertionSort(Insertion);
            MergeSort(Mergesort);
            OriginalQuickSort(Quicksort);
            QuickMedianOfThreeSort(QuicksortMedian);
            ShellSort(Shell);
        }

        #region Algorithms
        /// <summary>
        /// Swaps the specified list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="n">The n.</param>
        /// <param name="m">The m.</param>
        public static void Swap(List<int> list, int n, int m)
        {
            int temp = list[n];
            list[n] = list[m];
            list[m] = temp;
        }

        /// <summary>
        /// Sinks the sort.
        /// </summary>
        /// <param name="list">The list.</param>
        public static void SinkSort(List<int> list)
        {
            bool sorted = false;
            int pass = 0;

            //Sort until sorted or enough passes completed
            while (!sorted && (pass < list.Count))
            {
                pass++;
                sorted = true; //Assume sorted until proven wrong

                for (int i = 0; i < list.Count - pass; i++)
                {
                    if (list[i] > list[i + 1]) //If out of order
                    {
                        Swap(list, i, i + 1); //Exchange
                        sorted = false;       //Found something out of order
                    }
                }
            }
        }

        /// <summary>
        /// Maximums the specified list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="n">The n.</param>
        /// <returns></returns>
        public static int Max(List<int> list, int n)
        {
            int max = 0;

            for (int i = 0; i < n; i++)
            {
                if (list[max] < list[i])
                    max = i;
            }

            return max;
        }

        /// <summary>
        /// Selections the sort.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="n">The n.</param>
        public static void SelectionSort(List<int> list, int n)
        {
            if (n <= 1)
                return;

            int max = Max(list, n);
            if (list[max] != list[n - 1])
                Swap(list, max, n - 1);

            SelectionSort(list, n - 1);
        }

        /// <summary>
        /// Insertions the sort.
        /// </summary>
        /// <param name="list">The list.</param>
        public static void InsertionSort(List<int> list)
        {
            int temp, j;

            for (int i = 1; i < list.Count; i++)
            {
                temp = list[i]; //Value to be inserted

                for (j = i; j > 0 && temp < list[j - 1]; j--) //Find location of value to be inserted
                {
                    list[j] = list[j - 1]; //Move items down to make room for it
                }

                list[j] = temp; //Insert new value
            }
        }

        /// <summary>
        /// Merges the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public static List<int> merge(List<int> left, List<int> right)
        {
            List<int> result = new List<int>();

            //Add smaller of tops of two lists to result as long as
            //both lists contain more values
            while (left.Count > 0 && right.Count > 0)
            {
                if (left[0] < right[0])
                {
                    result.Add(left[0]);
                    left.RemoveAt(0); //Discard
                }
                else
                {
                    result.Add(right[0]);
                    right.RemoveAt(0); //Discard
                }
            }

            //One of the two sublists is now empty
            //Add rest of left, if any
            while (left.Count > 0)
            {
                result.Add(left[0]);
                left.RemoveAt(0);
            }

            //Add rest of right, if any
            while (right.Count > 0)
            {
                result.Add(right[0]);
                right.RemoveAt(0);
            }
            return result;
        }

        /// <summary>
        /// Appends the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public static List<int> append(List<int> left, List<int> right)
        {
            List<int> result = new List<int>(left);

            foreach (int x in right)
                result.Add(x);
            return result;
        }

        /// <summary>
        /// Merges the sort.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns></returns>
        public static List<int> MergeSort(List<int> list)
        {
            if (list.Count <= 1) //Is there only 1 item in the list?
                return list;

            List<int> result = new List<int>();
            List<int> left = new List<int>();
            List<int> right = new List<int>();

            //Create left and right sublists of about hald the size of the list
            int middle = list.Count / 2;
            for (int i = 0; i < middle; i++)
                left.Add(list[i]);

            for (int i = middle; i < list.Count; i++)
                right.Add(list[i]);

            ////Recursively apply the MergeSort to each "half"
            left = MergeSort(left);
            right = MergeSort(right);

            //If all in right >= all in left, append right at end of left
            //to save time/steps in the next recursion
            if (left[left.Count - 1] <= right[0])
                return append(left, right);

            //Now merge the two, maintaining sorted order
            result = merge(left, right);
            return result;
        }

        /// <summary>
        /// Quicks the sort.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        public static void QuickSort(List<int> list, int start, int end)
        {
            int left = start;
            int right = end;

            if (left >= right)
                return;

            //Partition into left and right subsets
            while (left < right)
            {
                while (list[left] <= list[right] && left < right)
                    right--; //burn candle from right

                if (left < right) //exchange if needed
                    Swap(list, left, right);

                while (list[left] <= list[right] && left < right)
                    left++; //burn candle from left

                if (left < right) //exchange if needed
                    Swap(list, left, right);

                QuickSort(list, start, left - 1); //Recursively sort "left" partition
                QuickSort(list, right + 1, end); //Recursively sort "right" partition
            }
        }

        /// <summary>
        /// Originals the quick sort.
        /// </summary>
        /// <param name="list">The list.</param>
        public static void OriginalQuickSort(List<int> list)
        {
            QuickSort(list, 0, list.Count - 1);
        }

        /// <summary>
        /// Inserts the sort.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        public static void InsertSort(List<int> list, int start, int end)
        {
            int temp, j;

            for (int i = start + 1; i <= end; i++)
            {
                temp = list[i];

                for (j = i; j > start && temp < list[j - 1]; j--)
                {
                    list[j] = list[j - 1];
                }

                list[j] = temp;
            }
        }

        /// <summary>
        /// Quicks the median of three sort.
        /// </summary>
        /// <param name="list">The list.</param>
        public static void QuickMedianOfThreeSort(List<int> list)
        {
            QuickMedOfThreeSort(list, 0, list.Count - 1);
        }

        /// <summary>
        /// Quicks the med of three sort.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        public static void QuickMedOfThreeSort(List<int> list, int start, int end)
        {
            const int cutoff = 10; //Point at which we switch to InsertionSort

            if (start + cutoff > end)
                InsertSort(list, start, end);

            else
            {
                int middle = (start + end) / 2; //Find the median of the three for the pivot
                if (list[middle] < list[start]) //by sorting them and pivot is
                    Swap(list, start, middle);  //in the middle position

                if (list[end] < list[start])
                    Swap(list, start, end);

                if (list[end] < list[middle])
                    Swap(list, middle, end - 1);

                //Place pivot at Position(end - 1) since we know that list[end] >= list[middle]
                int pivot = list[middle];
                Swap(list, middle, end - 1);

                //Begin partitioning
                int left, right;
                for (left = start, right = end - 1; ; )
                {
                    while (list[++left] < pivot)
                        ;
                    while (pivot < list[--right])
                        ;
                    if (left < right)
                        Swap(list, left, right);
                    else
                        break;
                }

                //Restore pivot
                Swap(list, left, end - 1);

                QuickMedOfThreeSort(list, start, left - 1); //Recursively sort left subset
                QuickMedOfThreeSort(list, left + 1, end); //Recursively sort right subset
            }
        }

        /// <summary>
        /// Shells the sort.
        /// </summary>
        /// <param name="list">The list.</param>
        private static void ShellSort(List<int> list)
        {
            //Start with gap of N/2; divide by 2.2 each time until it reaches one or zero
            for (int gap = list.Count / 2; gap > 0; gap = (gap == 2 ? 1 : (int)(gap / 2.2)))
            {
                //Sort a subset by insertion
                int temp, j;
                for (int i = gap; i < list.Count; i++)
                {
                    temp = list[i];

                    for (j = i; j >= gap && temp < list[j - gap]; j -= gap)
                    {
                        list[j] = list[j - gap];
                    }

                    list[j] = temp;
                }
            }
            #endregion

        }
    }
}