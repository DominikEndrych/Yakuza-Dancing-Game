using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancerController : MonoBehaviour
{
    [Range(0,1)]
    [SerializeField] float _switchProbability;      // Probability of animation switch

    [SerializeField] int _numberOfAnimations;       // Number of aninations in animator

    private Animator _animator;
    private int _currentAnimation = 0;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void ChangeAnimation()
    {
        if(Random.Range(0f, 1f) <= _switchProbability)
        {
            int triggerIdx = _currentAnimation;
            while(triggerIdx == _currentAnimation)
            {
                triggerIdx = Random.Range(0, _numberOfAnimations);      // Get random trigger
                _animator.SetTrigger("Dance" + triggerIdx);             // Trigger new animation
                _currentAnimation = triggerIdx;
                Debug.Log("Animation changed to " + triggerIdx);
            }
        }
    }
}
