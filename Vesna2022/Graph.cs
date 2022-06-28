using System;
using System.Collections.Generic;
using System.Text;

namespace Vesna2022
{
    internal class Graph
    {
        public List<Vertex> LVertexes = new List<Vertex>(); //Список всех вершин
        public List<Edge> LEdges = new List<Edge>();        //Список всех ребер

        public int VertexCount { get { return LVertexes.Count; } }
        public int EdgeCount { get { return LEdges.Count; } }

        public void AddVertex(Vertex ver)
        {
            if (LVertexes.Contains(ver) || LVertexes == null) return;
            LVertexes.Add(ver);
        }

        public void AddEdge(Vertex from, Vertex to, double len = 1.0)
        {
            var ed = new Edge(from, to, len);
            if (ed != null)
            {
                LEdges.Add(ed);
                if (!from.adjLEdges.Contains(ed) || !to.adjLEdges.Contains(ed))
                {
                    from.adjLEdges.Add(ed);
                    to.adjLEdges.Add(ed);
                }
            }
        }

        public void RemoveVertex(Vertex ver)
        {
            LVertexes.Remove(ver);
        }

        public Vertex FindVertex(Vertex vertexName) //Поиск вершины
        {
            foreach (var v in LVertexes)
            {
                if (v.Name.Equals(vertexName))
                {
                    return v;
                }
            }
            return null;
        }

        public void BFS(Vertex startVertex)     //Поиск в ширину (обход графа)
        {
            //v - вершина; u - вершина смежная с v;
            //инициализация
            foreach (Vertex vertex in LVertexes)
            {
                vertex.distance = double.MaxValue; //(бесконечность)
                vertex.prevVertex = null;          //предшественник
                vertex.color = Colors_Vertex.White;
            }

            startVertex.color = Colors_Vertex.Gray;
            startVertex.distance = 0;
            startVertex.prevVertex = null;
            Queue<Vertex> Q = new Queue<Vertex>();
            Q.Enqueue(startVertex);             //Добавить в конец очереди

            while (Q.Count > 0)
            {
                Vertex u = Q.Dequeue();     //Удаляет элемент из начала очереди и возвращает его

                foreach (Edge ee in u.adjLEdges)
                {
                    Vertex v = ee.To;
                    if (v.color == Colors_Vertex.White)
                    {
                        v.color = Colors_Vertex.Gray;
                        v.distance = u.distance + 1;
                        v.prevVertex = u;
                        Q.Enqueue(v);
                    }

                }
                u.color = Colors_Vertex.Black;

            }

        }

        //Метод ближашего соседа (з. Коммивояжера)
        public static void KNN(Graph gr, Vertex start)
        {

            double mind = Double.MaxValue;
            Edge tem = null;
            Vertex next = start;
            start.visited = true;
            List<Edge> edgesnn = new List<Edge>();
            int k = 0;
            do
            {
                mind = Double.MaxValue;
                if (gr.LVertexes.Count - 1 == k)
                {
                    foreach (Edge ee in gr.LEdges)
                    {
                        if (ee.From == next && ee.To == start)
                        {
                            edgesnn.Add(ee);
                            break;
                        }
                    }
                    break;

                }
                foreach (Edge e in next.adjLEdges)
                {
                    if (e.From == next && e.To.visited != true)
                    {
                        if (mind > e.Weight)
                        {
                            mind = e.Weight;
                            tem = e;

                        }
                    }

                }

                edgesnn.Add(tem);
                tem.To.prevVertex = next;
                next = tem.To;

                next.visited = true;
                k++;
            } while (start != next);
            double summ = 0;
            foreach (Edge e in edgesnn)
            {
                Console.Write(String.Format(e.ToString() + "-->"));
                summ += e.Weight;
            }
            Console.WriteLine("\nСумма: {0}", summ);

        }


