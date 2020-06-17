using System;

namespace Assets.Scripts.StateManagement
{
    public class PaddleState
    {
        public float MovementLag = 8f;

        public event Action OnSpawnNewBall;

        public void SpawnNewBall()
        {
            OnSpawnNewBall?.Invoke();
        }

        public void ControllerInitialized()
        {
            SpawnNewBall();
        }
    }
}
