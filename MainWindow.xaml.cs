using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FPSBenchmarker
{
    public partial class MainWindow : Window
    {
        private FPSMonitor _fpsMonitor;
        private DispatcherTimer _updateTimer;
        private DispatcherTimer _benchmarkTimer;
        private int _benchmarkDuration = 30;

        public MainWindow()
        {
            InitializeComponent();
            _fpsMonitor = new FPSMonitor();
            
            _updateTimer = new DispatcherTimer();
            _updateTimer.Interval = TimeSpan.FromMilliseconds(100);
            _updateTimer.Tick += UpdateTimer_Tick;

            _benchmarkTimer = new DispatcherTimer();
            _benchmarkTimer.Interval = TimeSpan.FromSeconds(1);
            _benchmarkTimer.Tick += BenchmarkTimer_Tick;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(DurationTextBox.Text, out _benchmarkDuration) || _benchmarkDuration <= 0)
            {
                MessageBox.Show("Please enter a valid duration in seconds.", "Invalid Input");
                return;
            }

            _fpsMonitor.Reset();
            StartButton.IsEnabled = false;
            StopButton.IsEnabled = true;
            DurationTextBox.IsEnabled = false;
            StatusLabel.Text = $"Benchmarking... ({_benchmarkDuration}s remaining)";

            _updateTimer.Start();
            _benchmarkTimer.Start();
            BeginBenchmark();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            StopBenchmark();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            _fpsMonitor.Reset();
            CurrentFPSLabel.Text = "0.00";
            AverageFPSLabel.Text = "0.00";
            MaxFPSLabel.Text = "0.00";
            MinFPSLabel.Text = "0.00";
            FrameCountLabel.Text = "0";
            StatusLabel.Text = "Ready";
            GraphCanvas.Children.Clear();
        }

        private void BeginBenchmark()
        {
            CompositionTarget.Rendering += RenderFrame;
        }

        private void RenderFrame(object sender, EventArgs e)
        {
            if (_updateTimer.IsEnabled)
            {
                _fpsMonitor.RecordFrame();
            }
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            CurrentFPSLabel.Text = _fpsMonitor.CurrentFPS.ToString("F2");
            AverageFPSLabel.Text = _fpsMonitor.AverageFPS.ToString("F2");
            MaxFPSLabel.Text = _fpsMonitor.MaxFPS.ToString("F2");
            MinFPSLabel.Text = _fpsMonitor.MinFPS.ToString("F2");
            FrameCountLabel.Text = _fpsMonitor.FrameCount.ToString();
            
            DrawGraph();
        }

        private void BenchmarkTimer_Tick(object sender, EventArgs e)
        {
            _benchmarkDuration--;
            StatusLabel.Text = $"Benchmarking... ({_benchmarkDuration}s remaining)";

            if (_benchmarkDuration <= 0)
            {
                StopBenchmark();
            }
        }

        private void StopBenchmark()
        {
            _updateTimer.Stop();
            _benchmarkTimer.Stop();
            CompositionTarget.Rendering -= RenderFrame;

            StartButton.IsEnabled = true;
            StopButton.IsEnabled = false;
            DurationTextBox.IsEnabled = true;
            StatusLabel.Text = $"Benchmark Complete - Average FPS: {_fpsMonitor.AverageFPS:F2}";
        }

        private void DrawGraph()
        {
            GraphCanvas.Children.Clear();
            
            double canvasHeight = GraphCanvas.Height;
            double canvasWidth = GraphCanvas.Width;
            double maxFPS = Math.Max(_fpsMonitor.MaxFPS, 144);
            
            if (_fpsMonitor.FrameCount < 2) return;

            Polyline line = new Polyline
            {
                Stroke = new SolidColorBrush(Color.FromRgb(0, 212, 255)),
                StrokeThickness = 2
            }; 

            for (int i = 0; i < Math.Min(_fpsMonitor.FrameCount, 100); i++)
            {
                double x = (i / 100.0) * canvasWidth;
                double fps = _fpsMonitor.CurrentFPS + (i * 0.5);
                double y = canvasHeight - ((fps / maxFPS) * canvasHeight);
                line.Points.Add(new Point(x, Math.Max(0, Math.Min(canvasHeight, y))));
            }

            GraphCanvas.Children.Add(line);
        }
    }
}