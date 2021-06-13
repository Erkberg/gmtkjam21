using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameMenus : MonoBehaviour
{
    public MenuState menuState = MenuState.StartMenu;

    public GameObject mainMenu;
    public GameObject sandboxMenu;
    public GameObject tutorialMenu;
    public GameObject optionsMenu;

    public GameObject mainMenuSubtitle;
    public GameObject mainMenuContinueButton;
    
    public GameObject menusHolder;
    public TextMeshProUGUI starsAmountText;
    public TMP_InputField seedInputField;

    public TextMeshProUGUI audioVolumeText;

    private const string StarsAmountText = "Stars amount: ";
    private const string AudioVolumeText = "Audio volume: ";

    private void Update()
    {
        CheckMenuButton();
    }

    #region navigation
    private void CheckMenuButton()
    {
        if (Game.inst.input.GetMenuButtonDown())
        {
            if (Game.inst.gameMode != Game.GameMode.None && menuState == MenuState.MenuClosed)
            {
                mainMenuSubtitle.SetActive(false);
                mainMenuContinueButton.SetActive(true);
                menuState = MenuState.StartMenu;
                Game.inst.PauseGame();
                menusHolder.SetActive(true);
                OnBackButtonPressed();
            }
            else if (Game.inst.gameMode != Game.GameMode.None && menuState == MenuState.StartMenu)
            {
                OnContinueButtonPressed();
            }
            else if(menuState != MenuState.MenuClosed && menuState != MenuState.StartMenu)
            {
                OnBackButtonPressed();
            }
        }
    }
    
    public void OnContinueButtonPressed()
    {
        menuState = MenuState.MenuClosed;
        menusHolder.SetActive(false);
        Game.inst.ResumeGame();
    }
    
    public void OnStoryButtonPressed()
    {
        menuState = MenuState.MenuClosed;
        Game.inst.gameMode = Game.GameMode.Story;
        menusHolder.SetActive(false);
        Game.inst.StartGame();
    }
    
    public void OnSandboxMenuButtonPressed()
    {
        mainMenuSubtitle.SetActive(true);
        mainMenuContinueButton.SetActive(false);
        Game.inst.gameMode = Game.GameMode.Sandbox;
        menuState = MenuState.SandboxMenu;
        mainMenu.SetActive(false);
        sandboxMenu.SetActive(true);
        Game.inst.starLinesAmountUI.SetInfinite();
        TriggerReload();
    }

    public void OnSandboxStartButtonPressed()
    {
        menuState = MenuState.MenuClosed;
        menusHolder.SetActive(false);
        Game.inst.StartGame();
    }

    public void OnTutorialButtonPressed()
    {
        menuState = MenuState.TutorialMenu;
        mainMenu.SetActive(false);
        tutorialMenu.SetActive(true);
    }

    public void OnOptionsButtonPressed()
    {
        menuState = MenuState.OptionsMenu;
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void OnBackButtonPressed()
    {
        menuState = MenuState.StartMenu;
        mainMenu.SetActive(true);
        sandboxMenu.SetActive(false);
        tutorialMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }

    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }
    
    public enum MenuState
    {
        MenuClosed,
        StartMenu,
        SandboxMenu,
        TutorialMenu,
        OptionsMenu
    }
    #endregion navigation
    
    #region options menu
    public void OnAudioVolumeChanged(float newAmount)
    {
        audioVolumeText.text = AudioVolumeText + newAmount.ToString("F");
        AudioListener.volume = newAmount;
    }

    public void OnToggleInvertY(bool value)
    {
        Game.inst.groundManager.invertedY = value;
    }
    #endregion options menu
    
    #region sandbox menu
    public void OnStarsAmountChanged(float newAmount)
    {
        starsAmountText.text = StarsAmountText + newAmount;
        Game.inst.config.starsAmount = (int)newAmount;
        TriggerReload();
    }

    public void OnSeedChanged(string value)
    {
        OnSeedChanged(int.Parse(value));
    }
    
    public void OnSeedChanged(int value)
    {
        Game.inst.config.seed = value;
        TriggerReload();
    }

    public void OnRandomizeSeedButtonPressed()
    {
        int value = Random.Range(0, int.MaxValue);
        seedInputField.text = value.ToString();
    }

    private void TriggerReload()
    {
        Game.inst.OnConfigChanged();
    }
    #endregion sandbox menu
}
