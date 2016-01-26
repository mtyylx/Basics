using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace SortingAlgorithms
{
    class Tester
    {
        static void Main(string[] args)
        {
            int arraySize = 10;
            int arrayUpperLimits = 100;
            int repeatTimes = 1;
            bool enableLogging = true;

            int[] inputArray;
            double[] executionTime = new double[repeatTimes]; 
            Stopwatch stopwatch = new Stopwatch();

            //Slow - O(n^2)
            InsertionSort mInsertionSort = new InsertionSort();
            SelectionSort mSelectionSort = new SelectionSort();
            BubbleSort mBubbleSort = new BubbleSort();

            //Fast - O(n*lgn)
            MergeSort mMergeSort = new MergeSort();
            HeapSort mHeapSort = new HeapSort();
            QuickSort mQuickSort = new QuickSort();
           
            //Linear - O(n)
            CountingSort mCountingSort = new CountingSort();
            RadixSort mRadixSort = new RadixSort();
            BucketSort mBucketSort = new BucketSort();

            //Test the execution time repeatly.
            for (int i = 0; i < repeatTimes; i++)
            {
                //Always create new Array object, even though the contents are the same.
                //Because each array will be modified at the end of each loop.
                inputArray = ArrayHandler.Create(arraySize, enableLogging, arrayUpperLimits);
                //inputArray = ArrayHandler.CreateAlmostSorted(arraySize, enableLogging);
                //inputArray = new int[] { 122, 44, 122, 55, 33, 55, 44, 23};
                stopwatch.Start();
                //----------------------------------------------------------------------------------------//
                //             A L G O R I T H M         T E S T E D        H E R E                       //
                //----------------------------------------------------------------------------------------//

                //------- 0.Sort in VC# -------
                //Array.Sort(inputArray);                                                         //7ms 10^5

                //#####################################  O(n*n)  #############################################

                //------- 1.Insertion Sort ~ O(n^2) -------
                mInsertionSort.Sort(inputArray);                                                //95ms 10^4
                //mInsertionSort.SortWithTrace(inputArray);
                //mInsertionSort.Sort_Recursive(inputArray);

                //------- 2.Selection Sort ~ O(n^2) -------
                //mSelectionSort.Sort(inputArray);                                                //164ms 10^4
                //mSelectionSort.SortWithTrace(inputArray);

                //------- 3.Bubble Sort ~ O(n^2) -------
                //mBubbleSort.Sort(inputArray);                                                   //600ms 10^4
                //mBubbleSort.OriginalBubbleSort(inputArray);                                     //550ms 10^4

                //###################################  O(n*lgn)  #############################################

                //------- 4.Merge Sort ~ O(n*lgn) -------
                //mMergeSort.Sort(inputArray);                                                    //27ms 10^5
                //mMergeSort.Sort_Enhanced(inputArray);                                           //25ms 10^5

                //------- 5.Heap Sort ~ O(n*lgn) -------
                //mHeapSort.Sort(inputArray);                                                     //53ms 10^5

                //------- 6.Quick Sort ~ O(n*lgn) -------
                //mQuickSort.Sort(inputArray);                                                      //40ms 10^5
                //mQuickSort.Sort_Hoare(inputArray, enableLogging);                               //23ms 10^5
                //mQuickSort.Sort_Lomuto(inputArray, enableLogging);

                //######################################  O(n)  ##############################################

                //------- 7.Counting Sort ~ O(n) -------
                //inputArray = mCountingSort.Sort(inputArray);                                    //2ms 10^5

                //------- 8.Radix Sort ~ O(n) -------
                //inputArray = mRadixSort.Sort(inputArray, enableLogging);                        //114ms 10^5

                //------- 9.Bucket Sort ~ O(n) -------
                //inputArray = mBucketSort.Sort(inputArray);                                      //13ms 10^5

                //------------------------------------------------------------------------------------------
                //             A L G O R I T H M         T E S T        E N D E D
                //------------------------------------------------------------------------------------------
                stopwatch.Stop();
                executionTime[i] = stopwatch.ElapsedMilliseconds;
                Console.Write(executionTime[i] + " ");
                Console.WriteLine("");
                stopwatch.Reset();
                ArrayHandler.Print(inputArray, enableLogging);
            }

            //Print Execution Time
            double ts = executionTime.Average();
            Console.WriteLine("Average Execution Time in milliseconds: " + ts);

            Console.ReadLine();
        }
    }
}

//------------------------------------------------------
//----   Insertion Sort和Selection Sort的对比分析   ------
//------------------------------------------------------
//Insertion Sort内循环扫描的是靠左<已排序>区域，目标是在内循环结束时把未排序区域的第一个元素插入到已排序区域的合适位置。a, b, ..., i, | j, ...
//Selection Sort内循环扫描的是靠右<未排序>区域，目标是在内循环结束时把未排序区域的第一个元素换成未排序区域的最小值，即把最小元素插入到已排序区域的最最后面。a, b, ... | i, j, ...
//这两种排序都是双For循环，因此时间复杂度是O(n^2)
