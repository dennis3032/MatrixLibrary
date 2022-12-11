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

namespace matrixForm
{
    public partial class MatrixForm : Form
    {
        private double[,] matrix1;
        private double[,] matrix2;
        private double[,] matrixResult;
        private double[,] matrixPtr;


        private  int row1 = 0;
        private  int col1 = 0;
        private int row2 = 0;
        private  int col2 = 0;
        private  int row3 = 0;
        private int col3 = 0;
        public MatrixForm()
        {
            InitializeComponent();
        }

        // заполнение матрицы 1 и 2 из datagridview
        public string filling(int flag)
        {
            if (flag == 1)// заполняем первую матрицу
            {
                for (int i = 0; i < dataGridView1.RowCount; i++) // заполнение массива
                {
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {
                        string str = Convert.ToString(dataGridView1.Rows[i].Cells[j].Value);
                        try
                        {
                            if (str == "") // проверка на пустоту элемента массива
                            {
                                throw new Exception("Отсутсвует элемент матрицы 1 !");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            return ex.Message;
                        }

                        try
                        { // проверка элемента массива на число
                            for (int h = 0; h < str.Length; h++)
                            {
                                if (!(str[h] >= '0' && str[h] <= '9' || str[h] == '-' || str[h] == ','))
                                {
                                    throw new Exception("Элемент матрицы 1 не является числом!");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            return ex.Message;
                        }

                        matrix1[i, j] = Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value);
                    }
                }
                return "ОК";
            }
            else   // заполняем вторую матрицу
            {
                for (int i = 0; i < dataGridView2.RowCount; i++) // заполнение массива
                {
                    for (int j = 0; j < dataGridView2.ColumnCount; j++)
                    {
                        string str = Convert.ToString(dataGridView2.Rows[i].Cells[j].Value);
                        try
                        {
                            if (str == "") // проверка на пустоту элемента массива
                            {
                                throw new Exception("Отсутсвует элемент матрицы 2 !");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            return ex.Message;
                        }

                        try
                        { // проверка элемента массива на число
                            for (int h = 0; h < str.Length; h++)
                            {
                                if (!(str[h] >= '0' && str[h] <= '9' || str[h] == '-' || str[h] == ',' || str[h] == '.'))
                                {
                                    throw new Exception("Элемент матрицы 2 не является числом!");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            return ex.Message;
                        }

                        matrix2[i, j] = Convert.ToDouble(dataGridView2.Rows[i].Cells[j].Value);
                    }
                }
                return "ОК";
            }

        }


        //вывод результата
        public void fillingResult(int row, int col)
        {

            // в случае изменения размера массива - перерисовка 
            dataGridView3.RowCount = row;
            dataGridView3.ColumnCount = col;

          // DataGridViewRow normalRowSize;
            for (int i = 0; i < dataGridView3.RowCount; i++)
            {
                // меняем название главных строк
                dataGridView3.Rows[i].HeaderCell.Value = i.ToString();
                //размер ячеек
               // normalRowSize = dataGridView3.Rows[i];
               // normalRowSize.Height = (dataGridView3.Height / row) - 5;
            }


            for (int i = 0; i < dataGridView3.ColumnCount; i++)
            {
                // меняем название главных строк
                dataGridView3.Columns[i].HeaderText = i.ToString();
            }

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    dataGridView3.Rows[i].Cells[j].Value = matrixResult[i, j];
                }
            }
        }


        //вывод матрицы 1
        public void fillingMatrix1(int row, int col)
        {

            // в случае изменения размера массива - перерисовка 
            dataGridView1.RowCount = row;
            dataGridView1.ColumnCount = col;

            //DataGridViewRow normalRowSize;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                // меняем название главных строк
                dataGridView1.Rows[i].HeaderCell.Value = i.ToString();
                //normalRowSize = dataGridView1.Rows[i];
                //normalRowSize.Height = (dataGridView1.Height / row) - 5;
            }


            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                // меняем название главных строк
                dataGridView1.Columns[i].HeaderText = i.ToString();
            }

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = matrix1[i, j];
                }
            }
        }


