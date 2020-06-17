using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class EndGameMenu : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Text victoryText;
    [SerializeField] private Text defeatText;

    private void Start()
    {
        restartButton.onClick.AddListener(HandleRestartClicked);
        quitButton.onClick.AddListener(HandleQuitClicked);
    }

    private void HandleRestartClicked()
    {
        GameManager.Instance.RestartGame();
    }
    private void HandleQuitClicked()
    {
        GameManager.Instance.QuitGame();
    }

    public void ShowVictoryText(bool victory)
    {
        victoryText.gameObject.SetActive(victory);
        defeatText.gameObject.SetActive(!victory);
    }
}
