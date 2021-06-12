using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMenus : MonoBehaviour
{
    public MenuState menuState = MenuState.StartMenu;

    public GameObject mainMenu;
    public GameObject sandboxMenu;
    public GameObject tutorialMenu;
    public GameObject optionsMenu;
    
    public GameObject menusHolder;
    public TextMeshProUGUI starsAmountText;
    public TMP_InputField seedInputField;

    private const string StarsAmountText = "Stars amount: ";
    
    #region navigation
    public void OnOpenedMenu()
    {
        menuState = GameMenus.MenuState.StartMenu;
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
        menuState = MenuState.SandboxMenu;
        mainMenu.SetActive(false);
        sandboxMenu.SetActive(true);
    }

    public void OnSandboxStartButtonPressed()
    {
        menuState = MenuState.MenuClosed;
        Game.inst.gameMode = Game.GameMode.Sandbox;
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
