using System;

namespace Node{
    public class AVLNode<T> where T : IComparable<T>{
    public T Value {get; set; }
    public AVLNode<T> Left {get; set; }
    public AVLNode<T> Right {get; set; } 
    public int Height {get; set; }

    public AVLNode(T value){
        Value = value;
        Height = 1;
    }
    }
}
 