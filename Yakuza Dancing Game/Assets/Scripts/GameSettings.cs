using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance;

    private AudioClip _audioClip;

    private void Awake()
    {
        // I need only one instance
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetAudioClip(AudioClip newClip)
    {
        _audioClip = newClip;
    }

    public AudioClip GetAudioClip()
    {
        return _audioClip;
    }
}
