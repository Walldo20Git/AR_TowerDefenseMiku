using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject gameplayHUD;      // GameplayHUD_Root
    [SerializeField] GameObject guidancePanel;    // AR_GuidancePanel

    void Start()
    {
        mainMenuPanel.SetActive(true);
        gameplayHUD.SetActive(false);
        guidancePanel.SetActive(false);
    }

    public void Play()
    {
        mainMenuPanel.SetActive(false);
        gameplayHUD.SetActive(true);
        guidancePanel.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}