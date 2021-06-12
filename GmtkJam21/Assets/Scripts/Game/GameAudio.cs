using System;
using System.Collections;
using System.Collections.Generic;
using ErksUnityLibrary;
using UnityEngine;

public class GameAudio : MonoBehaviour
{
    public AudioSource asMusic;
    public AudioSource asAtmo;
    public AudioSource asSounds;

    public AudioClip atmoAbove;
    public AudioClip atmoBelow;

    private float atmoVolume;

    private const float FadeDuration = 0.167f;

    private void Awake()
    {
        atmoVolume = asAtmo.volume;
    }

    public void OnSwitchToGround()
    {
        StartCoroutine(asAtmo.FadeOut(FadeDuration, () => PlayAtmo(atmoBelow)));
    }

    public void OnSwitchToSky()
    {
        StartCoroutine(asAtmo.FadeOut(FadeDuration, () => PlayAtmo(atmoAbove)));
    }

    private void PlayAtmo(AudioClip clip)
    {
        asAtmo.clip = clip;
        asAtmo.volume = atmoVolume;
        asAtmo.Play();
    }
}
