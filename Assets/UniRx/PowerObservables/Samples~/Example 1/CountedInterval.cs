using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Gbros.UniRx.PowerObservablesSamples.Example1
{
    [RequireComponent(typeof(Text))]
    [AddComponentMenu(Constants.ComponentPath + nameof(CountedInterval))]
    public class CountedInterval : MonoBehaviour
    {
        [SerializeField] private BoolReactiveProperty pause = new BoolReactiveProperty();
        [SerializeField] private Text hintText;
        private Text text;                                      // UI text component reference 

        void Awake()
        {
            text = GetComponent<Text>();                        // get text component reference so we can display time

            Assert.IsNotNull(hintText, "Hint text is null. Drag and drop component with text to display value");    // validate that serialize field is correctly refferenced

            PowerObservable.CountedInterval(pause)              // Create Counted Interval and pass pause Observable to be able to pause/unpause stream                     
                           .Select(time => $"TIME {time}")      // Select formatted text from string
                           .SubscribeToText(text)               // subscribe to text component reference. Every interval tick subscribe method will be called and refresh UI
                           .AddTo(this);                        // dispose subscription on component destroy

            pause.Select(isPaused => $"SPACEBAR - " + (isPaused ? "UNPAUSE" : "PAUSE"))   // Select formatted text from string
                 .SubscribeToText(hintText)                                               // subscribe to hint text to refresh UI every pause value changed 
                 .AddTo(this);
        }

        private void OnEnable()
        {
            pause.Value = false;                                // resume on enable
        }

        private void OnDisable()
        {
            pause.Value = true;                                 // pause on disable
        }

        void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Space)) return;       // reteurn if spacebar is not pressed

            pause.Value = !pause.Value;                         // toggle pause
        }
    }
}
