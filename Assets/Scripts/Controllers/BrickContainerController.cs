using Assets.Scripts.StateManagement;
using UnityEngine;

namespace Assets.Scripts.Controllers {
    public class BrickContainerController : GameStateAware
    {
        [SerializeField]
        private GameObject brickPrefab;
        private BricksContainerState state;
        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
            state = GameState.BricksContainerState;
            GenerateBricks();
        }

        private void GenerateBricks()
        {
            for (var i = 0; i < state.RowsX; i++)
            {
                for (var j = 0; j < state.RowsY; j++)
                {
                    for (var k = 0; k < state.RowsZ; k++)
                    {
                        var brick = Instantiate(brickPrefab);
                        brick.transform.SetParent(this.transform);
                        brick.transform.localPosition = new Vector3(i, -(float)j/3 , k );
                    }
                }
            }

        }
    }
}
