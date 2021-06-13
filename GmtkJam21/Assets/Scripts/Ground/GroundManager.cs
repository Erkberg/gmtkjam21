using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    public StarIsland starIslandPrefab;
    public StarLineLink starLineLinkPrefab;
    public LevelEnd levelEndPrefab;

    public Transform starIslandsHolder;
    public Transform starLineLinksHolder;

    public List<StarIsland> starIslands = new List<StarIsland>();
    public List<StarLineLink> starLineLinks = new List<StarLineLink>();
    public LevelEnd currentLevelEnd;

    public Vector3 offsetToStars = new Vector3(0f, 16f, 0f);
    
    public void OnSwitchToGround()
    {
        
    }

    public void CreateObstacles(LevelData levelData)
    {
        ResetObstacles();

    }

    public void OnStarCreated(InteractableStar star, StarData.StarType starType = StarData.StarType.Regular)
    {
        StarIsland island = Instantiate(starIslandPrefab, starIslandsHolder);
        island.transform.position = star.transform.position - offsetToStars;
        island.id = star.id;
        starIslands.Add(island);

        if (starType == StarData.StarType.StartPoint)
        {
            Game.inst.groundPlayer.Spawn(island.transform.position);
        }
        
        if (starType == StarData.StarType.EndPoint)
        {
            currentLevelEnd = Instantiate(levelEndPrefab, transform);
            currentLevelEnd.transform.position = island.transform.position;
        }
    }

    public void OnLineCreated(StarLine line)
    {
        StarLineLink link = Instantiate(starLineLinkPrefab, starLineLinksHolder);
        link.transform.position = line.GetMidPoint() - offsetToStars;
        link.Init(line);
        starLineLinks.Add(link);
    }

    public void OnLineDestroyed(StarLine line)
    {
        foreach (StarLineLink starLineLink in starLineLinks.ToList())
        {
            if (starLineLink.starLine.id == line.id)
            {
                starLineLinks.Remove(starLineLink);
                starLineLink.Kill();
                break;
            }
        }
    }

    public void ResetAll()
    {
        ResetIslands();
        ResetLinks();
        ResetObstacles();
        
        if(currentLevelEnd)
            Destroy(currentLevelEnd.gameObject);
    }

    public void ResetIslands()
    {
        foreach (StarIsland starIsland in starIslands.ToList())
        {
            starIsland.Kill();
        }
        starIslands.Clear();
    }

    public void ResetLinks()
    {
        foreach (StarLineLink starLineLink in starLineLinks.ToList())
        {
            starLineLink.Kill();
        }
        starLineLinks.Clear();
    }

    public void ResetObstacles()
    {
        
    }
}
