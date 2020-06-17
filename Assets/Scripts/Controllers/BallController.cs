using Assets.Scripts.StateManagement;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Controllers {

    [RequireComponent(typeof(Rigidbody),typeof(SphereCollider))]
    public class BallController : GameStateAware
    {
        private new Rigidbody rigidbody;
        private SphereCollider sphereCollider;
        private BallState state;
    

        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
            rigidbody = GetComponent<Rigidbody>();
            sphereCollider = GetComponent<SphereCollider>();

            state = GameState.BallSpawned();
            state.Release += ReleaseFromPaddle;

            HoldOnPaddle();
        }

        void Update()
        {
            
        }

        private void ReleaseFromPaddle()
        {
            sphereCollider.gameObject.transform.SetParent(transform.parent.parent);
            sphereCollider.enabled = true;
            rigidbody.WakeUp();
            PushBallByVector(Vector3.one);
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == 8)
            {
                GameState.BallDropped(state);
                Destroy(this.gameObject);
            }

            if (collision.gameObject.CompareTag("Paddle"))
            {
                var ballPos = collision.gameObject.transform.position;
                var paddlePos = transform.position;

                PushBallByVector(ballPos - paddlePos);
            }
        }

        private void HoldOnPaddle()
        {
            sphereCollider.enabled = false;
            rigidbody.Sleep();
            state.Capture();
        }

        private void PushBallByVector(Vector3 direction)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.ResetInertiaTensor();
            rigidbody.AddForce(direction.normalized * state.Speed, ForceMode.Impulse);
        }

    }
}
