using System;
using UniRx;

namespace Gbros.UniRx
{
    public partial class PowerObservable
    {
        /// <summary>
        /// Returns an observable sequence with aggregated time since subscribe
        /// </summary>
        /// <param name="tick">Tick in seconds</param>
        /// <param name="scheduler">Scheduler</param>
        /// <returns></returns>
        public static IObservable<TimeSpan> CountedInterval(float tick = DefaultTick, IScheduler scheduler = null)
        {
            scheduler = scheduler ?? DefaultScheduler;
            return Observable
                .Interval(TimeSpan.FromSeconds(tick), scheduler)
                .Scan(TimeSpan.Zero, (result, item) => result += TimeSpan.FromSeconds(tick));
        }

        /// <summary>
        /// Returns an observable sequence with aggregated time since subscribe with pause option
        /// </summary>
        /// <param name="pause">Pause sequence</param>
        /// <param name="tick">Tick in seconds</param>
        /// <param name="scheduler">Scheduler</param>
        /// <returns></returns>
        public static IObservable<TimeSpan> CountedInterval(IObservable<bool> pause, float tick = DefaultTick, IScheduler scheduler = null)
            => PausableInterval(pause, tick, scheduler)
              .Scan(TimeSpan.Zero, (result, intervalTick) => result += intervalTick);
    }
}
