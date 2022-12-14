using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace matrixForm
{
    public partial class Calc : Form
    {
        private MatrixForm mainForm;
        public Calc()
        {
            InitializeComponent();
        }

        public Calc(MatrixForm f)
        {
            InitializeComponent();
            mainForm = f;
        }

        //сохранение в файл матрицы А
        private void button1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|ALL filese(*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }


            int row = Convert.ToInt32(numericUpDown1.Value);
            int col = Convert.ToInt32(numericUpDown2.Value);

            Random rand1 = new Random();

            string filename = saveFileDialog1.FileName;
            //  StringBuilder str = new StringBuilder();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
        
           Thread t1 = new Thread((o) =>
               {
                   using (StreamWriter writer = new StreamWriter(filename, false))
                   {
                       for (int i = 0; i < row; i++)
                       {
                           // StringBuilder str = new StringBuilder();
                           for (int j = 0; j < col; j++)
                           {
                               writer.Write(rand1.Next(-20, 20) + " ");
                             
                           }

                           writer.Write("\r\n");
                       }
                       writer.Dispose();
                   }
               });
            t1.Start();
            t1.Join();
            stopwatch.Stop();
            textBox1.Text = Convert.ToString(stopwatch.ElapsedMilliseconds) + "ms";


        }

        // сохранение в файл матрицы В
        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|ALL filese(*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            int row = Convert.ToInt32(numericUpDown3.Value);
            int col = Convert.ToInt32(numericUpDown4.Value);

            Random rand1 = new Random();

            string filename = saveFileDialog1.FileName;
            //  StringBuilder str = new StringBuilder();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Thread t1 = new Thread((o) =>
            {
                using (StreamWriter writer = new StreamWriter(filename, false))
                {
                    for (int i = 0; i < row; i++)
                    {
                        // StringBuilder str = new StringBuilder();
                        for (int j = 0; j < col; j++)
                        {
                            writer.Write(rand1.Next(-20, 20) + " ");
                        }

                        writer.Write("\r\n");
                    }
                    writer.Dispose();
                }
            });
            t1.Start();
            t1.Join();
            stopwatch.Stop();
            textBox2.Text = Convert.ToString(stopwatch.ElapsedMilliseconds) + "ms";
        }

        //сложение больших матриц
        private void button3_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|ALL filese(*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            string filename = saveFileDialog1.FileName;

            int row1 = Convert.ToInt32(numericUpDown1.Value);
            int col1 = Convert.ToInt32(numericUpDown2.Value);

            int row2 = Convert.ToInt32(numericUpDown3.Value);
            int col2 = Convert.ToInt32(numericUpDown4.Value);
            try
            {
                if (row1 != row2 || col1 != col2)
                {
                    throw new Exception("Невозможно сложить матрицы разного размера! ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }


            double[,] matrix1 = new double[row1, col1];
            double[,] matrix2 = new double[row2, col2];

            Random rand1 = new Random();
            Thread t1 = new Thread((o) =>
            {
                for (int i = 0; i < row1; i++)
                {
                    for (int j = 0; j < col1; j++)
                    {
                        matrix1[i, j] = rand1.Next(-20, 20);
                    }
                }

            });
            t1.Start();


            Random rand2 = new Random();
            Thread t2 = new Thread((o) =>
            {
                for (int i = 0; i < row2; i++)
                {
                    for (int j = 0; j < col2; j++)
                    {
                        matrix2[i, j] = rand2.Next(-20, 20);
                    }
                }
            });
            t2.Start();

            double[,] matrix3 = new double[row1, col1];
            t1.Join();
            t2.Join();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Thread t3 = new Thread((o) =>
            {
                MatrixLibDLL.Lib.addition(ref matrix1, ref matrix2, ref matrix3, row1, col1);
                using (StreamWriter writer = new StreamWriter(filename, false))
                {
                    for (int i = 0; i < row1; i++)
                    {
                        //StringBuilder str = new StringBuilder();
                        for (int j = 0; j < col1; j++)
                        {
                            writer.Write(matrix3[i, j] + "  ");
                        }

                        writer.Write("\r\n");
                    }
                    writer.Dispose();
                }
            });
            t3.Start();
            t3.Join(); //!!!!!
            stopwatch.Stop();
            textBox3.Text = Convert.ToString(stopwatch.ElapsedMilliseconds) + "ms";
        }

        //вычитание больших матриц
        private void button4_Click(object sender, EventArgs e)
        {

            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|ALL filese(*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            string filename = saveFileDialog1.FileName;

            int row1 = Convert.ToInt32(numericUpDown1.Value);
            int col1 = Convert.ToInt32(numericUpDown2.Value);

            int row2 = Convert.ToInt32(numericUpDown3.Value);
            int col2 = Convert.ToInt32(numericUpDown4.Value);
            try
            {
                if (row1 != row2 || col1 != col2)
                {
                    throw new Exception("Невозможно сложить матрицы разного размера! ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }


            double[,] matrix1 = new double[row1, col1];
            double[,] matrix2 = new double[row2, col2];

            Random rand1 = new Random();
            Thread t1 = new Thread((o) =>
            {
                for (int i = 0; i < row1; i++)
                {
                    for (int j = 0; j < col1; j++)
                    {
                        matrix1[i, j] = rand1.Next(-20, 20);
                    }
                }

            });
            t1.Start();


            Random rand2 = new Random();
            Thread t2 = new Thread((o) =>
            {
                for (int i = 0; i < row2; i++)
                {
                    for (int j = 0; j < col2; j++)
                    {
                        matrix2[i, j] = rand2.Next(-20, 20);
                    }
                }
            });
            t2.Start();

            double[,] matrix3 = new double[row1, col1];
            t1.Join();
            t2.Join();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Thread t3 = new Thread((o) =>
            {
                MatrixLibDLL.Lib.subtraction(ref matrix1, ref matrix2, ref matrix3, row1, col1);
                using (StreamWriter writer = new StreamWriter(filename, false))
                {
                    for (int i = 0; i < row1; i++)
                    {
                        //StringBuilder str = new StringBuilder();
                        for (int j = 0; j < col1; j++)
                        {
                            writer.Write(matrix3[i, j] + "  ");
                        }

                        writer.Write("\r\n");
                    }
                    writer.Dispose();
                }
            });
            t3.Start();
            t3.Join(); //!!!!!
            stopwatch.Stop();
            textBox3.Text = Convert.ToString(stopwatch.ElapsedMilliseconds) + "ms";
        }

        // умножение матриц
        private void button5_Click(object sender, EventArgs e)
        {

            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|ALL filese(*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            string filename = saveFileDialog1.FileName;

            int row1 = Convert.ToInt32(numericUpDown1.Value);
            int col1 = Convert.ToInt32(numericUpDown2.Value);

            int row2 = Convert.ToInt32(numericUpDown3.Value);
            int col2 = Convert.ToInt32(numericUpDown4.Value);
            try
            {
                if (col1 != row2)
                {
                    throw new Exception("Невозможно перемножить матрицы разного размера! ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }


            double[,] matrix1 = new double[row1, col1];
            double[,] matrix2 = new double[row2, col2];

            Random rand1 = new Random();
            Thread t1 = new Thread((o) =>
            {
                for (int i = 0; i < row1; i++)
                {
                    for (int j = 0; j < col1; j++)
                    {
                        matrix1[i, j] = rand1.Next(-20, 20);
                    }
                }

            });
            t1.Start();


            Random rand2 = new Random();
            Thread t2 = new Thread((o) =>
            {
                for (int i = 0; i < row2; i++)
                {
                    for (int j = 0; j < col2; j++)
                    {
                        matrix2[i, j] = rand2.Next(-20, 20);
                    }
                }
            });
            t2.Start();

            double[,] matrix3 = new double[row1, col2];
            t1.Join();
            t2.Join();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Thread t3 = new Thread((o) =>
            {
                MatrixLibDLL.Lib.multiplication(ref matrix1, ref matrix2, ref matrix3, row1, col1, row2, col2);
                using (StreamWriter writer = new StreamWriter(filename, false))
                {
                    for (int i = 0; i < row1; i++)
                    {
                        //StringBuilder str = new StringBuilder();
                        for (int j = 0; j < col1; j++)
                        {
                            writer.Write(matrix3[i, j] + "  ");
                        }

                        writer.Write("\r\n");
                    }
                    writer.Dispose();
                }
            });
            t3.Start();
            t3.Join(); //!!!!!
            stopwatch.Stop();
            textBox3.Text = Convert.ToString(stopwatch.ElapsedMilliseconds) + "ms";
        }

        // транспонировать А
        private void button8_Click(object sender, EventArgs e)
        {

            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|ALL filese(*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            string filename = saveFileDialog1.FileName;

            int row1 = Convert.ToInt32(numericUpDown1.Value);
            int col1 = Convert.ToInt32(numericUpDown2.Value);


            double[,] matrix1 = new double[row1, col1];
            double[,] matrixRes = new double[col1, row1];

            Random rand1 = new Random();
            Thread t1 = new Thread((o) =>
            {
                for (int i = 0; i < row1; i++)
                {
                    for (int j = 0; j < col1; j++)
                    {
                        matrix1[i, j] = rand1.Next(-20, 20);
                    }
                }

            });
            t1.Start();
            t1.Join();


            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Thread t2 = new Thread((o) =>
            {
                MatrixLibDLL.Lib.transpose(ref matrix1, row1, col1, ref matrixRes);
                using (StreamWriter writer = new StreamWriter(filename, false))
                {
                    for (int i = 0; i < col1; i++)
                    {
                        //StringBuilder str = new StringBuilder();
                        for (int j = 0; j < row1; j++)
                        {
                            writer.Write(matrixRes[i, j] + "  ");
                        }

                        writer.Write("\r\n");
                    }
                    writer.Dispose();
                }
            });
            t2.Start();
            t2.Join(); //!!!!!
            stopwatch.Stop();
            textBox1.Text = Convert.ToString(stopwatch.ElapsedMilliseconds) + "ms";
        }

        // транспонировать B
        private void button9_Click(object sender, EventArgs e)
        {

            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|ALL filese(*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            string filename = saveFileDialog1.FileName;

            int row1 = Convert.ToInt32(numericUpDown3.Value);
            int col1 = Convert.ToInt32(numericUpDown4.Value);


            double[,] matrix1 = new double[row1, col1];
            double[,] matrixRes = new double[col1, row1];

            Random rand1 = new Random();
            Thread t1 = new Thread((o) =>
            {
                for (int i = 0; i < row1; i++)
                {
                    for (int j = 0; j < col1; j++)
                    {
                        matrix1[i, j] = rand1.Next(-20, 20);
                    }
                }

            });
            t1.Start();
            t1.Join();


            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Thread t2 = new Thread((o) =>
            {
                MatrixLibDLL.Lib.transpose(ref matrix1, row1, col1, ref matrixRes);
                using (StreamWriter writer = new StreamWriter(filename, false))
                {
                    for (int i = 0; i < col1; i++)
                    {
                        //StringBuilder str = new StringBuilder();
                        for (int j = 0; j < row1; j++)
                        {
                            writer.Write(matrixRes[i, j] + "  ");
                        }

                        writer.Write("\r\n");
                    }
                    writer.Dispose();
                }
            });
            t2.Start();
            t2.Join(); //!!!!!
            stopwatch.Stop();
            textBox2.Text = Convert.ToString(stopwatch.ElapsedMilliseconds) + "ms";
        }

        // обратная для А
        private void button6_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|ALL filese(*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            string filename = saveFileDialog1.FileName;

            int row1 = Convert.ToInt32(numericUpDown1.Value);
            int col1 = Convert.ToInt32(numericUpDown2.Value);

            try
            {
                if (row1 != col1)
                {
                    throw new Exception("Невозможно найти обратную матрицу для неквадртной исходной матрицы! ");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            double[,] matrix1 = new double[row1, col1];
          
            Random rand1 = new Random();
            Thread t1 = new Thread((o) =>
            {
                for (int i = 0; i < row1; i++)
                {
                    for (int j = 0; j < col1; j++)
                    {
                        matrix1[i, j] = rand1.Next(-20, 20);
                    }
                }
                double det = MatrixLibDLL.Lib.Reverse.FindDeterminant(matrix1, row1);
                try
                {
                    if (det == 0)
                    {
                        throw new Exception("Определитель равен нулю, невозможно найти обратную матрицу! ");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            });
            t1.Start();
            t1.Join();


            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Thread t2 = new Thread((o) =>
            {
                MatrixLibDLL.Lib.reverse(ref matrix1, row1, col1);
                using (StreamWriter writer = new StreamWriter(filename, false))
                {
                    for (int i = 0; i < col1; i++)
                    {
                        //StringBuilder str = new StringBuilder();
                        for (int j = 0; j < row1; j++)
                        {
                            writer.Write(matrix1[i, j] + "  ");
                        }

                        writer.Write("\r\n");
                    }
                    writer.Dispose();
                }
            });
            t2.Start();
            t2.Join(); //!!!!!
            stopwatch.Stop();
            textBox1.Text = Convert.ToString(stopwatch.ElapsedMilliseconds) + "ms";
        }

       // обратная для B
        private void button7_Click(object sender, EventArgs e)
        {

            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|ALL filese(*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            string filename = saveFileDialog1.FileName;

            int row1 = Convert.ToInt32(numericUpDown3.Value);
            int col1 = Convert.ToInt32(numericUpDown4.Value);

            try
            {
                if (row1 != col1)
                {
                    throw new Exception("Невозможно найти обратную матрицу для неквадртной исходной матрицы! ");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            double[,] matrix1 = new double[row1, col1];

            Random rand1 = new Random();
            Thread t1 = new Thread((o) =>
            {
                for (int i = 0; i < row1; i++)
                {
                    for (int j = 0; j < col1; j++)
                    {
                        matrix1[i, j] = rand1.Next(-20, 20);
                    }
                }
                double det = MatrixLibDLL.Lib.Reverse.FindDeterminant(matrix1, row1);
                try
                {
                    if (det == 0)
                    {
                        throw new Exception("Определитель равен нулю, невозможно найти обратную матрицу! ");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            });
            t1.Start();
            t1.Join();


            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Thread t2 = new Thread((o) =>
            {
                MatrixLibDLL.Lib.reverse(ref matrix1, row1, col1);
                using (StreamWriter writer = new StreamWriter(filename, false))
                {
                    for (int i = 0; i < col1; i++)
                    {
                        //StringBuilder str = new StringBuilder();
                        for (int j = 0; j < row1; j++)
                        {
                            writer.Write(matrix1[i, j] + "  ");
                        }

                        writer.Write("\r\n");
                    }
                    writer.Dispose();
                }
            });
            t2.Start();
            t2.Join(); //!!!!!
            stopwatch.Stop();
            textBox2.Text = Convert.ToString(stopwatch.ElapsedMilliseconds) + "ms";
        }
    }
}
