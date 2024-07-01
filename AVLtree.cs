using System;

<<<<<<< HEAD
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
=======
namespace AVLTree {
    public class AVLTree<T> where T : IComparable<T> {
        private AVLNode<T> root;

        // Returns the height of the given node or 0 if the node is null.
        private int Height(AVLNode<T> node)
>>>>>>> be3c685efd42f0af1fc35c5832cfdd0c427509f2
        {
            return node?.Height ?? 0;
        }

<<<<<<< HEAD
        private Node Balance(Node node)
        {
            int balance = GetBalance(node);

            if (balance > 1)
            {
                if (GetBalance(node.Left) < 0)
                    node.Left = RotateLeft(node.Left);
                return RotateRight(node);
=======
        // Updates the height of the given node based on the heights of its children.
        private void UpdateHeight(AVLNode<T> node) {
            node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;
        }

        // Gets the balance factor of the node. A balance factor of -1, 0, or 1 means the node is balanced.
        private int GetBalanceFactor(AVLNode<T> node) {
            return node == null ? 0 : Height(node.Left) - Height(node.Right);
        }

        // Performs a left rotation to maintain balance.
        private AVLNode<T> RotateLeft(AVLNode<T> x) {
            AVLNode<T> y = x.Right;
            x.Right = y.Left;
            y.Left = x;
            UpdateHeight(x);
            UpdateHeight(y);
            return y;
        }

        // Performs a right rotation to maintain balance.
        private AVLNode<T> RotateRight(AVLNode<T> y) {
            AVLNode<T> x = y.Right;
            y.Left = x.Right;
            x.Right = y;
            UpdateHeight(y);
            UpdateHeight(x);
            return x;
        }

        // Combination of left and right rotation.
        private AVLNode<T> RotateLeftRight(AVLNode<T> node) {
            node.Left = RotateLeft(node.Left);
            return RotateRight(node);
        }

        // Combination of right and left rotation.
        private AVLNode<T> RotateRightLeft(AVLNode<T> node) {
            node.Right = RotateRight(node.Right);
            return RotateLeft(node);
        }

        // Balances the subtree rooted at the given node.
        private AVLNode<T> Balance(AVLNode<T> node) {
            int balanceFactor = GetBalanceFactor(node);

            if (balanceFactor > 1) {
                // Right-heavy subtree
                if (GetBalanceFactor(node.Right) > 0) {
                    // Right-Left case
                    node = RotateRightLeft(node);
                } else {
                    // Right-Right case
                    node = RotateLeft(node);
                }
>>>>>>> be3c685efd42f0af1fc35c5832cfdd0c427509f2
            }

            if (balance < -1)
            {
                if (GetBalance(node.Right) > 0)
                    node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }

            return node;
        }

<<<<<<< HEAD
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
=======
        // Public method to insert a value into the AVL tree.
        public void Insert(T value) {
            root = Insert(root, value);
        }

        // Recursive insertion with balancing.
        private AVLNode<T> Insert(AVLNode<T> node, T value) {
            if (node == null) {
                return new AVLNode<T>(value);
            }

            int compareResult = value.CompareTo(node.Value);
            if (compareResult < 0) {
                node.Left = Insert(node.Left, value);
            } else if (compareResult > 0) {
                node.Right = Insert(node.Right, value);
            } else {
                throw new InvalidOperationException("Duplicate values are not allowed");
            }
>>>>>>> be3c685efd42f0af1fc35c5832cfdd0c427509f2

            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;
            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;

            return x;
        }

<<<<<<< HEAD
        private Node RotateLeft(Node x)
        {
            Node y = x.Right;
            Node T2 = y.Left;

            y.Left = x;
            x.Right = T2;

            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;

            return y;
=======
        // Public method to check if the tree contains a specific value.
        public bool Contains(T value) {
            return Contains(root, value);
        }

        // Recursive search to check if the tree contains a specific value.
        private bool Contains(AVLNode<T> node, T value) {
            if (node == null) {
                return false;
            }

            int compareResult = value.CompareTo(node.Value);
            if (compareResult < 0) {
                return Contains(node.Left, value);
            } else if (compareResult > 0) {
                return Contains(node.Right, value);
            } else {
                return true;
            }
        }

        // Public method to delete a value from the AVL tree.
        public void Delete(T value) {
            root = Delete(root, value);
        }

        // Recursive deletion with balancing.
        private AVLNode<T> Delete(AVLNode<T> node, T value) {
            if (node == null) {
                return node;
            }

            int compareResult = value.CompareTo(node.Value);
            if (compareResult < 0) {
                node.Left = Delete(node.Left, value);
            } else if (compareResult > 0) {
                node.Right = Delete(node.Right, value);
            } else {
                // Node with only one child or no child
                if (node.Left == null || node.Right == null) {
                    AVLNode<T> temp = node.Left ?? node.Right;
                    if (temp == null) {
                        // No child case
                        temp = node;
                        node = null;
                    } else {
                        // One child case
                        node = temp;
                    }
                } else {
                    // Node with two children: get the inorder successor
                    AVLNode<T> temp = MinValueNode(node.Right);
                    node.Value = temp.Value;
                    node.Right = Delete(node.Right, temp.Value);
                }
            }
            if (node == null) {
                return node;
            }

            UpdateHeight(node);
            return Balance(node);
        }

        // Finds the node with the minimum value in the given subtree.
        private AVLNode<T> MinValueNode(AVLNode<T> node) {
            AVLNode<T> current = node;
            while (current.Left != null) {
                current = current.Left;
            }
            return current;
        }

        // Prints the tree using level-order traversal.
        public void PrintTree() {
            if (root == null) {
                Console.WriteLine("Tree is empty.");
                return;
            }

            Queue<AVLNode<T>> queue = new Queue<AVLNode<T>>();
            queue.Enqueue(root);
            while (queue.Count > 0) {
                AVLNode<T> current = queue.Dequeue();
                Console.Write(current.Value + " ");

                if (current.Left != null) {
                    queue.Enqueue(current.Left);
                }
                if (current.Right != null) {
                    queue.Enqueue(current.Right);
                }
            }
            Console.WriteLine();
        }

        // Performs in-order traversal of the tree.
        public void InOrderTraversal(Action<T> action) {
            InOrderTraversal(root, action);
        }

        // Recursive in-order traversal helper method.
        private void InOrderTraversal(AVLNode<T> node, Action<T> action) {
            if (node != null) {
                InOrderTraversal(node.Left, action);
                action(node.Value);
                InOrderTraversal(node.Right, action);
            }
        }

        // Performs pre-order traversal of the tree.
        public void PreOrderTraversal(Action<T> action) {
            PreOrderTraversal(root, action);
        }

        // Recursive pre-order traversal helper method.
        private void PreOrderTraversal(AVLNode<T> node, Action<T> action) {
            if (node != null) {
                action(node.Value);
                PreOrderTraversal(node.Left, action);
                PreOrderTraversal(node.Right, action);
            }
        }

        // Performs post-order traversal of the tree.
        public void PostOrderTraversal(Action<T> action) {
            PostOrderTraversal(root, action);
        }

        // Recursive post-order traversal helper method.
        private void PostOrderTraversal(AVLNode<T> node, Action<T> action) {
            if (node != null) {
                PostOrderTraversal(node.Left, action);
                PostOrderTraversal(node.Right, action);
                action(node.Value);
            }
>>>>>>> be3c685efd42f0af1fc35c5832cfdd0c427509f2
        }
    }
}
