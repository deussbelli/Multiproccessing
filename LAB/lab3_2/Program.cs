using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

//Написати паралельну програму, яка обчислює матричний вираз за варіантом.
//Всі матриці є квадратними, мають розмірність N і задаються випадковими цілими числами
//у діапазоні [–10; 10]. Програма повинна вирішувати такі завдання:
//1)	Кількість робочих потоків P, якими виконують паралельні обчислення,
//має задаватися користувачем;

//2)	Потрібно побудувати графік залежності часу виконання програми від розмірності матриць
//(N = 103, 104, 105 …) при однопоточному режимі та кількості потоків, яка відповідає кількості
//логічних ядер персонального комп’ютера.

//Варіант: 1
//Вираз, для  обчислення:	(M1+M2)*(M3+M4)+M5*M6

namespace MatrixComputation
{
    public class MainForm : Form
    {
        private TextBox textBoxThreads;
        private TextBox textBoxSize;
        private Button buttonCompute;
        private Chart chart1;
        private Label labelThreads;
        private Label labelSize;

        public MainForm()
        {
            this.Text = "Обчислення матричного виразу";
            this.Size = new Size(800, 600);
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            // Створення текстових полів
            textBoxThreads = new TextBox();
            textBoxThreads.Location = new Point(150, 20);
            textBoxThreads.Text = "4"; 

            textBoxSize = new TextBox();
            textBoxSize.Location = new Point(150, 60);
            textBoxSize.Text = "100"; 

            // Створення міток
            labelThreads = new Label();
            labelThreads.Text = "Кількість потоків (P):";
            labelThreads.Location = new Point(20, 20);
            labelThreads.AutoSize = true;

            labelSize = new Label();
            labelSize.Text = "Розмірність матриці (N):";
            labelSize.Location = new Point(20, 60);
            labelSize.AutoSize = true;

            // Створення кнопки
            buttonCompute = new Button();
            buttonCompute.Text = "Обчислити";
            buttonCompute.Location = new Point(150, 100);
            buttonCompute.Click += ButtonCompute_Click;

            // Створення графіку
            chart1 = new Chart();
            chart1.Location = new Point(20, 150);
            chart1.Size = new Size(750, 400);

            ChartArea chartArea = new ChartArea();
            chartArea.AxisX.Title = "Розмірність N";
            chartArea.AxisY.Title = "Час виконання (мс)";
            chart1.ChartAreas.Add(chartArea);

            Series seriesSingle = new Series("Однопотоковий");
            seriesSingle.ChartType = SeriesChartType.Line;
            seriesSingle.Color = Color.Blue;
            seriesSingle.MarkerStyle = MarkerStyle.Circle;
            seriesSingle.MarkerSize = 7;
            seriesSingle.ToolTip = "N = #VALX, Time = #VALY мс";
            chart1.Series.Add(seriesSingle);

            Series seriesMulti = new Series("Багатопотоковий");
            seriesMulti.ChartType = SeriesChartType.Line;
            seriesMulti.Color = Color.Green;
            seriesMulti.MarkerStyle = MarkerStyle.Circle;
            seriesMulti.MarkerSize = 7;
            seriesMulti.ToolTip = "N = #VALX, Time = #VALY мс";
            chart1.Series.Add(seriesMulti);

            this.Controls.Add(textBoxThreads);
            this.Controls.Add(textBoxSize);
            this.Controls.Add(labelThreads);
            this.Controls.Add(labelSize);
            this.Controls.Add(buttonCompute);
            this.Controls.Add(chart1);
        }

