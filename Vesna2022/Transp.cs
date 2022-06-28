using System;
using System.Collections.Generic;
using System.Text;

namespace Vesna2022
{
        delegate double func(double x);
        delegate double funcVec(Vector x);
        delegate double[] funcVecMas(double[] x);
        delegate Vector funcVecKriteriy(Vector x);

        class Transp
        {
            public static char name;
            public static void matrixChainOrder(int[] p, int n)
            {
                int[,] m = new int[n, n];
                int[,] bracket = new int[n, n];
                for (int i = 1; i < n; i++)
                    m[i, i] = 0;

                for (int L = 2; L < n; L++)
                {
                    for (int i = 1; i < n - L + 1; i++)
                    {
                        int j = i + L - 1;
                        m[i, j] = int.MaxValue;
                        for (int k = i; k <= j - 1; k++)
                        {
                            int q = m[i, k] + m[k + 1, j] +
                                   p[i - 1] * p[k] * p[j];

                            if (q < m[i, j])
                            {
                                m[i, j] = q;
                                bracket[i, j] = k;
                            }
                        }
                    }
                }
                name = 'A';
                Console.Write("\nОптимальная расстановка скобок : ");
                PrintBracket(1, n - 1, n, bracket);
                Console.Write("\nОптимальное количество перемножений : " + m[1, n - 1]);
            }
            public static void PrintBracket(int i, int j, int n, int[,] bracket)
            {
                if (i == j)
                {
                    Console.Write(name++);
                    return;
                }
                Console.Write("(");
                PrintBracket(i, bracket[i, j], n, bracket);
                PrintBracket(bracket[i, j] + 1, j, n, bracket);
                Console.Write(")");
            }

