using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class InteractableStar : MonoBehaviour
{
    public int id;
    public Animator animator;
    public float maxAnimatorSpeedOffset = 0.2f;

    private bool isFocused = false;

    private const string FocusedAnimBool = "focused";

    private void Awake()
    {
        animator.speed += Random.Range(-maxAnimatorSpeedOffset, maxAnimatorSpeedOffset);
    }

    public void InitWithId(int id)
    {
        this.id = id;
        name += id;
    }

    public void SetFocused(bool focused)
    {
        isFocused = focused;
        animator.SetBool(FocusedAnimBool, focused);
    }
    
    private void OnMouseDown()
    {
        Debug.Log($"mouse down on {name}");
    }

    private void OnMouseEnter()
    {
        Debug.Log($"mouse enter {name}");
    }

    private void OnMouseExit()
    {
        Debug.Log($"mouse exit {name}");
    }
}
