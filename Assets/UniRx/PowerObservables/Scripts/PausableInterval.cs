using System;
using UniRx;

namespace Gbros.UniRx
{
    public partial class PowerObservable
    {
        /// <summary>
        /// Returns an observable sequence with aggregated time since subscribe
        /// </summary>
        /// <param name="pause">Pause sequence</param>
        /// <param name="tick">Tick in seconds</param>
        /// <param name="scheduler">Scheduler</param>
        /// <returns></returns>
        public static IObservable<TimeSpan> PausableInterval(IObservable<bool> pause, float tick = DefaultTick, IScheduler scheduler = null)
        {
            scheduler = scheduler ?? DefaultScheduler;
            return Observable.Interval(TimeSpan.FromSeconds(tick), scheduler).WithLatestFrom(pause.StartWith(false), (_, isPaused) => (intervalTick: TimeSpan.FromSeconds(tick), isPaused))
                         .Where(x => !x.isPaused)
                         .Select(x => x.intervalTick);
        }
    }
}
