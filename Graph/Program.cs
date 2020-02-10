using System;

namespace Graph
{
    class Program
    {
        static void Main(string[] args)
        {
            var graph = new Graph();
            graph.CreateVertex('A');
            graph.CreateVertex('B');
            graph.CreateVertex('C');

            graph.AddEdge('A', 'C');
            graph.AddEdge('B', 'C');


            Console.WriteLine(graph.IsWeaklyConnected());
        }
    }
}
