using System;
using Node;

namespace AVLTree {
    public class AVLTree<T> where T : IComparable<T> {
        private AVLNode<T> root;

        // Returns the height of the given node or 0 if the node is null.
        private int Height(AVLNode<T> node)
        {
            return node?.Height ?? 0;
        }

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
            }
            return node;
        }

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

            UpdateHeight(node);
            return Balance(node);
        }

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
        }
    }
}
