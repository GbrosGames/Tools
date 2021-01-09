using UnityEngine;
using System;

namespace Gbros.StatelessSamples.Example4
{
    [AddComponentMenu(Constants.ComponentPath + nameof(InputMonitor))]
    public class InputMonitor : MonoBehaviour
    {
        public static Action LeftMouseButtonClick;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) LeftMouseButtonClick?.Invoke();
        }
    }
}