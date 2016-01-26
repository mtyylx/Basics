using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructures
{
    class LinkedList
    {
        public ListNode CreateLinkedList(int[] element)
        {
            ListNode prev = new ListNode(0);
            ListNode head = prev;
            for (int i = 0; i < element.Length; i++)
            {
                ListNode newNode = new ListNode(element[i]);
                prev.next = newNode;
                prev = prev.next;
            }
            return head.next;
        }

        //Delete the given node from the LinkedList (node cannot be tail node.)
        public void DeleteGivenNode(ListNode node)
        {
            if (node.next != null)
            {
                node.val = node.next.val;
                node.next = node.next.next;
            }
        }

        //Delete all nodes that has the given value. Iterative solution using dummy head.
        public ListNode DeleteGivenValue_Iterative(ListNode head, int value)
        {
            ListNode dummy = new ListNode(0);
            dummy.next = head;
            ListNode curr = dummy;

            while (curr.next != null)
            {
                if (curr.next.val == value) curr.next = curr.next.next;
                else curr = curr.next;
            }
            return dummy.next;
        }

        //Delete all nodes that has the given value. Elegant Recursive solution.
        public ListNode DeleteGivenValue_Recursive(ListNode head, int value)
        {
            //Tail
            if (head == null) return null;
            //Delete node
            if (head.val == value) return DeleteGivenValue_Recursive(head.next, value);
            //Recurse all node.
            else head.next = DeleteGivenValue_Recursive(head.next, value);
            return head;
        }

        //In each loop, Focus on reversing the "current node" (i.e. head) only.
        //Do not try to do too many things in one loop, because it confuse people.
        public ListNode ReverseLinkedList_Iterative(ListNode head)
        {
            //For the first node, prevNode is null
            //For the last node (null, loop exit condition), prevNode is the first node of the reversed linkedlist.
            ListNode prevNode = null;
            while (head != null)
            {
                ListNode nextNode = head.next;      //Before pointing "head.next" to the "prevNode", save the node "head.next" originally pointing at to "tail".
                head.next = prevNode;               //Reverse the current node by pointing "head.next" to the "prevNode".
                prevNode = head;                    //Current node completed. Move on to next node. Since the focus changed from head to next, prevNode should be changed to head.
                head = nextNode;                    //Since the focus changed from head to next, head should be changed to tail.
            }
            return prevNode;
        }

        //Bottom-Up Recursive Solution.
        //Line 1+2+5 => for return the tail of the list as new head.
        //Line 3+4 => for reverse the pointer while bottom-up.
        public ListNode ReverseLinkedList_Recursive_BottomUp(ListNode head)
        {
            if (head == null || head.next == null) return head;                 //Line 1: "head == null" is used to handle empty linkedlist. "head.next == null" is used to return the newHead(tail).
            ListNode tail = ReverseLinkedList_Recursive_BottomUp(head.next);    //Line 2: Recurse till the last node, and return it.
            head.next.next = head;      //Line 3: Change From [ prev -> head -> next ] to [ prev -> head <=> next ]
            head.next = null;           //Line 4: Change From [ prev -> head <=> next ] to [ prev -> head <- next ] + [ head -> null ]
            return tail;                                            //Line 5: work with Line 1 and 2 to pass the last node from deepest recursion to the top. Ensure the return value is the new head.
        }

        //Top-Down Recursive Solution.
        private ListNode ReverseLinkedList(ListNode prevNode, ListNode head)
        {
            if (head == null) return head;
            ListNode nextNode = head.next;      //IDENTICAL TO Iterative solution.
            head.next = prevNode;               //IDENTICAL TO Iterative solution.
            return ReverseLinkedList(head, nextNode);
        }

        public ListNode ReverseLinkedList_Recursive_TopDown(ListNode head)
        {
            return ReverseLinkedList(null, head);
        }

    }
    
    class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int x) { val = x; }
    }
}
