using UniRx;

namespace Gbros.UniRx
{
    public partial class PowerObservable
    {
        /// <summary>
        /// Default Tick in Seconds
        /// </summary>
        public const int DefaultTick = 1;

        /// <summary>
        /// Default Scheduler - Main Thread
        /// </summary>
        /// <value></value>
        public static IScheduler DefaultScheduler { get; } = Scheduler.MainThread;
    }
}