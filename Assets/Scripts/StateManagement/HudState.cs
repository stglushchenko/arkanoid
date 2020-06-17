using System;
namespace Assets.Scripts.StateManagement
{
    public class HudState
    {
        public int LivesCount;

        public event Action<int> LivesChanged;
        public void SetLivesChanged(int lives)
        {
            LivesCount = lives;
            LivesChanged?.Invoke(lives);
        }

        public void ControllerInitialized()
        {
            SetLivesChanged(LivesCount);
        }
    }
}
