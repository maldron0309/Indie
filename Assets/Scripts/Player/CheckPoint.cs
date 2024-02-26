using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private Respawn respawn;

    private void Awake()
    {
        respawn = GameObject.FindGameObjectWithTag("Respawns").GetComponent<Respawn>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            AudioManager.instance.PlaySfx("CheckPoint");
            respawn.respawnPoint = gameObject;
        }
    }
}
