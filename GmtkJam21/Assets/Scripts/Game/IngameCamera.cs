using System;
using System.Collections;
using System.Collections.Generic;
using ErksUnityLibrary;
using UnityEngine;

public class IngameCamera : MonoBehaviour
{
    public float rotationSpeed = 8f;
    public float skyAngle = -90f;
    public float groundAngle = 90f;

    private float currentTargetAngle;

    private void Awake()
    {
        currentTargetAngle = skyAngle;
    }

    void Update()
    {
        if (!transform.eulerAngles.x.IsApproxEqual(currentTargetAngle))
        {
            float angle = Mathf.LerpAngle(transform.eulerAngles.x, currentTargetAngle, rotationSpeed * Time.deltaTime);
            transform.SetRotationX(angle);
        }
    }
    
    public void SwitchToGround()
    {
        currentTargetAngle = groundAngle;
    }

    public void SwitchToSky()
    {
        currentTargetAngle = skyAngle;
    }
}
