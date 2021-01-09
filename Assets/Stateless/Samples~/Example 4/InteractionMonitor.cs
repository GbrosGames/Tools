using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Gbros.StatelessSamples.Example4
{
    [AddComponentMenu(Constants.ComponentPath + nameof(InteractionMonitor))]
    public class InteractionMonitor : MonoBehaviour
    {
        private List<IInteractable> interactablesInRange = new List<IInteractable>();

        private void OnEnable() => InputMonitor.LeftMouseButtonClick += Interact;
        private void OnDisable() => InputMonitor.LeftMouseButtonClick -= Interact;

        private void Interact()
        {
            if (!interactablesInRange.Any()) return;

            var interactable = interactablesInRange.Last();
            interactable.StartInteraction();
            interactablesInRange.Remove(interactable);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var interactable = other.GetComponentInParent<IInteractable>();
            if (interactable == null) return;

            interactablesInRange.Add(interactable);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            var interactableOutOfRange = collision.GetComponentInParent<IInteractable>();
            if (interactableOutOfRange == null) return;

            interactableOutOfRange.ExitInteraction();
            interactablesInRange.Remove(interactableOutOfRange);
        }
    }
}