using System;
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

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.collider.name);
    }

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

    public Vector2 GetDirection2D()
    {
        return GetEndPosition2D() - GetStartPosition2D();
    }

    public Vector3 GetStartPosition()
    {
        return startStar.transform.position;
    }

    public Vector3 GetEndPosition()
    {
        return endStar.transform.position;
    }
    
    public Vector2 GetStartPosition2D()
    {
        return new Vector2(GetStartPosition().x, GetStartPosition().z);
    }

    public Vector2 GetEndPosition2D()
    {
        return new Vector2(GetEndPosition().x, GetEndPosition().z);
    }
    
    public Vector2 GetOffsetStartPosition2D()
    {
        return GetStartPosition2D() + GetDirection2D().normalized * 0.1f;
    }

    public Vector2 GetOffsetEndPosition2D()
    {
        return GetEndPosition2D() - GetDirection2D().normalized * 0.1f;
    }

    public void Kill()
    {
        Debug.Log("kill");
        Destroy(gameObject);
    }
}
