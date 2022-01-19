#if DEMO
using UnityEngine;

namespace UnityUIEx.Demo.ui_ex.Scripts.Demo
{
    public sealed class Circle : MonoBehaviour
    {
        [SerializeField]
        private float radius;

        [SerializeField]
        private float startAngle;

        [SerializeField]
        private float speed;

        private float _counter;

        private void Awake()
        {
            _counter = startAngle;
        }

        private void FixedUpdate()
        {
            _counter += Time.fixedDeltaTime * speed;

            transform.position = new Vector3(
                Mathf.Sin(_counter) * radius,
                0f, 
                Mathf.Cos(_counter) * radius
            );
        }
    }
}
#endif