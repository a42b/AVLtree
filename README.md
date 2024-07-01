# AVLTree

A C# implementation of an AVL tree, a self-balancing binary search tree. This library provides efficient operations for insertion, deletion, and lookup.

## Features

- Self-balancing AVL tree
- Supports generic types
- Efficient lookup, insertion, and deletion
- In-order, pre-order, and post-order traversals

## Installation

Include the `AVLTree` class in your project.

```csharp
using AVLTree;
```

# Usage
Creating an AVL Tree

```csharp

var tree = new AVLTree<int>();
```
Inserting Values

```csharp

tree.Insert(10);
tree.Insert(20);
tree.Insert(5);
```
Checking for a Value

```csharp

bool contains = tree.Contains(10); // Returns true if the value is found
```
Deleting a Value

```csharp

tree.Delete(10);
```
Printing the Tree

Prints the tree using level-order traversal.

```csharp

tree.PrintTree();
```
Traversing the Tree

In-order traversal:

```csharp

tree.InOrderTraversal(Console.WriteLine);
```
Pre-order traversal:

```csharp

tree.PreOrderTraversal(Console.WriteLine);
```
Post-order traversal:

```csharp

tree.PostOrderTraversal(Console.WriteLine);
```
Example

```csharp

using System;
using AVLTree;

class Program {
    static void Main() {
        var tree = new AVLTree<int>();
        tree.Insert(10);
        tree.Insert(20);
        tree.Insert(5);

        tree.PrintTree(); // Output: 10 5 20

        tree.InOrderTraversal(Console.WriteLine); // Output: 5 10 20
    }
}

```
