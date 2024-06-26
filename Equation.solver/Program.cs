using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Optional;

namespace EquationSolver;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}

public class Equation_solver
{
    private readonly Guid _identity;
    private readonly Option<Equation, string> _equationInput;

    private Option<Solution, string> _equationResult;

    // private object _equationTree;


    public Equation_solver(string equationInput)
    {
        _identity = new Guid();

        _equationInput = Equation.From(equationInput);

        _equationInput.MatchNone(er => _equationResult = Option.None<Solution, string>(er));

        _equationInput.MatchSome(
            equation => _equationResult = Solution.From(equation)
        );
    }

    public string SolutionOrError()
    {
        //TODO Responde con la respuesta o un error
        return _equationResult.Match(er => er.Value, errorMsg => errorMsg);
    }
}

public class Solution
{
    public Tree.BinaryTree<string> Tree { get; }
    public string Value { get; private set; }

    private Solution(Equation inpuEquation)
    {
        Tree = inpuEquation.Tree;

        Solve_equation();
    }

    private void Solve_equation()
    {
        //x+3 = 0


        //Take the operator
        string operation = Tree.Root.Left.Value;

        //Take the value
        int value = int.Parse(Tree.Root.Left.Right.Value);
        //Take the result
        int result = int.Parse(Tree.Root.Right.Value);

        switch (operation)
        {
            case "-":
                value = result + value * -1;
                Tree.Root.Right = new Tree.Node<string>(value.ToString());
                break;
            case "/":
                value = result * value;
                Tree.Root.Right = new Tree.Node<string>(value.ToString());
                break;
            case "*":
                value = result / value;
                Tree.Root.Right = new Tree.Node<string>(value.ToString());
                break;
            case "+":
                value = result + value * -1;
                Tree.Root.Right = new Tree.Node<string>(value.ToString());
                break;
        }


        //delete the value
        Tree.Root.Left.Right = null;

        //change the operation to the variable
        Tree.Root.Left = Tree.Root.Left.Left;

        //delete the value
        Tree.Root.Left.Left = null;


        Value = Read_tree();
    }


    private string Read_tree()
    {
        string output = Tree.ToString();
        return output;
    }

    internal static Option<Solution, string> From(Equation inputEquation)
    {
        return Option.Some<Solution, string>(new Solution(inputEquation));


        return Option.None<Solution, string>("Error solving the equation");
    }
}

public class Equation

{
    private List<string> lstData = new();

    // private Tree.BinaryTree<string> tree = new();
    public string Value { get; }
    public Tree.BinaryTree<string> Tree { get; }

    private Equation(string input)
    {
        Value = input;

        lstData = input.Trim().Split(" ").ToList();

        Tree = new Tree.BinaryTree<string>();
        Parse_string_to_tree();
    }

    private void Parse_string_to_tree()
    {
        //=
        Tree.Add(lstData[3]);

        //Result
        Tree.Root.Right = new Tree.Node<string>(lstData.Last());

        //Operation
        Tree.Root.Left = new Tree.Node<string>(lstData[1]);
        //values
        Tree.Root.Left.Left = new Tree.Node<string>(lstData[0]);
        Tree.Root.Left.Right = new Tree.Node<string>(lstData[2]);
    }

    internal static Option<Equation, string> From(string? input)
    {
        if (input == null)
            return Option.None<Equation, string>("Error solving the equation");

        int counter = 0;
        foreach (char token in input)
            if (token == '+' || token == '-' || token == '*' || token == '/')
                counter++;
        if (counter > 1)
            return Option.None<Equation, string>("Error solving the equation");

        return Option.Some<Equation, string>(new Equation(input));
    }
}