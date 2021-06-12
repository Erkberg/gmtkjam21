using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private Controls controls;
        
    public void Awake()
    {
        controls = new Controls();
        controls.Default.Enable();
    }
    
    public void SetInputEnabled(bool enabled)
    {
        if (enabled)
        {
            controls.Default.Enable();
        }
        else
        {
            controls.Default.Disable();
        }
    }
        
    #region mouse
    //--------------------------------------------------------------------------------------------------------------
    public Vector2 GetMousePosition()
    {
        return Mouse.current.position.ReadValue();
    }
        
    public bool GetLeftMouseButtonDown()
    {
        return Mouse.current.leftButton.wasPressedThisFrame;
    }
        
    public bool GetRightMouseButtonDown()
    {
        return Mouse.current.rightButton.wasPressedThisFrame;
    }
    //--------------------------------------------------------------------------------------------------------------
    #endregion mouse

    #region movement axes
    //--------------------------------------------------------------------------------------------------------------
    public float GetHorizontalMovement()
    {
        return controls.Default.HorizontalMovement.ReadValue<float>();
    }
        
    public float GetVerticalMovement()
    {
        return controls.Default.VerticalMovement.ReadValue<float>();
    }

    public Vector2 GetMovement()
    {
        return new Vector2(GetHorizontalMovement(), GetVerticalMovement());
    }
    //--------------------------------------------------------------------------------------------------------------
    #endregion movement axes
    
    #region buttons
    public bool GetResetPlayerButtonDown()
    {
        return GetButtonDown(controls.Default.ResetPlayer);
    }
    #endregion buttons
    
    #region buttons general
    //--------------------------------------------------------------------------------------------------------------
    private bool GetButtonDown(InputAction action)
    {
        return action.triggered && action.ReadValue<float>() > 0;
    }
        
    private bool GetButton(InputAction action)
    {
        return action.ReadValue<float>() > 0;
    }
        
    private bool GetButtonUp(InputAction action)
    {
        return action.triggered && action.ReadValue<float>() == 0;
    }
    //--------------------------------------------------------------------------------------------------------------
    #endregion buttons general
}
