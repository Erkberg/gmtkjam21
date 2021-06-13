using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameNarration : MonoBehaviour
{
    public GameObject textbox;
    public TextMeshProUGUI text;

    public float waitTimePerChar = 0.1f;
    public float baseWaitTime = 1f;

    public void ShowText(string s)
    {
        StopAllCoroutines();
        text.text = s;
        textbox.SetActive(true);

        float waitTime = baseWaitTime + waitTimePerChar * s.Length;
        StartCoroutine(DisappearAfterSeconds(waitTime));
    }

    private IEnumerator DisappearAfterSeconds(float duration)
    {
        yield return new WaitForSeconds(duration);
        textbox.SetActive(false);
        text.text = string.Empty;
    }
}
