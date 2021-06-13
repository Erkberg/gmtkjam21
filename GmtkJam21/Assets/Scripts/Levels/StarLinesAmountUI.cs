using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StarLinesAmountUI : MonoBehaviour
{
    public Animator animator;
    public TextMeshProUGUI text;

    public void SetText(int amount, int maxAmount)
    {
        text.text = $"{amount} / {maxAmount}";
    }

    public void SetInfinite()
    {
        text.text = "∞";
    }

    public void TriggerPopup()
    {
        animator.SetTrigger("popup");
    }
}
