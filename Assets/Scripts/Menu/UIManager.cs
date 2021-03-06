﻿using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.StateManagement;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private MainMenu mainMenu;

    [SerializeField] private Camera dummyCamera;

    [SerializeField] private PauseMenu pauseMenu;

    [SerializeField] private EndGameMenu endGameMenu;

    public Events.EventFadeComplete OnMainMenuFadeComplete;

    void Start()
    {
        mainMenu.OnMainMenuFadeComplete.AddListener(HandleMainMenuFadeComplete);
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    private void HandleMainMenuFadeComplete(bool isFadeOut)
    {
        OnMainMenuFadeComplete.Invoke(isFadeOut);
    }

    private void HandleGameStateChanged(GameManager.ProcessState currentState, GameManager.ProcessState previousState)
    {
        pauseMenu.gameObject.SetActive(currentState == GameManager.ProcessState.Paused);

        endGameMenu.gameObject.SetActive(
            currentState == GameManager.ProcessState.Defeat 
            || currentState == GameManager.ProcessState.Victory);
        endGameMenu.ShowVictoryText(currentState == GameManager.ProcessState.Victory);
    }

    void Update()
    {
        if (GameManager.Instance.CurrentProcessState != GameManager.ProcessState.Pregame) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.StartGame();
        }
    }

    public void SetDummyCameraActive(bool active)
    {
        dummyCamera.gameObject.SetActive(active);
    }
}
