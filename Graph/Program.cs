using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    public class Vertex
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
        }

        public int AdjacentsCount()
        {
            return _adjacents.Count;
        }
    }

    public class Graph
    {
        private readonly List<Vertex> _vertices = new List<Vertex>();
        public IEnumerable<Vertex> Vertices
        {
            get { foreach (var vertex in _vertices) yield return vertex; }
        }


        private bool Exists(char name)
        {
            return _vertices.Exists(v => v.Name == name);
        }

        public void CreateVertex(char name)
        {
            if (Exists(name))
                return;

            _vertices.Add(new Vertex(name));
        }

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

        public int AdjacentsCount(char name)
        {
            var vertex = _vertices.SingleOrDefault(v => v.Name == name);
            if(vertex == null)
            {
                return -1;
            }

            return vertex.AdjacentsCount();
        }

        public int VerticesCount()
        {
            return _vertices.Count;
        }

        // Uses BFS to determine if a graph is connected by visisting each of the vertices.
        public bool IsConnectedUndirected()
        {
            if (_vertices.Count == 0)
                return false;

            if (_vertices.Count == 1)
                return true;

            var v = _vertices[0];

            var q = new Queue<Vertex>();
            var seen = new HashSet<Vertex>();

            q.Enqueue(v);

            while (q.Count > 0)
            {
                Vertex curr = q.Dequeue();

                if (!seen.Contains(curr))
                {
                    seen.Add(curr);
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

        public bool IsWeaklyConnected()
        {
            return false;
        }

    }


    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
