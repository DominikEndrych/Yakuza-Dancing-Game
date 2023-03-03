using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancerController : MonoBehaviour
{
    [Range(0,1)]
    [SerializeField] float _switchProbability;      // Probability of animation switch

    [SerializeField] int _numberOfAnimations;       // Number of aninations in animator

    private Animator _animator;
    private int _currentAnimation = -1;             // Initialize current animation with number that will not be generated
    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        ChangeAnimation();  // Choose random first animation
    }

    public void ChangeAnimation()
    {
        if(Random.Range(0f, 1f) <= _switchProbability)
        {
            int triggerIdx = Random.Range(0, _numberOfAnimations);

            // Change index if generated number was the same
            // Method with while generated a bug so I need to do it this way
            if (triggerIdx == _currentAnimation)
            {
                if (triggerIdx == _numberOfAnimations) triggerIdx = triggerIdx - 1;
                else if (triggerIdx == 0) triggerIdx = 1;
                else triggerIdx = triggerIdx + 1;
            }

            _animator.SetTrigger("Dance" + triggerIdx);             // Trigger new animation
            _currentAnimation = triggerIdx;
            Debug.Log("Animation changed to " + triggerIdx);
        }
    }
}
