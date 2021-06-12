using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class StartMenu : MonoBehaviour
{
    public GameObject holder;
    public TextMeshProUGUI starsAmountText;
    public TMP_InputField seedInputField;

    private const string StarsAmountText = "Stars amount: ";
    
    public void OnStartButtonPressed()
    {
        holder.SetActive(false);
        Game.inst.OnGameStarted();
    }

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
        int value = Random.Range(0, Int32.MaxValue);
        seedInputField.text = value.ToString();
    }

    private void TriggerReload()
    {
        Game.inst.OnConfigChanged();
    }
}
