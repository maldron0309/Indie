using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTarget : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float speed;
    [SerializeField] private Transform player;
    [SerializeField] private float sightDistance;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Check if the player is within sight distance
        if (distanceToPlayer <= sightDistance)
        {
                // Calculate the direction towards the player only along the x-axis
                float directionX = Mathf.Sign(player.position.x - transform.position.x);

                // Move towards the player
                rb.velocity = new Vector2(directionX * speed, rb.velocity.y);
        }
        else
        {
            // Player is not within sight distance, stop moving
            rb.velocity = Vector2.zero;
        }
    }
}