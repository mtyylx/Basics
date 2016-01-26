using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortingAlgorithms
{
    class QuickSort
    {
        //基本思想：分治法
        //1.分段：选Pivot，对这部分数组分段并重新排序，使之满足左部 < pivot < 右部
        //2.解决：递归第一步分段直至数组只剩一个元素
        //3.合并：由于第一步分段时已经原位排序，所以不需要合并算法。

        //QuickSort与MergeSort对比
        //QuickSort分段用的是：找pivot然后将数组调教为左部 < pivot < 右部
        //MergeSort分段用的是：固定不变的对半分方法
        //
        //QuickSort不需要合并这一步，因为他是原址运算，分段的同时就已经有序了
        //MergeSort不是原址操作，在合并的时候需要额外的空间才可以完成

        //Lomuto分区方式将数组分为四个区域：
        //    LEFT Partition    ||   RIGHT Partition  ||  Unsorted ||  Pivot
        // a1  a2  a3 ...       ||   ax ...           ||  ay ...   ||  aN
        // i = start to end - 1      divide               i            end

        //Hoare分区方式将数组分为三个区域：
        //    LEFT Partition      ||     Unsorted     ||     RIGHT Partition
        //    left: start ---> ...                       ... <--- end :right

        public void Sort(int[] A)
        {
            Divide(A, 0, A.Length - 1);
        }

        private void Divide(int[] A, int start, int end)
        {
            if (start < end)
            {
                int pivot = Partition_Backup(A, start, end);
                Divide(A, start, pivot - 1);
                Divide(A, pivot + 1, end);
            }
        }

        //负责首先选pivot、然后对start-end区间内数组分段并重新排序，以满足=>左部 < pivot < 右部
        private int Partition(int[] A, int start, int end)
        {
            int pivot = A[end];
            int divide = start;                                     //divide含义是：右部的第一个元素
            for (int i = start; i < end; i++)
            {
                if (A[i] <= pivot)                                  //元素小于pivot就应该归入左部（divide++），否则归入右部（i++）
                {
                    ArrayHandler.Swap(ref A[i], ref A[divide]);     //和Divide位置的元素交换的效果就是可以把这个元素加入左边部分
                    divide++;
                }
            }

            for (int i = end - 1; i >= divide; i--)     //右移右部的所有元素，将Pivot值插入divide位置，就得到了满足“左部 < pivot < 右部”特性的数组
            {
                ArrayHandler.Swap(ref A[i], ref A[i + 1]);
            }

            return divide;
        }

        //Hoare干净版本
        private void Divide_Backup(int[] A, int start, int end)
        {
            if (start < end)
            {
                int divide = Partition_Backup(A, start, end);
                Divide(A, start, divide);
                Divide(A, divide + 1, end);//Hoare分区方式和Lomuto不同而且pivot位置也不同，所以划分也不同。
            }
        }

        private int Partition_Backup(int[] A, int start, int end)
        {
            int pivot = A[start];//这里pivot选择的是第一个元素
            int left = start - 1;
            int right = end + 1;
            while (true)
            {
                do { right--; }
                while (A[right] > pivot);//直到找到一个不大于pivot的右部元素

                do { left++; }
                while (A[left] < pivot);//直到找到一个不小于pivot的左部元素

                if (left < right)
                    ArrayHandler.Swap(ref A[left], ref A[right]);
                else
                    return right;//直到左右部接壤了，就说明所有元素都扫描完了，且一定能保证左边的都小于pivot，右边的都大于pivot
            }
        }


        #region Quick Sort using Lomuto Partition 包含计算过程
        private int Partition_Lomuto(int[] mArray, int start, int end, bool logEnabled)
        {
            int pivot = mArray[end];
            int split = start;
            for (int idx = start; idx < end; idx++)
            {
                if (mArray[idx] <= pivot)
                {
                    ArrayHandler.Swap(ref mArray[idx], ref mArray[split]);
                    split++;
                }
                if (logEnabled)
                {
                    for (int x = start; x <= end; x++)
                    {
                        Console.Write(mArray[x] + " ");
                        if (x == end - 1)
                        {
                            Console.Write("| ");
                        }
                    }
                    Console.WriteLine("");
                }
            }
            for (int idx = end - 1; idx >= split; idx--)
            {
                ArrayHandler.Swap(ref mArray[idx], ref mArray[idx + 1]);
            }
            if (logEnabled)
            {
                for (int x = start; x <= end; x++)
                {
                    Console.Write(mArray[x] + " ");
                    if (x == split - 1 || (x == split && split != end))
                    {
                        Console.Write("| ");
                    }
                }
                Console.WriteLine(" (Pivot " + pivot + " inserted)");
            }
            return split;
        }

        private void QuickSortRecursive_Lomuto(int[] mArray, int start, int end, bool logEnabled)
        {
            if (start < end)
            {
                if (logEnabled)
                {
                    for (int x = start; x <= end; x++)
                    {
                        Console.Write(mArray[x] + " ");
                    }
                    Console.WriteLine("to be Quick Sorted");
                }
                int pivot = Partition_Lomuto(mArray, start, end, logEnabled);
                QuickSortRecursive_Lomuto(mArray, start, pivot - 1, logEnabled);
                QuickSortRecursive_Lomuto(mArray, pivot + 1, end, logEnabled);
            }
            else if (logEnabled)
            {
                for (int x = start; x <= end; x++)
                {
                    Console.Write(mArray[x] + " ");
                }
                Console.WriteLine("Recursive End Reached.");
            }
        }

        public void Sort_Lomuto(int[] mArray, bool enableLogging)
        {
            logEnabled = enableLogging;
            QuickSortRecursive_Lomuto(mArray, 0, mArray.Length - 1, logEnabled);
        }
        #endregion

        #region Quick Sort using Hoare Partition 包含计算过程
        private int Partition_Hoare(int[] mArray, int start, int end)
        {
            int pivot = mArray[start];
            int lowIdx = start - 1;
            int highIdx = end + 1;
            while (true)
            {
                //Pointer will stop at the element <= Pivot on the RIGHT Partition, then to be swapped or exit
                //Use DO...WHILE instead of WHILE can avoid infinite swapping loop when the first and last element are equal. e.g. 4 9 8 4, while loop will not exit.
                do
                {
                    highIdx--;//Move the pointer anyway whether condition met or not.
                    #region Trace
                    if (logEnabled)
                    {
                        for (int x = start; x <= end; x++)
                        {
                            Console.Write(mArray[x] + " ");
                        }
                        Console.WriteLine("");

                        //Print Pointer Locations
                        for (int x = start; x <= end; x++)
                        {
                            if (x == lowIdx || x == highIdx)
                            {
                                Console.Write("^");
                                for (int num = 0; num < mArray[x].ToString().Length; num++)
                                {
                                    Console.Write(" ");
                                }
                            }
                            else
                            {
                                for (int num = 0; num < mArray[x].ToString().Length + 1; num++)
                                {
                                    Console.Write(" ");
                                }
                            }
                        }
                        Console.WriteLine("");
                    }
                    #endregion
                }
                while (mArray[highIdx] > pivot);

                //Pointer will stop at the element >= Pivot on the LEFT Partition, then to be swapped or exit
                do
                {
                    lowIdx++;//Move the pointer anyway whether condition met or not.
                    #region Trace
                    if (logEnabled)
                    {
                        for (int x = start; x <= end; x++)
                        {
                            Console.Write(mArray[x] + " ");
                        }
                        Console.WriteLine("");

                        //Print Pointer Locations
                        for (int x = start; x <= end; x++)
                        {
                            if (x == lowIdx || x == highIdx)
                            {
                                Console.Write("^");
                                for (int num = 0; num < mArray[x].ToString().Length; num++)
                                {
                                    Console.Write(" ");
                                }
                            }
                            else
                            {
                                for (int num = 0; num < mArray[x].ToString().Length + 1; num++)
                                {
                                    Console.Write(" ");
                                }
                            }
                        }
                        Console.WriteLine("");
                    }
                    #endregion 
                }
                while (mArray[lowIdx] < pivot);

                //Swap this pair of element based on their relative position of pivot.
                //两个指针相遇之时，就是左右分区明确界限之时。
                if (lowIdx < highIdx)
                {
                    ArrayHandler.Swap(ref mArray[lowIdx], ref mArray[highIdx]);
                    #region Trace
                    if (logEnabled)
                    {
                        for (int x = start; x <= end; x++)
                        {
                            Console.Write(mArray[x] + " ");
                        }
                        Console.WriteLine("(" + mArray[highIdx] + " and " + mArray[lowIdx] + " swapped)");

                        //Print Pointer Locations
                        for (int x = start; x <= end; x++)
                        {
                            if (x == lowIdx || x == highIdx)
                            {
                                Console.Write("^");
                                for (int num = 0; num < mArray[x].ToString().Length; num++)
                                {
                                    Console.Write(" ");
                                }
                            }
                            else
                            {
                                for (int num = 0; num < mArray[x].ToString().Length + 1; num++)
                                {
                                    Console.Write(" ");
                                }
                            }
                        }
                        Console.WriteLine("");
                    }
                    #endregion
                }
                else
                {
                    #region Trace
                    if (logEnabled)
                    {
                        for (int x = start; x <= end; x++)
                        {
                            if (x == highIdx)
                            {
                                Console.Write(mArray[x] + " | ");
                            }
                            else
                                Console.Write(mArray[x] + " ");
                        }
                        Console.WriteLine(" (Partition Done)");
                    }
                    #endregion
                    return highIdx;
                }
            }
        }

        private void QuickSortRecursive_Hoare(int[] mArray, int start, int end)
        {
            if (start < end)
            {
                #region Trace
                if (logEnabled)
                {
                    Console.WriteLine("--------------------------------------------------");
                    for (int x = start; x <= end; x++)
                    {
                        Console.Write(mArray[x] + " ");
                    }
                    Console.WriteLine("to be Quick Sorted");
                    Console.WriteLine("Pivot = " + mArray[start]);
                }
                #endregion

                //Pivot Location != Split Location
                int split = Partition_Hoare(mArray, start, end);
                //需要注意这里和Lomuto分区方式不同，Hoare分区不能按 start ~ (pivot-1) 和 (pivot+1) ~ end 来迭代
                //必须要包含Pivot迭代，按照 start ~ pivot 和 (pivot+1) ~ end 来迭代
                //导致这种区别的原因是：
                //经过Lomuto分区的处理后，可以确保分区点（split）左边的元素一定比Pivot小，右边的元素一定比Pivot大，<并且>分区点（split）就是Pivot所在点
                //经过Hoare分区的处理后，只能够确保分区点（split）左边的元素一定比Pivot小，右边的元素一定比Pivot大，<但是>Pivot并不在split位置
                //即，数组的元素只是大致的按相对于pivot的值分成了左右两个区域，但是pivot并不会呆在这个两个区域的交界面,而是一定在这两个分区之一里面，即split和Pivot所在idx不相同。
                //由于迭代排序只需要确保分成两个区域不断削减问题规模即可，所以pivot在什么位置无所谓，因此Hoare算法比Lomuto运算量小三倍，Lomuto相当于是多做了无用功，但是确实Lomuto也更直觉好理解一些。
                QuickSortRecursive_Hoare(mArray, start, split);
                QuickSortRecursive_Hoare(mArray, split + 1, end);
            }
            else if (logEnabled)
            {
                Console.WriteLine("Recurse Reached End.");
            }
        }

        public void Sort_Hoare(int[] mArray, bool enableLogging)
        {
            logEnabled = enableLogging;
            QuickSortRecursive_Hoare(mArray, 0, mArray.Length - 1);
        }
        #endregion

        private bool logEnabled = false;
        public bool LogEnabled
        {
            get { return logEnabled; }
            set { logEnabled = value; }
        }

    }
}
