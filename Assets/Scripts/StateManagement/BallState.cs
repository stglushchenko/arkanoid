using System;
namespace Assets.Scripts.StateManagement
{
    public class BallState
    {
        public float Speed = 5f;
        private bool isReleased;
        public event Action Release;

        public void Capture()
        {
            isReleased = false;
        }

        public void ReleaseFromPaddle()
        {
            if (!isReleased)
            {
                Release?.Invoke();
                isReleased = true;
            }
        }
    }
}
