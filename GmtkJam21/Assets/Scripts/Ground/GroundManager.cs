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

    public List<StarIsland> starIslands = new List<StarIsland>();
    public List<StarLineLink> starLineLinks = new List<StarLineLink>();

    public Vector3 offsetToStars = new Vector3(0f, 16f, 0f);
    
    public void OnSwitchToGround()
    {
        
    }

    public void OnStarCreated(InteractableStar star)
    {
        StarIsland island = Instantiate(starIslandPrefab, starIslandsHolder);
        island.transform.position = star.transform.position - offsetToStars;
        island.id = star.id;
        starIslands.Add(island);
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
