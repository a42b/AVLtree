using System;
using Node;

namespace AVLTree {
    public class AVLTree<T> where T : IComparable<T>{
        private AVLNode<T> root;

        private int Height(AVLNode<T> node)
        {
            return node?.Height ?? 0;
        }

        private void UpdateHeight(AVLNode<T> node){
            node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;
        }

        private int GetBalanceFactor(AVLNode<T> node){
            return node == null ? 0 : Height(node.Left) - Height(node.Right);
        }

        private AVLNode<T> RotateLeft(AVLNode<T> x){
            AVLNode<T> y = x.Right;
            x.Right = y.Left;
            y.Left = x;
            UpdateHeight(x);
            UpdateHeight(y);
            return y;
        }
        private AVLNode<T> RotateRight(AVLNode<T> y){
            AVLNode<T> x = y.Right;
            y.Left = x.Right;
            x.Right = y;
            UpdateHeight(y);
            UpdateHeight(x);
            return x;
        }

        private AVLNode<T> RotateLeftRight(AVLNode<T> node){
            node.Left = RotateLeft(node.Left);
            return RotateRight(node);
        }

        private AVLNode<T> RotateRightLeft(AVLNode<T> node){
            node.Right = RotateRight(node.Right);
            return RotateLeft(node);
        }

        private AVLNode<T> Balance(AVLNode<T> node){
            int balanceFactor = GetBalanceFactor(node);

            if (balanceFactor > 1){
                if (GetBalanceFactor(node.Right) > 0){
                    node = RotateRightLeft(node);
                } else {
                    node = RotateLeft(node);
                }
            }
            return node;
        }

        public void Insert(T value){
            root = Insert(root, value);
        }

        private AVLNode<T> Insert(AVLNode<T> node, T value){
            if (node == null){
                return new AVLNode<T>(value);
            }

            int compareResult = value.CompareTo(node.Value);
            if (compareResult < 0){
                node.Left = Insert(node.Left, value);
            } else if (compareResult > 0){
                node.Right = Insert(node.Right, value);
            }
            else {
                throw new InvalidOperationException("Duplicate value are not allowed");
            }

            UpdateHeight(node);
            return Balance(node);
        }

        public bool Contains(T value){
            return Contains(root, value);
        }

        private bool Contains(AVLNode<T> node, T value){
            if (node == null){
                return false;
            }

            int compareResult = value.CompareTo(node.Value);
            if (compareResult < 0){
                return Contains(node.Left, value);
            } else if (compareResult > 0){
                return Contains(node.Right, value);
            } else {
                return true;
            }
        }

        public void Delete(T value){
            root = Delete(root, value);
        }

        private AVLNode<T> Delete(AVLNode<T> node, T value){
            if (node == null){
                return node;
            }

            int compareResult = value.CompareTo(node.Value);
            if (compareResult < 0){
                node.Left = Delete(node.Left, value);
            } else if (compareResult > 0){
                node.Right = Delete(node.Right, value);
            } else {
                if (node.Left == null || node.Right == null){
                    AVLNode<T> temp = node.Left ?? node.Right;
                    if (temp == null){
                        temp = node;
                        node = null;
                    } else {
                        node = temp;
                    }
                } else {
                    AVLNode<T> temp = MinValueNode(node.Right);
                    node.Value = temp.Value;
                    node.Right = Delete(node.Right, temp.Value);
                }
            }
            if (node == null){
                return node;
            }

            UpdateHeight(node);
            return Balance(node);
        }

        private AVLNode<T> MinValueNode(AVLNode<T> node){
            AVLNode<T> current = node;
            while(current.Left != null){
                current = current.Left;
            }

            return current;
        }

        public void PrintTree(){
            if (root == null)
            {
                Console.WriteLine("Tree is empty.");
                return;
            }

            Queue<AVLNode<T>> queue = new Queue<AVLNode<T>>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                AVLNode<T> current = queue.Dequeue();
                Console.Write(current.Value + " ");

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

        public void InOrderTraversal(Action<T> action){
            InOrderTraversal(root, action);
        }

        private void InOrderTraversal(AVLNode<T> node, Action<T> action){
            if (node != null){
                InOrderTraversal(node.Left, action);
                action(node.Value);
                InOrderTraversal(node.Right, action);
            }
        }

        public void PreOrderTraversal(Action<T> action){
            PreOrderTraversal(root, action);
        }

        private void PreOrderTraversal(AVLNode<T> node, Action<T> action){
            if (node != null){
                action(node.Value);
                PreOrderTraversal(node.Left, action);
                PreOrderTraversal(node.Right, action);
            }
        }

        public void PostOrderTraversal(Action<T> action){
            PostOrderTraversal(root, action);
        }

        private void PostOrderTraversal(AVLNode<T> node, Action<T> action){
            if (node != null){
                PostOrderTraversal(node.Left, action);
                PostOrderTraversal(node.Right, action);
                action(node.Value);
            }
        }
    }
}