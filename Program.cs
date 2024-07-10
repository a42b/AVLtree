using System;
using AVLTree;

namespace AVLTreeProject
{
    public class Program
    {
        static void Main(string[] args)
        {
            AVLTree<int> intTree = new AVLTree<int>();
            intTree.Insert(20);
            intTree.Insert(25);
            intTree.Insert(21);
            intTree.Insert(100);
            intTree.Insert(80);
            Console.WriteLine(intTree.Contains(80));
            Console.WriteLine(intTree.Contains(90));
        }
    }
}
