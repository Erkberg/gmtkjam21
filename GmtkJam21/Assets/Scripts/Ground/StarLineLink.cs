using System.Collections;
using System.Collections.Generic;
using ErksUnityLibrary;
using UnityEngine;

public class StarLineLink : MonoBehaviour
{
    public StarLine starLine;
    
    public void Init(StarLine starLine)
    {
        this.starLine = starLine;
        transform.SetScaleX(starLine.GetLength());
        transform.right = starLine.GetDirection();
    }
    
    public void Kill()
    {
        Destroy(gameObject);
    }
}
