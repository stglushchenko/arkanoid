using Assets.Scripts.StateManagement;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Controllers {

    [RequireComponent(typeof(CharacterController))]
    public class PaddleController : GameStateAware
    {
        private float initialHeightAboveThePaddle = 1.5f;
        private CharacterController characterController;
        
        [SerializeField]
        private BallController ballControllerPrefab;

        private PaddleState state;

        public override void Start()
        {
            base.Start();
            characterController = GetComponent<CharacterController>();
            state = GameState.PaddleState;

            state.OnSpawnNewBall += SpawnBall;

            state.ControllerInitialized();
        }

        private void SpawnBall()
        {
            var ball = Instantiate(ballControllerPrefab);

            ball.gameObject.transform.SetParent(transform);
            ball.gameObject.transform.localPosition = Vector3.up * initialHeightAboveThePaddle;
        }

        void Update()
        {
            //TODO: decouple this from GameManager
            //TODO: create a separate MouseManager to get user input and subscribe to changes in mouseManager here
            if (GameManager.Instance.CurrentProcessState != GameManager.ProcessState.Running)
            {
                return;
            }
            UpdatePosition();
            ReleaseSpawnedBall();
        }

        private void ReleaseSpawnedBall()
        {
            if (Input.GetMouseButton((int)MouseButton.LeftMouse))
            {
                GameState.ReleaseBall();
            }
        }

        private void UpdatePosition()
        {
            var newPosition = GetMouseCoordsOnBottom();
            if (newPosition.HasValue)
            {
                characterController.Move((newPosition.Value - transform.position) / state.MovementLag);
            }
        }

        private Vector3? GetMouseCoordsOnBottom()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            int layerMask = 1 << 8; // layer 8 - BottomWall
            var hits = Physics.RaycastAll(ray, float.PositiveInfinity, layerMask);
            if (hits.Length > 0)
                return hits[0].point;
            return null;
        }

    }
}