        public void DFS(Vertex startVertex) //Поиск в глубину (обход графа)
        {
            foreach (Vertex vv in LVertexes)
            {
                vv.color = Colors_Vertex.White;
                vv.prevVertex = null;
            }

            startVertex.time = 0;

            foreach (Vertex vv in LVertexes)
            {
                if (vv.color == Colors_Vertex.White)   //До вызова DFS_Visit она белая
                {
                    DFS_Visit(startVertex);
                }
            }
        }

        public void DFS_Visit(Vertex u)     //Для DFS
        {
            u.color = Colors_Vertex.Gray;    //Красим в серую
            u.discovered = u.time;
            u.time += 1;    //Счетчик времени увеличиваем на 1

            foreach (Edge ee in u.adjLEdges)
            {
                Vertex v = ee.To;
                if (v.color == Colors_Vertex.White)
                {
                    v.prevVertex = u;
                    DFS_Visit(v);
                }

            }
            u.color = Colors_Vertex.Black;
            u.finished = u.time;
            u.time += 1;
        }

        public void BFSForGraphConnectivity(Vertex startVertex)     //BFS для связности
        {
            //v - вершина; u - вершина смежная с v;

            startVertex.color = Colors_Vertex.Gray;
            startVertex.distance = 0;
            startVertex.prevVertex = null;
            Queue<Vertex> Q = new Queue<Vertex>();
            Q.Enqueue(startVertex);             //Добавить в конец очереди

            while (Q.Count > 0)
            {
                Vertex u = Q.Dequeue();     //Удаляет элемент из начала очереди и возвращает его

                foreach (Edge ee in u.adjLEdges)
                {
                    Vertex v = ee.To;
                    if (v.color == Colors_Vertex.White)
                    {
                        v.color = Colors_Vertex.Gray;
                        v.distance = u.distance + 1;
                        v.prevVertex = u;
                        Q.Enqueue(v);
                    }

                }
                //Q.Dequeue();
                u.color = Colors_Vertex.Black;

            }

        }

        public int GraphConnectivity()      //Связность графа
        {
            foreach (Vertex vertex in LVertexes)
            {
                vertex.distance = double.MaxValue; //(бесконечность)
                vertex.prevVertex = null;          //предшественник
                vertex.color = Colors_Vertex.White;
            }

            int ks = 0;         //кол-во связей
            while (true)
            {
                foreach (Vertex vv in LVertexes)
                {
                    if (vv.distance == double.MaxValue)
                    {
                        ks++;
                        BFSForGraphConnectivity(vv);
                    }

                }
                return ks;
            }

        }

        //Печатает кратчайшие пути из стартовой вершины до нужной вершины
        public void PrintPath(Vertex startVertex, Vertex vertex)
        {
            if (startVertex == vertex) Console.WriteLine("{0}", startVertex);
            else if (vertex.prevVertex == null) Console.WriteLine("Пути из {0} в {1} нет", startVertex, vertex);
            else PrintPath(startVertex, vertex.prevVertex); Console.WriteLine("Итого: {0}", vertex);
        }
        public class FindPath
        {
            private List<int> currentPath = new List<int>();

            private bool isEnd = false;

            private int[,] graph;
            private int endPoint;

            private int[] flows;

            private List<int> banList = new List<int>();

            public FindPath(int[,] graph, int endPoint)
            {
                this.endPoint = endPoint;
                this.graph = graph;
                this.flows = new int[graph.GetLength(0)];
            }

            public int[] findPath()
            {
                currentPath.Add(graph[0, 0]);

                while (true)
                {
                    if (isEnd)
                    {
                        break;
                    }

                    currentPath.Add(findNextPoint());
                    if (currentPath[currentPath.Count - 1] == -1)
                        currentPath.RemoveAt(currentPath.Count - 1);

                    if (currentPath.Contains(endPoint))
                    {
                        break;
                    }
                }

                return currentPath.ToArray();
            }

            private int findNextPoint()
            {
                int nextPoint = -1;

                nextPoint = findNeighbourWithBiggestCapacity();

                if (nextPoint == -1)
                {
                    nextPoint = findBackwardsNeighbourWithBiggestCapacity();
                    if (nextPoint == -1)
                    {
                        if (currentPath[currentPath.Count - 1] == graph[0, 0])
                        {
                            isEnd = true;
                            return -1;
                        }
                        backtrackOnOnePoint();
                    }
                }

                return nextPoint;
            }

