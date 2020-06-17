using Assets.Scripts.StateManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers
{
    public class HudController: GameStateAware
    {
        private HudState state;

        public Text LivesLabel;


        public override void Start()
        {
            base.Start();
            
            state = GameState.HudState;

            state.LivesChanged += State_LivesChanged;
            state.ControllerInitialized();
        }

        private void State_LivesChanged(int lives)
        {
            LivesLabel.text = $"Lives: {lives}";
        }
    }
}
