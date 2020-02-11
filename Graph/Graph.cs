using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    public class Graph
    {
        private readonly List<Vertex> _vertices = new List<Vertex>();
        private readonly List<Edge> _edges = new List<Edge>();

        private bool NameExists(char name)
        {
            return _vertices.Exists(v => v.Name == name);
        }

        private bool EdgeExists(char name1, char name2)
        {
            return _edges.Exists(e => e.From.Name == name1 && e.To.Name == name2);
        }

        public void CreateVertex(char name)
        {
            if (NameExists(name))
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

            if (EdgeExists(name1, name2))
                return;

            var newEdge = new Edge(vertex1, vertex2);
            
            _edges.Add(newEdge);
            
            if(HasCycle(newEdge.From))
            {
                _edges.Remove(newEdge);
            }
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

        public bool IsWeaklyConnected() 
        {
            if(_vertices.Count == 1)
            {
                return true;
            }

            if(_edges.Count == 0)
            {
                return false;
            }

            var q = new Queue<Vertex>();
            var seen = new HashSet<Vertex>();

            var src = _edges[0].From;

            q.Enqueue(src);

            while (q.Count > 0)
            {
                Vertex curr = q.Dequeue();

                if(!seen.Contains(curr))
                {
                    seen.Add(curr);
                }

                var adjacents = _edges.Where(e => e.From == curr).Select(e => e.To)
                                      .Union(_edges.Where(e => e.To == curr).Select(e => e.From));

                foreach (var adjacent in adjacents)
                {
                    if (!seen.Contains(adjacent))
                    {
                        q.Enqueue(adjacent);
                    }
                }
            }

            return _vertices.Count == seen.Count;
        }

        public bool HasCycle(Vertex v)
        {
            var q = new Queue<Vertex>();
            var seen = new HashSet<Vertex>();

            q.Enqueue(v);

            while(q.Count != 0)
            {
                var curr = q.Dequeue();

                if(!seen.Contains(curr))
                {
                    seen.Add(curr);
                }

                var adjacents = _edges.Where(e => e.From == curr).Select(e => e.To);
                foreach (var adjacent in adjacents)
                {
                    if(adjacent == v)
                    {
                        return true;
                    }

                    if(!seen.Contains(adjacent))
                    {
                        q.Enqueue(adjacent);
                    }
                }
            }

            return false;
        }

        //public bool HasCycle(Vertex v)
        //{
        //    var unvisited = new HashSet<Vertex>(_vertices);
        //    var closed = new HashSet<Vertex>();
        //    var open = new HashSet<Vertex>();
        //    var found = false;
        //    DfsVisit(v, unvisited, closed, open, ref found);
        //    return found;
        //}

        //private void DfsVisit(Vertex v, HashSet<Vertex> unvisited, HashSet<Vertex> closed, HashSet<Vertex> open, ref bool flag)
        //{
        //    open.Add(v);
        //    unvisited.Remove(v);

        //    var adjacents = _edges.Where(e => e.From == v).Select(e => e.To).ToList();
        //    foreach(var vertex in adjacents)
        //    {
        //        if(unvisited.Contains(vertex))
        //        {
        //            DfsVisit(vertex, unvisited, closed, open, ref flag);
        //        }
        //        else if(open.Contains(vertex))
        //        {
        //            flag = true;
        //            return;
        //        }
        //    }

        //    closed.Add(v);
        //    open.Remove(v);
        //}
    }
}
