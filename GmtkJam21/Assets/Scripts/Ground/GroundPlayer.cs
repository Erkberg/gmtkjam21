using System;
using System.Collections;
using System.Collections.Generic;
using ErksUnityLibrary;
using UnityEngine;

public class GroundPlayer : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 1f;
    
    private Vector3 spawnPosition;

    private void Update()
    {
        if (Game.inst.IsIngame() && Game.inst.ingameState == Game.IngameState.Ground)
        {
            Move();

            if (Game.inst.input.GetResetPlayerButtonDown())
            {
                Respawn();
            }
        }
    }

    private void Move()
    {
        Vector2 input = Game.inst.input.GetMovement() * moveSpeed;
        rb.SetVelocityX(input.x);
        rb.SetVelocityZ(input.y);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 4)
        {
            Game.inst.audio.OnDrown();
            Respawn();
        }
        else
        {
            LevelEnd levelEnd = other.GetComponent<LevelEnd>();
            if (levelEnd)
            {
                Game.inst.levels.OnLevelFinished();
            }
            
            Obstacle obstacle = other.GetComponent<Obstacle>();
            if (obstacle)
            {
                Respawn();
            }
        }
    }

    public void Spawn(Vector3 position)
    {
        spawnPosition = position;
        transform.position = position;
    }

    private void Respawn()
    {
        transform.position = spawnPosition;
    }
}
