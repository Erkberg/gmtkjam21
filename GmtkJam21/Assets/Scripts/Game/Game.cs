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
    public GameMode gameMode = GameMode.None;

    public GameConfig config;
    public GameBackground background;
    public StarCreator starCreator;
    public IngameCamera cam;
    public GroundManager groundManager;
    public GroundPlayer groundPlayer;
    public GameInput input;
    public GameMenus menus;
    public GameAudio audio;

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

    public void PauseGame()
    {
        
    }

    public void ResumeGame()
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
        audio.OnSwitchToGround();
    }
    
    public void SwitchToSky()
    {
        ingameState = IngameState.Sky;
        cam.SwitchToSky();
        audio.OnSwitchToSky();
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
        None,
        Story,
        Sandbox
    }
}