        private async void ButtonCompute_Click(object sender, EventArgs e)
        {
            int P;
            int N;

            if (!int.TryParse(textBoxThreads.Text, out P) || P <= 0)
            {
                MessageBox.Show("Будь ласка, введіть коректну кількість потоків (позитивне ціле число).");
                return;
            }

            if (!int.TryParse(textBoxSize.Text, out N) || N <= 0)
            {
                MessageBox.Show("Будь ласка, введіть коректну розмірність матриці N (позитивне ціле число).");
                return;
            }

            MessageBox.Show("Обчислення почато");

            chart1.Series["Однопотоковий"].Points.Clear();
            chart1.Series["Багатопотоковий"].Points.Clear();

            int[] Ns = { N, N * 2, N * 4, N * 8 }; 

            // Запуск обчислень асинхронно, щоб не блокувати інтерфейс
            await Task.Run(() =>
            {
                foreach (int n in Ns)
                {
                    // Однопотоковий режим
                    var timeSingle = ComputeExpression(n, 1);

                    // Багатопотоковий режим
                    var timeMulti = ComputeExpression(n, P);

                    // Додавання даних на графік в потоці UI
                    this.Invoke(new Action(() =>
                    {
                        // Для серії "Однопотоковий"
                        int indexSingle = chart1.Series["Однопотоковий"].Points.AddXY(n, timeSingle);
                        var pointSingle = chart1.Series["Однопотоковий"].Points[indexSingle];
                        pointSingle.ToolTip = $"N = {n}, Time = {timeSingle} мс";

                        // Для серії "Багатопотоковий"
                        int indexMulti = chart1.Series["Багатопотоковий"].Points.AddXY(n, timeMulti);
                        var pointMulti = chart1.Series["Багатопотоковий"].Points[indexMulti];
                        pointMulti.ToolTip = $"N = {n}, Time = {timeMulti} мс";
                    }));
                }
            });

            AddEndPointMarkers();

            MessageBox.Show("Обчислення завершено");
        }

        private void AddEndPointMarkers()
        {
            var seriesSingle = chart1.Series["Однопотоковий"];
            if (seriesSingle.Points.Count > 0)
            {
                var lastPoint = seriesSingle.Points[seriesSingle.Points.Count - 1];
                lastPoint.MarkerStyle = MarkerStyle.Circle;
                lastPoint.MarkerSize = 10;
                lastPoint.MarkerColor = Color.Red;
            }

            var seriesMulti = chart1.Series["Багатопотоковий"];
            if (seriesMulti.Points.Count > 0)
            {
                var lastPoint = seriesMulti.Points[seriesMulti.Points.Count - 1];
                lastPoint.MarkerStyle = MarkerStyle.Circle;
                lastPoint.MarkerSize = 10;
                lastPoint.MarkerColor = Color.Red;
            }
        }

        private long ComputeExpression(int N, int P)
        {
            int[,] M1 = GenerateMatrix(N);
            int[,] M2 = GenerateMatrix(N);
            int[,] M3 = GenerateMatrix(N);
            int[,] M4 = GenerateMatrix(N);
            int[,] M5 = GenerateMatrix(N);
            int[,] M6 = GenerateMatrix(N);

            int[,] S1 = new int[N, N];
            int[,] S2 = new int[N, N];
            int[,] P1 = new int[N, N];
            int[,] P2 = new int[N, N];
            int[,] Result = new int[N, N];

            var stopwatch = Stopwatch.StartNew();

            // Паралельні обчислення
            ParallelOptions po = new ParallelOptions();
            po.MaxDegreeOfParallelism = P;

            // S1 = M1 + M2
            Parallel.For(0, N, po, i =>
            {
                for (int j = 0; j < N; j++)
                {
                    S1[i, j] = M1[i, j] + M2[i, j];
                }
            });

            // S2 = M3 + M4
            Parallel.For(0, N, po, i =>
            {
                for (int j = 0; j < N; j++)
                {
                    S2[i, j] = M3[i, j] + M4[i, j];
                }
            });

            // P1 = S1 * S2
            Parallel.For(0, N, po, i =>
            {
                for (int j = 0; j < N; j++)
                {
                    int sum = 0;
                    for (int k = 0; k < N; k++)
                    {
                        sum += S1[i, k] * S2[k, j];
                    }
                    P1[i, j] = sum;
                }
            });

            // P2 = M5 * M6
            Parallel.For(0, N, po, i =>
            {
                for (int j = 0; j < N; j++)
                {
                    int sum = 0;
                    for (int k = 0; k < N; k++)
                    {
                        sum += M5[i, k] * M6[k, j];
                    }
                    P2[i, j] = sum;
                }
            });

            // Result = P1 + P2
            Parallel.For(0, N, po, i =>
            {
                for (int j = 0; j < N; j++)
                {
                    Result[i, j] = P1[i, j] + P2[i, j];
                }
            });

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private int[,] GenerateMatrix(int N)
        {
            Random rand = new Random();
            int[,] matrix = new int[N, N];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    matrix[i, j] = rand.Next(-10, 11);
                }
            }
            return matrix;
        }

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new MainForm());
        }
    }
}
