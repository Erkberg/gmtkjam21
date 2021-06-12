using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class InteractableStar : MonoBehaviour
{
    public Animator animator;
    public float maxAnimatorSpeedOffset = 0.2f;

    private void Awake()
    {
        animator.speed += Random.Range(-maxAnimatorSpeedOffset, maxAnimatorSpeedOffset);
    }
}
