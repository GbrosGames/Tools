using System;
using UniRx;

namespace Gbros.UniRx
{
    public partial class PowerObservable
    {
        public const int DefaultTick = 1;
        public static IScheduler DefaultScheduler { get; } = Scheduler.MainThread;
    }
}