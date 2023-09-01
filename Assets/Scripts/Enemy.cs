using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform[] patrolSpots;
    private int currPatrolSpotIndex = 0;
    [SerializeField] private float minStopDistance = 0.2f;

    private bool isFacingRight = false;

    private void Awake()
    {
        if (rb == null)
            rb = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
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
        if (Vector2.Distance(currPos, movePos) <= minStopDistance)
        {
            // Update to the next target
            currPatrolSpotIndex += 1;

            // If we've gone past the end of our array... (if our current point index is equal or bigger than the length of our list)
            if (currPatrolSpotIndex >= patrolSpots.Length)
            {
                // ...loop back to the start by setting the current index to 0
                currPatrolSpotIndex = 0;
            }
        }

        //Get the normalised direction vector bw target & curr pos
        Vector2 direction = (movePos - currPos).normalized;
        
        bool movingRight = IsMovingRight(direction);
        
        // If new patrol direction is none that means obj has hit the patrol spot
        if (movingRight != isFacingRight)
        {
            Flip();
        }
        isFacingRight = IsMovingRight(direction);
        
        // Move towards the target position with given speed
        currPos = Vector2.MoveTowards(currPos, movePos, speed * Time.deltaTime);
        transform.position = currPos;
    }
    
    // Flips the game object to face it in the same direction its moving
    private void Flip()
    {
        transform.eulerAngles = isFacingRight ? new Vector3(0, -180, 0) : new Vector3(0, 0, 0);
    }
    
    private bool IsMovingRight(Vector2 direction)
    {
        return direction switch
        {
            { x: > 0} => true,
            { x: < 0} => false,
            _ => false,
        };
    }
}
