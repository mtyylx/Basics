using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace SortingAlgorithms
{
    class InsertionSort
    {
        //基本思想
        //在一开始将第一个元素视为已排序数组，然后不断的把后面的元素插入这个已排序数组中的相应位置，使得这个数组一直是已排序的，直到最后一个元素。
        //这么做的好处是，由于前面的数组一定是从小到大排列的，所以只要待插入的元素不再比已排序数组由右向左扫描的元素小的时候，他的位置一定就在这个元素的相邻右边。
        //  |--------已排序部分-------|----待插入排序部分-----|            
        //  |   a1  a2  a3  a4  a5  |    a6  a7  a8  ...  |         
        //      0    <-------   i        j  ------>  Length
        //
        //a6和a5,a4,a3,a2,a1按顺序一次比较的时候，发现比到a3的时候a6终于大于a3了，因此由于a6>a3 && a6<a4 => 一定是a3 a6 a4这样的顺序。
        //                
        //需要注意的是，为了保证插入排序是稳定的，必须得将内循环的条件设为keyIns < A[i],即只能是小于而不能是小于等于。                 
        public void Sort(int[] A)
        {
            int j, i, keyIns;
            for (j = 1; j < A.Length; j++)
            {
                keyIns = A[j];
                for (i = j - 1; i >= 0 && keyIns < A[i]; i--)  //条件也可写成if else break的方式在里面
                {
                    A[i + 1] = A[i];
                }
                A[i + 1] = keyIns; //跳出循环时i已经自减，i+1就是第一个不满足keyIns < A[i]的元素右边。
            }
        }

        //Insertion Sort with Log
        public void SortWithTrace(int[] A)
        {
            int j, i, keyIns;
            for (j = 1; j < A.Length; j++)
            {
                keyIns = A[j];
                ArrayHandler.Print(A, 0, j - 1, true);
                Console.Write("| ");
                ArrayHandler.Print(A, j, A.Length - 1, true);
                Console.WriteLine("");

                for (i = j - 1; i >= 0 && keyIns < A[i]; i--)
                {
                    A[i + 1] = A[i];
                    ArrayHandler.Print(A, 0, j - 1, true);
                    Console.Write("| ");
                    ArrayHandler.Print(A, j, A.Length - 1, true);
                    Console.Write("  [" + keyIns + "]");
                    Console.WriteLine("");

                }
                A[i + 1] = keyIns; //跳出循环时i已经自减，i+1就是第一个不满足elementToBeInserted < mArray[i]的元素右边。
                ArrayHandler.Print(A, 0, i, true);
                Console.Write("[" + keyIns + "] ");
                ArrayHandler.Print(A, i + 2, j - 1, true);
                Console.Write("| ");
                ArrayHandler.Print(A, j, A.Length - 1, true);
                Console.Write("      [Done]");
                Console.WriteLine("");
                Console.WriteLine("------------------------------------------------");
            }
        }

        //  具体计算过程
        //  31 71 21 71 54 95 4
        //  31 | 71 21 71 54 95 4
        //  31 [71] | 71 21 71 54 95 4       [Done]
        //  ------------------------------------------------
        //  31 71 | 21 71 54 95 4
        //  31 71 | 71 71 54 95 4   [21]
        //  31 31 | 71 71 54 95 4   [21]
        //  [21] 31 | 71 71 54 95 4       [Done]
        //  ------------------------------------------------
        //  21 31 71 | 71 54 95 4
        //  21 31 71 [71] | 71 54 95 4       [Done]
        //  ------------------------------------------------
        //  21 31 71 71 | 54 95 4
        //  21 31 71 71 | 71 95 4   [54]
        //  21 31 71 71 | 71 95 4   [54]
        //  21 31 [54] 71 | 71 95 4       [Done]
        //  ------------------------------------------------
        //  21 31 54 71 71 | 95 4
        //  21 31 54 71 71 [95] | 95 4       [Done]
        //  ------------------------------------------------
        //  21 31 54 71 71 95 | 4
        //  21 31 54 71 71 95 | 95   [4]
        //  21 31 54 71 71 71 | 95   [4]
        //  21 31 54 71 71 71 | 95   [4]
        //  21 31 54 54 71 71 | 95   [4]
        //  21 31 31 54 71 71 | 95   [4]
        //  21 21 31 54 71 71 | 95   [4]
        //  [4] 21 31 54 71 71 | 95       [Done]
        //  ------------------------------------------------
        //  4 21 31 54 71 71 95

        #region Insertion Sort Using Recursive Structure.
        public void Sort_Recursive(int[] A)
        {
            InsertionSort_Recursive(A, A.Length - 1);
        }

        // n should be the ending element idx. i.e. A.Length - 1
        //可以看到这里采用递归方式的插入排序其实就相当于非递归方式里面使用的for循环而已。
        //因为实际上还是双循环的结构，因此运算量并没有改善。
        private void InsertionSort_Recursive(int[] A, int n)
        {
            if (n > 0)
            {
                int x = n - 1;
                InsertionSort_Recursive(A, x);
                Insert(A, n);
            }
            //Equal to:
            //Insert(A, 1）
            //Insert(A, 2)
            //Insert(A, ...)
            //...
            //Insert(A, n)
        }

        //To Insert A[n] into already inordered A[1...(n-1)] array to make it still inorder.
        private void Insert(int[] A, int n)
        {
            //Console.WriteLine("Insert(A, " + n + ")");
            int key = A[n]; //Store the value, to be compared with every digits in array [0 to i]
            int i = n - 1;
            while (i >= 0 && A[i] > key) // Move each digit right if the value is bigger than KEY
            {
                A[i + 1] = A[i];
                ///PrintArray(mArray);
                i--;
            }
            A[i + 1] = key;
            //PrintArray(mArray);
        }
        #endregion
    }
}
