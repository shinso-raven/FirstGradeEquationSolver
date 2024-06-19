
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

    private string _equationInput;

    private string _equationResult;

    private object _equationTree;


    public Equation_solver(string equationInput)
    {
        _equationInput = equationInput;
        //TODO: Convertir a un arbol


        //TODO: Resolver arbol
        _equationTree = new { };

    }


    public string SolutionOrError()
    {
        //TODO Responde con la respuesta o un error
        return "x = -3";
    }
}