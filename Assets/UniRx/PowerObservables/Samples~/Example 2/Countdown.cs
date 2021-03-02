using System;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Gbros.UniRx.PowerObservablesSamples.Example1
{
    [RequireComponent(typeof(Text))]
    [AddComponentMenu(Constants.ComponentPath + nameof(Countdown))]
    public class Countdown : MonoBehaviour
    {
        [SerializeField] float Time = 5f;                           // Initial Time in seconds
        [SerializeField] private Text hintText;
        private Text text;                                          // UI text component reference 
        private CompositeDisposable disposables = new CompositeDisposable();    // initialize holder for subscription

        void Awake()
        {
            text = GetComponent<Text>();                            // get text component reference so we can display time

            Assert.IsNotNull(hintText, "Hint text is null. Drag and drop component with text to display value");    // validate that serialize field is correctly refferenced

            disposables.AddTo(this);                                // dispose disposables on destroy
        }

        private void StartTimer()
        {
            text.text = TimeSpan.FromSeconds(Time).ToString();      // update UI countdown text
            hintText.text = "SPACEBAR - RESTART";                   // update UI hint text

            disposables.Clear();                                    // clear container from disposables and dispose previous subscriptions

            PowerObservable.Countdown(Time)                         // Create Countdown and pass initial time  
                           .Subscribe(OnTick, OnComplete)           // Call OnTick method every tick, and call OnComplete method when countdown is done
                           .AddTo(disposables);                     // dispose subscription on component destroy if compontent is destroyed
        }

        private void OnTick(TimeSpan time)
        {
            text.text = time.ToString();                            // update UI text every tick to its value
        }

        private void OnComplete()
        {
            text.text = "Done!";                                    // update UI text when countdown's done
            hintText.text = "SPACEBAR - START";                     // update UI hint text
        }

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Space)) return;           // reteurn if spacebar is not pressed

            StartTimer();                                           // start or restart timer
        }
    }
}
