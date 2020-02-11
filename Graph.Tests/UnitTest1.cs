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
        public void NotAddVertexAlreadyExistName()
        {
            var graph = new Graph();
            graph.CreateVertex('A');
            graph.CreateVertex('A');

            Assert.True(graph.VerticesCount() == 1);
        }

        [Fact]
        public void AddEdgeBetweenVertices()
        {
            var graph = new Graph();
            graph.CreateVertex('A');
            graph.CreateVertex('B');
            graph.CreateVertex('C');

            graph.AddEdge('A', 'C');
            graph.AddEdge('B', 'C');

            Assert.True(graph.AdjacentsCount('C') == 0);
            Assert.True(graph.AdjacentsCount('B') == 1);
            Assert.True(graph.AdjacentsCount('A') == 1);
        }

        [Fact]
        public void NotAddSameEdgeTwiceIfExist()
        {
            var graph = new Graph();
            graph.CreateVertex('A');
            graph.CreateVertex('B');
            graph.CreateVertex('C');

            graph.AddEdge('A', 'C');
            graph.AddEdge('B', 'C');
            graph.AddEdge('A', 'C');

            Assert.True(graph.AdjacentsCount('C') == 0);
            Assert.True(graph.AdjacentsCount('B') == 1);
            Assert.True(graph.AdjacentsCount('A') == 1);
        }

        [Fact]
        public void ReturnTrueWhenGraphIsConnected1()
        {
            var graph = new Graph();
            graph.CreateVertex('A');
            graph.CreateVertex('B');
            graph.CreateVertex('C');

            graph.AddEdge('A', 'C');
            graph.AddEdge('B', 'C');

            Assert.True(graph.IsWeaklyConnected());
        }

        [Fact]
        public void ReturnTrueWhenGraphIsConnectedTwoVerticesOneEdge()
        {
            var graph = new Graph();
            graph.CreateVertex('A');
            graph.CreateVertex('B');

            graph.AddEdge('B', 'A');

            Assert.True(graph.IsWeaklyConnected());
        }

        [Fact]
        public void ReturnTrueWhenGraphIsConnectedTwoVerticesTwoEdges()
        {
            var graph = new Graph();
            graph.CreateVertex('A');
            graph.CreateVertex('B');

            graph.AddEdge('B', 'A');
            graph.AddEdge('A', 'B');

            Assert.True(graph.IsWeaklyConnected());
        }

        [Fact]
        public void ReturnTrueWhenGraphIsConnected3()
        {
            var graph = new Graph();
            graph.CreateVertex('A');
            graph.CreateVertex('B');

            graph.AddEdge('B', 'A');

            Assert.True(graph.IsWeaklyConnected());
        }

        [Fact]
        public void ReturnTrueWhenGraphHasNoConnectionsAndOnlyOneVertex()
        {
            var graph = new Graph();
            graph.CreateVertex('A'); 

            Assert.True(graph.IsWeaklyConnected());
        }

        [Fact]
        public void ReturnFalseWhenGraphIsNotConnectedNoEdges()
        {
            var graph = new Graph();
            graph.CreateVertex('A');
            graph.CreateVertex('B');

            Assert.False(graph.IsWeaklyConnected());
        }

        [Fact]
        public void ReturnFalseWhenGraphIsNotConnected2()
        {
            var graph = new Graph();
            graph.CreateVertex('A');
            graph.CreateVertex('B');
            graph.CreateVertex('C');
            graph.CreateVertex('D');

            graph.AddEdge('B', 'A');
            graph.AddEdge('C', 'D');

            Assert.False(graph.IsWeaklyConnected());
        }

        [Fact]
        public void ReturnFalseWhenGraphIsNotConnectedNoVertices()
        {
            var graph = new Graph();

            Assert.False(graph.IsWeaklyConnected());

        }

        [Fact]
        public void HaveACycleIntroducedOneVertexOneEdge()
        {
            var graph = new Graph();

            graph.CreateVertex('A');
            graph.AddEdge('A', 'A');

            Assert.True(graph.EdgesCount() == 0);
        }

        [Fact]
        public void HaveACycleIntroducedTriangle()
        {
            var graph = new Graph();
            graph.CreateVertex('A');
            graph.CreateVertex('B');
            graph.CreateVertex('C');

            graph.AddEdge('A', 'C');
            graph.AddEdge('C', 'B');
            graph.AddEdge('B', 'A');

            Assert.True(graph.EdgesCount() == 2);
        }
    }
}
