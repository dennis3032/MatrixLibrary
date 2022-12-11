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



// произведение
/*

//определение обратной матрицы (нуждается в доработке)
int** reverse(int** ptr_A, int size)
{
	int temp;
	int** ptr_B = new int*[size];
	for (int i = 0; i < size; i++)
	{
		ptr_B[i] = new int[size];
	}
	for (int i = 0; i < size; i++)
		for (int j = 0; j < size; j++)
		{
			ptr_B[i][j] = 0;

			if (i == j)
				ptr_B[i][j] = 1;
		}
	for (int k = 0; k < size; k++)
	{
		temp = ptr_A[k][k];
		for (int j = 0; j < size; j++)
		{
			ptr_A[k][j] /= temp;
			ptr_B[k][j] /= temp;
		}
		for (int i = k + 1; i < size; i++)
		{
			temp = ptr_A[i][k];
			for (int j = 0; j < size; j++)
			{
				ptr_A[i][j] -= ptr_A[k][j] * temp;
				ptr_B[i][j] -= ptr_B[k][j] * temp;
			}
		}
	}
	for (int k = size - 1; k > 0; k--)
	{
		for (int i = k - 1; i >= 0; i--)
		{
			temp = ptr_A[i][k];
			for (int j = 0; j < size; j++)
			{
				ptr_A[i][j] -= ptr_A[k][j] * temp;
				ptr_B[i][j] -= ptr_B[k][j] * temp;
			}
		}
	}

	for (int i = 0; i < size; i++)
		for (int j = 0; j < size; j++)
			ptr_A[i][j] = ptr_B[i][j];

	return ptr_A;
}
//транспортирование
int** transpose(int** ptr_A, int size)
{
	int tmp;
	for (int i = 0; i < size; ++i)
	{
		for (int j = i; j < size; ++j)
		{
			tmp = ptr_A[i][j];
			ptr_A[i][j] = ptr_A[j][i];
			ptr_A[j][i] = tmp;
		}
	}
	return ptr_A;
}


// НЕ НУЖНО !!!
int** initMatrix(matrix* m, int size)  // создание матрицы
{
	int l = 0;
	//int** ptr = (int**)malloc(size * sizeof(int*));

	// Выделить память для массива указателей
	int** ptr =  new int* [size];//со

	 // Выделить память для каждого указателя
	for (int i = 0; i < size; i++)
	{
		ptr[i] = new int[size];
		//ptr[a] = (int*)malloc(size * sizeof(int));
	}

	//printf("elements:\n");

	for (int i = 0; i < size; i++)
	{
		for (int j = 0; j < size; j++)
		{
			//while ((scanf("%lf", &l) != 1) || (getchar() != '\n'))
			//{
			//	printf("Error, try again\n");
			//	while (getchar() != '\n');
			//}
			cin >> l;
			ptr[i][j] = l;
		}
	}
	m->razmer = size;
	m->Mat_A = ptr;

	return(ptr);
	free(ptr);
}*/

/*
// НЕ НКЖНО!!!
void deletMatrix(int** ptr, int size)    // уничтожение
{
	for (int i = 0; i < size; i++)
	{
		free(ptr[i]);
	}
	free(ptr);
}*/
/*
int** stepen(int** ptr_A, int size, int n) //  степень
{

	if (n < 0)  return NULL;
	else if (n == 0)
	{
		int** ptr_C = (int**)malloc(size * sizeof(int*));

		for (int a = 0; a < size; a++)
		{
			ptr_C[a] = (int*)malloc(size * sizeof(int));

		}
		for (int i = 0; i < size; i++)

			for (int j = 0; j < size; j++)

				if (i == j) ptr_C[i][j] = 1;

				else ptr_C[i][j] = 0;


		return ptr_C;
	}
	else if (n == 1) return ptr_A;


	int** ptr_G = composition(ptr_A, ptr_A, size);
	for (int i = 2; i < n; i++) {
		ptr_G = composition(ptr_G, ptr_A, size);
	}
	return ptr_G;

	free(ptr_G);

}*/