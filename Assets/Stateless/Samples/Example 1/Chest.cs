using Stateless;
using UnityEngine;

namespace Gbros.StatelessSamples.Example1
{
    [AddComponentMenu(Constants.ComponentPath + nameof(Chest))]
    public class Chest : MonoBehaviour
    {
        public enum State { Opened, Closed }

        public State CurrentState => stateMachine.State;

        #region SerializedFields
        [SerializeField] AnimationClip openClip;
        [SerializeField] AnimationClip closeClip;
        #endregion SerializedFields

        private StateMachine<State, KeyCode> stateMachine;
        private Animation animation;

        private void Awake()
        {
            animation = GetComponent<Animation>();

            stateMachine = new StateMachine<State, KeyCode>(State.Closed);

            stateMachine
                .Configure(State.Opened)
                .OnEntry(OnOpened)
                .Permit(KeyCode.Space, State.Closed);

            stateMachine
                .Configure(State.Closed)
                .OnEntry(OnClosed)
                .Permit(KeyCode.Space, State.Opened);
        }

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Space)) return;
            ToggleState();
        }

        public void ToggleState() => stateMachine.Fire(KeyCode.Space);

        private void OnClosed()
        {
            animation.clip = closeClip;
            animation.Play();
            // play close sound
        }

        private void OnOpened()
        {
            animation.clip = openClip;
            animation.Play();
            // play open sound
        }
    }
}
