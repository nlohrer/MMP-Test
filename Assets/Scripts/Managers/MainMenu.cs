using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static int LastScene = 0; // letzte szene merken

    public void OnPlayButton() // wechsel in Deep Diver scene (main game)
    {
        LastScene = 1;
        SceneManager.LoadScene(1);
    }

    public void OnBounceButton() // wechsel in Bounce gamemode
    {
        LastScene = 3;
        SceneManager.LoadScene(3);
    }

    public void OnTryAgainButton() // wechsel in die letzte scene (Bounce mode oder main mode je nachdem was davor gespielt wurde)
    {
        SceneManager.LoadScene(LastScene);
    }

    public void OnQuitButton() // application quit für builds
    {
        Application.Quit();
    }

    public void OnMainMenuButton() // von der GameOver scene zurück in die MainMenu scene
    {
        SceneManager.LoadScene(0);
    }
}
