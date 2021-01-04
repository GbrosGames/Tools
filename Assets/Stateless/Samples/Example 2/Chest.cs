using Stateless;
using UnityEngine;

namespace Gbros.StatelessSamples.Example2
{
    [AddComponentMenu(Constants.ComponentPath + nameof(Chest))]
    public class Chest : MonoBehaviour
    {
        public enum State { Opened, Closed }
        public enum Trigger { Open, Close }

        private StateMachine<State, Trigger> stateMachine;
        public State CurrentState => stateMachine.State;

        #region SerializedFields
        [SerializeField] AnimationClip openClip;
        [SerializeField] AnimationClip closeClip;
        #endregion SerializedFields

        private Animation animation;

        private void Awake()
        {
            animation = GetComponent<Animation>();

            stateMachine = new StateMachine<State, Trigger>(State.Closed);

            stateMachine
                .Configure(State.Opened)
                .OnEntry(OnOpened)
                .Permit(Trigger.Close, State.Closed);

            stateMachine
                .Configure(State.Closed)
                .OnEntry(OnClosed)
                .Permit(Trigger.Open, State.Opened);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Open();
                return;
            }

            if (Input.GetMouseButtonDown(1))
            {
                Close();
                return;
            }
        }

        public void Open()
        {
            if (!stateMachine.CanFire(Trigger.Open)) return;
            stateMachine.Fire(Trigger.Open);
        }
        public void Close()
        {
            if (!stateMachine.CanFire(Trigger.Close)) return;
            stateMachine.Fire(Trigger.Close);
        }

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