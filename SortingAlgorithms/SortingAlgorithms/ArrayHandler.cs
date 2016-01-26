using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace SortingAlgorithms
{
    class ArrayHandler
    {
        //定义方法的形参时如果已经赋值，那么这个形参就是optional的，不一定要有。
        public static int[] Create(int length, bool enableLogging, int max, int min = 0)
        {
            Random rand = new Random();
            int[] mArray = new int[length];
            for (int i = 0; i < length; i++)
            {
                mArray[i] = rand.Next(min, max);
            }
            Print(mArray, enableLogging);

            return mArray;
        }

        public static int[] CreateAlmostSorted(int length, bool enableLogging)
        {
            int[] mArray = new int[length];
            for (int i = 0; i < mArray.Length; i++)
            {
                if (i == 0)
                    mArray[i] = mArray.Length;
                else
                    mArray[i] = i;
            }
            Print(mArray, enableLogging);
            return mArray;
        }

        //Create a element whos have the given digit
        public static int[] CreateGivenDigit(int length, int digit, bool enableLogging)
        {
            Random rand = new Random();
            if (digit < 1)
            {
                return null;
            }
            int[] mArray = new int[length];
            int min, max;
            if (digit == 1)
            {
                min = 0;
                max = 9;
            }
            else
            {
                min = (int)Math.Pow(10, digit - 1);
                max = (int)Math.Pow(10, digit) - 1;
            }

            for (int i = 0; i < length; i++)
            {
                mArray[i] = rand.Next(min, max);
            }
            Print(mArray, enableLogging);

            return mArray;
        }
        
        //泛型方法的使用：
        //首先在方法名后面紧跟着使用尖括号把泛型的名字括起来（一般都用T来代替）
        //然后方法的形参中传入的参数类型也用T来定义，方法体里面相应的运算赋值也都用T来定义和使用
        //这样到时候实际调用这个方法的时候，就会根据调用的实参类型来推理出这个T到底是int还是double还是任何类型
        //泛型方法的最大优点就是重用方法非常容易，而且不需要考虑强制转换问题。
        public static void Print<T>(T[] myArray, bool logEnabled = false)
        {
            if (logEnabled)
            {
                foreach (T element in myArray)
                {
                    Console.Write(element + " ");
                }
                Console.WriteLine();
            }
        }

        //Print subarray of myArray.
        public static void Print<T>(T[] myArray, int start, int end, bool logEnabled = false)
        {
            if (logEnabled && start>=0 && end < myArray.Length && start <= end)
            {
                for (int i = start; i <= end; i++)
                {
                    Console.Write(myArray[i] + " ");
                }
            }
        }

        public static void Swap<T>(ref T arg1, ref T arg2)
        {
            T temp; //方法体中的局部变量需要和入参的类型一致
            temp = arg1;
            arg1 = arg2;
            arg2 = temp;
        }

        public static int[] CollectInputArray()
        {
            Console.WriteLine("Type in an array of integer. Separate each element using ','.");
            Console.WriteLine("Hit ENTER to finish.");
            Console.Write("Your Array: ");
            string userString = Console.ReadLine();
            string[] userStringArray = userString.Split(',');
            int[] userIntArray = new int[userStringArray.Length];
            for (int i = 0; i < userStringArray.Length; i++)
            {
                if (userStringArray[i] != "")
                {
                    userIntArray[i] = Convert.ToInt32(userStringArray[i]);
                }
                else
                {
                    userIntArray[i] = -1;
                }
            }
            return userIntArray;
        }

        //Return the value of a selected digit in given element, from right to left, digit enumerated from 1 to n.
        public static int ExtractDigitValue(int element, int selectedDigit)
        {
            int digitNumber = GetElementDigit(element);
            int reminder = element;
            int quotient = 0;
            for (int x = digitNumber - 1; x >= selectedDigit; x--)
            {
                reminder = reminder % (int)Math.Pow(10, x); //Calculate the reminder until reach the selected digit 先求余
            }
            quotient = reminder / (int)Math.Pow(10, selectedDigit - 1); //Calculate the quotient to get the digit value 再求商
            return quotient;
        }

        //Return the total digit of this element
        public static int GetElementDigit(int element)
        {
            double temp = Math.Log10(element);
            return (int)Math.Floor(temp) + 1;
        }

        //Return the max element of a given array, so that we can know the max digit of the array.
        public static int GetMaxElement(int[] mArray)
        {
            int max = 0;
            for (int i = 0; i < mArray.Length; i++)
            {
                if (mArray[i] > max)
                {
                    max = mArray[i];
                }
            }
            return max;
        }
    }
}
