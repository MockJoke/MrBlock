using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D ObstacleRigidbody;
    
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform[] patrolSpots;
    private int currPatrolSpotIndex = 0;
    
    private bool canMove = true;
    private bool isMovingRight;
    
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
        var currPos = transform.position;
        Vector2 movePos = new Vector2(patrolSpots[currPatrolSpotIndex].position.x, currPos.y);
        
        currPos = Vector2.MoveTowards(currPos, movePos, speed * Time.deltaTime);
        transform.position = currPos;

        if (Vector2.Distance(currPos, movePos) < 0.2f)
        {
            if (currPatrolSpotIndex < patrolSpots.Length - 1)
                currPatrolSpotIndex++;
            else
                currPatrolSpotIndex = 0;

            if (currPatrolSpotIndex == 0 || currPatrolSpotIndex == patrolSpots.Length - 1)
                Flip();
        }
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
