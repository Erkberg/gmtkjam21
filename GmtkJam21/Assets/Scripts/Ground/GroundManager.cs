using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    public StarIsland starIslandPrefab;
    public StarLineLink starLineLinkPrefab;
    public LevelEnd levelEndPrefab;
    public Obstacle obstaclePrefab;

    public Transform starIslandsHolder;
    public Transform starLineLinksHolder;
    public Transform obstaclesHolder;

    public List<StarIsland> starIslands = new List<StarIsland>();
    public List<StarLineLink> starLineLinks = new List<StarLineLink>();
    public List<Obstacle> obstacles = new List<Obstacle>();
    public LevelEnd currentLevelEnd;

    public Vector3 offsetToStars = new Vector3(0f, 16f, 0f);
    public bool invertedY = false;
    
    public void OnSwitchToGround()
    {
        
    }
    
    private Vector3 CheckInvertY(Vector3 position)
    {
        if (invertedY)
            position.z *= -1;

        return position;
    }

    public void CreateObstacles(LevelData levelData)
    {
        ResetObstacles();

        for (int i = 0; i < levelData.obstacles.Count; i++)
        {
            ObstacleData obstacleData = levelData.obstacles[i];
            Obstacle obstacle = Instantiate(obstaclePrefab, obstaclesHolder);
            Vector3 position = new Vector3(obstacleData.position.x, Game.inst.starCreator.positionY, obstacleData.position.y) - offsetToStars;
            position = CheckInvertY(position);
            obstacle.transform.position = position;
            obstacles.Add(obstacle);
        }
    }

    public void OnStarCreated(InteractableStar star, StarData.StarType starType = StarData.StarType.Regular)
    {
        StarIsland island = Instantiate(starIslandPrefab, starIslandsHolder);
        Vector3 position = star.transform.position - offsetToStars;
        position = CheckInvertY(position);
        island.transform.position = position;
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
        Vector3 position = line.GetMidPoint() - offsetToStars;
        position = CheckInvertY(position);
        link.transform.position = position;
        link.Init(line, invertedY);
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
        foreach (Obstacle obstacle in obstacles.ToList())
        {
            obstacle.Kill();
        }
        obstacles.Clear();
    }
}
