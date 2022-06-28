using System;
using System.Collections.Generic;
using System.Text;

namespace Vesna2022
{
    internal class Edge
    {
        public Vertex From { get; set; }   //От куда 
        public Vertex To { get; set; }     //Куда
        public double Weight { get; set; }

        public Edge()
        {
            From = null;
            To = null;
            Weight = 0;
        }

        public Edge(Vertex from, Vertex to, double weight = 1.0)
        {
            From = from;
            To = to;
            Weight = weight;
        }

        public override string ToString()
        {
            return $"({From}; {To}; {Weight})";
        }

    }
}
