using System;
using System.Collections.Generic;
using System.Text;

namespace Vesna2022
{
    public enum Colors_Vertex
    {
        White = 1,
        Gray,
        Black
    }

    internal class Vertex
    {
        public string Name { get; set; }
        public Vertex prevVertex;    //предыдущая вершина, от которой пришли
        public double distance;     //суммарное расстояние
        public Colors_Vertex color;
        public List<Edge> adjLEdges;   //набор смежных ребер (т.е. ребра, которые выходят из данной вершины)
        public List<Vertex> adjLVertexes = new List<Vertex>(); //набор смежных вершин - не нужен
        public bool visited { get; set; } //Для KNN

        //*Для DFS*
        public int discovered;  //обнаруженная вершина
        public int finished;    //обработанная вершина
        public int time;        //метка времени

        Vertex()
        {
            Name = "No name";
            adjLEdges = new List<Edge>();
        }

        public Vertex(string name)
        {
            Name = name;
            adjLEdges = new List<Edge>();
        }

        public int CountEdgesVertex { get { return adjLEdges.Count; } }     //Кол-во ребер, идущих от вершины

        public override string ToString()
        {
            return string.Format("Name: ({0})", Name);
        }

    }
}