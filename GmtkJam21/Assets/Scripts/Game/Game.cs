using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Game : MonoBehaviour
{
    public static Game inst;

    public MenuState menuState = MenuState.StartMenu;
    public IngameState ingameState = IngameState.Sky;

    public GameConfig config;
    public GameBackground background;
    public StarCreator starCreator;
    public StartMenu startMenu;
    public IngameCamera cam;

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
        menuState = MenuState.MenuClosed;
    }

    public void SwitchToGround()
    {
        ingameState = IngameState.Ground;
        cam.SwitchToGround();
    }
    
    public void SwitchToSky()
    {
        ingameState = IngameState.Sky;
        cam.SwitchToSky();
    }
    
    public void OnOpenedMenu()
    {
        menuState = MenuState.StartMenu;
    }

    public bool IsIngame()
    {
        return menuState == MenuState.MenuClosed;
    }

    public enum MenuState
    {
        MenuClosed,
        StartMenu
    }

    public enum IngameState
    {
        Sky,
        Ground,
        Transition
    }
}
