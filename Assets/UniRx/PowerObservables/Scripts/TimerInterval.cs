using System;
using UniRx;

namespace Gbros.UniRx
{
    public partial class PowerObservable
    {
        /// <summary>
        /// Returns an observable sequence that produces a value after each period for certain duration
        /// </summary>
        /// <param name="duration">Duration of sequence</param>
        /// <param name="tick">Tick in seconds</param>
        /// <param name="scheduler">Scheduler</param>
        /// <returns></returns>
        public static IObservable<TimeSpan> TimerInterval(float duration, float tick = DefaultTick, IScheduler scheduler = null)
        {
            scheduler ??= DefaultScheduler;
            return Observable.Interval(TimeSpan.FromSeconds(tick), scheduler)
              .Select(_ => TimeSpan.FromSeconds(tick))
              .Take(TimeSpan.FromSeconds(duration), scheduler);
        }

        /// <summary>
        /// Returns an observable sequence that produces a value after each period for certain duration with ability to pause.
        /// </summary>
        /// <param name="pause">Pause sequence</param>
        /// <param name="duration">Duration of sequence</param>
        /// <param name="tick">Tick in seconds</param>
        /// <param name="scheduler">Scheduler</param>
        /// <returns></returns>
        public static IObservable<TimeSpan> TimerInterval(IObservable<bool> pause, float duration, float tick = DefaultTick, IScheduler scheduler = null)
        {
            scheduler ??= DefaultScheduler;
            return PausableInterval(pause, tick, scheduler).Take(TimeSpan.FromSeconds(duration), scheduler);
        }
    }
}
