using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarLine : MonoBehaviour
{
    public LineRenderer line;
    public MeshCollider meshCollider;
    public InteractableStar startStar;
    public InteractableStar endStar;
    public string id;

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
        SetCollider();
        id = startStar.id + " - " + endStar.id;
    }

    public void SetEndPosition(Vector3 pos)
    {
        line.SetPosition(1, pos);
    }

    private void SetCollider()
    {
        Mesh mesh = new Mesh();
        line.BakeMesh(mesh, true);
        meshCollider.sharedMesh = mesh;
    }

    public Vector3 GetMidPoint()
    {
        return (startStar.transform.position + endStar.transform.position) / 2;
    }

    public float GetLength()
    {
        return Vector3.Distance(startStar.transform.position, endStar.transform.position);
    }

    public Vector3 GetDirection()
    {
        return endStar.transform.position - startStar.transform.position;
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
