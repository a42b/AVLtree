using System;
using System.Collections.Generic;

namespace AVLTree
{
    public class AVLTree<T> where T : IComparable<T>
    {
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

        public int Height(Node node)
        {
            return node?.Height ?? 0;
        }

        private void UpdateHeight(Node node)
        {
            node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;
        }

        public int GetHeight()
        {
            return Height(root);
        }

        private int GetBalanceFactor(Node node)
        {
            return node == null ? 0 : Height(node.Left) - Height(node.Right);
        }

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

        private Node RotateLeftRight(Node node)
        {
            node.Left = RotateLeft(node.Left);
            return RotateRight(node);
        }

        private Node RotateRightLeft(Node node)
        {
            node.Right = RotateRight(node.Right);
            return RotateLeft(node);
        }

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

        public void Insert(T data)
        {
            root = Insert(root, data);
        }

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

        public bool Contains(T data)
        {
            return Contains(root, data);
        }

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

        public void Delete(T data)
        {
            root = Delete(root, data);
        }

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

        private Node GetMinValueNode(Node node)
        {
            Node current = node;
            while (current.Left != null)
                current = current.Left;
            return current;
        }

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

        public void InOrderTraversal(Action<T> action)
        {
            InOrderTraversal(root, action);
        }

        private void InOrderTraversal(Node node, Action<T> action)
        {
            if (node != null)
            {
                InOrderTraversal(node.Left, action);
                action(node.Data);
                InOrderTraversal(node.Right, action);
            }
        }

        public void PreOrderTraversal(Action<T> action)
        {
            PreOrderTraversal(root, action);
        }

        private void PreOrderTraversal(Node node, Action<T> action)
        {
            if (node != null)
            {
                action(node.Data);
                PreOrderTraversal(node.Left, action);
                PreOrderTraversal(node.Right, action);
            }
        }

        public void PostOrderTraversal(Action<T> action)
        {
            PostOrderTraversal(root, action);
        }

        private void PostOrderTraversal(Node node, Action<T> action)
        {
            if (node != null)
            {
                PostOrderTraversal(node.Left, action);
                PostOrderTraversal(node.Right, action);
                action(node.Data);
            }
        }

        // Checks if the balance factor of each node is either -1, 0, or 1.
        public bool IsBalanced()
        {
            return IsBalanced(root);
        }

        private bool IsBalanced(Node node)
        {
            if (node == null)
                return true;

            int balanceFactor = GetBalanceFactor(node);
            if (balanceFactor < -1 || balanceFactor > 1)
                return false;

            return IsBalanced(node.Left) && IsBalanced(node.Right);
        }

        // Checks if the height information of each node is correct.
        public bool IsHeightCorrect()
        {
            return IsHeightCorrect(root);
        }

        private bool IsHeightCorrect(Node node)
        {
            if (node == null)
                return true;

            int expectedHeight = Math.Max(Height(node.Left), Height(node.Right)) + 1;
            if (node.Height != expectedHeight)
                return false;

            return IsHeightCorrect(node.Left) && IsHeightCorrect(node.Right);
        }
    }
}
