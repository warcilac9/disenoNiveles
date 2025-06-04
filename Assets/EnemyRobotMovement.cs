using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRobotMovement : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float idleTime = 1f;

    [Header("Sprite Settings")]
    [SerializeField] private bool isFacingRight = true;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Vector3 targetPoint;
    private bool isWaiting = false;
    private float waitTimer = 0f;

    private void Start()
    {
        // Set initial target to point B
        targetPoint = pointB.position;
        
        // If no sprite renderer is assigned, try to get it from the object
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    private void Update()
    {
        if (isWaiting)
        {
            // Handle idle time between movements
            waitTimer += Time.deltaTime;
            if (waitTimer >= idleTime)
            {
                isWaiting = false;
                waitTimer = 0f;
                // Switch target point
                targetPoint = (targetPoint == pointA.position) ? pointB.position : pointA.position;
            }
        }
        else
        {
            MoveToTarget();
        }
    }

    private void MoveToTarget()
    {
        // Calculate movement direction
        Vector3 direction = (targetPoint - transform.position).normalized;
        
        // Move the enemy
        transform.position += direction * moveSpeed * Time.deltaTime;
        
        // Flip sprite based on movement direction
        if (direction.x > 0 && !isFacingRight)
        {
            FlipSprite();
        }
        else if (direction.x < 0 && isFacingRight)
        {
            FlipSprite();
        }
        
        // Check if reached target point
        if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
        {
            isWaiting = true;
        }
    }

    private void FlipSprite()
    {
        // Switch the way the enemy is labelled as facing
        isFacingRight = !isFacingRight;
        
        // Flip the sprite by multiplying the x scale by -1
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }

    // Visualize patrol points in editor
    private void OnDrawGizmos()
    {
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(pointA.position, 0.3f);
            Gizmos.DrawWireSphere(pointB.position, 0.3f);
            Gizmos.DrawLine(pointA.position, pointB.position);
        }
    }
}
