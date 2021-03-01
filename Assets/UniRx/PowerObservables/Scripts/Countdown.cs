using System;
using UniRx;

namespace Gbros.UniRx
{
    public partial class PowerObservable
    {
        /// <summary>
        /// Returns an observable sequence that produces a differed time value for certain duration. Completes when there is no time left.
        /// </summary>
        /// <param name="initialTime">Start time</param>
        /// <param name="tick">Tick in seconds</param>
        /// <param name="scheduler">Scheduler</param>
        /// <returns></returns>
        public static IObservable<TimeSpan> Countdown(float initialTime, float tick = DefaultTick, IScheduler scheduler = null)
        {
            scheduler = scheduler ?? DefaultScheduler;
            return Observable.Interval(TimeSpan.FromSeconds(tick), scheduler)
                  .Scan(TimeSpan.FromSeconds(initialTime), (result, item) => result -= TimeSpan.FromSeconds(tick))
                  .TakeWhile(timeLeft => timeLeft > TimeSpan.Zero);
        }

        /// <summary>
        /// Returns an observable sequence that produces a differed time value for certain duration with ability to pause and resume. Completes when there is no time left. 
        /// </summary>
        /// <param name="initialTime">Start time</param>
        /// <param name="tick">Tick in seconds</param>
        /// <param name="scheduler">Scheduler</param>
        /// <returns></returns>
        public static IObservable<TimeSpan> Countdown(IObservable<bool> pause, float initialTime, float tick = DefaultTick, IScheduler scheduler = null)
            => PausableInterval(pause, tick, scheduler)
              .Scan(TimeSpan.FromSeconds(initialTime), (result, intervalTick) => result -= intervalTick)
              .TakeWhile(timeLeft => timeLeft > TimeSpan.Zero);
    }
}
