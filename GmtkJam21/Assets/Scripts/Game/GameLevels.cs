using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevels : MonoBehaviour
{
    public List<LevelData> levels;
    public int currentLevel = 0;

    private void Update()
    {
        if (Game.inst.gameMode == Game.GameMode.Story && Game.inst.input.GetCheatButtonDown())
        {
            OnLevelFinished();
        }
    }

    public void StartFromFirstLevel()
    {
        currentLevel = 0;
        StartCurrentLevel();
    }
    
    public void OnLevelFinished()
    {
        Game.inst.interaction.ResetStarLines();
        currentLevel++;

        if (currentLevel >= levels.Count)
        {
            Debug.Log("game finished!");
        }
        else
        {
            StartCurrentLevel();
        }
    }
    
    public void StartCurrentLevel()
    {
        LevelData currentLevelData = levels[currentLevel];
        Game.inst.narration.ShowText(currentLevelData.narrationStart);
        Game.inst.starCreator.CreateLevelStars(currentLevelData);
        Game.inst.groundManager.CreateObstacles(currentLevelData);
        Game.inst.starLinesAmountUI.SetText(0, GetCurrentMaxLines());
    }

    public int GetCurrentMaxLines()
    {
        return levels[currentLevel].maxStarLines;
    }
}
