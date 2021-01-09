using UnityEngine;

namespace Gbros.StatelessSamples.Example4
{
    [AddComponentMenu(Constants.ComponentPath + nameof(ChestAnimation))]
    [RequireComponent(typeof(Chest))]
    public class ChestAnimation : MonoBehaviour
    {
        #region SerializedFields
        [SerializeField] AnimationClip chestOpenClip;
        [SerializeField] AnimationClip chestCloseClip;
        #endregion SerializedFields

        private Chest chest;
        private Animation animation;

        private void Awake()
        {
            chest = GetComponent<Chest>();
            animation = GetComponent<Animation>();
        }

        private void OnEnable()
        {
            chest.Closed += OnChestClosed;
            chest.Opened += OnChestOpened;
        }
        private void OnDisable()
        {
            chest.Closed -= OnChestClosed;
            chest.Opened -= OnChestOpened;
        }

        private void OnChestClosed()
        {
            animation.clip = chestCloseClip;
            animation.Play();
        }
        private void OnChestOpened()
        {
            animation.clip = chestOpenClip;
            animation.Play();
        }
    }
}