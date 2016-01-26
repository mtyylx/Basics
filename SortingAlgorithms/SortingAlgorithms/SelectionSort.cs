using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortingAlgorithms
{
    class SelectionSort
    {
        //At the beginning, sorted part = empty, while unsorted part = whole array. 
        //At every step, algorithm finds minimal element in the unsorted part and adds it to the end of the sorted one. 
        //When unsorted part becomes empty, algorithm stops.
        //由于最小的元素总是在最开始被选出来，因此可以保证内循环每次新选出的最小值放在未排序部分的第一个位置一定能保证它大于等于所有已排序元素。然后这个元素就可以作为已排序最后一个元素了。
        //外循环中每次循环的默认初始最小值是已排序部分的最后一个元素值，这样可以保证第一次循环会扫描数组所有元素（0 - Length）
        //
        //  |--------已排序部分-------|--------待插入排序部分------|            
        //  |   a1  a2  a3  a4  a5  |  a6  a7  a8  a9  .....  |         
        //      0              i-1     i   j  ------>  Length
        //  找出 i to Length中最先出现的最小的元素，这样算法是稳定的，可用于Radix这种有卫星数据的排序。

        public void Sort(int[] A)
        {
            int i, j, minIdx;
            for (i = 0; i < A.Length - 1; i++) //The last element will be automatically sorted.
            {
                minIdx = i; //Default min should be the first element of the unsorted part.
                for (j = i + 1; j < A.Length; j++)
                {
                    if (A[j] < A[minIdx]) //STABLE Algorithm: Does not refresh minIdx when equal element found. 只记录第一个最小的元素。
                    {
                        minIdx = j;
                    }
                }
                
                if (i != minIdx) //STABLE Algorithm: Does not swap if the ith element is already the smallest (or equally smallest from i to N).
                {
                    ArrayHandler.Swap(ref A[i], ref A[minIdx]); //Now the ith element become sorted. Sorted.Length++, Unsorted.Length--
                }
            }
        }

        //Selection Sort with log
        public void SortWithTrace(int[] A)
        {
            int i, j, minIdx;
            for(i = 0; i < A.Length - 1; i++)
            {
                minIdx = i;
                Console.WriteLine("----------------------------------------------------------");
                ArrayHandler.Print(A, 0, i - 1, true);
                Console.Write("| ");
                ArrayHandler.Print(A, i, minIdx - 1, true);
                Console.Write("<");
                ArrayHandler.Print(A, minIdx, minIdx, true);
                Console.Write("> ");
                ArrayHandler.Print(A, minIdx + 1, A.Length - 1, true);
                Console.Write("        Current Min = " + A[minIdx]);
                Console.WriteLine("");

                for (j = i + 1; j < A.Length; j++)
                {
                    if (A[minIdx]>A[j])
                    {
                        minIdx = j;
                    }
                    ArrayHandler.Print(A, 0, i - 1, true);
                    Console.Write("| ");
                    ArrayHandler.Print(A, i, minIdx - 1, true);
                    Console.Write("<");
                    ArrayHandler.Print(A, minIdx, minIdx, true);
                    Console.Write("> ");
                    ArrayHandler.Print(A, minIdx + 1, A.Length - 1, true);
                    Console.Write("        Current Min = " + A[minIdx]);
                    Console.WriteLine("");
                }
                if(minIdx != i)
                {
                    ArrayHandler.Swap(ref A[i], ref A[minIdx]);
                    ArrayHandler.Print(A, 0, i - 1, true);
                    Console.Write("| ");
                    Console.Write("<");
                    ArrayHandler.Print(A, i, i, true);
                    Console.Write("> ");
                    ArrayHandler.Print(A, i + 1, A.Length - 1, true);
                    Console.Write("        Swapped!");
                    Console.WriteLine("");
                }
            }
        }

        // 具体计算过程
        //----------------------------------------------------------
        //| <122 > 44 122 55 33 55 44 23         Current Min = 122
        //| 122 <44 > 122 55 33 55 44 23         Current Min = 44
        //| 122 <44 > 122 55 33 55 44 23         Current Min = 44
        //| 122 <44 > 122 55 33 55 44 23         Current Min = 44
        //| 122 44 122 55 <33 > 55 44 23         Current Min = 33
        //| 122 44 122 55 <33 > 55 44 23         Current Min = 33
        //| 122 44 122 55 <33 > 55 44 23         Current Min = 33
        //| 122 44 122 55 33 55 44 <23 >         Current Min = 23
        //| <23 > 44 122 55 33 55 44 122         Swapped!
        //----------------------------------------------------------
        //23 | <44 > 122 55 33 55 44 122         Current Min = 44
        //23 | <44 > 122 55 33 55 44 122         Current Min = 44
        //23 | <44 > 122 55 33 55 44 122         Current Min = 44
        //23 | 44 122 55 <33 > 55 44 122         Current Min = 33
        //23 | 44 122 55 <33 > 55 44 122         Current Min = 33
        //23 | 44 122 55 <33 > 55 44 122         Current Min = 33
        //23 | 44 122 55 <33 > 55 44 122         Current Min = 33
        //23 | <33 > 122 55 44 55 44 122         Swapped!
        //----------------------------------------------------------
        //23 33 | <122 > 55 44 55 44 122         Current Min = 122
        //23 33 | 122 <55 > 44 55 44 122         Current Min = 55
        //23 33 | 122 55 <44 > 55 44 122         Current Min = 44
        //23 33 | 122 55 <44 > 55 44 122         Current Min = 44
        //23 33 | 122 55 <44 > 55 44 122         Current Min = 44
        //23 33 | 122 55 <44 > 55 44 122         Current Min = 44
        //23 33 | <44 > 55 122 55 44 122         Swapped!
        //----------------------------------------------------------
        //23 33 44 | <55 > 122 55 44 122         Current Min = 55
        //23 33 44 | <55 > 122 55 44 122         Current Min = 55
        //23 33 44 | <55 > 122 55 44 122         Current Min = 55
        //23 33 44 | 55 122 55 <44 > 122         Current Min = 44
        //23 33 44 | 55 122 55 <44 > 122         Current Min = 44
        //23 33 44 | <44 > 122 55 55 122         Swapped!
        //----------------------------------------------------------
        //23 33 44 44 | <122 > 55 55 122         Current Min = 122
        //23 33 44 44 | 122 <55 > 55 122         Current Min = 55
        //23 33 44 44 | 122 <55 > 55 122         Current Min = 55
        //23 33 44 44 | 122 <55 > 55 122         Current Min = 55
        //23 33 44 44 | <55 > 122 55 122         Swapped!
        //----------------------------------------------------------
        //23 33 44 44 55 | <122 > 55 122         Current Min = 122
        //23 33 44 44 55 | 122 <55 > 122         Current Min = 55
        //23 33 44 44 55 | 122 <55 > 122         Current Min = 55
        //23 33 44 44 55 | <55 > 122 122         Swapped!
        //----------------------------------------------------------
        //23 33 44 44 55 55 | <122 > 122         Current Min = 122
        //23 33 44 44 55 55 | <122 > 122         Current Min = 122
        //23 33 44 44 55 55 122 122
    }
}
