using System;
using Xunit;

namespace Graph.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void AddVertexToGraph()
        {
            var graph = new Graph();
            graph.CreateVertex('A');

            Assert.True(graph.VerticesCount() == 1);
        }

        [Fact]
        public void AddEdgeBetweenVertices()
        {
            var graph = new Graph();
            graph.CreateVertex('A');
            graph.CreateVertex('B');

            graph.AddEdge('A', 'B');

            Assert.True(graph.AdjacentsCount('A') == 1);
        }

        [Fact]
        public void BeConnected()
        {
            var graph = new Graph();
            graph.CreateVertex('A');
            graph.CreateVertex('B');
            graph.CreateVertex('C');
            graph.CreateVertex('D');
            graph.CreateVertex('E');
            graph.CreateVertex('F');

            graph.AddEdge('A', 'B');
            graph.AddEdge('B', 'E');
            graph.AddEdge('B', 'C');
            graph.AddEdge('E', 'F');
            graph.AddEdge('F', 'D');

            Assert.True(graph.IsConnected());
        }
    }
}
