using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInteraction : MonoBehaviour
{
    public StarLine starLinePrefab;
    public Transform starLinesHolder;
    public Camera mainCam;
    public InteractableStar focusedStar;
    public StarLine currentStarLine;

    void Update()
    {
        CheckFocusedStar();
        CheckMouseButtons();
        CheckStarLineEndPosition();
        CheckMouseWheel();
    }

    private void CheckMouseWheel()
    {
        
    }

    private void CheckStarLineEndPosition()
    {
        if (currentStarLine)
        {
            Ray ray = mainCam.ScreenPointToRay(Mouse.current.position.ReadValue());
            currentStarLine.SetEndPosition(ray.GetPoint(Game.inst.starCreator.positionY));
        }
    }

    private void CheckMouseButtons()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (focusedStar)
            {
                if (currentStarLine)
                {
                    if (focusedStar == currentStarLine.endStar)
                    {
                        // cancel line if the same as start star
                        CancelStarLine();
                    }
                    else
                    {
                        // end line at end star and immediately start new line there
                        currentStarLine.SetEndStar(focusedStar);
                        currentStarLine = Instantiate(starLinePrefab, starLinesHolder);
                        currentStarLine.SetStartStar(focusedStar);
                    }
                }
                else
                {
                    // regular new line at focused star
                    currentStarLine = Instantiate(starLinePrefab, starLinesHolder);
                    currentStarLine.SetStartStar(focusedStar);
                }
            }
        }
        else if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            if (currentStarLine)
            {
                CancelStarLine();
            }
        }
    }

    private void CancelStarLine()
    {
        Destroy(currentStarLine.gameObject);
        currentStarLine = null;
    }

    private void CheckFocusedStar()
    {
        RaycastHit hit;
        Ray ray = mainCam.ScreenPointToRay(Mouse.current.position.ReadValue());
        
        if (Physics.Raycast(ray, out hit)) 
        {
            InteractableStar hitStar = hit.transform.GetComponent<InteractableStar>();
            HandleStarHit(hitStar);
        }
        else
        {
            OnNoStarFocused();
        }
    }

    private void HandleStarHit(InteractableStar hitStar)
    {
        if (hitStar)
        {
            if (focusedStar && focusedStar != hitStar)
            {
                focusedStar.SetFocused(false);
            }
            else
            {
                focusedStar = hitStar;
                focusedStar.SetFocused(true);
            }  
        }
        else
        {
            OnNoStarFocused();
        }
    }

    private void OnNoStarFocused()
    {
        if (focusedStar)
        {
            focusedStar.SetFocused(false);
            focusedStar = null;
        }
    }
}
