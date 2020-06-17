using System;
using Assets.Scripts;
using UnityEngine.Events;

public class Events
{
    [Serializable] public class EventFadeComplete : UnityEvent<bool> { }
    [Serializable] public class EventGameProcessState : UnityEvent<GameManager.ProcessState, GameManager.ProcessState> { }

}
