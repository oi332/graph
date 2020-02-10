using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    public class Graph
    {
        private readonly List<Vertex> _vertices = new List<Vertex>();
        private readonly List<Edge> _edges = new List<Edge>();


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

            _edges.Add(new Edge(vertex1, vertex2));
        }

        public int AdjacentsCount(char name)
        {
            var vertex = _vertices.SingleOrDefault(v => v.Name == name);
            if(vertex == null)
            {
                return -1;
            }

            return _edges.Where(e => e.From == vertex).Select(e => e.To).ToList().Count;
        }

        public int VerticesCount()
        {
            return _vertices.Count;
        }

        public int EdgesCount()
        {
            return _edges.Count;
        }

        private bool OppositeDirectionExists(Edge edge)
        {
            return _edges.Exists(e => e.From == edge.To && e.To == edge.From);
        }
        private List<Edge> TransformToUndirected()
        {
            var missingEdges = new List<Edge>();

            foreach (var edge in _edges)
            {
                if (!OppositeDirectionExists(edge))
                {
                    missingEdges.Add(new Edge(edge.To, edge.From));
                }
            }
            missingEdges.AddRange(_edges);
            return missingEdges;
        }

        public bool IsWeaklyConnected()
        {
            var edges = TransformToUndirected();
            if(_vertices.Count == 1)
            {
                return true;
            }

            if(edges.Count == 0)
            {
                return false;
            }


            var q = new Queue<Vertex>();
            var seen = new HashSet<Vertex>();

            var src = edges[0].From;

            q.Enqueue(src);

            while (q.Count > 0)
            {
                Vertex curr = q.Dequeue();

                if(!seen.Contains(curr))
                {
                    seen.Add(curr);
                }

                var adjacents = edges.Where(e => e.From == curr).Select(e => e.To).ToList();

                foreach (var adjacent in adjacents)
                {
                    if(!seen.Contains(adjacent))
                    {
                        seen.Add(adjacent);
                        q.Enqueue(adjacent);
                    }
                }
            }
            foreach (var item in seen)
            {
                Console.WriteLine(item.Name);
            }
            return _vertices.Count == seen.Count;
        }
    }
}
