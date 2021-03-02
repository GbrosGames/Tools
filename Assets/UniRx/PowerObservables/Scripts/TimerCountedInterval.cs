using System;
using UniRx;

namespace Gbros.UniRx
{
    public partial class PowerObservable
    {
        /// <summary>
        /// Returns an observable sequence with aggregated time since subscribe. Lasts for certain duration.
        /// </summary>
        /// <param name="duration">Duration of sequence</param>
        /// <param name="tick">Tick in seconds</param>
        /// <param name="scheduler">Scheduler</param>
        /// <returns></returns>
        public static IObservable<TimeSpan> TimerCountedInterval(float duration, float tick = DefaultTick, IScheduler scheduler = null)
        {
            scheduler ??= DefaultScheduler;
            return CountedInterval(tick, scheduler)
                  .TakeUntil(Observable.Timer(TimeSpan.FromSeconds(duration), scheduler));
        }

        /// <summary>
        /// Returns an observable sequence with aggregated time since subscribe. Lasts for certain duration.
        /// </summary>
        /// <param name="pause">Pause sequence</param>
        /// <param name="duration">Duration of sequence</param>
        /// <param name="tick">Tick in seconds</param>
        /// <param name="scheduler">Scheduler</param>
        /// <returns></returns>
        public static IObservable<TimeSpan> TimerCountedInterval(IObservable<bool> pause, float duration, float tick = DefaultTick, IScheduler scheduler = null)
        {
            scheduler ??= DefaultScheduler;
            return CountedInterval(pause, tick, scheduler)
                .TakeUntil(Observable.Timer(TimeSpan.FromSeconds(duration), scheduler));
        }
    }
}
