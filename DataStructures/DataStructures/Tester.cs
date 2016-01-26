using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    class Tester
    {
        static void Main(string[] args)
        {
            //TreeTraversal();
            //ImplementQueueUsingStack();
            //ImplementStackUsingQueue();
            //UseHash();
            //TestLinkedList();
        }

        public static void TreeTraversal()
        {
            //Define a binary tree.
            //            1
            //          /   \
            //         2     3
            //        / \   / \
            //       4  5   6  7 

            TreeNode root = new TreeNode(1);
            root.left = new TreeNode(2);
            root.right = new TreeNode(3);
            root.left.left = new TreeNode(4);
            root.left.right = new TreeNode(5);
            root.right.left = new TreeNode(6);
            root.right.right = new TreeNode(7);

            List<int> preResult = root.PreOrderTraversal(root);     //1, 2, 4, 5, 3, 6, 7
            List<int> preResult_i = root.PreOrderTraversal_Iterative1(root);
            List<int> preResult_i2 = root.PreOrderTraversal_Iterative2(root);

            List<int> inResult = root.InOrderTraversal(root);       //4, 2, 5, 1, 6, 3, 7
            List<int> inResult_i = root.InOrderTraversal_Iterative1(root); 

            List<int> postResult = root.PostOrderTraversal(root);   //4, 5, 2, 6, 7, 3, 1
            List<int> postResult_i = root.PostOrderTraversal_Iterative1(root);
            List<int> postResult_i2 = root.PostOrderTraversal_Iterative2(root);

            List<List<int>> levelResult = root.LevelOrderTraversal(root);
        }

        public static void ImplementQueueUsingStack()
        {
            QueueWithStack myQ = new QueueWithStack();
            myQ.Enqueue(1);
            myQ.Enqueue(2);
            myQ.Enqueue(3);
            Console.WriteLine(myQ.Dequeue());
            Console.WriteLine(myQ.Dequeue());
            myQ.Enqueue(4);
            myQ.Enqueue(5);
            Console.WriteLine(myQ.Dequeue());
            Console.WriteLine(myQ.Dequeue());
            Console.WriteLine(myQ.Peek());
        }

        public static void ImplementStackUsingQueue()
        {
            StackWithQueue myStack = new StackWithQueue();
            myStack.Push(1);
            myStack.Push(2);
            myStack.Push(3);
            Console.WriteLine(myStack.Pop());
            Console.WriteLine(myStack.Pop());
            myStack.Push(4);
            myStack.Push(5);
            Console.WriteLine(myStack.Pop());
            Console.WriteLine(myStack.Pop());
            Console.WriteLine(myStack.Peek());
        }

        public static void UseHash()
        {
            Hash myhash = new Hash();
            myhash.UseHashTable();
            myhash.UseDictionary();
        }

        public static void TestLinkedList()
        {
            LinkedList myLinkedList = new LinkedList();
            ListNode head = myLinkedList.CreateLinkedList(new int[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            head = myLinkedList.ReverseLinkedList_Iterative(head);
            head = myLinkedList.ReverseLinkedList_Recursive_TopDown(head);
            head = myLinkedList.ReverseLinkedList_Recursive_BottomUp(head);

            ListNode newHead = myLinkedList.DeleteGivenValue_Iterative(head, 2);
            
            ListNode nodeDeleted = head.next.next.next.next.next.next.next;
            myLinkedList.DeleteGivenNode(nodeDeleted);
        }
    }
}
