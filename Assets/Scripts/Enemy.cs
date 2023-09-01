using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform[] patrolSpots;
    private int currPatrolSpotIndex = 0;
    [SerializeField] private float stopDistance = 0.1f;
    
    private bool canMove = true;
    private bool isMovingRight = false;

    private void Awake()
    {
        if (rb == null)
            rb = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (canMove)
            Move();
    }
    
    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if(collision.gameObject.CompareTag("Wall"))
    //     {
    //         ObstacleRigidbody.gravityScale *= -1; 
    //     }
    // }

    private void Move()
    {
        Vector2 currPos = transform.position;
        Vector2 movePos = new Vector2(patrolSpots[currPatrolSpotIndex].position.x, patrolSpots[currPatrolSpotIndex].position.y);

        // If we are closer to our target than our minimum distance...
        if (Vector2.Distance(currPos, movePos) <= stopDistance)
        {
            // Update to the next target
            currPatrolSpotIndex += 1;

            // If we've gone past the end of our list...
            // (if our current point index is equal or bigger than
            // the length of our list)
            if (currPatrolSpotIndex >= patrolSpots.Length)
            {
                // ...loop back to the start by setting 
                // the current point index to 0
                currPatrolSpotIndex = 0;
            }
        }

        // Now, move in the direction of our target

        // Get the direction
        // Subtract the current position from the target position to get a distance vector
        // Normalise changes it to be length 1, so we can then multiply it by our speed / force
        Vector2 direction = ((Vector2)patrolSpots[currPatrolSpotIndex].position - (Vector2)transform.position).normalized;

        if (direction.x > 0f)
        {
            Flip();
        }
        else if (direction.x < 0f)
        {
            Flip();
        }
        
        // Move in the correct direction with the set force strength
        //rb.AddForce(direction * forceStrength);
        currPos = Vector2.MoveTowards(currPos, movePos, speed * Time.deltaTime);
        transform.position = currPos;
    }

    private void Flip()
    {
        if (isMovingRight)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            isMovingRight = false;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            isMovingRight = true;
        }
    }
}
