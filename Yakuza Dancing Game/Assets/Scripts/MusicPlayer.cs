using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MusicPlayer : MonoBehaviour
{
    public bool IsActive { get => _isActive; }
    public UnityEvent OnMusicEnd;

    [SerializeField] AudioSource _audioSource;

    private bool _isActive;
    private void Awake()
    {
        _audioSource.clip = GameSettings.Instance.GetAudioClip();   // Set audio clip to play
    }

    private void Update()
    {
        // Check for clip ending is something is playing
        if(_isActive)
        {
            if(!_audioSource.isPlaying)
            {
                Debug.Log("Music ended");
                OnMusicEnd.Invoke();        // Invoke ending event
                _isActive = false;
            }
        }
    }

    public void StartMusic()
    {
        _audioSource.Play();
        _isActive = true;
    }
}
