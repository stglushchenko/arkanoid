using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.StateManagement
{
    public class BricksContainerState
    {
        public event Action NoMoreBricks;
        public readonly int RowsX = 4;
        public readonly int RowsY = 2;
        public readonly int RowsZ = 4;

        private int currentBricksNumber = 0;

        public void BrickAdded()
        {
            currentBricksNumber++;
        }

        public void BrickDestroyed()
        {
            currentBricksNumber--;
            if (currentBricksNumber == 0)
            {
                NoMoreBricks?.Invoke();
            }
        }
    }
}
