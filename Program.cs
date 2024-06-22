using System;
using Node;
using AVLTree;

public class Program{
    static void Main(){
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