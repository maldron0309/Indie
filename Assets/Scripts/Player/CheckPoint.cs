using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private BoxCollider2D checkPointcollider;
    private Respawn respawn;

    private void Awake()
    {
        checkPointcollider = GetComponent<BoxCollider2D>();
        respawn = GameObject.FindGameObjectWithTag("Respawns").GetComponent<Respawn>();

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            respawn.respawnPoint = this.gameObject;
            checkPointcollider.enabled = false;
        }
    }



}
