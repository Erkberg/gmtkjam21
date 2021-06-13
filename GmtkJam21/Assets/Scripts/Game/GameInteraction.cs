using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private bool CanCreateNewStarLine()
    {
        if (Game.inst.gameMode != Game.GameMode.Story)
        {
            return true;
        }
        else
        {
            return finishedStarLines.Count < Game.inst.levels.GetCurrentMaxLines();
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
                        
                        if(Game.inst.gameMode == Game.GameMode.Story)
                            CheckLineOverlap();
                        
                        finishedStarLines.Add(currentStarLine);
                        Game.inst.starLinesAmountUI.SetText(finishedStarLines.Count, Game.inst.levels.GetCurrentMaxLines());
                        Game.inst.groundManager.OnLineCreated(currentStarLine);
                        currentStarLine = null;

                        TryStartNewStarLine();
                    }
                }
                else
                {
                    // regular new line at focused star
                    TryStartNewStarLine();
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

    private void TryStartNewStarLine()
    {
        if (CanCreateNewStarLine())
        {
            currentStarLine = Instantiate(starLinePrefab, starLinesHolder);
            currentStarLine.SetStartStar(focusedStar);
            Game.inst.audio.OnAddLine();
        }
        else
        {
            CancelStarLine();
            Game.inst.starLinesAmountUI.TriggerPopup();
        }
    }

    private void CheckLineOverlap()
    {
        foreach (StarLine otherStarLine in finishedStarLines.ToList())
        {
            if (otherStarLine != null)
            {
                if (LineSegmentsIntersect(currentStarLine.GetOffsetStartPosition2D(), currentStarLine.GetOffsetEndPosition2D(),
                    otherStarLine.GetOffsetStartPosition2D(), otherStarLine.GetOffsetEndPosition2D()))
                {
                    RemoveStarLine(otherStarLine);
                }
            }
        }
    }
    
    public static bool LineSegmentsIntersect(Vector2 lineOneA, Vector2 lineOneB, Vector2 lineTwoA, Vector2 lineTwoB) { return (((lineTwoB.y - lineOneA.y) * (lineTwoA.x - lineOneA.x) > (lineTwoA.y - lineOneA.y) * (lineTwoB.x - lineOneA.x)) != ((lineTwoB.y - lineOneB.y) * (lineTwoA.x - lineOneB.x) > (lineTwoA.y - lineOneB.y) * (lineTwoB.x - lineOneB.x)) && ((lineTwoA.y - lineOneA.y) * (lineOneB.x - lineOneA.x) > (lineOneB.y - lineOneA.y) * (lineTwoA.x - lineOneA.x)) != ((lineTwoB.y - lineOneA.y) * (lineOneB.x - lineOneA.x) > (lineOneB.y - lineOneA.y) * (lineTwoB.x - lineOneA.x))); }

    private void CancelStarLine()
    {
        if (currentStarLine)
        {
            Game.inst.audio.OnRemoveLine();
            currentStarLine.Kill();
            currentStarLine = null;
        }
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
                RemoveStarLine(hitStarLine);
            }
        }
        else
        {
            OnNoStarFocused();
        }
    }

    private void RemoveStarLine(StarLine line)
    {
        Game.inst.groundManager.OnLineDestroyed(line);
        Game.inst.audio.OnRemoveLine();
        line.Kill();

        StartCoroutine(UpdateListAtEndOfFrame());
    }

    private IEnumerator UpdateListAtEndOfFrame()
    {
        yield return null;
        finishedStarLines = finishedStarLines.Where(item => item != null).ToList();
        Game.inst.starLinesAmountUI.SetText(finishedStarLines.Count, Game.inst.levels.GetCurrentMaxLines());
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
                Game.inst.audio.OnUnfocusStar();
            }
            else
            {
                if (!focusedStar)
                {
                    Game.inst.audio.OnFocusStar();
                    focusedStar = hitStar;
                    focusedStar.SetFocused(true);
                }
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
            Game.inst.audio.OnUnfocusStar();
            focusedStar.SetFocused(false);
            focusedStar = null;
        }
    }

    public void ResetStarLines()
    {
        foreach (StarLine starLine in finishedStarLines.ToList())
        {
            starLine.Kill();
        }
        
        finishedStarLines.Clear();
    }
}
