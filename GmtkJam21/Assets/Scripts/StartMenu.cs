using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public GameObject holder;
    public TextMeshProUGUI starsAmountText;

    private const string StarsAmountText = "Stars amount: ";
    
    public void OnStartButtonPressed()
    {
        holder.SetActive(false);
    }

    public void OnStarsAmountChanged(float newAmount)
    {
        starsAmountText.text = StarsAmountText + newAmount;
        Game.inst.config.starsAmount = (int)newAmount;
        TriggerReload();
    }

    private void TriggerReload()
    {
        Game.inst.OnConfigChanged();
    }
}
