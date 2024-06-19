using EquationSolver;

namespace Equation.Test;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [TestCase("x + 3 = 0 ", "x = -3")]
    [TestCase("x * 3 = 0 ", "x = 0")]
    public void Equation_solves_simple_solution(string entrada, string salida)
    {
        Equation_solver eq = new Equation_solver(entrada);

        string resultado = eq.SolutionOrError();

        Assert.That(resultado, Is.EqualTo(salida));
    }

    [TestCase("(x + 3)/5 = 0 ")]
    public void Equation_print_error_solution(string entrada)
    {
        string salida = "Error solving the equation";
        Equation_solver eq = new Equation_solver(entrada);

        string resultado = eq.SolutionOrError();

        Assert.That(resultado, Is.EqualTo(salida));
    }


}