using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevator : MonoBehaviour
{
    [Header("Movement Points")]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float waitTime = 2f;
    
    private Transform platformParent; // Reference to the platform to move
    private Collider2D triggerCollider;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool movingToB = true;
    private float waitTimer = 0f;

    private void Awake()
    {
        // Get the parent platform (what we'll actually move)
        platformParent = transform.parent;
        
        // Get the trigger collider on this child object
        triggerCollider = GetComponent<Collider2D>();
        
        // Safety check
        if (platformParent == null)
        {
            Debug.LogError("This script should be on a child object of the platform!");
            enabled = false;
        }
    }

    private void Start()
    {
        // Initialize platform position to point A
        platformParent.position = pointA.position;
        targetPosition = pointB.position;
    }

    private void Update()
    {
        if (isMoving)
        {
            if (waitTimer > 0f)
            {
                // Waiting at destination
                waitTimer -= Time.deltaTime;
                if (waitTimer <= 0f)
                {
                    // Switch direction after waiting
                    movingToB = !movingToB;
                    targetPosition = movingToB ? pointB.position : pointA.position;
                }
            }
            else
            {
                // Move parent platform towards target
                platformParent.position = Vector3.MoveTowards(
                    platformParent.position, 
                    targetPosition, 
                    moveSpeed * Time.deltaTime
                );
                
                // Check if reached target
                if (Vector3.Distance(platformParent.position, targetPosition) < 0.01f)
                {
                    waitTimer = waitTime;
                    
                    // Re-enable trigger when back at start point
                    if (!movingToB && triggerCollider != null)
                    {
                        triggerCollider.enabled = true;
                        isMoving = false;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isMoving)
        {
            StartMovement();
        }
    }

    private void StartMovement()
    {
        isMoving = true;
        // Disable trigger collider to prevent reactivation
        if (triggerCollider != null)
        {
            triggerCollider.enabled = false;
        }
        // Set initial target
        targetPosition = pointB.position;
        movingToB = true;
    }

    private void OnDrawGizmos()
    {
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(pointA.position, 0.2f);
            Gizmos.DrawWireSphere(pointB.position, 0.2f);
            Gizmos.DrawLine(pointA.position, pointB.position);
            
            // Draw line from this object to show it's a child
            Gizmos.color = Color.yellow;
            if (transform.parent != null)
            {
                Gizmos.DrawLine(transform.position, transform.parent.position);
            }
        }
    }
}
