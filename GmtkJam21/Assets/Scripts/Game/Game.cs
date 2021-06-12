using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Game : MonoBehaviour
{
    public static Game inst;

    public GameState gameState;
    public GameConfig config;
    public GameBackground background;
    public StarCreator starCreator;
    public StartMenu startMenu;

    private void Awake()
    {
        inst = this;
        startMenu.OnRandomizeSeedButtonPressed();
        starCreator.CreateRandomStars(config.starsAmount, config. seed);
    }

    public void OnConfigChanged()
    {
        starCreator.RecreateRandomStars(config.starsAmount, config. seed);
        background.ResetBackgrounds();
    }

    public void OnGameStarted()
    {
        gameState = GameState.Ingame;
    }

    public enum GameState
    {
        StartMenu,
        Ingame
    }
}
