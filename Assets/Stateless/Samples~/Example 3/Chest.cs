using Stateless;
using System;
using UnityEngine;

namespace Gbros.StatelessSamples.Example3
{
    [AddComponentMenu(Constants.ComponentPath + nameof(Chest))]
    public class Chest : MonoBehaviour
    {
        public enum State { Opened, Closed }
        public enum Trigger { Open, Close }

        public State CurrentState => stateMachine.State;

        public event Action Opened;
        public event Action Closed;

        private StateMachine<State, Trigger> stateMachine;

        private void Awake()
        {
            stateMachine = new StateMachine<State, Trigger>(State.Closed);

            stateMachine
              .Configure(State.Opened)
              .OnEntry(OnChestOpened)
              .Permit(Trigger.Close, State.Closed);

            stateMachine
                .Configure(State.Closed)
                .OnEntry(OnChestClosed)
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

        private void OnChestClosed() => Closed?.Invoke();
        private void OnChestOpened() => Opened?.Invoke();
    }
}