using System;
using System.Collections.Generic;
using System.Text;

namespace Vesna2022
{
    public class MaxFlow
    {
        private int maxFlow = 0;

        public int getMaxFlow(int[,] graph, int endPoint)
        {
            Graph.FindPath findPath = new Graph.FindPath(graph, endPoint);

            while (true)
            {
                int[] path = findPath.findPath();
                if (path[path.Length - 1] != endPoint)
                {
                    break;
                }
                int bottleNeckVal = findPath.getBottleNeckValue(path);
                drawPath(path);
                Console.WriteLine("Наименьший сквозной путь (Принцип 'бутылочного горлышка'): " + bottleNeckVal);
                maxFlow += bottleNeckVal;
                findPath.fixFlows(path, bottleNeckVal);

                findPath.clearTraces();
            }

            Console.WriteLine();
            return maxFlow;
        }

        private void drawPath(int[] path)
        {
            for (int ctr = 0; ctr < path.Length; ctr++)
            {
                Console.Write(path[ctr] + "->");
            }
            Console.WriteLine();
        }



    }
}
