using UniRx;
using UnityEngine;

namespace Gbros.StatelessSamples
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerBase : MonoBehaviour
    {
        private Rigidbody2D rigidBody;
        void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            rigidBody.isKinematic = true;
        }

        void Update()
        {
            var screenDistance = Camera.main.WorldToScreenPoint(transform.position).z;
            var newPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenDistance));
            transform.position = new Vector3(newPosition.x, newPosition.y, newPosition.z);
        }
    }
}
