using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortingAlgorithms
{
    class BucketSort
    {
        //这里使用List<T>[]的原因是：
        //原数组中有可能出现多个值相等的元素，这些元素需要放到一个Bucket里面，
        //但是我们又不能很简单的知道有多少个这样相等的元素，因此没法用需要指定长度的一般数组，而必须用动态长度的泛型List
        //List<T>[]本质上是泛型List数组，即泛型数组的数组，这个数组整体上是一个一般的Array，特殊的地方在于每一个元素都是一个泛型List，可以自动增长。
        
        //这里使用List<T>作为bucket来存储元素的好处是：
        //泛型List不仅类型安全，而且存储空间可以自动扩增

        //对比Array / ArrayList / List<>这三者特性的区别
        //Array的内存分配是连续的，但是定义时必须指定长度，且对元素的增删很麻烦。
        //ArrayList的内存分配是动态增减的因此无需指定长度，但是他里面存储的元素类型是没有限制的，会导致装箱和拆箱的性能损耗。
        //List<>的内存分配也是动态增减的，而且他对于所存储的元素有类型限制，是类型安全的。

        public int[] Sort(int[] mArray)
        {
            int[] sorted = new int[mArray.Length];
            int max = ArrayHandler.GetMaxElement(mArray);
            
            //定义一个泛型数组数组，每个整数值算一个泛型数组，也就是一个bucket，所以从0到最大值一共max+1个
            List<int>[] bucket = new List<int>[max + 1];

            //初始化每一个泛型数组
            for (int i = 0; i <= max; i++)
            {
                bucket[i] = new List<int>();
            }

            //把原始数组的元素放到每个泛型数组里，值相同的就在泛型数组里扩增即可
            for (int i = 0; i < mArray.Length; i++)
            {
                bucket[mArray[i]].Add(mArray[i]);
            }

            int x = 0;
            for(int i = 0; i <= max; i++)
            {
                if (bucket[i].Count > 0)
                {
                    for (int j = 0; j < bucket[i].Count; j++)
                    {
                        sorted[x] = bucket[i][j];
                        x++;
                    }
                }
            }

            //本质上BucketSort和CountingSort的原理很像，都是把元素值放到数组index里面折腾
            //只不过BucketSort利用了泛型数组的方便功能，不需要算histo数组再往回倒。

            return sorted;
        }
    }
}
