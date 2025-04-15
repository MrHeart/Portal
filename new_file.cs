using System;

class Program
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
