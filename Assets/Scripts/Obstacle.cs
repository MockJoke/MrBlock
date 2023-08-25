﻿using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private Rigidbody2D ObstacleRigidbody;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            ObstacleRigidbody.gravityScale *= -1; 
        }
    }
}