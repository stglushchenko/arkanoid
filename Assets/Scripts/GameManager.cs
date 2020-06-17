using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.StateManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts {

    public class GameManager : Singleton<GameManager>
    {
        public enum ProcessState
        {
            Pregame,
            Running,
            Paused,
            Defeat,
            Victory
        }
        public ProcessState CurrentProcessState { get; private set; } = ProcessState.Pregame;
        public GameState GameState { get; private set; }

        public Events.EventGameProcessState OnGameStateChanged;

        private string currentLevelName = string.Empty;

        [SerializeField]
        private GameObject[] systemPrefabs;

        private IList<GameObject> instancedSystemPrefabs;
        private IList<AsyncOperation> loadOperations = new List<AsyncOperation>();

        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(this);

            InstantiateSystemPrefabs();

            UIManager.Instance.OnMainMenuFadeComplete.AddListener(HandleMainMenuFadeComplete);
        }

        private void HandleMainMenuFadeComplete(bool isFadeOut)
        {
            if (!isFadeOut)
            {
                UnloadLevel(currentLevelName);
            }
        }

        private void InstantiateSystemPrefabs()
        {
            instancedSystemPrefabs = systemPrefabs?.Select(Instantiate).ToList();
        }

        void Update()
        {
            if (CurrentProcessState == GameManager.ProcessState.Pregame) return;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }

        public void LoadLevel(string levelName)
        {
            var ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
            if (ao == null)
            {
                Debug.LogError($"[{nameof(GameManager)}] Failed to load level {levelName}");
                return;
            }

            ao.completed += OnLoad;
            loadOperations.Add(ao);
            currentLevelName = levelName;
        }

        public void UpdateProcessState(ProcessState processState)
        {
            var previousGameState = CurrentProcessState;
            CurrentProcessState = processState;
            switch (CurrentProcessState)
            {
                case ProcessState.Pregame:
                case ProcessState.Running:
                case ProcessState.Victory:
                case ProcessState.Defeat:
                    Time.timeScale = 1f;
                    break;
                case ProcessState.Paused:
                    Time.timeScale = 0f;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            OnGameStateChanged.Invoke(CurrentProcessState, previousGameState);
        }

        private void OnLoad(AsyncOperation ao)
        {
            if (loadOperations.Contains(ao))
            {
                loadOperations.Remove(ao);

                if (!loadOperations.Any())
                {
                    UpdateProcessState(ProcessState.Running);
                }
            }

            Debug.Log("Level loaded");
        }

        public void UnloadLevel(string levelName)
        {
            var ao = SceneManager.UnloadSceneAsync(levelName);
            if (ao == null)
            {
                Debug.LogError($"[{nameof(GameManager)}] Failed to load level {levelName}");
                return;
            }
            
            ao.completed += OnUnload;
        }

        private void OnUnload(AsyncOperation obj)
        {
            Debug.Log("Level unloaded");
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            foreach (var instancedSystemPrefab in instancedSystemPrefabs)
            {
                Destroy(instancedSystemPrefab);
            }
            instancedSystemPrefabs.Clear();
        }

        public void StartGame()
        {
            GameState = new GameState();
            LoadLevel(LevelNames.Main);
        }

        public void TogglePause()
        {
            UpdateProcessState(CurrentProcessState == ProcessState.Running ? ProcessState.Paused: ProcessState.Running);
        }

        public void RestartGame()
        {
            UpdateProcessState(ProcessState.Pregame);
        }

        public void Victory()
        {
            UpdateProcessState(ProcessState.Victory);
        }

        public void Defeat()
        {
            UpdateProcessState(ProcessState.Defeat);
        }

        public void QuitGame()
        {
            // auto-saving and cleanup
            
            Application.Quit();
        }
    }
}
