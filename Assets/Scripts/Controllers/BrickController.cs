using Assets.Scripts.StateManagement;
using UnityEngine;

namespace Assets.Scripts.Controllers {
    public class BrickController : GameStateAware
    {
        private BricksContainerState containerState;
        public override void Start()
        {
            base.Start();
            containerState = GameState.BricksContainerState;
            containerState.BrickAdded();
        }

        void OnCollisionExit(Collision _)
        {
            containerState.BrickDestroyed();
            Destroy(this.gameObject);
        }
    }
}
