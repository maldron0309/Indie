using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    private const string player = "playertrigger";

    void OnTriggerEnter2D(UnityEngine.Collider2D other)
    {
        if (other.CompareTag(player))
        {
            AudioManager.instance.PlaySfx("Duck");
            Destroy(gameObject); //once coin is collected, destroy coin
            Debug.Log("P");
        }
        else
        {
            Debug.Log("Not Player");
        }
    }
}
