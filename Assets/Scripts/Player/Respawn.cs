using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject player;
    public GameObject respawnPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            player.transform.position = respawnPoint.transform.position;
        }
    }

    public void RespawnPlayer()
    {
        player.transform.position = respawnPoint.transform.position;
    }
}
