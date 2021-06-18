using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab_1_AI_NETforms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();

                List<double[]> list_array = new List<double[]>();

                int[,] input = new int[,] { { 0, 0 }, { 0, 1 }, { 1, 0 }, { 1, 1 } };
                int[] outputs = { 0, 1, 1, 1 };

                double minValue = -1;
                double maxValue = 1;

                double[] learningRate = { 0.05, 0.1, 0.25, 0.5, 0.75, 0.9 };

                double[] weights = { GetRandomNumber(minValue, maxValue), GetRandomNumber(minValue, maxValue), GetRandomNumber(minValue, maxValue) };
                Console.WriteLine("[" + weights[0] + " , " + weights[1] + " , " + weights[2] + "]");


                double totalError = 0;

                int epoch = 20;

                double[,] e_list = new double[learningRate.Length, epoch];

                for (int j = 0; j < learningRate.Length; j++)
                {
                    int s_j = j;
                    Console.WriteLine("эксперимент №" + (++s_j));
                    Console.WriteLine("коэффициент обучения = " + learningRate[j]);
                    double[] weights_tmp = { weights[0], weights[1], weights[2] };
                    list_array.Add(weights_tmp);

                    for (int n = 0; n < epoch; n++)
                    {
                        int s_e = n;
                        if (n == 0)
                            Console.WriteLine("Эпоха №" + (++s_e));

                        totalError = 0;

                        for (int i = 0; i < 4; i++)
                        {
                            int s_i = i;

                            Console.WriteLine("Итерация №" + (++s_i));

                            double output = input[i, 0] * weights_tmp[0] + input[i, 1] * weights_tmp[1] + 1 * weights_tmp[2];

                            Console.WriteLine("S = " + output);

                            int O = (output >= 0) ? 1 : 0;

                            Console.WriteLine("O = " + O);

                            double error = outputs[i] - O;
                            totalError = totalError + (0.5 * Math.Pow(error, 2));

                            Console.WriteLine("E = " + totalError);

                            double new_w0 = learningRate[j] * error * input[i, 0];
                            double new_w1 = learningRate[j] * error * input[i, 1];
                            double new_w2 = learningRate[j] * error * 1;

                            Console.WriteLine("[" + new_w0 + " , " + new_w1 + " , " + new_w2 + "]");

                            weights_tmp[0] += new_w0;
                            weights_tmp[1] += new_w1;
                            weights_tmp[2] += new_w2;

                            Console.WriteLine("[" + weights_tmp[0] + " , " + weights_tmp[1] + " , " + weights_tmp[2] + "]");

                            // list_array.Add(weights_tmp);
                        }

                        e_list[j, n] = totalError / input.Length;

                        Console.WriteLine("Среднеквадратичная ошибка на эпохе = " + totalError / input.Length);
                        Console.WriteLine("========================================");
                    }
                }

                for (int x = 0; x < learningRate.Length; x++)
                {
                    string str = "";

                    for (int y = 0; y < epoch; y++)
                    {
                        str += " " + Math.Round(e_list[x, y], 3) + " ";
                    }
                    Console.WriteLine(str + "\n");
                }

            //foreach (var ar in list_array) {
            //    Console.WriteLine("[" + ar[0] + " , " + ar[1] + " , " + ar[2] + "]");
            //}
            int e_index = 1;

            for (int x = 0; x < learningRate.Length; x++)
            {
                string n_exp = "exp" + e_index;
                chart1.Series.Add(n_exp);
                chart1.Series[n_exp].BorderWidth = 3;
                chart1.Series[n_exp].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chart1.Series[n_exp].LegendText = "Эксперимент №" + e_index;
                
                for (int y = 0; y < epoch; y++)
                {
                    chart1.Series[n_exp].Points.AddXY(y+1, e_list[x, y]);
                }
                e_index++;
            }
            
        }
    }
}