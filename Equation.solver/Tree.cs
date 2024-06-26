namespace EquationSolver;

public class Tree
{
    public class BinaryTree<T> where T : IComparable<T>
    {
        public Node<T> Root { get; private set; } = null!;

        public void Add(T value)
        {
            if (Root == null)
                Root = new Node<T>(value);
            else
                Root.Add(value);
        }


        public override string ToString()
        {
            string output = "";

            output += $"{Root.Left.Value} {Root.Value} {Root.Right.Value}";

            return output;
        }
    }

    public class Node<T> where T : IComparable<T>
    {
        public T Value { get; private set; }
        public Node<T> Left { get; set; } = null!;
        public Node<T> Right { get; set; } = null!;

        public Node(T value)
        {
            Value = value;
        }

        public void Add(T newValue)
        {
            if (newValue.CompareTo(Value) < 0)
            {
                if (Left == null)
                    Left = new Node<T>(newValue);
                else
                    Left.Add(newValue);
            }
            else
            {
                if (Right == null)
                    Right = new Node<T>(newValue);
                else
                    Right.Add(newValue);
            }
        }
    }
}