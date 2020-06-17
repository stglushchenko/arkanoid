using System.Collections.Generic;

namespace Assets.Scripts.StateManagement
{
    public class GameState
    {
        public PaddleState PaddleState { get; }
        public IList<BallState> BallStates { get; }
        public HudState HudState { get; }
        public BricksContainerState BricksContainerState { get;}

        public int Lives { get; private set; }

        public GameState()
        {
            PaddleState = new PaddleState();
            BallStates = new List<BallState>();
            Lives = 5;
            HudState = new HudState {LivesCount = Lives};
            BricksContainerState = new BricksContainerState();
            BricksContainerState.NoMoreBricks += BricksContainerState_NoMoreBricks;
        }

        private void BricksContainerState_NoMoreBricks()
        {
            GameManager.Instance.Victory();
        }

        public BallState BallSpawned()
        {
            var ballState = new BallState();

            BallStates.Add(ballState);

            return ballState;
        }

        public void BallDropped(BallState state)
        {
            BallStates.Remove(state);

            HudState.SetLivesChanged(--Lives);

            if (Lives > 0)
            {
                PaddleState.SpawnNewBall();
            }
            else
            {
                GameManager.Instance.Defeat();
            }
        }

        public void ReleaseBall()
        {
            foreach (var ballState in BallStates)
            {
                ballState.ReleaseFromPaddle();
            }
        }
    }

}
