using System;

namespace Vesna2022
{
    class Program
    {
        public static Vector ffff(Vector x)
        {
            Vector fc = new Vector(3);
            fc[0] = x[0] * x[0] + x[0] * x[1] + x[1] * x[1];
            fc[1] = x[0] * x[0] + x[1];
            fc[2] = x[1] + 2 * x[1] * x[1];
            return fc;
        }

        //Граф для Алгоритма Форда-Фалкерсона
        private static int[,] masForMaxFlow = new int[,]
        {
            //Пример с зачета (Теста)
            //1 - исток
            //Первый элемент - начальная вершина
            //Второй элемент - ближайшая вершина
            //Третий элемент - пропускная способность
            { 1,2, 10 },
            { 1,4, 8 },
            { 2,3, 5 },
            { 4,5, 15 },
            { 2,4, 6 },
            { 2,5, 4 },
            { 4,3, 7 },
            { 3,5, 3 },
            { 5,6, 12 },
            { 3,6, 8 }
        };

        static void Main(string[] args)
        {
            var res = new Transp();
            int[] arr = { 40, 20, 30, 10, 30 };
            int nnn = arr.Length;
            Transp.matrixChainOrder(arr, nnn);
            int[,] mas1 = { { 3, 1, 2 },
                           { 2, 1, 0 },
                           { -3, 5, -5 } };
            int[,] mas2 = {{1, 2, 3},
                            {4, 5, 6},
                            {7, 8, 9}};
            Console.WriteLine("Транспортная задача:");
            Vector sklad = new Vector(new double[] { 30, 40, 30 });
            Vector magazin = new Vector(new double[] { 20, 25, 25, 30 });
            Matrix matrixC = new Matrix(new double[,] { { 2, 1, 3, 1 }, { 6, 5, 1, 3 }, { 1, 2, 4, 5 } });
            Matrix minst = Transp.TransportTask_MinSt(sklad, magazin, matrixC);
            Console.WriteLine("\nЧистая стратегия: ");
            Transp.PureStr(mas1);

            Console.WriteLine("\nСмешання стратегия: ");
            var matrixMixed1 = new double[2, 3];
            matrixMixed1[0, 0] = 1;
            matrixMixed1[0, 1] = 3;
            matrixMixed1[0, 2] = 2;
            matrixMixed1[1, 0] = 2;
            matrixMixed1[1, 1] = 1;
            matrixMixed1[1, 2] = 3;
            Console.WriteLine($"Vсм = {Transp.GameTheoryMixedStrateg(matrixMixed1)}");

            Console.WriteLine("\nПример 5 (Смешання стратегия): ");
            var matrixMixed2 = new double[2, 4];
            matrixMixed2[0, 0] = 2;
            matrixMixed2[0, 1] = 3;
            matrixMixed2[0, 2] = 1;
            matrixMixed2[1, 0] = 1;
            matrixMixed2[1, 1] = 2;
            matrixMixed2[1, 2] = 5;
            Console.WriteLine($"Vсм = {Transp.GameTheoryMixedStrateg(matrixMixed2)}");


            Console.WriteLine("\nАлгоритм поиска максимального потока. Алгоритм Форда-Фалкерсона:");
            MaxFlow mf = new MaxFlow();
            Console.WriteLine("Max поток: " + mf.getMaxFlow(masForMaxFlow, 6));  //6 - это сток


            Console.WriteLine("\nМетод ближайшего соседа:");
            Graph graph = new Graph();
            Vertex v1 = new Vertex("1");
            Vertex v2 = new Vertex("2");
            Vertex v3 = new Vertex("3");
            Vertex v4 = new Vertex("4");
            Vertex v5 = new Vertex("5");

            graph.AddVertex(v1);
            graph.AddVertex(v2);
            graph.AddVertex(v3);
            graph.AddVertex(v4);
            graph.AddVertex(v5);

            graph.AddEdge(v1, v2, 9);
            graph.AddEdge(v1, v3, 6);
            graph.AddEdge(v1, v4, 4);
            graph.AddEdge(v1, v5, 6);
            graph.AddEdge(v2, v3, 12);
            graph.AddEdge(v2, v4, 5);
            graph.AddEdge(v2, v5, 7);
            graph.AddEdge(v2, v1, 9);
            graph.AddEdge(v3, v1, 6);
            graph.AddEdge(v3, v2, 12);
            graph.AddEdge(v3, v4, 8);
            graph.AddEdge(v3, v5, 4);
            graph.AddEdge(v4, v1, 4);
            graph.AddEdge(v4, v2, 5);
            graph.AddEdge(v4, v3, 8);
            graph.AddEdge(v4, v5, 10);
            graph.AddEdge(v5, v1, 6);
            graph.AddEdge(v5, v2, 7);
            graph.AddEdge(v5, v3, 4);
            graph.AddEdge(v5, v4, 10);

            Graph.KNN(graph, v1);


            Console.ReadKey();


        }


    }
}
