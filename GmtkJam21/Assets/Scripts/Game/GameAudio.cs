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

    [Space]
    public AudioClip atmoAbove;
    public AudioClip atmoBelow;

    public AudioClip switchAbove;
    public AudioClip switchBelow;

    [Space]
    public AudioClip drownClip;
    public AudioClip obstacleClip;

    public AudioClip focusStarClip;
    public AudioClip unfocusStarClip;

    public AudioClip addLineClip;
    public AudioClip removeLineClip;

    public AudioClip levelFinishedClip;

    private float atmoVolume;

    private const float FadeDuration = 0.167f;

    private void Awake()
    {
        atmoVolume = asAtmo.volume;
    }

    public void OnSwitchToGround()
    {
        asSounds.PlayOneShotRandomVolumePitch(switchBelow);
        StartCoroutine(asAtmo.FadeOut(FadeDuration, () => PlayAtmo(atmoBelow)));
    }

    public void OnSwitchToSky()
    {
        asSounds.PlayOneShotRandomVolumePitch(switchAbove);
        StartCoroutine(asAtmo.FadeOut(FadeDuration, () => PlayAtmo(atmoAbove)));
    }

    public void OnDrown()
    {
        asSounds.PlayOneShotRandomVolumePitch(drownClip);
    }
    
    public void OnObstacle()
    {
        asSounds.PlayOneShotRandomVolumePitch(obstacleClip);
    }

    public void OnLevelFinished()
    {
        asSounds.PlayOneShotRandomVolume(levelFinishedClip);
    }

    public void OnFocusStar()
    {
        asSounds.PlayOneShotRandomVolume(focusStarClip, baseVolume: 0.5f);
    }
    
    public void OnUnfocusStar()
    {
        //asSounds.PlayOneShotRandomVolumePitch(unfocusStarClip);
    }
    
    public void OnAddLine()
    {
        asSounds.PlayOneShotRandomVolume(addLineClip);
    }
    
    public void OnRemoveLine()
    {
        asSounds.PlayOneShotRandomVolume(removeLineClip);
    }

    private void PlayAtmo(AudioClip clip)
    {
        asAtmo.clip = clip;
        asAtmo.volume = atmoVolume;
        asAtmo.Play();
    }
}
