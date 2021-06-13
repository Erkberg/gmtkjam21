using System.Collections;
using System.Collections.Generic;
using ErksUnityLibrary;
using UnityEngine;

public class StarLineLink : MonoBehaviour
{
    public StarLine starLine;
    
    public void Init(StarLine starLine, bool invertedY)
    {
        this.starLine = starLine;
        transform.SetScaleX(starLine.GetLength());
        transform.right = starLine.GetDirection();

        if (invertedY)
            transform.eulerAngles *= -1;
    }
    
    public void Kill()
    {
        Destroy(gameObject);
    }
}
