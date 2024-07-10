using System;
using System.Collections.Generic;

namespace AVLTree
{
    public class AVLTree<T> where T : IComparable<T>
    {
        // AVL tree node class
        public class Node
        {
            public T Data;
            public Node Left, Right;
            public int Height;

            public Node(T data)
            {
                Data = data;
                Height = 1;
            }
        }

        private Node root;

        // Returns the height of the given node or 0 if the node is null.
        public int Height(Node node)
        {
            return node?.Height ?? 0;
        }

        // Updates the height of the given node based on the heights of its children.
        private void UpdateHeight(Node node)
        {
            node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;
        }

        public int GetHeight()
        {
            return Height(root);
        }

        // Gets the balance factor of the node. A balance factor of -1, 0, or 1 means the node is balanced.
        private int GetBalanceFactor(Node node)
        {
            return node == null ? 0 : Height(node.Left) - Height(node.Right);
        }

        // Performs a left rotation balance.
        private Node RotateLeft(Node x)
        {
            Node y = x.Right;
            Node T2 = y.Left;

            y.Left = x;
            x.Right = T2;

            UpdateHeight(x);
            UpdateHeight(y);

            return y;
        }

        // Performs a right rotation  balance.
        private Node RotateRight(Node y)
        {
            Node x = y.Left;
            Node T2 = x.Right;

            x.Right = y;
            y.Left = T2;

            UpdateHeight(y);
            UpdateHeight(x);

            return x;
        }

        // Combination of left and right rotation.
        private Node RotateLeftRight(Node node)
        {
            node.Left = RotateLeft(node.Left);
            return RotateRight(node);
        }

        // Combination of right and left rotation.
        private Node RotateRightLeft(Node node)
        {
            node.Right = RotateRight(node.Right);
            return RotateLeft(node);
        }

        // Balances the subtree rooted at the given node.
        private Node Balance(Node node)
        {
            int balanceFactor = GetBalanceFactor(node);

            if (balanceFactor > 1)
            {
                if (GetBalanceFactor(node.Left) < 0)
                    node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }

            if (balanceFactor < -1)
            {
                if (GetBalanceFactor(node.Right) > 0)
                    node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }

            return node;
        }

        // Public method to insert a value into the AVL tree.
        public void Insert(T data)
        {
            root = Insert(root, data);
        }

        // Recursive insertion with balancing.
        private Node Insert(Node node, T data)
        {
            if (node == null)
                return new Node(data);

            int compare = data.CompareTo(node.Data);
            if (compare < 0)
                node.Left = Insert(node.Left, data);
            else if (compare > 0)
                node.Right = Insert(node.Right, data);
            else
                throw new InvalidOperationException("Duplicate values are not allowed");

            UpdateHeight(node);
            return Balance(node);
        }

        // Public method to check if the tree contains a specific value.
        public bool Contains(T data)
        {
            return Contains(root, data);
        }

        // Recursive search to check if the tree contains a specific value.
        private bool Contains(Node node, T data)
        {
            if (node == null)
                return false;

            int compare = data.CompareTo(node.Data);
            if (compare < 0)
                return Contains(node.Left, data);
            else if (compare > 0)
                return Contains(node.Right, data);
            else
                return true;
        }

        // Public method to delete a value from the AVL tree.
        public void Delete(T data)
        {
            root = Delete(root, data);
        }

        // Recursive deletion with balancing.
        private Node Delete(Node node, T data)
        {
            if (node == null)
                return node;

            int compare = data.CompareTo(node.Data);
            if (compare < 0)
                node.Left = Delete(node.Left, data);
            else if (compare > 0)
                node.Right = Delete(node.Right, data);
            else
            {
                if (node.Left == null || node.Right == null)
                {
                    Node temp = node.Left ?? node.Right;
                    if (temp == null)
                    {
                        temp = node;
                        node = null;
                    }
                    else
                        node = temp;
                }
                else
                {
                    Node temp = GetMinValueNode(node.Right);
                    node.Data = temp.Data;
                    node.Right = Delete(node.Right, temp.Data);
                }
            }

            if (node == null)
                return node;

            UpdateHeight(node);
            return Balance(node);
        }

        // Finds the node with the minimum value in the given subtree.
        private Node GetMinValueNode(Node node)
        {
            Node current = node;
            while (current.Left != null)
                current = current.Left;
            return current;
        }

        // Prints the tree using level-order traversal.
        public void PrintTree()
        {
            if (root == null)
            {
                Console.WriteLine("Tree is empty.");
                return;
            }

            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                Node current = queue.Dequeue();
                Console.Write(current.Data + " ");

                if (current.Left != null)
                {
                    queue.Enqueue(current.Left);
                }
                if (current.Right != null)
                {
                    queue.Enqueue(current.Right);
                }
            }
            Console.WriteLine();
        }

        // Performs in-order traversal of the tree.
        public void InOrderTraversal(Action<T> action)
        {
            InOrderTraversal(root, action);
        }

        // Recursive in-order traversal helper method.
        private void InOrderTraversal(Node node, Action<T> action)
        {
            if (node != null)
            {
                InOrderTraversal(node.Left, action);
                action(node.Data);
                InOrderTraversal(node.Right, action);
            }
        }

        // Performs pre-order traversal of the tree.
        public void PreOrderTraversal(Action<T> action)
        {
            PreOrderTraversal(root, action);
        }

        // Recursive pre-order traversal helper method.
        private void PreOrderTraversal(Node node, Action<T> action)
        {
            if (node != null)
            {
                action(node.Data);
                PreOrderTraversal(node.Left, action);
                PreOrderTraversal(node.Right, action);
            }
        }

        // Performs post-order traversal of the tree.
        public void PostOrderTraversal(Action<T> action)
        {
            PostOrderTraversal(root, action);
        }

        // Recursive post-order traversal helper method.
        private void PostOrderTraversal(Node node, Action<T> action)
        {
            if (node != null)
            {
                PostOrderTraversal(node.Left, action);
                PostOrderTraversal(node.Right, action);
                action(node.Data);
            }
        }
    }
}
