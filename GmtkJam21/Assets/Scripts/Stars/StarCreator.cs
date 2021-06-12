using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StarCreator : MonoBehaviour
{
    public InteractableStar starPrefab;
    public Transform starsHolder;
    public float minStarDistance = 1f;
    public float positionY = 8f;
    public float maxPositionX = 6.5f;
    public float maxPositionZ = 3.5f;

    private List<InteractableStar> stars = new List<InteractableStar>();

    public void RecreateRandomStars(int amount, int seed)
    {
        ClearStars();
        CreateRandomStars(amount, seed);
    }
    
    public void CreateRandomStars(int amount, int seed)
    {
        Random.InitState(seed);

        for (int i = 0; i < amount; i++)
        {
            InteractableStar star = Instantiate(starPrefab, starsHolder);
            star.transform.position = GetRandomValidStarPosition();
            stars.Add(star);
        }
    }

    private void ClearStars()
    {
        foreach (InteractableStar star in stars.ToList())
        {
            Destroy(star.gameObject);
        }
        
        stars.Clear();
    }

    private Vector3 GetRandomValidStarPosition()
    {
        Vector3 position = GetRandomStarPosition();
        int counter = 0;

        while (IsPositionTooCloseToExistingStars(position) && counter < 1000)
        {
            position = GetRandomStarPosition();
            counter++;
        }

        return position;
    }

    private bool IsPositionTooCloseToExistingStars(Vector3 position)
    {
        bool tooClose = false;

        foreach (InteractableStar star in stars)
        {
            if (Vector3.Distance(star.transform.position, position) < minStarDistance)
            {
                tooClose = true;
                break;
            }
        }

        return tooClose;
    }

    private Vector3 GetRandomStarPosition()
    {
        return new Vector3(Random.Range(-maxPositionX, maxPositionX), positionY, Random.Range(-maxPositionZ, maxPositionZ));
    }
}
