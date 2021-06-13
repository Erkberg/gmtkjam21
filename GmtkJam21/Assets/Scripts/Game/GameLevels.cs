using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevels : MonoBehaviour
{
    public List<LevelData> levels;
    public int currentLevel = 0;

    private const string EndText = "Reached end of current story content...\n Congratulations and thanks for playing!\n(try Sandbox mode for a relaxed, creative experience)";

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
            Game.inst.narration.ShowText(EndText);
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
