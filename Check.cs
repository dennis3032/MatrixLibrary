using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace matrixForm
{

    public partial class Check : Form
    {
        private MatrixForm mainForm;
        public Check()
        {
            InitializeComponent();

        }
        public Check(MatrixForm f)
        {
            InitializeComponent();
            mainForm = f;
        }

        public void sum(double[,] matrix2, double[,] matrixRes, int row2, int col2, int rowRes, int colRes)
        {
            try
            {
                if (row2 != rowRes || col2 != colRes)
                {
                    throw new Exception("Невозможно сложить матрицы разного размера! ");
                }
                else
                {
                    dataGridView2.RowCount = row2;
                    dataGridView2.ColumnCount = col2;

                    dataGridView1.RowCount = rowRes;
                    dataGridView1.ColumnCount = colRes;

                    dataGridView3.RowCount = rowRes;
                    dataGridView3.ColumnCount = colRes;

                    double[,] matrixResult = new double[row2, col2];

                    MatrixLibDLL.Lib.subtraction(ref matrixRes, ref matrix2, ref matrixResult, row2, col2);

                    for (int i = 0; i < rowRes; i++) 
                    {
                        for (int j = 0; j < colRes; j++)
                        {
                            dataGridView1.Rows[i].Cells[j].Value = matrixRes[i, j];
                            dataGridView2.Rows[i].Cells[j].Value = matrix2[i, j];
                            dataGridView3.Rows[i].Cells[j].Value = matrixResult[i, j];
                        }
                    }

                    textBox1.Text = "Проверка вычитанием прошла успешно!";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        public void sub(double[,] matrix2, double[,] matrixRes, int row2, int col2, int rowRes, int colRes)
        {
            try
            {
                if (row2 != rowRes || col2 != colRes)
                {
                    throw new Exception("Невозможно сложить матрицы разного размера! ");
                }
                else
                {
                    dataGridView2.RowCount = row2;
                    dataGridView2.ColumnCount = col2;

                    dataGridView1.RowCount = rowRes;
                    dataGridView1.ColumnCount = colRes;

                    dataGridView3.RowCount = rowRes;
                    dataGridView3.ColumnCount = colRes;

                    double[,] matrixResult = new double[row2, col2];

                    MatrixLibDLL.Lib.addition(ref matrixRes, ref matrix2, ref matrixResult, row2, col2);

                    for (int i = 0; i < rowRes; i++)
                    {
                        for (int j = 0; j < colRes; j++)
                        {
                            dataGridView1.Rows[i].Cells[j].Value = matrixRes[i, j];
                            dataGridView2.Rows[i].Cells[j].Value = matrix2[i, j];
                            dataGridView3.Rows[i].Cells[j].Value = matrixResult[i, j];
                        }
                    }

                    textBox1.Text = "Проверка сложением прошла успешно!";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        public void mult(double[,] matrix2, double[,] matrixRes, int row2, int col2, int rowRes, int colRes)
        {
            try
            {
                if (colRes != row2)
                {
                    throw new Exception("Невозможно перемножить матрицы разного размера! ");
                }
                else
                {
                    dataGridView2.RowCount = row2;
                    dataGridView2.ColumnCount = col2;

                    dataGridView1.RowCount = rowRes;
                    dataGridView1.ColumnCount = colRes;

                    dataGridView3.RowCount = rowRes;
                    dataGridView3.ColumnCount = col2;

                    for (int i = 0; i < rowRes; i++)
                    {
                        for (int j = 0; j < colRes; j++)
                        {
                            dataGridView1.Rows[i].Cells[j].Value = matrixRes[i, j];

                        }
                    }
                    for (int i = 0; i < row2; i++)
                    {
                        for (int j = 0; j < col2; j++)
                        {
                            dataGridView2.Rows[i].Cells[j].Value = matrix2[i, j];

                        }
                    }


                    double[,] matrixResult = new double[rowRes, col2];

                    MatrixLibDLL.Lib.division(ref matrixRes, ref matrix2, ref matrixResult, rowRes,row2,col2, colRes);

                    
                    for (int i = 0; i < rowRes; i++)
                    {
                        for (int j = 0; j < col2; j++)
                        { 
                            dataGridView3.Rows[i].Cells[j].Value = matrixResult[i, j];
                            
                        }
                    }

                
                    textBox1.Text = "Проверка делением прошла успешно!";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
