using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortingAlgorithms
{
    class MergeSort
    {
        //基本思想：分治法
        //1.分段：将数组对半划分
        //2.解决：递归第一步分段，也就是划分出来的两部分再对半划分，直至这两部分只剩下一个元素的时候，结束划分(start < end 不再成立)
        //3.合并：用机制将这两部分混合在一起使整体有序，然后再用相同机制混合刚混合成的两部分，直至混合得到全部的数组。
        //-----------------------------------------------------------------------
        //重点1：如何Divide，并及时触底反弹开始Merge --> 设置递归收敛条件(start<end)
        //举例：对于一个完整的数组从头开始Divide：
        //                    a0 to a7
        //      (a0 to a3)       |        (a4 to a7)
        //(a0 to a1)|(a2 to a3)  |  (a4 to a5)|(a6 to a7)
        //
        //        a0, a1, a2, a3, a4, a5, a6, a7    一次Divide后的Merge函数调用写为：Merge(A, start=0, middle=(0+7)/2=3, end=7)
        //        |           |               |
        //        start       middle          end
        //-----------------------------------------------------------------------
        //重点2：如何把两个部分从临时数组中按大小拷贝至原数组的相应位置 --> 用极值保护尾端(int.MaxValue)
        //举例：在两个部分的结尾分别加上最大值以确保其中一个扫描到头后，他的最后一个元素一定大于另一部分的任何元素:
        //   a1, a2, a3, a4, <aF> | b1, b2, b3, b4, b5, b6, <bF>
        //a部分拷贝完成后，一定有<aF>大于b部分任何元素。
        //且拷贝时的for循环以原数组长度作为循环变量，而不用去关心两部分各自的长度。
        //-----------------------------------------------------------------------

        public void Sort(int[] A)
        {
            Divide(A, 0, A.Length - 1);
        }

        private void Divide(int[] A, int start, int end)
        {
            if (start < end)
            {
                int middle = (start + end) / 2;     //Use '+' to get middle point.
                Divide(A, start, middle);
                Divide(A, middle + 1, end);
                Merge(A, start, middle, end);
            }
        }

        private void Merge(int[] A, int start, int middle, int end)
        {
            int sizeL = middle - start + 1;
            int sizeR = end - middle;
            int[] temp = new int[sizeL + sizeR + 2];

            for (int i = 0; i < sizeL; i++)
            {
                temp[i] = A[start + i];
            }
            temp[sizeL] = int.MaxValue;
            
            for (int i = 0; i < sizeR; i++)
            {
                temp[sizeL + 1 + i] = A[middle + 1 + i];
            }
            temp[sizeL + sizeR + 1] = int.MaxValue;

            int idxL = 0;
            int idxR = sizeL + 1;
            for (int i = start; i <= end; i++)      //Use original index to loop, ensure every element is filled.
            {
                if(temp[idxL] <= temp[idxR])        //Use '<=' to make this algorithm STABLE.
                {
                    A[i] = temp[idxL];
                    idxL++;
                }
                else
                {
                    A[i] = temp[idxR];
                    idxR++;
                }
            }
        }

        //错误的方法，这么写确实简洁，但是没有考虑到两个数组在拷贝的时候其中一个先拷完的情况下，剩下的数组没拷就结束的问题。
        private void MergeWrong(int[] A, int start, int middle, int end)
        {
            int sizeL = middle - start + 1;
            int sizeR = end - (middle + 1) + 1;
            int[] temp = new int[sizeL + sizeR];

            for (int i = 0; i < sizeL + sizeR; i++)
            {
                temp[i] = A[i + start];
            }

            int idxL = 0;
            int idxR = idxL + sizeL;
            for (int i = start; idxL < sizeL && idxR < sizeL + sizeR; i++) //与运算做循环条件提前退出，剩下的没拷
            {
                if (temp[idxL] < temp[idxR])
                {
                    A[i] = temp[idxL];
                    idxL++;
                }
                else
                {
                    A[i] = temp[idxR];
                    idxR++;
                }
            }
        }



        //---------------------------------------------------------------------------
        //The original Merge Sort Algorithm described in Introduction of Algorithm.
        public void SortWithTrace(int[] A)
        {
            Divide(A, 0, A.Length - 1);
        }

        private void DivideWithTrace(int[] A, int p, int r)
        {
            //Sort array that has more than one element.
            if (p < r)
            {
                int q = (p + r) / 2;        //Divide: Keep divide the problem to two smaller problems.
                DivideWithTrace(A, p, q);         //Conquer: Tackle with the smaller problem by recurse.
                DivideWithTrace(A, q + 1, r);     //Conquer: Tackle with the smaller problem by recurse.
                MergeWithTrace(A, p, q, r);          //Combine: Reached the end and bounce back, start to conquer the smallest problem.
            }
            //else => this array is already in order. Recurse reached the end, start going back.
        }

        private void MergeWithTrace(int[] A, int p, int q, int r)
        {
            #region Print Calculation Procedure
            Console.Write("Merge(A, " + p + ", " + q + ", " + r + ") => (");
            for (int idx = p; idx <= q; idx++)
            {
                Console.Write(A[idx] + ((idx != q) ? (",") : ("")));
            }
            Console.Write(") # (");
            for (int idx = q + 1; idx <= r; idx++)
            {
                Console.Write(A[idx] + ((idx != r) ? (",") : ("")));
            }
            #endregion

            //Create two empty subarrays. i.e. since it's not in-place modification, it needs extra space to store the element value.
            //Number of Elements within A[p...to...q] = q - p then add 1.
            int i, j;
            int sizeLeft = q - p + 1;               //从A[p]到A[q]之间一共存在元素个数 = q - p + 1
            int sizeRight = r - (q + 1) + 1;        //从A[q + 1]到A[r]之间一共存在元素个数 = r - (q + 1) + 1
            int[] Left = new int[sizeLeft + 1];     //两个数组增加长度1，用来放sentinel辨识数组底部。
            int[] Right = new int[sizeRight + 1];
            Left[sizeLeft] = Right[sizeRight] = int.MaxValue; //Use Sentinels for easier coding.

            //Base on the index given (p/q/r), copy element value from "A[p...to...q]" and "A[q+1...to...r]" to two new arrays respectively.
            for (i = 0; i < sizeLeft; i++)
            {
                Left[i] = A[p + i];
            }
            for (j = 0; j < sizeRight; j++)
            {
                Right[j] = A[q + 1 + j];
            }

            //Compare elements in two arrays one by one, smaller one will be copied to the original array A.
            i = 0;
            j = 0;
            for (int x = p; x <= r; x++)
            {
                if (Left[i] <= Right[j])
                {
                    A[x] = Left[i];
                    i++;
                }
                else
                {
                    A[x] = Right[j];
                    j++;
                }
            }
            //At this point the input array A is already in order.
            //This is valid ONLY WHEN the two subarray of A are already IN ORDER themselves.
            //e.g. Left Array = {a1, a2, a3}; Right Array = {b1, b2, b3}; both Left and Right Array are IN-ORDER
            //Thus, a1 < a2 < a3 and b1 < b2 < b3
            //If a1 < b1, then a1 < b1 < b2 < b3,
            //i.e. a1 is the smallest of all.

            #region Print Calculation Procedure
            Console.Write(") => (");
            for (int idx = p; idx <= r; idx++)
            {
                Console.Write(A[idx] + ((idx != r) ? (",") : ("")));
            }
            Console.WriteLine(")");
            #endregion

        }


        //---------------------------------------------------------------------------
        //The enhanced Merge Sort Algorithm described in Introduction of Algorithm.
        //Avoided repeatedly creating 2 new temp arrays for copying.
        //Use only ONE temp array (size of N+2) which already defined and allocated outside the function.
        //Each recurse call will only use part of this array.
        public void Sort_Enhanced(int[] A)
        {
            int[] temp = new int[A.Length + 2];
            Divide_Enhanced(A, temp, 0, A.Length - 1);
        }

        private void Divide_Enhanced(int[] A, int[] temp, int p, int r)
        {
            if (p < r)
            {
                int q = (p + r) / 2;
                Divide_Enhanced(A, temp, p, q);
                Divide_Enhanced(A, temp, q + 1, r);
                Merge_Enhanced(A, temp, p, q, r);
            }
        }

        private void Merge_Enhanced(int[] A, int[] temp, int p, int q, int r)
        {
            #region Print Calculation Procedure
            Console.Write("Merge(A, " + p + ", " + q + ", " + r + ") => (");
            for (int idx = p; idx <= q; idx++)
            {
                Console.Write(A[idx] + ((idx != q) ? (",") : ("")));
            }
            Console.Write(") # (");
            for (int idx = q + 1; idx <= r; idx++)
            {
                Console.Write(A[idx] + ((idx != r) ? (",") : ("")));
            }
            #endregion

            int i, j;
            int sizeLeft = q - p + 1;
            int sizeRight = r - q;
            for (i = 0; i < sizeLeft; i++)
            {
                temp[i] = A[p + i];
            }
            temp[sizeLeft] = int.MaxValue;
            for (j = 0; j < sizeRight; j++)
            {
                temp[sizeLeft + 1 + j] = A[q + 1 + j];
            } 
            temp[sizeLeft + sizeRight + 1] = int.MaxValue;
            i = 0;
            j = sizeLeft + 1;
            for (int x = p; x <= r; x++)
            {
                if (temp[i] <= temp[j])
                {
                    A[x] = temp[i];
                    i++;
                }
                else
                {
                    A[x] = temp[j];
                    j++;
                }

            }
            #region Print Calculation Procedure
            Console.Write(") => (");
            for (int idx = p; idx <= r; idx++)
            {
                Console.Write(A[idx] + ((idx != r) ? (",") : ("")));
            }
            Console.WriteLine(")");
            #endregion

        }
    
    }

    #region 代码实际展开的详细分析和计算过程
    //本质上递归的最终目的就是计算出一组正确的A,p,q,r组合，从而通过串行调用Merge(A,p,q,r)来对目标进行排序。
    //递归会在不断深入的过程中最后触底（即不再满足p<r时），之后就是一层层的上升返回，
    //在上升的过程中，会一次次的用当时的A,p,q,r组合输入Merge过程，并在这个过程中从最小的只有两个元素的数组开始进行combine，上升至水面的时候就是整个A都combine完成的时候。
    //Example:
    //MergeSort(A, 0, 3)
    //{
    //    if (0 < 3)                    <<------------深入
    //    {
    //        q = (0 + 3) / 2 = 1
    //        //MergeSort(A, 0, 1)
    //        if (0 < 1)                <<------------深入
    //        {
    //            q = (0 + 1) / 2 = 0
    //            //MergeSort(A, 0, 0)
    //            if (0 < 0) {}         <<------------深入触底
    //            //MergeSort(A, 1, 1)
    //            if (1 < 1) {}         <<------------深入触底
    //            Merge(A, 0, 0, 1)     <<============上浮过程中开始combine，从最小的两元素数组开始
    //        }
    //        //MergeSort(A, 2, 3)      
    //        if (2 < 3)                <<------------深入
    //        {
    //            q = (2 + 3) / 2 = 2
    //            //MergeSort(A, 2, 2)
    //            if (2 < 2) {}         <<------------深入触底
    //            //MergeSort(A, 3, 3)
    //            if (3 < 3) {}         <<------------深入触底
    //            Merge(A, 2, 2, 3)     <<============上浮过程中开始combine，从最小的两元素数组开始
    //        }
    //        Merge(A, 0, 1, 3)         <<============上浮过程中继续combine，最后将A中的所有元素combine完成
    //    }
    //}
    //所以其实上面的递归运行过程简化以后实际就等于连续运行Merge过程三次的结果。
    //<Overall> 8,9,7,4
    //Merge(A, 0, 0, 1) => 8|9 => 8,9
    //Merge(A, 2, 2, 3) => 7|4 => 4,7
    //Merge(A, 0, 1, 3) => (8,9)|(4,7) => 4,7,8,9
    //递归这么多轮的目的只是为了能够正确的输入p，q，r来做Merge而已。
    #endregion

    #region 运算细节
    //    Given An Array: 8 9 7 4 (Length = 2^n)

    //<STEP 1>
    //MergeSort(A, 0, 3)
    //{
    //    if (0 < 3)
    //    {
    //        q = (0 + 3) / 2 = 1;
    //        MergeSort(A, 0, 1)
    //        MergeSort(A, 2, 3)
    //        Merge(A, 0, 1, 3)
    //    }
    //}

    //<STEP 2>
    //MergeSort(A, 0, 3)
    //{
    //    if (0 < 3)
    //    {
    //        q = (0 + 3) / 2 = 1
    //        //MergeSort(A, 0, 1)
    //        if (0 < 1)
    //        {
    //            q = (0 + 1) / 2 = 0
    //            MergeSort(A, 0, 0)
    //            MergeSort(A, 1, 1)
    //            Merge(A, 0, 0, 1)
    //        }
    //        //MergeSort(A, 2, 3)
    //        if (2 < 3)
    //        {
    //            q = (2 + 3) / 2 = 2
    //            MergeSort(A, 2, 2)
    //            MergeSort(A, 3, 3)
    //            Merge(A, 2, 2, 3)
    //        }
    //        Merge(A, 0, 1, 3)
    //    }
    //}

    //<STEP 3>
    //MergeSort(A, 0, 3)
    //{
    //    if (0 < 3)
    //    {
    //        q = (0 + 3) / 2 = 1
    //        //MergeSort(A, 0, 1)
    //        if (0 < 1)
    //        {
    //            q = (0 + 1) / 2 = 0
    //            //MergeSort(A, 0, 0)
    //            if (0 < 0) {}
    //            //MergeSort(A, 1, 1)
    //            if (1 < 1) {}
    //            Merge(A, 0, 0, 1)
    //        }
    //        //MergeSort(A, 2, 3)
    //        if (2 < 3)
    //        {
    //            q = (2 + 3) / 2 = 2
    //            //MergeSort(A, 2, 2)
    //            if (2 < 2) {}
    //            //MergeSort(A, 3, 3)
    //            if (3 < 3) {}
    //            Merge(A, 2, 2, 3)
    //        }
    //        Merge(A, 0, 1, 3)
    //    }
    //}

    //<Overall> 8,9,7,4
    //Merge(A, 0, 0, 1) => 8|9 => 8,9
    //Merge(A, 2, 2, 3) => 7|4 => 4,7
    //Merge(A, 0, 1, 3) => (8,9)|(4,7) => 4,7,8,9
    //--------------------------------------------------------------

    //Given An Array: 8 9 7 4 2 (Length is not of 2^n)

    //<STEP 1>
    //MergeSort(A, 0, 4)
    //{
    //    if (0 < 4)
    //    {
    //        q = (0 + 4) / 2 = 2;
    //        MergeSort(A, 0, 2)
    //        MergeSort(A, 3, 4)
    //        Merge(A, 0, 2, 4)
    //    }
    //}

    //<STEP 2>
    //MergeSort(A, 0, 4)
    //{
    //    if (0 < 4)
    //    {
    //        q = (0 + 4) / 2 = 2
    //        //MergeSort(A, 0, 2)
    //        if (0 < 2)
    //        {
    //            q = (0 + 2) / 2 = 1
    //            MergeSort(A, 0, 1)
    //            MergeSort(A, 2, 2)
    //            Merge(A, 0, 1, 2)
    //        }
    //        //MergeSort(A, 3, 4)
    //        if (3 < 4)
    //        {
    //            q = (3 + 4) / 2 = 3
    //            MergeSort(A, 3, 3)
    //            MergeSort(A, 4, 4)
    //            Merge(A, 3, 3, 4)
    //        }
    //        Merge(A, 0, 2, 4)
    //    }
    //}

    //<STEP 3>
    //MergeSort(A, 0, 4)
    //{
    //    if (0 < 4)
    //    {
    //        q = (0 + 4) / 2 = 2
    //        //MergeSort(A, 0, 2)
    //        if (0 < 2)
    //        {
    //            q = (0 + 2) / 2 = 1
    //            //MergeSort(A, 0, 1)
    //            if (0 < 1)
    //            {
    //                q = (0 + 1) / 2 = 0
    //                MergeSort(A, 0, 0)
    //                MergeSort(A, 1, 1)
    //                Merge(A, 0, 0, 1)
    //            }
    //            //MergeSort(A, 2, 2)
    //            if (2 < 2) {}
    //            Merge(A, 0, 1, 2)
    //        }
    //        //MergeSort(A, 3, 4)
    //        if (3 < 4)
    //        {
    //            q = (3 + 4) / 2 = 3
    //            //MergeSort(A, 3, 3)
    //            if (3 < 3) {}
    //            //MergeSort(A, 4, 4)
    //            if (4 < 4) {}
    //            Merge(A, 3, 3, 4)
    //        }
    //        Merge(A, 0, 2, 4)
    //    }
    //}

    //<STEP 4>
    //MergeSort(A, 0, 4)
    //{
    //    if (0 < 4)
    //    {
    //        q = (0 + 4) / 2 = 2
    //        //MergeSort(A, 0, 2)
    //        if (0 < 2)
    //        {
    //            q = (0 + 2) / 2 = 1
    //            //MergeSort(A, 0, 1)
    //            if (0 < 1)
    //            {
    //                q = (0 + 1) / 2 = 0
    //                //MergeSort(A, 0, 0)
    //                if (0 < 0) {}
    //                //MergeSort(A, 1, 1)
    //                if (1 < 1) {}
    //                Merge(A, 0, 0, 1)
    //            }
    //            //MergeSort(A, 2, 2)
    //            if (2 < 2) {}
    //            Merge(A, 0, 1, 2)
    //        }
    //        //MergeSort(A, 3, 4)
    //        if (3 < 4)
    //        {
    //            q = (3 + 4) / 2 = 3
    //            //MergeSort(A, 3, 3)
    //            if (3 < 3) {}
    //            //MergeSort(A, 4, 4)
    //            if (4 < 4) {}
    //            Merge(A, 3, 3, 4)
    //        }
    //        Merge(A, 0, 2, 4)
    //    }
    //}

    //<Overall> 8,9,7,4,2
    //Merge(A, 0, 0, 1) => 8|9 => 8|9
    //Merge(A, 0, 1, 2) => (8,9)|7 => (7,8,9)
    //Merge(A, 3, 3, 4) => 4|2 => (2,4)
    //Merge(A, 0, 2, 4) => (7,8,9)|(2,4) => (2,4,7,8,9)
    //--------------------------------------------------------------
    //------------- ArraySize从1到9的实际运算过程实例 ------------------
    //--------------------------------------------------------------
    //void MergeSort(int[] mArray, int p, int r)
    //{
    //    if (p < r)
    //    {
    //        int q = (p + r) / 2;
    //        MergeSort(mArray, p, q);
    //        MergeSort(mArray, q + 1, r);
    //        MergeProcedure(mArray, p, q, r);
    //    }
    //}
    //--------------------------------------------------------------
    //<Overall> 8
    //Already in order. (p = r)
    //--------------------------------------------------------------
    //<Overall> 8,9
    //Merge(A, 0, 0, 1) => (8) # (9) => (8,9)
    //--------------------------------------------------------------
    //<Overall> 8,9,7
    //Merge(A, 0, 0, 1) => (8) # (9) => (8,9)
    //Merge(A, 0, 1, 2) => (8,9) # (7) => (7,8,9)
    //--------------------------------------------------------------
    //<Overall> 8,9,7,4
    //Merge(A, 0, 0, 1) => 8|9 => 8,9
    //Merge(A, 2, 2, 3) => 7|4 => 4,7
    //Merge(A, 0, 1, 3) => (8,9)|(4,7) => 4,7,8,9
    //--------------------------------------------------------------
    //<Overall> 8,9,7,4,2
    //Merge(A, 0, 0, 1) => 8|9 => 8|9
    //Merge(A, 0, 1, 2) => (8,9)|7 => (7,8,9)
    //Merge(A, 3, 3, 4) => 4|2 => (2,4)
    //Merge(A, 0, 2, 4) => (7,8,9)|(2,4) => (2,4,7,8,9)
    //--------------------------------------------------------------
    //<Overall> 8,9,7,4,2,6
    //Merge(A, 0, 0, 1) => (8) # (9) => (8,9)
    //Merge(A, 0, 1, 2) => (8,9) # (7) => (7,8,9)
    //Merge(A, 3, 3, 4) => (4) # (2) => (2,4)
    //Merge(A, 3, 4, 5) => (2,4) # (6) => (2,4,6)
    //Merge(A, 0, 2, 5) => (7,8,9) # (2,4,6) => (2,4,6,7,8,9)
    //--------------------------------------------------------------
    //<Overall> 8,9,7,4,2,6,1
    //Merge(A, 0, 0, 1) => (8) # (9) => (8,9)
    //Merge(A, 2, 2, 3) => (7) # (4) => (4,7)
    //Merge(A, 0, 1, 3) => (8,9) # (4,7) => (4,7,8,9)
    //Merge(A, 4, 4, 5) => (2) # (6) => (2,6)
    //Merge(A, 4, 5, 6) => (2,6) # (1) => (1,2,6)
    //Merge(A, 0, 3, 6) => (4,7,8,9) # (1,2,6) => (1,2,4,6,7,8,9)
    //--------------------------------------------------------------
    //<Overall> 8,9,7,4,2,6,1,3
    //Merge(A, 0, 0, 1) => (8) # (9) => (8,9)
    //Merge(A, 2, 2, 3) => (7) # (4) => (4,7)
    //Merge(A, 0, 1, 3) => (8,9) # (4,7) => (4,7,8,9)
    //Merge(A, 4, 4, 5) => (2) # (6) => (2,6)
    //Merge(A, 6, 6, 7) => (1) # (3) => (1,3)
    //Merge(A, 4, 5, 7) => (2,6) # (1,3) => (1,2,3,6)
    //Merge(A, 0, 3, 7) => (4,7,8,9) # (1,2,3,6) => (1,2,3,4,6,7,8,9)
    //--------------------------------------------------------------
    //<Overall> 8,9,7,4,2,6,1,3,5
    //Merge(A, 0, 0, 1) => (8) # (9) => (8,9)
    //Merge(A, 0, 1, 2) => (8,9) # (7) => (7,8,9)
    //Merge(A, 3, 3, 4) => (4) # (2) => (2,4)
    //Merge(A, 0, 2, 4) => (7,8,9) # (2,4) => (2,4,7,8,9)
    //Merge(A, 5, 5, 6) => (6) # (1) => (1,6)
    //Merge(A, 7, 7, 8) => (3) # (5) => (3,5)
    //Merge(A, 5, 6, 8) => (1,6) # (3,5) => (1,3,5,6)
    //Merge(A, 0, 4, 8) => (2,4,7,8,9) # (1,3,5,6) => (1,2,3,4,5,6,7,8,9)
    //--------------------------------------------------------------
    //<Overall> 8,9,7,4,2,6,1,3,5,0
    //Merge(A, 0, 0, 1) => (8) # (9) => (8,9)
    //Merge(A, 0, 1, 2) => (8,9) # (7) => (7,8,9)
    //Merge(A, 3, 3, 4) => (4) # (2) => (2,4)
    //Merge(A, 0, 2, 4) => (7,8,9) # (2,4) => (2,4,7,8,9)
    //Merge(A, 5, 5, 6) => (6) # (1) => (1,6)
    //Merge(A, 5, 6, 7) => (1,6) # (3) => (1,3,6)
    //Merge(A, 8, 8, 9) => (5) # (0) => (0,5)
    //Merge(A, 5, 7, 9) => (1,3,6) # (0,5) => (0,1,3,5,6)
    //Merge(A, 0, 4, 9) => (2,4,7,8,9) # (0,1,3,5,6) => (0,1,2,3,4,5,6,7,8,9)
    #endregion

}
