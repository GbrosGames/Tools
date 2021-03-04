using System;
using UniRx;

namespace Gbros.UniRx
{
    public static class ObservableExtensions
    {
        /// <summary>
        /// Pause or resume observable wheter value is true or false 
        /// </summary>
        /// <param name="observable"></param>
        /// <param name="pause"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IObservable<T> WithPause<T>(this IObservable<T> observable, IObservable<bool> pause)
            => observable.WithLatestFrom(pause.StartWith(false), (value, isPaused) => (value, isPaused))
                         .Where(x => !x.isPaused)
                         .Select(x => x.value);
    }
}