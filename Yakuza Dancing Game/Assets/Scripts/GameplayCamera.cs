using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class GameplayCamera : MonoBehaviour
{
    public UnityEvent OnCameraFinished;     // Event when camera finished its cycle

    private CinemachineVirtualCamera _vcam;
    private Animator _animator;

    private void Awake()
    {
        _vcam = GetComponent<CinemachineVirtualCamera>();
        _animator = GetComponent<Animator>();
    }

    // Invoke finishing event
    public void CameraFinished()
    {
        OnCameraFinished.Invoke();
    }

    public void HideCamera()
    {
        _vcam.Priority = 0;
    }

    public void StartCamera()
    {
        _animator.SetTrigger("Start Movement");
        _vcam.Priority = 50;
    }
}
