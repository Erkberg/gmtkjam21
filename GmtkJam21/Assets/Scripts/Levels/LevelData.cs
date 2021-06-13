using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelData : ScriptableObject
{
    public int id;
    public string narrationStart;
    public string narrationEnd;
    public int maxStarLines = -1;
    public List<StarData> stars;
    public List<ObstacleData> obstacles;
}