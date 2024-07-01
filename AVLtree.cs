using System;

namespace AVLtree
{
    public class AVLTree<T> where T : IComparable
    {
        // AVL tree node class
        private class Node
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
                return node;

            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
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

            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
            return Balance(node);
        }

        private Node GetMinValueNode(Node node)
        {
            Node current = node;
            while (current.Left != null)
                current = current.Left;
            return current;
        }

        public int GetHeight()
        {
            return GetHeight(root);
        }

        private int GetHeight(Node node)
        {
            return node?.Height ?? 0;
        }

        private Node Balance(Node node)
        {
            int balance = GetBalance(node);

            if (balance > 1)
            {
                if (GetBalance(node.Left) < 0)
                    node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }

            if (balance < -1)
            {
                if (GetBalance(node.Right) > 0)
                    node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }

            return node;
        }

        private int GetBalance(Node node)
        {
            return node == null ? 0 : GetHeight(node.Left) - GetHeight(node.Right);
        }

        private Node RotateRight(Node y)
        {
            Node x = y.Left;
            Node T2 = x.Right;

            x.Right = y;
            y.Left = T2;

            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;
            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;

            return x;
        }

        private Node RotateLeft(Node x)
        {
            Node y = x.Right;
            Node T2 = y.Left;

            y.Left = x;
            x.Right = T2;

            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;

            return y;
        }
    }
}
