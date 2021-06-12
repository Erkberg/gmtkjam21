using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBackground : MonoBehaviour
{
    public List<ParticleSystem> backgroundParticles;

    public void ResetBackgrounds()
    {
        foreach (ParticleSystem backgroundParticle in backgroundParticles)
        {
            backgroundParticle.Clear();
            backgroundParticle.Play();
        }
    }
}
