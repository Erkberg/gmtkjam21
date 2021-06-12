using System;
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

    public List<StarLine> finishedStarLines = new List<StarLine>();

    void Update()
    {
        if (Game.inst.IsIngame())
        {
            switch (Game.inst.ingameState)
            {
                case Game.IngameState.Sky:
                    CheckFocusedStar();
                    CheckFocusedStarLine();
        
                    CheckMouseButtons();
                    CheckStarLineEndPosition();
        
                    CheckMouseWheel();
                    break;
            
                case Game.IngameState.Ground:
                    CheckMouseWheel();
                    break;
            
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private void CheckMouseWheel()
    {
        float scrollValue = Mouse.current.scroll.ReadValue().y;
        if (scrollValue < 0f && Game.inst.ingameState == Game.IngameState.Sky)
        {
            Game.inst.SwitchToGround();
        }
        else if (scrollValue > 0f && Game.inst.ingameState == Game.IngameState.Ground)
        {
            Game.inst.SwitchToSky();
        }
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
                        finishedStarLines.Add(currentStarLine);
                        Game.inst.groundManager.OnLineCreated(currentStarLine);
                        
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
        if (finishedStarLines.Contains(currentStarLine))
        {
            finishedStarLines.Remove(currentStarLine);
        }

        currentStarLine.Kill();
        currentStarLine = null;
    }
    
    private void CheckFocusedStarLine()
    {
        RaycastHit hit;
        Ray ray = mainCam.ScreenPointToRay(Mouse.current.position.ReadValue());
        
        if (Physics.Raycast(ray, out hit)) 
        {
            StarLine hitStarLine = hit.transform.GetComponent<StarLine>();
            if (hitStarLine && !currentStarLine && Mouse.current.rightButton.wasPressedThisFrame)
            {
                Game.inst.groundManager.OnLineDestroyed(hitStarLine);
                hitStarLine.Kill();
            }
        }
        else
        {
            OnNoStarFocused();
        }
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