        public void fillingMatrix1(int row, int col, double[,] matrix)
        {

            // в случае изменения размера массива - перерисовка 
            dataGridView1.RowCount = row;
            dataGridView1.ColumnCount = col;

           // DataGridViewRow normalRowSize;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                // меняем название главных строк
                dataGridView1.Rows[i].HeaderCell.Value = i.ToString();

               // normalRowSize = dataGridView1.Rows[i];
               // normalRowSize.Height = (dataGridView1.Height / row) - 5;
            }


            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                // меняем название главных строк
                dataGridView1.Columns[i].HeaderText = i.ToString();
            }

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = matrix[i, j];
                }
            }
        }


        //вывод матрицы 2
        public void fillingMatrix2(int row, int col)
        {
            // в случае изменения размера массива - перерисовка 
            dataGridView2.RowCount = row;
            dataGridView2.ColumnCount = col;

           // DataGridViewRow normalRowSize;
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                // меняем название главных строк
                dataGridView2.Rows[i].HeaderCell.Value = i.ToString();

                //normalRowSize = dataGridView2.Rows[i];
                //normalRowSize.Height = (dataGridView2.Height / row) - 5;
            }


            for (int i = 0; i < dataGridView2.ColumnCount; i++)
            {
                // меняем название главных строк
                dataGridView2.Columns[i].HeaderText = i.ToString();
            }

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    dataGridView2.Rows[i].Cells[j].Value = matrix2[i, j];
                }
            }
        }


        public void fillingMatrix2(int row, int col, double[,] matrix)
        {

            // в случае изменения размера массива - перерисовка 
            dataGridView2.RowCount = row;
            dataGridView2.ColumnCount = col;

           // DataGridViewRow normalRowSize;
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                // меняем название главных строк
                dataGridView2.Rows[i].HeaderCell.Value = i.ToString();

               // normalRowSize = dataGridView2.Rows[i];
               // normalRowSize.Height = (dataGridView2.Height / row) - 5;
            }


            for (int i = 0; i < dataGridView2.ColumnCount; i++)
            {
                // меняем название главных строк
                dataGridView2.Columns[i].HeaderText = i.ToString();
            }

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    dataGridView2.Rows[i].Cells[j].Value = matrix[i, j];
                }
            }
        }


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

          //  DataGridViewRow normalRowSize;
            for (int i = 0; i < row; i++)  // название главных столбцов (индексы)
            {
                dataGridView1.Columns[i].HeaderText = i.ToString();
                dataGridView1.Rows[i].HeaderCell.Value = i.ToString();

                dataGridView2.Columns[i].HeaderText = i.ToString();
                dataGridView2.Rows[i].HeaderCell.Value = i.ToString();

                dataGridView3.Columns[i].HeaderText = i.ToString();
                dataGridView3.Rows[i].HeaderCell.Value = i.ToString();

               // normalRowSize = dataGridView1.Rows[i];
                //normalRowSize.Height = (dataGridView1.Height / row) - 5;

               // normalRowSize = dataGridView2.Rows[i];
               // normalRowSize.Height = (dataGridView2.Height / row) - 5;

               // normalRowSize = dataGridView3.Rows[i];
               // normalRowSize.Height = (dataGridView3.Height / row) - 5;
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

        // фомирование str, включающей матрицы
        private string createTextMatrix()
        {
            StringBuilder str = new StringBuilder();

            row1 = dataGridView1.RowCount;
            col1 = dataGridView1.ColumnCount;

            row2 = dataGridView2.RowCount;
            col2 = dataGridView2.ColumnCount;

            row3 = dataGridView3.RowCount;
            col3 = dataGridView3.ColumnCount;

            str.Append("Matrix A: \r\n");
            for (int i = 0; i < row1; i++)
            {
                for (int j = 0; j < col1; j++)
                {
                    str.Append(String.Format("{0,5}", dataGridView1.Rows[i].Cells[j].Value)).Append("  ");

                }
                str.Append("\r\n");
            }

            str.Append("\r\nMatrix B: \r\n");
            for (int i = 0; i < row2; i++)
            {
                for (int j = 0; j < col2; j++)
                {
                    str.Append(String.Format("{0,5}", dataGridView2.Rows[i].Cells[j].Value)).Append("  ");

                }
                str.Append("\r\n");
            }

            str.Append("\r\nMatrix Result: \r\n");
            for (int i = 0; i < row3; i++)
            {
                for (int j = 0; j < col3; j++)
                {
                    str.Append(String.Format("{0,5}", dataGridView3.Rows[i].Cells[j].Value)).Append("  ");

                }
                str.Append("\r\n");
            }

            return str.ToString();
        }

        // кнопка отчистить для первой матрицы
        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < numericUpDown1.Value; i++)
            {
                for (int j = 0; j < numericUpDown2.Value; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = 0;
                }
            }
        }


        // кнопка отчистить для второй матрицы
        private void button9_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < numericUpDown3.Value; i++)
            {
                for (int j = 0; j < numericUpDown4.Value; j++)
                {
                    dataGridView2.Rows[i].Cells[j].Value = 0;
                }
            }
        }


        //изменение размера столбцов для первой матрицы 
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            int tmp = dataGridView1.ColumnCount;

            // в случае изменения размера массива - перерисовка 
            dataGridView1.ColumnCount = Convert.ToInt32(numericUpDown2.Value);


            int i = dataGridView1.ColumnCount - 1;

            // меняем название главных столбцов
            dataGridView1.Columns[i].HeaderText = i.ToString();

            if (tmp < dataGridView1.ColumnCount)
            {
                // дополоняем нулями
                for (int j = 0; j < Convert.ToInt32(numericUpDown1.Value); j++)
                {
                    dataGridView1.Rows[j].Cells[i].Value = 0;
                }
            }

        }


        //изменение размера строк для первой матрицы
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int tmp = dataGridView1.RowCount;

            // в случае изменения размера массива - перерисовка 
            dataGridView1.RowCount = Convert.ToInt32(numericUpDown1.Value);

           // DataGridViewRow normalRowSize;
            for (int j = 0; j < dataGridView1.RowCount; j++)
            {
              //  normalRowSize = dataGridView1.Rows[j];
                //normalRowSize.Height = (dataGridView1.Height / dataGridView1.RowCount) - 5;
            }

            int i = dataGridView1.RowCount - 1;

            // меняем название главных строк
            dataGridView1.Rows[i].HeaderCell.Value = i.ToString();

            if (tmp < dataGridView1.RowCount)
            {
                // дополоняем нулями
                for (int j = 0; j < Convert.ToInt32(numericUpDown2.Value); j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = 0;
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

            int tmp = dataGridView2.RowCount;

            // в случае изменения размера массива - перерисовка 
            dataGridView2.RowCount = Convert.ToInt32(numericUpDown3.Value);

            //DataGridViewRow normalRowSize;
           /* for (int j = 0; j < dataGridView2.RowCount; j++)
            {
                normalRowSize = dataGridView2.Rows[j];
                normalRowSize.Height = (dataGridView2.Height / dataGridView2.RowCount) - 5;
            }*/

            int i = dataGridView2.RowCount - 1;

            // меняем название главных строк
            dataGridView2.Rows[i].HeaderCell.Value = i.ToString();

            if (tmp < dataGridView2.RowCount)
            {
                // дополоняем нулями
                for (int j = 0; j < Convert.ToInt32(numericUpDown4.Value); j++)
                {
                    dataGridView2.Rows[i].Cells[j].Value = 0;
                }
            }
        }


        //изменение размера столбцов для второй матрицы 
        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {

            int tmp = dataGridView2.ColumnCount;

            // в случае изменения размера массива - перерисовка 
            dataGridView2.ColumnCount = Convert.ToInt32(numericUpDown4.Value);

            int i = dataGridView2.ColumnCount - 1;

            // меняем название главных столбцов
            dataGridView2.Columns[i].HeaderText = i.ToString();

            if (tmp < dataGridView2.ColumnCount)
            {
                // дополоняем нулями
                for (int j = 0; j < Convert.ToInt32(numericUpDown3.Value); j++)
                {
                    dataGridView2.Rows[j].Cells[i].Value = 0;
                }
            }
        }


        // транспонирование первой матрицы 
        private void button2_Click(object sender, EventArgs e)
        {

            row1 = dataGridView1.RowCount;
            col1 = dataGridView1.ColumnCount;

            matrix1 = new double[row1, col1];
            matrixPtr = new double[col1, row1];

            string str1 = filling(1); //заполняем первую матрицу

            if (str1 == "ОК")
            {
                MatrixLibDLL.Lib.transpose(ref matrix1, row1, col1, ref matrixPtr);
                numericUpDown1.Value = col1;
                numericUpDown2.Value = row1;
                fillingMatrix1(col1, row1, matrixPtr);

            }
        }


        //обратная для первой матрицы
        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                row1 = dataGridView1.RowCount;
                col1 = dataGridView1.ColumnCount;

                if (row1 != col1)
                {
                    throw new Exception("Невозможно найти обратную матрицу для неквадртной исходной матрицы! ");
                }
                else
                {
                    matrix1 = new double[row1, col1];
                    string str1 = filling(1); //заполняем первую матрицу

                    if (str1 == "ОК")
                    {
                        double det = MatrixLibDLL.Lib.Reverse.FindDeterminant(matrix1, row1);

                        if (det == 0)
                        {
                            throw new Exception("Определитель равен нулю, невозможно найти обратную матрицу! ");
                        }
                        else
                        {
                            MatrixLibDLL.Lib.reverse(ref matrix1, row1, col1);
                            fillingMatrix1(row1, col1);
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        // сложение матриц
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                row1 = dataGridView1.RowCount;
                col1 = dataGridView1.ColumnCount;

                row2 = dataGridView2.RowCount;
                col2 = dataGridView2.ColumnCount;

                if (row1 != row2 || col1 != col2)
                {
                    throw new Exception("Невозможно сложить матрицы разного размера! ");
                }
                else
                {
                    matrix1 = new double[row1, col1];
                    matrix2 = new double[row2, col2];
                    matrixResult = new double[row2, col2];

                    string str1 = filling(1); //заполняем первую матрицу
                    string str2 = filling(2);//заполняем вторую матрицу
                    if (str1 == "ОК" && str2 == "ОК")
                    {
                        MatrixLibDLL.Lib.addition(ref matrix1, ref matrix2, ref matrixResult, row1, col1);
                        fillingResult(row1, col1);
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
                row1 = dataGridView1.RowCount;
                col1 = dataGridView1.ColumnCount;

                row2 = dataGridView2.RowCount;
                col2 = dataGridView2.ColumnCount;

                if (row1 != row2 || col1 != col2)
                {
                    throw new Exception("Невозможно вычитать матрицы разного размера! ");
                }
                else
                {
                    matrix1 = new double[row1, col1];
                    matrix2 = new double[row2, col2];
                    matrixResult = new double[row2, col2];

                    string str1 = filling(1); //заполняем первую матрицу
                    string str2 = filling(2);//заполняем вторую матрицу
                    if (str1 == "ОК" && str2 == "ОК")
                    {
                        MatrixLibDLL.Lib.subtraction(ref matrix1, ref matrix2, ref matrixResult, row1, col1);
                        fillingResult(row1, col1);
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
                    dataGridView1.Rows[i].Cells[j].Value = rand.Next(-100,100);

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
                row1 = dataGridView1.RowCount;
                col1 = dataGridView1.ColumnCount;

                row2 = dataGridView2.RowCount;
                col2 = dataGridView2.ColumnCount;

                if (col1 != row2)
                {
                    throw new Exception("Невозможно перемножить матрицы разного размера! ");
                }
                else
                {
                    matrix1 = new double[row1, col1];
                    matrix2 = new double[row2, col2];
                    matrixResult = new double[row1, col2];

                    string str1 = filling(1); //заполняем первую матрицу
                    string str2 = filling(2);//заполняем вторую матрицу
                    if (str1 == "ОК" && str2 == "ОК")
                    {
                        MatrixLibDLL.Lib.multiplication(ref matrix1, ref matrix2, ref matrixResult, row1, row2, col1, col2);
                        fillingResult(row1, col2);
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
                row1 = dataGridView1.RowCount;
                col1 = dataGridView1.ColumnCount;

                if (row1 != col1)
                {
                    throw new Exception("Невозможно найти определитель для неквадртной исходной матрицы! ");
                }
                else
                {
                    matrix1 = new double[row1, col1];
                    string str1 = filling(1); //заполняем первую матрицу

                    if (str1 == "ОК")
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
                row2 = dataGridView2.RowCount;
                col2 = dataGridView2.ColumnCount;

                if (row2 != col2)
                {
                    throw new Exception("Невозможно найти определитель для неквадртной исходной матрицы! ");
                }
                else
                {
                    matrix2 = new double[row2, col2];
                    string str1 = filling(2); //заполняем первую матрицу

                    if (str1 == "ОК")
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


        //обратная для второй матрицы
        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                row2 = dataGridView2.RowCount;
                col2 = dataGridView2.ColumnCount;

                if (row2 != col2)
                {
                    throw new Exception("Невозможно найти обратную матрицу для неквадртной исходной матрицы! ");
                }
                else
                {
                    matrix2 = new double[row2, col2];
                    string str1 = filling(2); //заполняем вторую матрицу

                    if (str1 == "ОК")
                    {
                        double det = MatrixLibDLL.Lib.Reverse.FindDeterminant(matrix2, row2);

                        if (det == 0)
                        {
                            throw new Exception("Определитель равен нулю, невозможно найти обратную матрицу! ");
                        }
                        else
                        {
                            MatrixLibDLL.Lib.reverse(ref matrix2, row2, col2);
                            fillingMatrix2(row2, col2);
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }


        // транспонирование второй матрицы 
        private void button7_Click(object sender, EventArgs e)
        {

            row2 = dataGridView2.RowCount;
            col2 = dataGridView2.ColumnCount;

            matrix2 = new double[row2, col2];
            matrixPtr = new double[col2, row2];

            string str1 = filling(2); //заполняем вторую матрицу

            if (str1 == "ОК")
            {
                MatrixLibDLL.Lib.transpose(ref matrix2, row2, col2, ref matrixPtr);
                numericUpDown3.Value = col2;
                numericUpDown4.Value = row2;
                fillingMatrix2(col2, row2, matrixPtr);

            }
        }


        // сохранить в файл
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|ALL filese(*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            string filename = saveFileDialog1.FileName;

            string str = createTextMatrix();

            System.IO.File.WriteAllText(filename, str.ToString());
            MessageBox.Show("Файл сохранен!");
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
            string str = createTextMatrix();

            // печать строки str
            e.Graphics.DrawString(str, new Font("Arial", 14), Brushes.Black, 0, 0);
        }

        private void консольToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("hello");
        }

        // ввод матрицы А из файла
       
        private void матрицуАToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|ALL filese(*.*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            string filename = openFileDialog1.FileName;
            string[] filetext = System.IO.File.ReadAllLines(filename);

            string[] strPtr = filetext[0].Split(' ');
            int column = strPtr.Length;

            matrixPtr = new double[filetext.Length, column];
            for (int i = 0; i < filetext.Length; i++) // идем построчно
            {
                string[] str = filetext[i].Split(' ');
                for (int j = 0; j < column; j++)
                {
                    matrixPtr[i, j] = Convert.ToDouble(str[j]);
                }
            }
            fillingMatrix1(filetext.Length, column, matrixPtr);


            MessageBox.Show("Файл открыт!");
        }

        // ввод матрицы В из файла
        private void матрицуВToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|ALL filese(*.*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            string filename = openFileDialog1.FileName;
            string[] filetext = System.IO.File.ReadAllLines(filename);

            string[] strPtr = filetext[0].Split(' ');
            int column = strPtr.Length;

            matrixPtr = new double[filetext.Length, column];
            for (int i = 0; i < filetext.Length; i++) // идем построчно
            {
                string[] str = filetext[i].Split(' ');
                for (int j = 0; j < column; j++)
                {
                    matrixPtr[i, j] = Convert.ToDouble(str[j]);
                }
            }
            fillingMatrix2(filetext.Length, column, matrixPtr);


            MessageBox.Show("Файл открыт!");
        }

        // проверка сложения 
        private void сложенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Check check = new Check(this);
            check.Show();

            double[,] matrix_B = new double[dataGridView2.RowCount, dataGridView2.ColumnCount];
            double[,] matrix_Res = new double[dataGridView3.RowCount, dataGridView3.ColumnCount];

            for (int i = 0; i < dataGridView2.RowCount; i++) // идем построчно
            {
                for (int j = 0; j < dataGridView2.ColumnCount; j++)
                {
                    matrix_B[i, j] = Convert.ToDouble(dataGridView2.Rows[i].Cells[j].Value);
                }
            }

            for (int i = 0; i < dataGridView3.RowCount; i++) // идем построчно
            {
                for (int j = 0; j < dataGridView3.ColumnCount; j++)
                {
                    matrix_Res[i, j] = Convert.ToDouble(dataGridView3.Rows[i].Cells[j].Value);
                }
            }

            check.sum(matrix_B, matrix_Res, dataGridView2.RowCount, dataGridView2.ColumnCount, dataGridView3.RowCount, dataGridView3.ColumnCount);

        }

        // проверка вычитания 
        private void вычитанияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Check check = new Check(this);
            check.Show();

            double[,] matrix_B = new double[dataGridView2.RowCount, dataGridView2.ColumnCount];
            double[,] matrix_Res = new double[dataGridView3.RowCount, dataGridView3.ColumnCount];

            for (int i = 0; i < dataGridView2.RowCount; i++) 
            {
                for (int j = 0; j < dataGridView2.ColumnCount; j++)
                {
                    matrix_B[i, j] = Convert.ToDouble(dataGridView2.Rows[i].Cells[j].Value);
                }
            }

            for (int i = 0; i < dataGridView3.RowCount; i++) 
            {
                for (int j = 0; j < dataGridView3.ColumnCount; j++)
                {
                    matrix_Res[i, j] = Convert.ToDouble(dataGridView3.Rows[i].Cells[j].Value);
                }
            }

            check.sub(matrix_B, matrix_Res, dataGridView2.RowCount, dataGridView2.ColumnCount, dataGridView3.RowCount, dataGridView3.ColumnCount);

        }
        //проверка умножения
        private void умноженияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Check check = new Check(this);
            check.Show();

            double[,] matrix_B = new double[dataGridView2.RowCount, dataGridView2.ColumnCount];
            double[,] matrix_Res = new double[dataGridView3.RowCount, dataGridView3.ColumnCount];

            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                for (int j = 0; j < dataGridView2.ColumnCount; j++)
                {
                    matrix_B[i, j] = Convert.ToDouble(dataGridView2.Rows[i].Cells[j].Value);
                }
            }

            for (int i = 0; i < dataGridView3.RowCount; i++)
            {
                for (int j = 0; j < dataGridView3.ColumnCount; j++)
                {
                    matrix_Res[i, j] = Convert.ToDouble(dataGridView3.Rows[i].Cells[j].Value);
                }
            }


            check.mult(matrix_B, matrix_Res, dataGridView2.RowCount, dataGridView2.ColumnCount, dataGridView3.RowCount, dataGridView3.ColumnCount);

        }
    }
}
