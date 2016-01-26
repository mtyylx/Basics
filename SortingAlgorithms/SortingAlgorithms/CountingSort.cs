using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortingAlgorithms
{
    class CountingSort
    {   
        //Counting Sort：Not Comparison Sort
        //必须满足的前提条件：数组元素必须是整数
        //核心思想:利用数组中元素数值的分布情况直接计算出空位个数，然后把元素塞到指定空位上，因此不需要比较。
        //这么做的依据就是数组元素之间最小的粒度是1，因此如果知道小于等于X的元素有N个，那么等于X的元素就一定排列在第N个元素。

        private int[] Sort(int[] mArray, int upperLimits)
        {
            int[] histoArray = new int[upperLimits + 1];
            int[] sortedArray = new int[mArray.Length];
            
            //Create an  all-zero histogram array: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
            for (int idx = 0; idx < histoArray.Length; idx++)
            {
                histoArray[idx] = 0;
            }

            //Collect value distribution of mArray: [0, 2, 0, 0, 1, 0, 0, 0, 3, 0]
            for (int idx = 0; idx < mArray.Length; idx++)
            {
                histoArray[mArray[idx]]++;
            }

            //Collect accumlated value distribution of mArray
            for (int idx = 1; idx < histoArray.Length; idx++)
            {
                histoArray[idx] = histoArray[idx - 1] + histoArray[idx];
            }

            //Directly put each element in mArray to the correct position in sorted array
            for (int idx = mArray.Length - 1; idx >= 0; idx--)
            {
                //定义 X = histoArray[mArray[idx]]，则X的含义是：原数组中有X个元素小于等于该idx中存的值
                sortedArray[histoArray[mArray[idx]] - 1] = mArray[idx];
                //Update the value statistic after element extracted
                histoArray[mArray[idx]]--;
            }
            return sortedArray;
        }

        public int[] Sort(int[] mArray)
        {
            int max = 0;
            for (int idx = 0; idx < mArray.Length; idx++)
            {
                if (mArray[idx] >= max)
                {
                    max = mArray[idx];
                }
            }

            return Sort(mArray, max);
        }
    }
}
