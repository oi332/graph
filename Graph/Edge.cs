namespace Graph
{
    public class Edge
    {
        public Vertex From { get; }
        public Vertex To { get; }

        public Edge(Vertex from, Vertex to)
        {
            From = from;
            To = to;
        }
    }
}
