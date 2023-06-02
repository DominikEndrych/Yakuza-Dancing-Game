using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");      // Load main gameplay scene
    }

    public void SelectSong(AudioClip newSong)
    {
        GameSettings.Instance.SetAudioClip(newSong);
    }
}