            public static void PureStr(int[,] matrix)
            {
                int minInRow = MinInRow(matrix);
                int maxInCol = MaxInColu(matrix);

                if (minInRow > maxInCol)
                {
                    Console.WriteLine(String.Format("\nВерхняя граница: {0} = Нижняя граница: {1}", minInRow, maxInCol));
                }
                if (minInRow < maxInCol)
                {
                    Console.WriteLine(String.Format("\nВерхняя граница: {0} = Нижняя граница: {1}", minInRow, maxInCol));
                }
                if (minInRow == maxInCol)
                {
                    Console.WriteLine($"/nСедловая точка = {matrix[minInRow, maxInCol]}");
                    Console.WriteLine(String.Format("\nВерхняя граница: {0} = Нижняя граница: {1}", minInRow, maxInCol));
                }

            }
            public static int MinInRow(int[,] matrix)
            {
                int min = Int32.MaxValue;
                int max = Int32.MinValue;
                int[] Min = new int[matrix.GetLength(1)];
                int[] Max = new int[matrix.GetLength(1)];
                for (int i = 0; i < matrix.GetLength(1); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (matrix[i, j] < min)
                            min = matrix[i, j];
                        Min[i] = min;
                    }
                    min = Int32.MaxValue;
                }
                for (int i = 0; i < matrix.GetLength(1); i++)
                {
                    if (max < Min[i])
                        max = Min[i];
                }
                return max;
            }
            private static int MaxInColu(int[,] matrix)
            {
                int min = Int32.MaxValue;
                int max = Int32.MinValue;
                int[] Min = new int[matrix.GetLength(1)];
                int[] Max = new int[matrix.GetLength(1)];
                for (int i = 0; i < matrix.GetLength(1); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (matrix[j, i] > max)
                            max = matrix[j, i];
                        Max[j] = max;
                    }
                    max = Int32.MinValue;
                }
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (min > Max[j])
                        min = Max[j];
                }
                return min;
            }


            //Смешанная стратегия
            public static double GameTheoryMixedStrateg(double[,] matrix)
            {
                if (matrix.GetLength(0) > 2)
                {
                    throw new Exception("Рядов больше 2-ух");
                    //return 0;
                }

                double min = double.MaxValue;

                int iMin = 0;

                int jMin = 0;

                for (var i = 0; i < matrix.GetLength(1); i++)
                {
                    var squareMatrix = new double[2, 2];

                    squareMatrix[0, 0] = matrix[0, i];

                    squareMatrix[1, 0] = matrix[1, i];

                    for (var j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (i != j && i < j)
                        {
                            squareMatrix[0, 1] = matrix[0, j];

                            squareMatrix[1, 1] = matrix[1, j];

                            var del = (squareMatrix[0, 0] + squareMatrix[1, 1] - squareMatrix[0, 1] - squareMatrix[1, 0]);

                            if (del != 0)
                            {
                                var p1 = (squareMatrix[1, 1] - squareMatrix[1, 0]) / del;

                                var p2 = 1 - p1;

                                var q1 = (squareMatrix[1, 1] - squareMatrix[0, 1]) / del;

                                var q2 = 1 - q1;

                                if (p1 < 0 || p2 < 0 || q1 < 0 || q2 < 0)
                                {
                                    continue;
                                }

                                var v = squareMatrix[0, 0] * p1 * q1 + squareMatrix[0, 1] * p1 * q2 + squareMatrix[1, 0] * p2 * q1 + squareMatrix[1, 1] * p2 * q2;

                                if (v < min)
                                {
                                    min = v;

                                    iMin = i;

                                    jMin = j;
                                }
                            }
                        }
                    }
                }

                Console.WriteLine($"Столбец 1 : {iMin + 1}");

                Console.WriteLine($"Ряд 1 : {jMin + 1}");

                return min;
            }

        
        public static Matrix TransportTask_MinSt(Vector a, Vector b, Matrix c)
        {
            //a количество товара у поставщиков
            //b количество товара у потребителя
            //с стоимость перевозки ij

            if (!Equaleble(a, b, c))
            {
                Console.WriteLine("Error");
                return new Matrix(1, 1);
            }
            Matrix x = new Matrix(a.Size, b.Size);
            double s = 0;

            List<double[]> min_znach = FindMinInMatrix(c.Copy());
            foreach (double[] kord in min_znach)
            {
                if (AllEmpty(a)) break;
                int i = (int)kord[0];
                int j = (int)kord[1];
                if (a[i] == 0) continue;
                if (b[j] == 0) continue;
                if (a[i] > b[j])
                {
                    x[i, j] = b[j];
                    s += b[j] * c[i, j];
                    a[i] -= b[j];
                    b[j] = 0;
                }
                else if (a[i] < b[j])
                {
                    x[i, j] = a[i];
                    b[j] -= a[i];
                    s += a[i] * c[i, j];
                    a[i] = 0;
                }
                else
                {
                    x[i, j] = a[i];
                    s += b[j] * c[i, j];
                    a[i] = 0;
                    b[j] = 0;
                }
            }
            Matrix delta = new Matrix(a.Size, b.Size);
            List<double[]> notnull = FindNotNullMat(x);


            Console.WriteLine("Сумма: {0}\n", s);
            return x;
        }

        private static bool Equaleble(Vector a, Vector b, Matrix c)
        {
            double suma = 0;
            double sumb = 0;
            bool res = true;
            for (int i = 0; i < a.Size; i++)
            {
                suma += a[i];
            }
            for (int i = 0; i < b.Size; i++)
            {
                sumb += b[i];
            }
            if (suma != sumb) res = false;
            if ((a.Size != c.GetCountRows()) && (b.Size != c.GetCountColumns()))
            {
                Console.WriteLine("Неправильно заполнена матрица");
                res = false;
            }
            return res;
        }

        private static bool AllEmpty(Vector a)
        {
            bool yeah = true;
            for (int i = 0; i < a.Size; i++)
            {
                if (a[i] != 0)
                {
                    yeah = false;
                }
            }
            return yeah;
        }

        private static List<double[]> FindMinInMatrix(Matrix m)
        {
            int sizeMas = m.GetCountRows() * m.GetCountColumns();

            List<double[]> ress = new List<double[]>();
            double min = 100;
            int k = 0;
            while (k < sizeMas)
            {
                double[] res = new double[2];
                min = 100;
                for (int i = 0; i < m.GetCountRows(); i++)
                {
                    for (int j = 0; j < m.GetCountColumns(); j++)
                    {
                        if (min > m[i, j])
                        {
                            min = m[i, j];
                            res[0] = i;
                            res[1] = j;
                        }
                    }
                }

                ress.Add(res);
                m[(int)res[0], (int)res[1]] = 100;
                k++;

            }
            return ress;
        }

        private static List<double[]> FindNotNullMat(Matrix m)
        {
            int sizeMas = m.GetCountRows() * m.GetCountColumns();

            List<double[]> ress = new List<double[]>();


            double[] res = new double[2];

            for (int i = 0; i < m.GetCountRows(); i++)
            {
                for (int j = 0; j < m.GetCountColumns(); j++)
                {
                    if (0 < m[i, j])
                    {
                        res[0] = i;
                        res[1] = j;
                        ress.Add(res);
                    }
                }
            }
            return ress;
        }
    }
}
