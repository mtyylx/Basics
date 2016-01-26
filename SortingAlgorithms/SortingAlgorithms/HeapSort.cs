using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortingAlgorithms
{
    class HeapSort
    {
        //基本思想
        //递归方式和非递归方式实现其实区别只在于MaxHeapify的实现，一个反复调用自己，一个用while循环。
        //建最大堆：自底向上将堆最大化，高度增加一级就需要递归一次保持这种最大堆的特性。（如果不从下往上，就无法保证最大堆特性）
        //取根节点：把根节点与未排序的数组最后一个元素交换，根节点已排序，此时已排序数组自增，未排序数组自减，再递归未排序数组使之保持最大堆特性。
        //              未排序                 已排序
        //       a0 a1 a2 ... ai-1 ai  |  ai+1 ... aN
        //          1  <--------   i
        //     将a0和ai交换，ai就会变为已排序的第一个元素，再对剩下未排序的进行最大堆化。

        //对比MergeSort和HeapSort递归时的异同：
        //堆排序的递归原子操作：已知这个堆的两个子堆都是有序的（最大堆），将这个堆有序化（最大堆化）。
        //归并排序的递归原子操作：已知这个数组的两个子数组都是有序的，将这个数组有序化。
        //堆排序显而易见的特性：如果这个堆没有子节点，那么这个堆肯定是最大堆。
        //归并排序显而易见的特性：如果数组只有一个元素，那么这个数组肯定是有序的。
        
        
        //heapSize is dynamic
        int heapSize;

        //前提两个子节点已经是最大堆，比较选出父节点、左儿子、右儿子这三者中最大的，和父节点交换，然后递归自己对修改的子堆进行最大堆化
        private void MaxHeapify_Recursive(int[] A, int i)
        {
            int leftChild = 2 * i + 1;
            int rightChild = 2 * i + 2;
            int max = i;

            if (leftChild < heapSize && A[leftChild] > A[max])    //需要判定是否存在leftChild
                max = leftChild;

            if (rightChild < heapSize && A[rightChild] > A[max])    //需要判定是否存在rightChild
                max = rightChild;

            if (max != i)
            {
                ArrayHandler.Swap(ref A[i], ref A[max]);
                MaxHeapify_Recursive(A, max);               //值交换后需要更新子堆
            }
        }

        //同样需要选出父节点、左儿子、右儿子三者中最大的，和父节点交换，只不过没有调用自己而是直接把i更新为子节点并设done标志位来确保最大堆属性
        private void MaxHeapify_NonRecursive(int[] A, int i)
        {
            bool done = false;
            while (!done)       //直到根节点大于全部子节点时退出
            {
                int left = 2 * i + 1;
                int right = 2 * i + 2;
                int max = i;

                if (left < heapSize && A[left] > A[max])
                    max = left;
                if (right < heapSize && A[right] > A[max])
                    max = right;
                if (max != i)
                {
                    ArrayHandler.Swap(ref A[i], ref A[max]);
                    i = max;                                   //关键！向下对子节点进行最大堆化，等效于递归
                }
                else
                    done = true;
            }
        }

        private void BuildMaxHeap(int[] A)
        {
            heapSize = A.Length;
            for(int i = heapSize/2 - 1; i >= 0; i--)        //A[heapSize/2-1]：堆中最后一个有子节点的元素
            {
                MaxHeapify_NonRecursive(A, i);                 //自底向上，将堆最大化
            }
        }

        public void Sort(int[] A)
        {
            BuildMaxHeap(A);                                //先建最大堆
            for(int i = A.Length - 1; i > 0; i--)
            {
                ArrayHandler.Swap(ref A[0], ref A[i]);      //循环取出根节点放最后
                heapSize--;                                 //然后对剩下的元素做最大堆化
                MaxHeapify_NonRecursive(A, 0);
            }
        }

        #region 堆排序 递归方式（有运算过程）
        //The heap is essentially an array that is represented/viewed as a binary tree structure, however, this binary tree structure is partially unsorted
        public void SortWithTrace(int[] A)
        {
            BuildMaxHeapWithTrace(A);
            //Extract the root of the binary tree in each iteration.
            for (int idx = A.Length - 1; idx > 0; idx--)
            {
                ArrayHandler.Swap(ref A[0], ref A[idx]);
                heapSize--;
                MaxHeapifyWithTrace(A, 0);
            }
        }

        //After HEAPIFY the array, the root element has the MAX value of the entire array.
        //But other element will not be in order because the HEAP property only guarantee what's on the same tree
        //it cannot take care of the element in different tree.

        //Note that: A Max Heap != A Fully Sorted Array
        //           A Max Heap = Root Element is MAX
        private void BuildMaxHeapWithTrace(int[] A)
        {
            int largestNonLeafIdx = A.Length / 2 - 1;
            heapSize = A.Length;
            for (int idx = largestNonLeafIdx; idx >= 0; idx--)
            {
                MaxHeapifyWithTrace(A, idx);
            }
        }

        private void MaxHeapifyWithTrace(int[] A, int i)
        {
            //Assume the binary tree rooted at A[i]'s Left child and Right child are both Max Heaps.
            int leftChildIdx = i * 2 + 1;
            int rightChildIdx = i * 2 + 2;
            int maxIdx = i;

            if (leftChildIdx < heapSize && A[maxIdx] < A[leftChildIdx])
            {
                maxIdx = leftChildIdx;
            }
            if (rightChildIdx < heapSize && A[maxIdx] < A[rightChildIdx])
            {
                maxIdx = rightChildIdx;
            }
            if (maxIdx != i)
            {
                ArrayHandler.Swap(ref A[maxIdx], ref A[i]);
                Console.Write("MaxHeapify(A, heapSize=" + heapSize + ", i=" + i + ") ----> ");
                ArrayHandler.Print(A);
                MaxHeapifyWithTrace(A, maxIdx);
            }

        }
        #endregion

    }
}
