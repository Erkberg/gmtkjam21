using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTerrain : MonoBehaviour
{
    public float starHeight = 1f;
    public float starLineHeight = 1f;
    
    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            List<InteractableStar> stars = Game.inst.starCreator.stars;
            Vector3 offset = Vector3.down * Game.inst.starCreator.positionY * 2;

            foreach (InteractableStar star in stars)
            {
                Gizmos.DrawWireSphere(star.transform.position + offset, 0.1f);
            }
        }
    }
}
