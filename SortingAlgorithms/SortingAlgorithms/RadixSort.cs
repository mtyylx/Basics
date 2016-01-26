using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortingAlgorithms
{
    class RadixSort
    {
        //Downscale the problem to 0-9 Insertion Sort
        //之所以要从最低位开始排序并且逐渐增高，是因为位数越高，重要性越大。这样最低位最先最高位最后可以确保数组完全有序。

        private bool logEnable = false;
        public bool LogEnable
        {
            get { return logEnable; }
            set { logEnable = value; }
        }
        
        
        public int[] Sort(int[] mArray, bool enableLogging)
        {
            logEnable = enableLogging;
            int maxDigit = ArrayHandler.GetElementDigit(ArrayHandler.GetMaxElement(mArray));

            for (int i = 1; i <= maxDigit; i++)
            {
                //mArray = CountingSortForRadixSort(mArray, i);
                //InsertionSortForRadixSort(mArray, i);
                SelectionSortForRadixSort(mArray, i);
                ArrayHandler.Print(mArray, logEnable);
            }
            return mArray;
        }

        //使用Counting Sort完成Radix Sort的子流程
        //需要注意的是Radix Sort选用的子排序算法必须满足stable这个特性，即输入的数组中数值相同的元素会保持原始顺序不变被输出
        //有了这个特性的保护，就能保证比如进行了个位的排序之后，再进行十位的排序时，不会因为十位数相同但个位数不相同的元素输出顺序打乱而前功尽弃。
        //所以说一个排序算法是稳定的的好处，就是可以用于保证这个算法作为附属算法处理带有关联数据的时候，能够保持整体一直有序。
        //Stable = Does not change the relative order of elements with equal keys.
        private int[] CountingSortForRadixSort(int[] mArray, int digit)
        {
            int[] hist = new int[10];
            int[] sorted = new int[mArray.Length];
            int[] selected = new int[mArray.Length];

            for (int i = 0; i < mArray.Length;  i++)
            {
                selected[i] = ArrayHandler.ExtractDigitValue(mArray[i], digit);
            }
            ArrayHandler.Print(selected, logEnable);

            for (int i = 0; i < hist.Length; i++)
            {
                hist[i] = 0;
            }

            for (int i = 0; i< mArray.Length; i++)
            {
                hist[selected[i]]++;
            }
            ArrayHandler.Print(hist, logEnable);

            for (int i = 1; i < hist.Length; i++)
            {
                hist[i] = hist[i] + hist[i - 1];
            }
            ArrayHandler.Print(hist, logEnable);

            for (int i = mArray.Length - 1; i >= 0; i--)
            {
                sorted[hist[selected[i]] - 1] = mArray[i];
                hist[selected[i]]--;
            }

            return sorted;
        }

        //实际上采用任何<稳定的>排序算法都可以完成Radix Sort的子流程。下面是用Insertion Sort进行的排序，但是显然效率很低。
        private void InsertionSortForRadixSort(int[] mArray, int digit)
        {
            int i, j, temp;
            for (i = 1; i < mArray.Length; i++)
            {
                temp = mArray[i];
                //When condition met, j will decrease 1 AFTER code inside for loop is executed.
                for (j = i - 1; j >= 0 && ArrayHandler.ExtractDigitValue(mArray[j], digit) > ArrayHandler.ExtractDigitValue(temp, digit); j--)
                {
                    mArray[j + 1] = mArray[j];
                }
                //When jth element no longer > ith element, ith element need to be place right behind jth element. i.e. j + 1 
                //Eg. 0 3 4 1 -met> 0 3 4 4 -met> 0 3 3 4 -not met> 0 <1> 3 4
                mArray[j + 1] = temp;
            }
        }

        private void SelectionSortForRadixSort(int[] mArray, int digit)
        {
            int i, j, minIdx;
            for (i = 0; i < mArray.Length - 1; i++)
            {
                minIdx = i;
                for(j = i+1; j < mArray.Length; j++)
                {
                    if (ArrayHandler.ExtractDigitValue(mArray[minIdx], digit)> ArrayHandler.ExtractDigitValue(mArray[j], digit))
                    {
                        minIdx = j;
                    }
                }
                if (minIdx != i)
                {
                    ArrayHandler.Swap(ref mArray[i], ref mArray[minIdx]);
                }
            }
        }
    }
}
