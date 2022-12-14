using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using MatrixLibDLL;
using System.Drawing.Printing;
using System.IO;
using System.Security.Cryptography;
using System.Threading;

namespace matrixForm
{
    public partial class MatrixForm : Form
    {
        
        private int hashCode = 0;

        //  private byte[] buf;
        public MatrixForm()
        {
            InitializeComponent();
        }

        // считывание матриц из data grid view
        public string readingFromDataGridView(ref double[,] matrix, int flag)
        {
            switch (flag)
            {
                //считываем первую матрицу
                case 1:
                    for (int i = 0; i < dataGridView1.RowCount; i++) // заполнение массива
                    {
                        for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        {
                            string str = Convert.ToString(dataGridView1.Rows[i].Cells[j].Value);
                            try
                            {
                                if (str == "") // проверка на пустоту элемента массива
                                {
                                    throw new Exception("Отсутсвует элемент матрицы А !");
                                }

                                // проверка элемента массива на число
                                for (int h = 0; h < str.Length; h++)
                                {
                                    if (!(str[h] >= '0' && str[h] <= '9' || str[h] == '-' || str[h] == ','))
                                    {
                                        throw new Exception("Элемент матрицы А не является числом!");
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                return ex.Message;
                            }

                            matrix[i, j] = Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value);
                        }
                    }
                    return "ok";

                //считываем вторую матрицу 
                case 2:
                    for (int i = 0; i < dataGridView2.RowCount; i++) // заполнение массива
                    {
                        for (int j = 0; j < dataGridView2.ColumnCount; j++)
                        {
                            string str = Convert.ToString(dataGridView2.Rows[i].Cells[j].Value);
                            try
                            {
                                if (str == "") // проверка на пустоту элемента массива
                                {
                                    throw new Exception("Отсутсвует элемент матрицы B !");
                                }

                                // проверка элемента массива на число
                                for (int h = 0; h < str.Length; h++)
                                {
                                    if (!(str[h] >= '0' && str[h] <= '9' || str[h] == '-' || str[h] == ','))
                                    {
                                        throw new Exception("Элемент матрицы B не является числом!");
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                return ex.Message;
                            }

                            matrix[i, j] = Convert.ToDouble(dataGridView2.Rows[i].Cells[j].Value);
                        }
                    }
                    return "ok";

                //считываем результирующую матрицу 
                default:
                    for (int i = 0; i < dataGridView3.RowCount; i++) // заполнение массива
                    {
                        for (int j = 0; j < dataGridView3.ColumnCount; j++)
                        {
                            string str = Convert.ToString(dataGridView3.Rows[i].Cells[j].Value);
                            try
                            {
                                if (str == "") // проверка на пустоту элемента массива
                                {
                                    throw new Exception("Отсутсвует элемент результирующей матрицы!");
                                }

                                // проверка элемента массива на число
                                for (int h = 0; h < str.Length; h++)
                                {
                                    if (!(str[h] >= '0' && str[h] <= '9' || str[h] == '-' || str[h] == ','))
                                    {
                                        throw new Exception("Элемент результирующей матрицы не является числом!");
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                return ex.Message;
                            }

                            matrix[i, j] = Convert.ToDouble(dataGridView3.Rows[i].Cells[j].Value);
                        }
                    }
                    return "ok";
            }
        }


        // запись матриц в data grid view (ИЗМЕНЕНИЕ КЛЕТОК)
        public void fillingInDataGridView(int row, int col, ref double[,] matrix, int flag)
        {
            switch (flag)
            {
                //запись первой матрицы
                case 1:
                    dataGridView1.RowCount = row;
                    dataGridView1.ColumnCount = col;

                    DataGridViewRow normalRowSize;
                    for (int i = 0; i < row; i++)
                    {
                        dataGridView1.Rows[i].HeaderCell.Value = i.ToString();
                        normalRowSize = dataGridView1.Rows[i];
                        normalRowSize.Height = (dataGridView1.Height / row) - 5;

                        for (int j = 0; j < col; j++)
                        {
                            dataGridView1.Columns[j].HeaderText = j.ToString();
                            dataGridView1.Rows[i].Cells[j].Value = matrix[i, j];
                        }
                    }

                    break;

                //запись второй матрицы
                case 2:

                    dataGridView2.RowCount = row;
                    dataGridView2.ColumnCount = col;

                    DataGridViewRow normalRowSize2;
                    for (int i = 0; i < row; i++)
                    {
                        dataGridView2.Rows[i].HeaderCell.Value = i.ToString();
                        normalRowSize2 = dataGridView2.Rows[i];
                        normalRowSize2.Height = (dataGridView2.Height / row) - 5;

                        for (int j = 0; j < col; j++)
                        {
                            dataGridView2.Columns[j].HeaderText = j.ToString();
                            dataGridView2.Rows[i].Cells[j].Value = matrix[i, j];
                        }
                    }
                    break;

                //запись результирующей матрицы
                default:

                    dataGridView3.RowCount = row;
                    dataGridView3.ColumnCount = col;

                    DataGridViewRow normalRowSize3;
                    for (int i = 0; i < row; i++)
                    {
                        dataGridView3.Rows[i].HeaderCell.Value = i.ToString();
                        normalRowSize3 = dataGridView3.Rows[i];
                        normalRowSize3.Height = (dataGridView3.Height / row) - 5;
                        for (int j = 0; j < col; j++)
                        {
                            dataGridView3.Columns[j].HeaderText = j.ToString();
                            dataGridView3.Rows[i].Cells[j].Value = matrix[i, j];
                        }
                    }
                    break;
            }
        }


        //(ИЗМЕНЕНИЕ КЛЕТОК)
        private void MatrixForm_Load(object sender, EventArgs e)
        {
            int row = 5;
            int col = 5;

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowCount = row;
            dataGridView1.ColumnCount = col;

            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.RowCount = row;
            dataGridView2.ColumnCount = col;

            dataGridView3.AllowUserToAddRows = false;
            dataGridView3.RowCount = row;
            dataGridView3.ColumnCount = col;

             DataGridViewRow normalRowSize;
            for (int i = 0; i < row; i++)  // название главных столбцов (индексы)
            {
                dataGridView1.Columns[i].HeaderText = i.ToString();
                dataGridView1.Rows[i].HeaderCell.Value = i.ToString();

                dataGridView2.Columns[i].HeaderText = i.ToString();
                dataGridView2.Rows[i].HeaderCell.Value = i.ToString();

                dataGridView3.Columns[i].HeaderText = i.ToString();
                dataGridView3.Rows[i].HeaderCell.Value = i.ToString();

                normalRowSize = dataGridView1.Rows[i];
                normalRowSize.Height = (dataGridView1.Height / row) - 5;

                normalRowSize = dataGridView2.Rows[i];
                normalRowSize.Height = (dataGridView2.Height / row) - 5;

                normalRowSize = dataGridView3.Rows[i];
                normalRowSize.Height = (dataGridView3.Height / row) - 5;
            }

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = 0;
                    dataGridView2.Rows[i].Cells[j].Value = 0;
                    dataGridView3.Rows[i].Cells[j].Value = 0;
                }
            }
        }

        // формирование строки из матриц
        private string createStringMatrix(int flag)
        {
            StringBuilder str = new StringBuilder();
            switch (flag)
            {
                // первая матрица
                case 1:
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        {
                            str.Append(dataGridView1.Rows[i].Cells[j].Value).Append(" ");

                        }
                        str.Append("\r\n");
                    }
                    str.Append(" ");//!!
                    return str.ToString();
                // вторая матрица
                case 2:
                    for (int i = 0; i < dataGridView2.RowCount; i++)
                    {
                        for (int j = 0; j < dataGridView2.ColumnCount; j++)
                        {
                            str.Append(dataGridView2.Rows[i].Cells[j].Value).Append(" ");
                        }
                        str.Append("\r\n");
                    }
                    str.Append(" ");//!!!
                    return str.ToString();
                //результирующая матрица
                case 3:
                    for (int i = 0; i < dataGridView3.RowCount; i++)
                    {
                        for (int j = 0; j < dataGridView3.ColumnCount; j++)
                        {
                            str.Append(dataGridView3.Rows[i].Cells[j].Value).Append(" ");
                        }
                        str.Append("\r\n");
                    }
                    str.Append(" ");//!!!
                    return str.ToString();
                //иначе все вместе
                default:
                    //str.Append("Matrix A: \r\n");
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        {
                            str.Append(dataGridView1.Rows[i].Cells[j].Value).Append(" ");
                        }
                        str.Append("\r\n");
                    }

                    // str.Append("\r\nMatrix B: \r\n");
                    for (int i = 0; i < dataGridView2.RowCount; i++)
                    {
                        for (int j = 0; j < dataGridView2.ColumnCount; j++)
                        {
                            str.Append(dataGridView2.Rows[i].Cells[j].Value).Append(" ");
                        }
                        str.Append("\r\n");
                    }

                    //  str.Append("\r\nMatrix Result: \r\n");
                    for (int i = 0; i < dataGridView3.RowCount; i++)
                    {
                        for (int j = 0; j < dataGridView3.ColumnCount; j++)
                        {
                            str.Append(dataGridView3.Rows[i].Cells[j].Value).Append(" ");
                        }
                        str.Append("\r\n");
                    }
                    str.Append(" "); //!!!
                    return str.ToString();


            }


        }


        // кнопка отчистить для первой матрицы
        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = 0;
                }
            }
        }


        // кнопка отчистить для второй матрицы
        private void button9_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                for (int j = 0; j < dataGridView2.ColumnCount; j++)
                {
                    dataGridView2.Rows[i].Cells[j].Value = 0;
                }
            }
        }


        //изменение размера столбцов для первой матрицы 
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {  // прошлое число столбцов
            int tmp = dataGridView1.ColumnCount;

            // новое число столбцов
            dataGridView1.ColumnCount = Convert.ToInt32(numericUpDown2.Value);

            if (tmp < dataGridView1.ColumnCount)
            {

                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    for (int j = tmp; j < dataGridView1.ColumnCount; j++)
                    {
                        dataGridView1.Rows[i].Cells[j].Value = 0;
                        dataGridView1.Columns[j].HeaderText = j.ToString();
                    }
                }
            }

        }


        //изменение размера строк для первой матрицы(ИЗМЕНЕНИЕ КЛЕТОК)
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            // прошлое число строк
            int tmp = dataGridView1.RowCount;

            // новое число строк
            dataGridView1.RowCount = Convert.ToInt32(numericUpDown1.Value);

            DataGridViewRow normalRowSize;
            if (tmp < dataGridView1.RowCount)
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)//!!!
                {
                    normalRowSize = dataGridView1.Rows[i];
                    normalRowSize.Height = (dataGridView1.Height / tmp) - 5;
                }

                for (int i = tmp; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Rows[i].HeaderCell.Value = i.ToString();

                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {
                        dataGridView1.Rows[i].Cells[j].Value = 0;
                    }

                }
            }
        }


        // кнопка отчистить для результирующей матрицы
        private void button10_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView3.RowCount; i++)
            {
                for (int j = 0; j < dataGridView3.ColumnCount; j++)
                {
                    dataGridView3.Rows[i].Cells[j].Value = 0;
                }
            }
        }


        //изменение размера строк для второй матрицы 
        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            //прошлое число строк
            int tmp = dataGridView2.RowCount;

            // новое число строк
            dataGridView2.RowCount = Convert.ToInt32(numericUpDown3.Value);

            DataGridViewRow normalRowSize;
            if (tmp < dataGridView2.RowCount)
            {
                for (int i = 0; i < dataGridView2.RowCount; i++)//!!!
                {
                    normalRowSize = dataGridView2.Rows[i];
                    normalRowSize.Height = (dataGridView2.Height / tmp) - 5;
                }

                for (int i = tmp; i < dataGridView2.RowCount; i++)
                {
                    dataGridView2.Rows[i].HeaderCell.Value = i.ToString();

                    for (int j = 0; j < dataGridView2.ColumnCount; j++)
                    {
                        dataGridView2.Rows[i].Cells[j].Value = 0;
                    }

                }
            }
        }


        //изменение размера столбцов для второй матрицы 
        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            // прошлое число столбцов
            int tmp = dataGridView2.ColumnCount;

            //новое число столбцов
            dataGridView2.ColumnCount = Convert.ToInt32(numericUpDown4.Value);

            if (tmp < dataGridView2.ColumnCount)
            {

                for (int i = 0; i < dataGridView2.RowCount; i++)
                {
                    for (int j = tmp; j < dataGridView2.ColumnCount; j++)
                    {
                        dataGridView2.Rows[i].Cells[j].Value = 0;
                        dataGridView2.Columns[j].HeaderText = j.ToString();
                    }
                }
            }
        }


        // транспонирование первой матрицы 
        private void button2_Click(object sender, EventArgs e)
        {

            int row1 = dataGridView1.RowCount;
            int col1 = dataGridView1.ColumnCount;

            double[,] matrix1 = new double[row1, col1];
            double[,] matrixPtr = new double[col1, row1];

            //считываем значения из data grid в первую матрицу
            string str1 = readingFromDataGridView(ref matrix1, 1);

            if (str1 == "ok")
            {
                MatrixLibDLL.Lib.transpose(ref matrix1, row1, col1, ref matrixPtr);
                numericUpDown1.Value = col1;
                numericUpDown2.Value = row1;
                fillingInDataGridView(col1, row1, ref matrixPtr, 1);
            }
        }


        //обратная для первой матрицы(ПОТОКИ)
        private void button3_Click(object sender, EventArgs e)
        {
            Thread t1 = new Thread((o) =>
            {
                try
                {
                    int row1 = dataGridView1.RowCount;
                    int col1 = dataGridView1.ColumnCount;

                    if (row1 != col1)
                    {
                        throw new Exception("Невозможно найти обратную матрицу для неквадртной исходной матрицы! ");
                    }
                    else
                    {
                        double[,] matrix1 = new double[row1, col1];

                        //считываем значения из data grid в первую матрицу
                        string str1 = readingFromDataGridView(ref matrix1, 1);

                        if (str1 == "ok")
                        {
                            double det = MatrixLibDLL.Lib.Reverse.FindDeterminant(matrix1, row1);

                            if (det == 0)
                            {
                                throw new Exception("Определитель равен нулю, невозможно найти обратную матрицу! ");
                            }
                            else
                            {
                                MatrixLibDLL.Lib.reverse(ref matrix1, row1, col1);
                                fillingInDataGridView(row1, col1, ref matrix1, 1);

                            }
                        }

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            });
            t1.Start(); // ???
            //t1.Join(); //

        }

        // сложение матриц
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                int row1 = dataGridView1.RowCount;
                int col1 = dataGridView1.ColumnCount;

                int row2 = dataGridView2.RowCount;
                int col2 = dataGridView2.ColumnCount;

                if (row1 != row2 || col1 != col2)
                {
                    throw new Exception("Невозможно сложить матрицы разного размера! ");
                }
                else
                {
                    double[,] matrix1 = new double[row1, col1];
                    double[,] matrix2 = new double[row2, col2];
                    double[,] matrixResult = new double[row2, col2];

                    //считываем значения из data grid в первую матрицу
                    string str1 = readingFromDataGridView(ref matrix1, 1);

                    //считываем значения из data grid во вторую матрицу
                    string str2 = readingFromDataGridView(ref matrix2, 2);

                    if (str1 == "ok" && str2 == "ok")
                    {
                        MatrixLibDLL.Lib.addition(ref matrix1, ref matrix2, ref matrixResult, row1, col1);
                        fillingInDataGridView(row1, col1, ref matrixResult, 3);
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

        }


        // вычетание матриц
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                int row1 = dataGridView1.RowCount;
                int col1 = dataGridView1.ColumnCount;

                int row2 = dataGridView2.RowCount;
                int col2 = dataGridView2.ColumnCount;

                if (row1 != row2 || col1 != col2)
                {
                    throw new Exception("Невозможно вычитать матрицы разного размера! ");
                }
                else
                {
                    double[,] matrix1 = new double[row1, col1];
                    double[,] matrix2 = new double[row2, col2];
                    double[,] matrixResult = new double[row2, col2];


                    //считываем значения из data grid в первую матрицу
                    string str1 = readingFromDataGridView(ref matrix1, 1);

                    //считываем значения из data grid во вторую матрицу
                    string str2 = readingFromDataGridView(ref matrix2, 2);

                    if (str1 == "ok" && str2 == "ok")
                    {
                        MatrixLibDLL.Lib.subtraction(ref matrix1, ref matrix2, ref matrixResult, row1, col1);
                        fillingInDataGridView(row1, col1, ref matrixResult, 3);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }


        //случайное заполнение первой матрицы
        private void button11_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = rand.Next(-100, 100);

                }
            }
        }


        //случайное заполнение второй матрицы
        private void button12_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                for (int j = 0; j < dataGridView2.ColumnCount; j++)
                {
                    dataGridView2.Rows[i].Cells[j].Value = rand.Next(-100, 100);

                }
            }
        }


        // умножение матриц
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                int row1 = dataGridView1.RowCount;
                int col1 = dataGridView1.ColumnCount;

                int row2 = dataGridView2.RowCount;
                int col2 = dataGridView2.ColumnCount;

                if (col1 != row2)
                {
                    throw new Exception("Невозможно перемножить матрицы разного размера! ");
                }
                else
                {
                    double[,] matrix1 = new double[row1, col1];
                    double[,] matrix2 = new double[row2, col2];
                    double[,] matrixResult = new double[row1, col2];

                    //считываем значения из data grid в первую матрицу
                    string str1 = readingFromDataGridView(ref matrix1, 1);

                    //считываем значения из data grid во вторую матрицу
                    string str2 = readingFromDataGridView(ref matrix2, 2);

                    if (str1 == "ok" && str2 == "ok")
                    {
                        MatrixLibDLL.Lib.multiplication(ref matrix1, ref matrix2, ref matrixResult, row1, row2, col1, col2);
                        fillingInDataGridView(row1, col2, ref matrixResult, 3);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }


        //поиск определителя первой матрицы
        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                int row1 = dataGridView1.RowCount;
                int col1 = dataGridView1.ColumnCount;

                if (row1 != col1)
                {
                    throw new Exception("Невозможно найти определитель для неквадртной исходной матрицы! ");
                }
                else
                {
                    double[,] matrix1 = new double[row1, col1];

                    //считываем значения из data grid в первую матрицу
                    string str1 = readingFromDataGridView(ref matrix1, 1);

                    if (str1 == "ok")
                    {
                        double det = MatrixLibDLL.Lib.Reverse.FindDeterminant(matrix1, row1);
                        textBox1.Text = "det = " + det;
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }


        //поиск определителя второй матрицы
        private void button14_Click(object sender, EventArgs e)
        {

            try
            {
                int row2 = dataGridView2.RowCount;
                int col2 = dataGridView2.ColumnCount;

                if (row2 != col2)
                {
                    throw new Exception("Невозможно найти определитель для неквадртной исходной матрицы! ");
                }
                else
                {
                    double[,] matrix2 = new double[row2, col2];

                    //считываем значения из data grid во вторую матрицу
                    string str1 = readingFromDataGridView(ref matrix2, 2);

                    if (str1 == "ok")
                    {
                        double det = MatrixLibDLL.Lib.Reverse.FindDeterminant(matrix2, row2);
                        textBox1.Text = "det = " + det;
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }


        //обратная для второй матрицы (ПОТОКИ)
        private void button8_Click(object sender, EventArgs e)
        {
            Thread t1 = new Thread((o) =>
            {
                try
            {
                int row2 = dataGridView2.RowCount;
                int col2 = dataGridView2.ColumnCount;

                if (row2 != col2)
                {
                    throw new Exception("Невозможно найти обратную матрицу для неквадртной исходной матрицы! ");
                }
                else
                {
                    double[,] matrix2 = new double[row2, col2];

                    //считываем значения из data grid во вторую матрицу
                    string str1 = readingFromDataGridView(ref matrix2, 2);


                    if (str1 == "ok")
                    {
                        double det = MatrixLibDLL.Lib.Reverse.FindDeterminant(matrix2, row2);

                        if (det == 0)
                        {
                            throw new Exception("Определитель равен нулю, невозможно найти обратную матрицу! ");
                        }
                        else
                        {
                            MatrixLibDLL.Lib.reverse(ref matrix2, row2, col2);
                            fillingInDataGridView(row2, col2, ref matrix2, 2);
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            });
            t1.Start();
        }


        // транспонирование второй матрицы 
        private void button7_Click(object sender, EventArgs e)
        {

            int row2 = dataGridView2.RowCount;
            int col2 = dataGridView2.ColumnCount;

            double[,] matrix2 = new double[row2, col2];
            double[,] matrixPtr = new double[col2, row2];

            //считываем значения из data grid во вторую матрицу
            string str1 = readingFromDataGridView(ref matrix2, 2);

            if (str1 == "ok")
            {
                MatrixLibDLL.Lib.transpose(ref matrix2, row2, col2, ref matrixPtr);
                numericUpDown3.Value = col2;
                numericUpDown4.Value = row2;
                fillingInDataGridView(col2, row2, ref matrixPtr, 2);
            }

        }

        // Печать
        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // объект для печати
            PrintDocument printDocument = new PrintDocument();

            // обработчик события печати
            printDocument.PrintPage += PrintPageHandler;

            // диалог настройки печати
            PrintDialog printDialog = new PrintDialog();

            // установка объекта печати для его настройки
            printDialog.Document = printDocument;

            // если в диалоге было нажато ОК
            if (printDialog.ShowDialog() == DialogResult.OK)
                printDialog.Document.Print(); // печатаем
        }


        // обработчик события печати
        private void PrintPageHandler(object sender, PrintPageEventArgs e)
        {
            // задаем текст для печати
            string str = createStringMatrix(4);

            // печать строки str
            e.Graphics.DrawString(str, new Font("Arial", 14), Brushes.Black, 0, 0);
        }


        // считывание матрицы А из файла
        private void матрицуАToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Filter = "Text files(*.txt)|*.txt|ALL filese(*.*)|*.*";

                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }

                string filename = openFileDialog1.FileName;
                string filetext = System.IO.File.ReadAllText(filename);

                if (filetext.GetHashCode() != hashCode)
                {
                    throw new Exception("Осторожно, файл был изменен! ");

                }
                else
                {

                    string line = "";
                    int flag = 1;
                    dataGridView1.RowCount = 0;

                    using (StreamReader sr = new StreamReader(filename, true))
                    {
                        while ((line = sr.ReadLine()) != null && flag == 1)
                        {
                            if (line[0] == ' ')
                            {
                                flag = 0;
                            }
                            else
                            {
                                string[] str = line.Split(' ');
                                dataGridView1.ColumnCount = str.Length - 1;
                                dataGridView1.RowCount++;

                                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                                {
                                    dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[j].Value = Convert.ToDouble(str[j]);

                                }

                            }
                        }
                        sr.Dispose();
                    }

                    MessageBox.Show("Файл открыт!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            // fillingInDataGridView(filetext.Length, column, ref matrixPtr, 1);
        }


        // считывание матрицы B из файла
        private void матрицуВToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Filter = "Text files(*.txt)|*.txt|ALL filese(*.*)|*.*";

                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }

                string filename = openFileDialog1.FileName;
                string filetext = System.IO.File.ReadAllText(filename);

                // textBox1.Text = Convert.ToString(hashCode);
                // textBox2.Text = Convert.ToString(filetext.GetHashCode());
                if (filetext.GetHashCode() != hashCode)
                {
                    throw new Exception("Осторожно, файл был изменен! ");
                }
                else
                {

                    string line = "";
                    int flag = 1;
                    dataGridView2.RowCount = 0;

                    using (StreamReader sr = new StreamReader(filename, true))
                    {
                        while ((line = sr.ReadLine()) != null && flag == 1)
                        {
                            if (line[0] == ' ')
                            {
                                flag = 0;
                            }
                            else
                            {
                                string[] str = line.Split(' ');
                                dataGridView2.ColumnCount = str.Length - 1;
                                dataGridView2.RowCount++;

                                for (int j = 0; j < dataGridView2.ColumnCount; j++)
                                {
                                    dataGridView2.Rows[dataGridView2.RowCount - 1].Cells[j].Value = Convert.ToDouble(str[j]);

                                }

                            }
                        }
                        sr.Dispose();
                    }
                    MessageBox.Show("Файл открыт!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

        }


        // проверка сложения 
        private void сложенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Check check = new Check(this);
            check.Show();

            int row2 = dataGridView2.RowCount;
            int col2 = dataGridView2.ColumnCount;

            int rowRes = dataGridView3.RowCount;
            int colRes = dataGridView3.ColumnCount;

            double[,] matrix_B = new double[row2, col2];
            double[,] matrix_Res = new double[rowRes, colRes];

            //считываем значения из data grid 
            string str1 = readingFromDataGridView(ref matrix_B, 2);

            //считываем значения из data grid 
            string str2 = readingFromDataGridView(ref matrix_Res, 3);

            if (str1 == "ok" && str2 == "ok")
            {
                check.sum(matrix_B, matrix_Res, row2, col2, rowRes, colRes);
            }
        }


        // проверка вычитания 
        private void вычитанияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Check check = new Check(this);
            check.Show();

            int row2 = dataGridView2.RowCount;
            int col2 = dataGridView2.ColumnCount;

            int rowRes = dataGridView3.RowCount;
            int colRes = dataGridView3.ColumnCount;

            double[,] matrix_B = new double[row2, col2];
            double[,] matrix_Res = new double[rowRes, colRes];

            //считываем значения из data grid 
            string str1 = readingFromDataGridView(ref matrix_B, 2);

            //считываем значения из data grid 
            string str2 = readingFromDataGridView(ref matrix_Res, 3);

            if (str1 == "ok" && str2 == "ok")
            {
                check.sub(matrix_B, matrix_Res, row2, col2, rowRes, colRes);
            }

        }


        //проверка умножения
        private void умноженияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Check check = new Check(this);
            check.Show();

            int row2 = dataGridView2.RowCount;
            int col2 = dataGridView2.ColumnCount;

            int rowRes = dataGridView3.RowCount;
            int colRes = dataGridView3.ColumnCount;

            double[,] matrix_B = new double[row2, col2];
            double[,] matrix_Res = new double[rowRes, colRes];

            //считываем значения из data grid 
            string str1 = readingFromDataGridView(ref matrix_B, 2);

            //считываем значения из data grid 
            string str2 = readingFromDataGridView(ref matrix_Res, 3);

            if (str1 == "ok" && str2 == "ok")
            {
                check.mult(matrix_B, matrix_Res, row2, col2, rowRes, colRes);
            }
        }


        //для больших матриц
        private void работаСБольшимиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Calc calc = new Calc(this);
            calc.Show();
        }

        //сохранить матрицу А в файл
        private void матрицаАToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|ALL filese(*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            string filename = saveFileDialog1.FileName;
            string str = createStringMatrix(1);

            //hashCode = str.GetHashCode(); // сохранили хэш
            // textBox1.Text = Convert.ToString(hashCode);

            string path = filename;
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLineAsync(str);
                writer.Dispose();
            }


            string filetext = System.IO.File.ReadAllText(filename);

            hashCode = filetext.GetHashCode();

            //  System.IO.File.WriteAllText(filename, str.ToString());
            MessageBox.Show("Файл сохранен!");

        }


        //сохранить матрицу B в файл
        private void матрицуВToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|ALL filese(*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            string filename = saveFileDialog1.FileName;
            string str = createStringMatrix(2);

            // hashCode = str.GetHashCode();

            string path = filename;
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLineAsync(str);
                writer.Dispose();
            }
            string filetext = System.IO.File.ReadAllText(filename);

            hashCode = filetext.GetHashCode();

            //  System.IO.File.WriteAllText(filename, str.ToString());
            MessageBox.Show("Файл сохранен!");
        }

        //сохранить результирующую матрицу в файл
        private void результатToolStripMenuItem_Click(object sender, EventArgs e)
        {

            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|ALL filese(*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            string filename = saveFileDialog1.FileName;
            string str = createStringMatrix(3);

            // hashCode = str.GetHashCode();

            string path = filename;
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLineAsync(str.ToString() + "\r\n");
                writer.Dispose();
            }
            string filetext = System.IO.File.ReadAllText(filename);

            hashCode = filetext.GetHashCode();

            //  System.IO.File.WriteAllText(filename, str.ToString());
            MessageBox.Show("Файл сохранен!");
        }

        // для проверки изменения файла
        public static byte[] getFileHash(string path)
        {
            using (MD5 md5 = MD5.Create())
            {
                return md5.ComputeHash(File.OpenRead(path));
            }
        }

        // считывание результата  из файла
        private void результатToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Filter = "Text files(*.txt)|*.txt|ALL filese(*.*)|*.*";

                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }

                string filename = openFileDialog1.FileName;
                string filetext = System.IO.File.ReadAllText(filename);

                // textBox1.Text = Convert.ToString(hashCode);
                // textBox2.Text = Convert.ToString(filetext.GetHashCode());
                if (filetext.GetHashCode() != hashCode)
                {
                    throw new Exception("Осторожно, файл был изменен! ");
                }
                else
                {
                    string line = "";
                    int flag = 1;
                    dataGridView3.RowCount = 0;

                    using (StreamReader sr = new StreamReader(filename, true))
                    {
                        while ((line = sr.ReadLine()) != null && flag == 1)
                        {
                            if (line[0] == ' ')
                            {
                                flag = 0;
                            }
                            else
                            {
                                string[] str = line.Split(' ');
                                dataGridView3.ColumnCount = str.Length - 1;
                                dataGridView3.RowCount++;

                                for (int j = 0; j < dataGridView3.ColumnCount; j++)
                                {
                                    dataGridView3.Rows[dataGridView3.RowCount - 1].Cells[j].Value = Convert.ToDouble(str[j]);

                                }

                            }
                        }
                        sr.Dispose();
                    }
                    MessageBox.Show("Файл открыт!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

       
    }
}
