using System;
using UniRx;
using UnityEngine;

namespace Gbros.UniRx.MessageBrokerSamples.Example4
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] int startAfterSeconds = 3;
        void Start()
        {
            Observable
                .Interval(TimeSpan.FromSeconds(1))              // tick every seconds
                .Take(startAfterSeconds)                        // limit ticks
                .Select(second => startAfterSeconds - second)   // decrease time left by second every interval
                .Subscribe(GameStarter)                         // subscribe to observable with custom GameStarter observer
                .AddTo(this);                                   // dispose if component is destroyed during countdown
        }

        private IObserver<long> GameStarter
         => Observer.Create<long>(                                          // base method to create custom observable
            onNext: secondsLeft => Debug.Log($"Start in {secondsLeft}"),    // every tick we are log time left to start the game
            onCompleted: GameStartMessage.Create().Publish);                // after given time we are publishing GameStartMessage to every receivers
    }

}
