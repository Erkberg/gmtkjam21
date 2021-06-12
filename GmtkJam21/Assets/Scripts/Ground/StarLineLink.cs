using System.Collections;
using System.Collections.Generic;
using ErksUnityLibrary;
using UnityEngine;

public class StarLineLink : MonoBehaviour
{
    public void Init(StarLine starLine)
    {
        transform.SetScaleX(starLine.GetLength() / 2);
        transform.right = starLine.GetDirection();
    }
    
    public void Kill()
    {
        Destroy(gameObject);
    }
}
