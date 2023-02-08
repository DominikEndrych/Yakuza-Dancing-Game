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
    private void Update()
    {
        // Check for clip ending is something is playing
        if(_isActive)
        {
            if(!_audioSource.isPlaying)
            {
                OnMusicEnd.Invoke();        // Invoke ending event
            }
        }
    }

    public void StartMusic()
    {
        _audioSource.Play();
    }
}
