using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortingAlgorithms
{
    class BubbleSort
    {
        //Modified Bubble Sort, only scan/swap neighbor pair.
        //相邻两元素作为一对，自左向右，大的交换至右侧，数组完整扫描一次后可确保整个数组最大元素一定在最后一个。（这是下沉排序，或者下冒泡排序。）
        //（同样的道理，如果是自右向左，那么最小的一定在第一个。这是冒泡排序，或者上冒泡排序）
        //由于已排序区域是逐渐增长的，因此可以内循环变长。
        //     a1, a2, a3, a4
        //     i   i+1
        //一遍一遍的扫，直到扫一遍都没有出现Swap位置。

        //在数组值随机分布的情况下，这两种算法性能相近
        //但数组值绝大多数已排好的情况下，相邻交换变长搜索比独立内外双循环耗时小非常非常多。因为前者收敛速度快。
        public void Sort(int[] A)
        {
            bool done = false;
            int sorted = 0;
            while (!done) //Sort done when no swap occurred in For loop.
            {
                done = true;
                for (int i = 0; i < A.Length - 1 - sorted; i++) //Unsorted part shrinked.
                {
                    if (A[i] > A[i + 1])
                    {
                        done = false;
                        ArrayHandler.Swap(ref A[i], ref A[i + 1]);
                    }
                }
                sorted++; //Last element sorted.
            }
        }

        //Original Bubble Sort, scan/swap every possible pair.
        //      a1, a2, a3, a4, ...
        //      i  ------>  A.Length - 1
        //          j  ----->  A.Length
        //内外循环完全独立，完全扫描一遍。
        public void OriginalBubbleSort(int[] A)
        {
            for (int i = 0; i < A.Length - 1; i++)
            {
                for (int j = i + 1; j < A.Length; j++)
                {
                    if (A[i] > A[j])
                    {
                        ArrayHandler.Swap(ref A[i], ref A[j]);
                    }
                }
            }
        }       
    }
}
