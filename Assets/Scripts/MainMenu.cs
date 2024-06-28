using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static int LastScene = 0;

    public void OnPlayButton()
    {
        LastScene = 1;
        SceneManager.LoadScene(1);
    }

    public void OnBounceButton()
    {
        LastScene = 3;
        SceneManager.LoadScene(3);
    }

    public void OnTryAgainButton()
    {
        SceneManager.LoadScene(LastScene);
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    public void OnMainMenuButton()
    {
        SceneManager.LoadScene(0);
    }
}
