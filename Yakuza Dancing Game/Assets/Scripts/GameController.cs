using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public UnityEvent OnGameStart;
    public UnityEvent OnGameplayStart;
    public UnityEvent OnLateGameplayStart;  // Invoked few seconds after OnGameplayStart
    public UnityEvent OnCameraChange;
    public UnityEvent OnGameplayEnd;
    public UnityEvent OnEarlyGameplayEnd;

    [SerializeField] float _lateGameplayWaitTime;
    [SerializeField] AudioSource _musicAudioSource;

    [Header("Cameras")]
    [SerializeField] List<GameplayCamera> _cameras;

    private int _currentCameraIndex = 0;

    private void Start()
    {
        OnGameStart.Invoke();
        
        // How long to wait before ending cutscene can start
        float endingWaitTime = _musicAudioSource.clip.length - 5.87f;
        float earlyEndWaitTime = _musicAudioSource.clip.length - (6.6f+ 6.7f);
        StartCoroutine(WaitForEnding(endingWaitTime));
        StartCoroutine(WaitForEarlyEnding(earlyEndWaitTime));
    }

    public void StartGameplay()
    {
        OnGameplayStart.Invoke();
        StartCoroutine(StartLateGameplay());
        _cameras[_currentCameraIndex].StartCamera();    // Start first camera
    }

    public void EndGameplay()
    {
        OnGameplayEnd.Invoke();
    }

    private IEnumerator StartLateGameplay()
    {
        yield return new WaitForSeconds(_lateGameplayWaitTime);

        OnLateGameplayStart.Invoke();
    }

    // Coroutine to wait until the song is almost over
    // This way I can start ending cutscene right at the end of the song
    private IEnumerator WaitForEnding(float seconds)
    {
        yield return new WaitForSeconds(seconds);       // Wait for certain time
        EndGameplay();                                  // End the gameplay
    }

    // Coroutine to wait until the gameplay is almost over so there are no bugs
    // This is the way to end mainly fever so it does not interfere with ending cutscene
    private IEnumerator WaitForEarlyEnding(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        OnEarlyGameplayEnd.Invoke();
    }

    #region Cameras
        public void SwitchCamera()
    {
        //Debug.Log("Start camera switch funciton");
        int index = Random.Range(0, _cameras.Count);

        // Change index if generated number was the same
        // Method with while generated a bug so I need to do it this way
        if (index == _currentCameraIndex)
        {
            if (index == _cameras.Count-1) index = index - 1;
            else if (index == 0) index = 1;
            else index = index + 1;
        }

        _cameras[index].StartCamera();                  // Start new camera
        //Debug.Log("Start new camera");
        _cameras[_currentCameraIndex].HideCamera();     // Hide current camer
        //Debug.Log("Hide");
        _currentCameraIndex = index;                    // Change current index of active camera

        OnCameraChange.Invoke();
    }

    #endregion
}
