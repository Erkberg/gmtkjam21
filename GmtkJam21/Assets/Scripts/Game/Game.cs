using System;
using System.Collections;
using System.Collections.Generic;
using ErksUnityLibrary;
using UnityEngine;
using Random = UnityEngine.Random;

public class Game : MonoBehaviour
{
    public static Game inst;

    public IngameState ingameState = IngameState.Sky;
    public GameMode gameMode = GameMode.Story;

    public GameConfig config;
    public GameBackground background;
    public StarCreator starCreator;
    public IngameCamera cam;
    public GroundManager groundManager;
    public GroundPlayer groundPlayer;
    public GameInput input;
    public GameMenus menus;

    private void Awake()
    {
        inst = this;
        menus.OnRandomizeSeedButtonPressed();
        starCreator.CreateRandomStars(config.starsAmount, config. seed);
    }

    public void OnStarsCreated()
    {
        groundPlayer.Spawn(groundManager.starIslands.GetRandomItem().transform.position);
    }

    public void StartGame()
    {
        
    }

    public void OnConfigChanged()
    {
        starCreator.RecreateRandomStars(config.starsAmount, config. seed);
        background.ResetBackgrounds();
    }

    public void SwitchToGround()
    {
        ingameState = IngameState.Ground;
        groundManager.OnSwitchToGround();
        cam.SwitchToGround();
    }
    
    public void SwitchToSky()
    {
        ingameState = IngameState.Sky;
        cam.SwitchToSky();
    }

    public bool IsIngame()
    {
        return menus.menuState == GameMenus.MenuState.MenuClosed;
    }

    public enum IngameState
    {
        Sky,
        Ground,
        Transition
    }

    public enum GameMode
    {
        Story,
        Sandbox
    }
}
