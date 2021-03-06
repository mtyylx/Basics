﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int x) { val = x; }

        //深度优先和广度优先的本质是相反的
        //深度优先DFS的思想本质上是栈(Stack)，而广度优先BFS的思想本质上是队列(Queue)
        //这个基本属性的不同就决定了DFS和BFS的实现方式是根本不同的。
        //DFS的栈属性使得它需要用迭代+栈来实现，或者用递归（本质上是函数栈，即压入栈的是函数的工作记录，后入先出）来实现非常容易。
        //BFS的队列属性使得它需要用迭代+队列来实现，想要用递归或栈实现很困难。

        //---------------------------------------------------------------------
        //-------------     D F S  +  R E C U R S I V E     -------------------
        //---------------------------------------------------------------------
        //DFS: 先序 Pre-order Traversal (ROOT -> Left -> Right) using Recursion
        public List<int> PreOrderTraversal(TreeNode root)
        {
            List<int> result = new List<int>();
            if (root != null)
            {
                result.Add(root.val);
                result.AddRange(PreOrderTraversal(root.left));
                result.AddRange(PreOrderTraversal(root.right));
            }
            return result;
        }

        //DFS: 中序 In-order Traversal (Left -> ROOT -> Right) using Recursion
        public List<int> InOrderTraversal(TreeNode root)
        {
            List<int> result = new List<int>();
            if (root != null)
            {
                result.AddRange(InOrderTraversal(root.left));
                result.Add(root.val);
                result.AddRange(InOrderTraversal(root.right));
            }
            return result;
        }

        //DFS: 后序 Post-order Traversal (Left -> Right -> ROOT) using Recursion
        public List<int> PostOrderTraversal(TreeNode root)
        {
            List<int> result = new List<int>();
            if (root != null)
            {
                result.AddRange(PostOrderTraversal(root.left));
                result.AddRange(PostOrderTraversal(root.right));
                result.Add(root.val);
            }
            return result;
        }


        //---------------------------------------------------------------------
        //-------------     D F S  +  I T E R A T I V E     -------------------
        //---------------------------------------------------------------------
        //DFS: 先序 Pre-order Traversal, Stack
        public List<int> PreOrderTraversal_Iterative1(TreeNode root)
        {
            List<int> result = new List<int>();
            Stack<TreeNode> stack = new Stack<TreeNode>();

            stack.Push(root);
            while (stack.Count != 0)
            {
                root = stack.Pop();
                if (root != null)
                {
                    result.Add(root.val);
                    //Prevent pushing null node to enhance performance.
                    if (root.right != null) stack.Push(root.right);
                    if (root.left != null) stack.Push(root.left);
                }
            }
            return result;
        }

        //DFS: 先序 Pre-order Traversal, Stack
        public List<int> PreOrderTraversal_Iterative2(TreeNode root)
        {
            List<int> result = new List<int>();
            Stack<TreeNode> stack = new Stack<TreeNode>();

            while (root != null || stack.Count != 0)
            {
                if (root != null)
                {
                    //Store root value first.
                    result.Add(root.val);
                    //Push right child into stack
                    stack.Push(root.right);
                    //Iterate left child first.
                    root = root.left;
                }
                else
                {
                    //if left child does not exist anymore, iterate the nearest right child.
                    root = stack.Pop();
                }

            }
            return result;
        }

        //DFS: 中序 In-order Traversal, Stack
        public List<int> InOrderTraversal_Iterative1(TreeNode root)
        {
            //Step 1: Iterate left subtree, push left node into stack.
            //Step 2: Stop push until node is null (its parent has no left child).
            //Step 3: Pop current node, store the value.
            //Step 4: Switch to the right subtree and iterate right subtree,
            List<int> result = new List<int>();
            Stack<TreeNode> stack = new Stack<TreeNode>();

            //Stop loop when both root is null and stack is empty.
            while (root != null || stack.Count != 0)
            {
                if (root != null)
                {
                    //Store root for later use
                    stack.Push(root);
                    //Iterate left child first.
                    root = root.left;
                }

                else
                {
                    //If left child does not exist anymore, extract its parent
                    root = stack.Pop();
                    //And store parent value
                    result.Add(root.val);
                    //Then iterate its right child.
                    root = root.right;
                }
            }
            return result;
        }

        //DFS: 后序 Post-order Traversal, Stack
        public List<int> PostOrderTraversal_Iterative1(TreeNode root)
        {
            //Postorder traversal is more complicated to implement comparing to Preorder and Inorder.
            //At what point shall we visit the root node:
            //Condition 1: The root node has no child at all.
            //Condition 2: The root's child has already been visited.
            List<int> result = new List<int>();
            Stack<TreeNode> stack = new Stack<TreeNode>();
            if (root != null)
                stack.Push(root);
            TreeNode prev = null;
            while (stack.Count != 0)
            {
                root = stack.Peek();

                bool noChild = (root.left == null && root.right == null);
                bool doneChild = false;

                if (prev != null && (prev == root.left || prev == root.right))
                    doneChild = true;

                //直到没有child了或者child已经访问完了才把栈顶元素出栈
                if (noChild || doneChild)
                {
                    root = stack.Pop();
                    result.Add(root.val);
                    prev = root;
                }
                else
                {
                    if (root.right != null)
                        stack.Push(root.right);
                    if (root.left != null)
                        stack.Push(root.left);
                }
            }

            return result;
        }

        //DFS: 后序 Post-order Traversal, (Reversed Pre-order Traversal)
        public List<int> PostOrderTraversal_Iterative2(TreeNode root)
        {
            List<int> result = new List<int>();

            //Iterative: Reverse Pre-Order Traversal.
            Stack<TreeNode> stack = new Stack<TreeNode>();
            while (root != null || stack.Count != 0)
            {
                if (root != null)
                {
                    result.Add(root.val);
                    if (root.left != null) stack.Push(root.left);
                    root = root.right;
                }
                else
                {
                    root = stack.Pop();
                }
            }
            result.Reverse();
            return result;
        }


        //---------------------------------------------------------------------
        //-------------     B F S  +  I T E R A T I V E     -------------------
        //---------------------------------------------------------------------
        //BFS: Level-order Traversal, Queue    
        public List<List<int>> LevelOrderTraversal(TreeNode root)
        {
            List<List<int>> result = new List<List<int>>();
            Queue<TreeNode> queue = new Queue<TreeNode>();

            queue.Enqueue(root);
            while (queue.Count != 0)
            {
                //Initiate a List<int> container for each level.
                List<int> level = new List<int>();
                //Store the static size as loop condition (this is a must because queue.Count is shrinking in each loop.)
                int size = queue.Count;
                for (int i = 0; i < size; i++)
                {
                    //Dequeue each node
                    root = queue.Dequeue();
                    //Save the node value
                    level.Add(root.val);
                    //And enqueue its child at the same time
                    if (root.left != null) queue.Enqueue(root.left);
                    if (root.right != null) queue.Enqueue(root.right);
                }
                //Save the level after dequeue all the node.
                result.Add(level);
            }
            return result;
        }


        //-------------     D F S  +  R E C U R S I V E  +  B O T T O M - U P   -------------------
        //Solution 1: Recursion without helper function.
        //List all root2leaf path in List<string>
        public List<string> ListBinaryTreePath1(TreeNode root)
        {
            //Note that every recursion create its own "result", so what was added in lower level (recursed into) has no affect to the current level.
            //Processed in Bottom-Up order, the current level will copy each entry (temp[i]) returned from lower level to its own result AFTER recurse bounce back.
            List<string> temp;
            List<string> result = new List<string>();

            //Null node (List count will remain 0 so NO value copied later.)
            if (root == null) return result;

            //Leaf node: Add current level (only one entry) and return
            if (root.left == null && root.right == null)
            {
                result.Add(root.val.ToString());
                return result;
            }

            //Recurse left subtree: Add current level value to every entry returned from the lower level.
            temp = ListBinaryTreePath1(root.left);
            for (int i = 0; i < temp.Count; i++) result.Add(root.val + "->" + temp[i]);

            //Recurse right subtree: Add current level value to every entry returned from the lower level.
            temp = ListBinaryTreePath1(root.right);
            for (int i = 0; i < temp.Count; i++) result.Add(root.val + "->" + temp[i]);

            return result;
        }

        //-------------     D F S  +  R E C U R S I V E  +  T O P - D O W N   -------------------
        //Solution 2: Recursion with helper function.
        //Key idea: While going deep during recursion, append the current node value into string "path", 
        //But at the same time, each level of recursion kepted its own version of "path"
        //Once reached leaf node, the "path" is done and add to string list "result"
        //该方案利用了string类型是绝对引用而List<string>里面装的是对象所以本质上是Reference Variable的特点，
        //用string类型的path确保每一层的path是独立的，但同时用List<string>类型的result确保所有的path都添加到一个List中并被返回
        private void ListBinaryTreePath2_Helper(TreeNode root, string path, List<string> result)
        {
            if (root.left == null && root.right == null) result.Add(path + root.val);
            if (root.left != null) ListBinaryTreePath2_Helper(root.left, path + root.val + "->", result);
            if (root.right != null) ListBinaryTreePath2_Helper(root.right, path + root.val + "->", result);
        }

        public List<string> ListBinaryTreePath2(TreeNode root)
        {
            List<string> treePath = new List<string>();
            if (root != null) ListBinaryTreePath2_Helper(root, "", treePath);
            return treePath;
        }


    }


}
