using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");      // Load main gameplay scene
    }

    public void ExitGame()
    {
        Application.Quit();     // Quit the game
        Debug.Log("Game quit");
    }

    public void SelectSong(AudioClip newSong)
    {
        GameSettings.Instance.SetAudioClip(newSong);
    }
}
