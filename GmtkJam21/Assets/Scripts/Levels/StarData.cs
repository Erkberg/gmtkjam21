using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StarData
{
    public StarType starType = StarType.Regular;
    public Vector2 position;

    public enum StarType
    {
        Regular,
        StartPoint,
        EndPoint,
        StarLinePickup,
        DisableObstacleButton
    }
}
