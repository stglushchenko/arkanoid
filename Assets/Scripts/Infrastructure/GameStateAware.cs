using UnityEngine;
using Assets.Scripts;
using Assets.Scripts.StateManagement;

public class GameStateAware : MonoBehaviour
{
    protected GameState GameState { get; private set; }

    public virtual void Start()
    {
        GameState = GameManager.Instance.GameState;
    }

}
