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

    [SerializeField] float _lateGameplayWaitTime;

    [Header("Cameras")]
    [SerializeField] List<GameplayCamera> _cameras;

    private int _currentCameraIndex = 0;

    private void Start()
    {
        OnGameStart.Invoke();
    }

    public void StartGameplay()
    {
        OnGameplayStart.Invoke();
        StartCoroutine(StartLateGameplay());
        _cameras[_currentCameraIndex].StartCamera();    // Start first camera
    }

    private IEnumerator StartLateGameplay()
    {
        yield return new WaitForSeconds(_lateGameplayWaitTime);

        OnLateGameplayStart.Invoke();
    }

    #region Cameras
    public void SwitchCamera()
    {
        int index = _currentCameraIndex;

        // Choose new camera
        while(index == _currentCameraIndex)
        {
            index = Random.Range(0, _cameras.Count);
        }

        _cameras[_currentCameraIndex].HideCamera();     // Hide current camera
        _cameras[index].StartCamera();                  // Start new camera
        _currentCameraIndex = index;                    // Change current index of active camera

        OnCameraChange.Invoke();
    }

    #endregion
}
