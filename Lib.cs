using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixLibDLL
{
    public class Lib
    {
        public class Reverse
        {
            // поиск алебраических дополнений
            public static double[,] FindDop(double[,] matrix1, int size)
            {
                double[,] matrixDop = new double[size, size];

                if (size == 1)
                {
                    matrixDop[0, 0] = -matrix1[0, 0];
                    return matrixDop;
                }
                else if (size == 2)
                {
                    for (int i = 0; i < size; i++)
                    {
                        for (int j = 0; j < size; j++)
                        {
                            if (i == j) matrixDop[i, j] = matrix1[i, j];
                            else matrixDop[i, j] = -matrix1[i, j];
                        }
                    }
                    return matrixDop;

                }
                else
                {
                    double[,] matrixPtr = new double[size - 1, size - 1];

                    for (int i = 0; i < size; i++)
                    {
                        for (int j = 0; j < size; j++)
                        {
                            // тут вычеркнутая матрица
                            withoutRowCol(matrix1, size, i, j, matrixPtr);
                            // определитель
                            double d = FindDeterminant(matrixPtr, size - 1);

                            if ((i + j) % 2 == 0) matrixDop[i, j] = d;
                            else matrixDop[i, j] = -d;
                        }
                    }
                    return matrixDop;
                }

            }

            //поиск определителя 
            public static double FindDeterminant(double[,] matrix1, int size)
            {
                double det = 0; // определитель
                int degree = 1; // (-1)^(1+j) 

                if (size == 1)
                {
                    return matrix1[0, 0];
                }
                else if (size == 2)
                {
                    return matrix1[0, 0] * matrix1[1, 1] - (matrix1[1, 0] * matrix1[0, 1]);
                }
                else
                {
                    double[,] matrixPtr = new double[size - 1, size - 1];

                    for (int j = 0; j < size; j++)
                    {
                        withoutRowCol(matrix1, size, 0, j, matrixPtr);
                        det = det + (degree * matrix1[0, j] * FindDeterminant(matrixPtr, size - 1));
                        degree = -degree;
                    }
                }

                return det;
            }

            //matrix без row-ой строки и col-того столбца, результат в newMatrix
            public static void withoutRowCol(double[,] matrix, int size, int row, int col, double[,] newMatrix)
            {
                int offRow = 0; //Смещение 
                int offCol = 0;
                for (int i = 0; i < size - 1; i++)
                {
                    //Пропустить row-ую строку
                    if (i == row)
                    {
                        offRow = 1;
                    }

                    offCol = 0; //Обнулить смещение столбца
                    for (int j = 0; j < size - 1; j++)
                    {
                        //Пропустить col-ый столбец
                        if (j == col)
                        {
                            offCol = 1; //Встретили нужный столбец, проускаем его смещением
                        }

                        newMatrix[i, j] = matrix[i + offRow, j + offCol];
                    }
                }
            }
        }

        public static void initZeroMatrix(ref double[,] matrix, int row, int colomn)
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < colomn; j++)
                {
                    matrix[i, j] = 0;
                }
            }
        }

        public static void initRandMatrix(ref double[,] matrix, int row, int colomn)
        {
            Random rand = new Random();

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < colomn; j++)
                {
                    matrix[i, j] = rand.Next(-10, 10);
                }
            }
        }

        public static void addElement(ref double[,] matrix, double element, int i, int j)
        {
            matrix[i, j] = element;
        }

        public static void addition(ref double[,] matrix_A, ref double[,] matrix_B, ref double[,] matrix_C, int row, int colomn)
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < colomn; j++)
                {
                    matrix_C[i, j] = matrix_A[i, j] + matrix_B[i, j];
                }
            }
        }

        public static void subtraction(ref double[,] matrix_A, ref double[,] matrix_B, ref double[,] matrix_C, int row, int colomn)
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < colomn; j++)
                {
                    matrix_C[i, j] = matrix_A[i, j] - matrix_B[i, j];
                }
            }
        }

        public static void multiplication(ref double[,] matrix_A, ref double[,] matrix_B, ref double[,] matrix_C, int row1, int row2, int col1, int col2)
        {
            for (int i = 0; i < row1; i++)
            {
                for (int j = 0; j < col2; j++)
                {
                    double tmp = 0;
                    for (int k = 0; k < col1; k++)
                    {
                        tmp += matrix_A[i, k] * matrix_B[k, j];
                    }
                    matrix_C[i, j] = tmp;
                }
            }
        }

        public static void division(ref double[,] matrix_A, ref double[,] matrix_B, ref double[,] matrix_C, int row1, int row2, int col1, int col2)
        {
          
            // поиск определителя
            double d = Reverse.FindDeterminant(matrix_B, row2);

            // поиск алг. дополнений
            double[,] matrixDop = new double[row2, col2];
            double[,] matrixPtr = new double[col2, row2];
            matrixDop = Reverse.FindDop(matrix_B, row2);

            // транспортируемая матрица алг.дополнений
            transpose(ref matrixDop, row2, col2, ref matrixPtr);

            // расчет обратной
            for (int i = 0; i < row2; i++)
            {
                for (int j = 0; j < col2; j++)
                {
                    double ptr = matrixPtr[i, j] * (1 / d);
                    //ptr = Math.Round(ptr, 3);
                    matrix_B[i, j] = ptr;
                }
            }

            multiplication(ref matrix_A, ref matrix_B, ref matrix_C, row1, row2, col1, col2);

        }


        public static void transpose(ref double[,] matrix_A, int row, int col, ref double[,] matrixDop)
        {

            for (int i = 0; i < row; ++i)
            {
                for (int j = 0; j < col; ++j)
                {
                    matrixDop[j, i] = matrix_A[i, j];
                }
            }
        }

        public static void reverse(ref double[,] matrix1, int row, int colomn)
        {
            // поиск определителя
            double d = Reverse.FindDeterminant(matrix1, row);

            // поиск алг. дополнений
            double[,] matrixDop = new double[row, colomn];
            double[,] matrixPtr = new double[colomn, row];
            matrixDop = Reverse.FindDop(matrix1, row);

            // транспортируемая матрица алг.дополнений
            transpose(ref matrixDop, row, colomn, ref matrixPtr);

            // расчет обратной
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < colomn; j++)
                {
                    double ptr = matrixPtr[i, j] * (1 / d);
                    ptr = Math.Round(ptr, 3);
                    matrix1[i, j] = ptr;
                }
            }

        }

    }
}
