using System;

public class Program
{
    static void Main()
    {
        Console.WriteLine("Traveling Salesman Problem:");
        int[,] graph = {
            { 0, 10, 15, 20 },
            { 10, 0, 35, 25 },
            { 15, 35, 0, 30 },
            { 20, 25, 30, 0 }
        };
        int n = graph.GetLength(0);
        int result = TSP(graph, n);
        Console.WriteLine($"Minimum cost: {result}");
        // Example of solving simultaneous equations
        double[,] coefficients = {
            { 2, -1, 1 },
            { 3, 3, 9 },
            { 3, 3, 5 }
        };
        double[] constants = { 8, 0, -6 };
        double[] solution = SolveSimultaneousEquations(coefficients, constants);

        Console.WriteLine("Solution to the simultaneous equations:");
        for (int i = 0; i < solution.Length; i++)
        {
            Console.WriteLine($"x{i + 1} = {solution[i]}");
        }
        // Example of solving a differential equation using Euler's method
        Func<double, double, double> dydx = (x, y) => x + y;
        double x0 = 0, y0 = 1, h = 0.1, xEnd = 1;
        double yEnd = SolveDifferentialEquationEuler(dydx, x0, y0, h, xEnd);
        Console.WriteLine($"Solution to the differential equation at x = {xEnd}: y = {yEnd}");
    }

    static double SolveDifferentialEquationEuler(Func<double, double, double> dydx, double x0, double y0, double h, double xEnd)
    {
        double x = x0;
        double y = y0;

        while (x < xEnd)
        {
            y += h * dydx(x, y);
            x += h;
        }

        return y;
    }

    static double[] SolveSimultaneousEquations(double[,] coefficients, double[] constants)
    {
        int n = constants.Length;
        for (int i = 0; i < n; i++)
        {
            // Make the diagonal contain all 1's
            double factor = coefficients[i, i];
            for (int j = 0; j < n; j++)
            {
                coefficients[i, j] /= factor;
            }
            constants[i] /= factor;

            // Make the other rows have 0 in current column
            for (int k = 0; k < n; k++)
            {
                if (k != i)
                {
                    factor = coefficients[k, i];
                    for (int j = 0; j < n; j++)
                    {
                        coefficients[k, j] -= factor * coefficients[i, j];
                    }
                    constants[k] -= factor * constants[i];
                }
            }
        }
        return constants;
    }

    static int TSP(int[,] graph, int n)
    {
        int[] vertex = new int[n - 1];
        for (int i = 1; i < n; i++)
            vertex[i - 1] = i;

        int minPath = int.MaxValue;
        do
        {
            int currentPathWeight = 0;
            int k = 0;
            for (int i = 0; i < vertex.Length; i++)
            {
                currentPathWeight += graph[k, vertex[i]];
                k = vertex[i];
            }
            currentPathWeight += graph[k, 0];
            minPath = Math.Min(minPath, currentPathWeight);
        } while (NextPermutation(vertex));

        return minPath;
    }

    static bool NextPermutation(int[] array)
    {
        int i = array.Length - 1;
        while (i > 0 && array[i - 1] >= array[i])
            i--;

        if (i <= 0)
            return false;

        int j = array.Length - 1;
        while (array[j] <= array[i - 1])
            j--;

        int temp = array[i - 1];
        array[i - 1] = array[j];
        array[j] = temp;

        j = array.Length - 1;
        while (i < j)
        {
            temp = array[i];
            array[i] = array[j];
            array[j] = temp;
            i++;
            j--;
        }

        return true;
    }
}
