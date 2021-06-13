using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevels : MonoBehaviour
{
    public List<LevelData> levels;
    public int currentLevel = 0;

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
        Game.inst.narration.ShowText(levels[currentLevel].narrationStart);
        Game.inst.starCreator.CreateLevelStars(levels[currentLevel]);
    }
}
