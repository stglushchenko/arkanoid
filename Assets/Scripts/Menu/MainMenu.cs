using Assets.Scripts;
using UnityEngine;
using static Assets.Scripts.GameManager.ProcessState;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animation mainMenuAnimator;
    [SerializeField] private AnimationClip fadeOutAnimation;
    [SerializeField] private AnimationClip fadeInAnimation;


    public Events.EventFadeComplete OnMainMenuFadeComplete;
    void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    private void HandleGameStateChanged(GameManager.ProcessState currentState, GameManager.ProcessState previousState)
    {
        if (previousState == Pregame && currentState == Running)
        {
            FadeOut();
        }

        if (previousState != Pregame && currentState == Pregame)
        {
            FadeIn();
        }
    }

    public void OnFadeOutComplete()
    {
        Debug.LogWarning("FadeOut Complete");
        OnMainMenuFadeComplete.Invoke(true);
    }

    public void OnFadeInComplete()
    {
        OnMainMenuFadeComplete.Invoke(false);
        UIManager.Instance.SetDummyCameraActive(true);
    }

    public void FadeOut()
    {
        UIManager.Instance.SetDummyCameraActive(false);

        mainMenuAnimator.Stop();
        mainMenuAnimator.clip = fadeOutAnimation;
        mainMenuAnimator.Play();
    }

    public void FadeIn()
    {
        mainMenuAnimator.Stop();
        mainMenuAnimator.clip = fadeInAnimation;
        mainMenuAnimator.Play();
    }
}
