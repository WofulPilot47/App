using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FPSBenchmarker
{
    public class FPSMonitor
    {
        private Queue<long> _frameTimes = new Queue<long>();
        private Stopwatch _stopwatch = new Stopwatch();
        private long _lastFrameTime = 0;
        public double CurrentFPS { get; private set; } = 0;
        public double AverageFPS { get; private set; } = 0;
        public double MinFPS { get; private set; } = 0;
        public double MaxFPS { get; private set; } = 0;
        public int FrameCount { get; private set; } = 0;

        public FPSMonitor()
        {
            _stopwatch.Start();
        }

        public void RecordFrame()
        {
            long currentTime = _stopwatch.ElapsedMilliseconds;
            
            if (_lastFrameTime > 0)
            {
                long deltaTime = currentTime - _lastFrameTime;
                if (deltaTime > 0)
                {
                    double fps = 1000.0 / deltaTime;
                    _frameTimes.Enqueue(deltaTime);
                    CurrentFPS = fps;
                    FrameCount++;

                    if (_frameTimes.Count > 300)
                    {
                        _frameTimes.Dequeue();
                    }

                    UpdateStatistics();
                }
            }
            
            _lastFrameTime = currentTime;
        }

        private void UpdateStatistics()
        {
            if (_frameTimes.Count == 0) return;

            var fpsList = _frameTimes.Select(dt => dt > 0 ? 1000.0 / dt : 0).ToList();
            AverageFPS = fpsList.Average();
            MinFPS = fpsList.Min();
            MaxFPS = fpsList.Max();
        }

        public void Reset()
        {
            _frameTimes.Clear();
            _stopwatch.Restart();
            _lastFrameTime = 0;
            CurrentFPS = 0;
            AverageFPS = 0;
            MinFPS = 0;
            MaxFPS = 0;
            FrameCount = 0;
        }
    }
}