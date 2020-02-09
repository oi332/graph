using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    class Vertex
    {
        private readonly IList<Vertex> _adjacents = new List<Vertex>();
        public char Name { get; }
        public IEnumerable<Vertex> Adjacents
        {
            get { foreach (var adjacent in _adjacents) yield return adjacent; }
        }
        

        public Vertex(char name)
        {
            Name = name;
        }
        public void AddAdjacent(Vertex v)
        {
            if (_adjacents.Contains(v))
            {
                return;
            }

            _adjacents.Add(v);
            v.AddAdjacent(this);
        }


    }

    class Graph
    {
        private readonly List<Vertex> _vertices = new List<Vertex>();
        public IEnumerable<Vertex> Vertices
        {
            get { foreach (var vertex in _vertices) yield return vertex; }
        }


        private bool Exists(char name)
        {
            var exist = _vertices.Exists(v => v.Name == name);
            if (exist)
            {
                return true;
            }
            return false;
        }

        // Create vertex
        public void CreateVertex(char name)
        {
            if (Exists(name))
                return;

            _vertices.Add(new Vertex(name));
        }

        // Add edge
        public void AddEdge(char name1, char name2)
        {
            var vertex1 = _vertices.SingleOrDefault(v => v.Name == name1);
            var vertex2 = _vertices.SingleOrDefault(v => v.Name == name2);

            if(vertex1 == null || vertex2 == null)
            {
                Console.WriteLine("Vertices do not exist. Add them first.");
                return;
            }

            vertex1.AddAdjacent(vertex2);
        }

        // Uses BFS to determine if a graph is connected by visisting each of the vertices.
        public bool isConnected()
        {
            if (_vertices.Count == 0)
            {
                return false;
            }

            var v = _vertices[0];

            Queue<Vertex> q = new Queue<Vertex>();
            HashSet<Vertex> seen = new HashSet<Vertex>();

            q.Enqueue(v);

            while (q.Count > 0)
            {
                Vertex curr = q.Dequeue();

                if (!seen.Contains(curr))
                {
                    seen.Add(curr);
                    Console.WriteLine(curr.Name);
                }

                foreach (var adjacent in curr.Adjacents)
                {
                    if (!seen.Contains(adjacent))
                    {
                        q.Enqueue(adjacent);
                    }
                }
            }

            return _vertices.Count == seen.Count;
        }

    }


    class Program
    {
        

        static void Main(string[] args)
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


            Console.WriteLine(graph.isConnected());
        }
    }
}
