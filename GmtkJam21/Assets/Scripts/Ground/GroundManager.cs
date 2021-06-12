using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    public StarIsland starIslandPrefab;
    public StarLineLink starLineLinkPrefab;
    
    public Transform starIslandsHolder;
    public Transform starLineLinksHolder;

    private List<StarIsland> starIslands = new List<StarIsland>();
    private List<StarLineLink> starLineLinks = new List<StarLineLink>();

    public Vector3 offsetToStars = new Vector3(0f, 16f, 0f);
    
    public void OnSwitchToGround()
    {
        
    }

    public void OnStarCreated(InteractableStar star)
    {
        StarIsland island = Instantiate(starIslandPrefab, starIslandsHolder);
        island.transform.position = star.transform.position - offsetToStars;
        starIslands.Add(island);
    }

    public void OnLineCreated(StarLine line)
    {
        StarLineLink link = Instantiate(starLineLinkPrefab, starLineLinksHolder);
        link.Init(line);
        starLineLinks.Add(link);
    }

    public void OnLineDestroyed(StarLine line)
    {
        
    }

    public void ResetAll()
    {
        ResetIslands();
        ResetLinks();
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
}
