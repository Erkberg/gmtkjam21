using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game inst;

    public GameConfig config;
    public StarCreator starCreator;

    private void Awake()
    {
        inst = this;
        starCreator.CreateRandomStars(config.starsAmount, config. seed);
    }
}
