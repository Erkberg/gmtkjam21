using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarLine : MonoBehaviour
{
    public LineRenderer line;
    public InteractableStar startStar;
    public InteractableStar endStar;

    public void SetStartStar(InteractableStar startStar)
    {
        this.startStar = startStar;
        name += $"_{startStar.id}";
        line.SetPosition(0, startStar.transform.position);
    }

    public void SetEndStar(InteractableStar endStar)
    {
        this.endStar = endStar;
        name += $"->{endStar.id}";
        SetEndPosition(endStar.transform.position);
    }

    public void SetEndPosition(Vector3 pos)
    {
        line.SetPosition(1, pos);
    }
}