            private int findNeighbourWithBiggestCapacity()
            {
                int nextPoint = -1;
                int maxFlow = 0;

                for (int ctr = 0; ctr < graph.GetLength(0); ++ctr)
                {
                    if (!banList.Contains(ctr) && graph[ctr, 0] == currentPath[currentPath.Count - 1])
                    {
                        if (graph[ctr, 2] > maxFlow)
                        {
                            maxFlow = graph[ctr, 2];
                            nextPoint = graph[ctr, 1];
                        }
                    }
                }

                return nextPoint;
            }

            private int findBackwardsNeighbourWithBiggestCapacity()
            {
                int nextPoint = -1;
                int maxFlow = 0;

                for (int ctr = 0; ctr < graph.GetLength(0); ++ctr)
                {
                    if (!banList.Contains(ctr) && graph[ctr, 1] == currentPath[currentPath.Count - 1] && !currentPath.Contains(graph[ctr, 0]) && flows[ctr] > maxFlow)
                    {
                        maxFlow = flows[ctr];
                        nextPoint = graph[ctr, 0];
                    }
                }

                return nextPoint;
            }

            private void backtrackOnOnePoint()
            {
                banList.Add(getBannedCtr(currentPath[currentPath.Count - 2], currentPath[currentPath.Count - 1]));
                currentPath.RemoveAt(currentPath.Count - 1);
            }

            private int getBannedCtr(int pt1, int pt2)
            {
                for (int ctr = 0; ctr < graph.GetLength(0); ++ctr)
                {
                    if (graph[ctr, 0] == pt1 && graph[ctr, 1] == pt2 || graph[ctr, 1] == pt1 && graph[ctr, 0] == pt2)
                    {
                        return ctr;
                    }
                }

                return 0;
            }

            public void clearTraces()
            {
                banList.Clear();
                currentPath.Clear();
            }


            public int getBottleNeckValue(int[] path)
            {
                int bottleNeckVal = int.MaxValue;
                for (int ctr1 = 0; ctr1 < path.Length - 1; ++ctr1)
                {
                    for (int ctr = 0; ctr < graph.GetLength(0); ++ctr)
                    {
                        if (graph[ctr, 0] == path[ctr1] && graph[ctr, 1] == path[ctr1 + 1])
                        {
                            if (graph[ctr, 2] < bottleNeckVal)
                            {
                                bottleNeckVal = graph[ctr, 2];
                                break;
                            }
                        }
                        //Получение обратного значения
                        if (ctr == graph.GetLength(0) - 1)
                        {
                            for (int ctr2 = 0; ctr2 < graph.GetLength(0); ++ctr2)
                            {
                                if (graph[ctr2, 1] == path[ctr1] && graph[ctr2, 0] == path[ctr1 + 1])
                                {
                                    if (flows[ctr2] < bottleNeckVal)
                                    {
                                        bottleNeckVal = flows[ctr2];
                                        break;
                                    }
                                }
                            }
                        }

                    }
                }

                return bottleNeckVal;
            }

            public void fixFlows(int[] path, int bottleNeckVal)
            {
                for (int ctr = 0; ctr < path.Length - 1; ++ctr)
                {
                    for (int ctr1 = 0; ctr1 < graph.GetLength(0); ++ctr1)
                    {
                        if (graph[ctr1, 0] == path[ctr] && graph[ctr1, 1] == path[ctr + 1])
                        {
                            graph[ctr1, 2] -= bottleNeckVal;
                            flows[ctr1] += bottleNeckVal;
                        }
                    }
                }
            }



        }
  
    public void View()
        {
            foreach (Vertex v in LVertexes)
            {
                Console.WriteLine("Vertex {0}", v);
                foreach (Edge e in v.adjLEdges)
                {
                    Console.WriteLine("Edge {0}", e);
                }
            }
        }


    }
}