using System;
using System.Timers;
using Avalonia.Threading;

namespace ecosystem;

public class TimerManager
{
    private readonly Timer timer;

    public TimerManager(Action tickAction, double interval)
    {
        timer = new Timer(interval);
        timer.Elapsed += (s, e) =>
        {
            Dispatcher.UIThread.Post(() => tickAction());
        };
        timer.AutoReset = true;
        timer.Start();
    }

    public void Stop() => timer.Stop();
    public void SetInterval(double interval) => timer.Interval = interval;
    public double GetInterval() => timer.Interval;
}
