using Lab10Csharp;
using System.Diagnostics;
using System.Threading;

internal class Program
{
    private static void Main(string[] args)
    {
        TaskManager tasks = new TaskManager();
        int[] elPerThread = new int[] { 250000, 250000, 250000, 250000 };
        int[] parallelValues = { 1, 2, 4, 8 };

        foreach (var parallelismDegree in parallelValues)
        {
            tasks.GenerateUnique(elPerThread);
            tasks.SortStringsParallel(parallelismDegree);
        }
        tasks.GenerateUnique(elPerThread);
        tasks.SortHashSet();

    }
}