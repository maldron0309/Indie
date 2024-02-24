using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    private const string player = "player";

    void OnTriggerEnter2D(UnityEngine.Collider2D other)
    {
        if (other.CompareTag(player))
        {
            Destroy(gameObject); //once coin is collected, destroy coin
            Debug.Log("Player");
        }
        else
        {
            Debug.Log("Not Player");
        }
    }
}
