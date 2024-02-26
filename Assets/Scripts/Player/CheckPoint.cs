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
            AudioManager.instance.PlaySfx("Checkpoint");
            respawn.respawnPoint = this.gameObject;
            checkPointcollider.enabled = false;
            print("check");
            // If you collision with the last checkepoint. It will return it(checkPointcollider.enabled = true;). 
            // If you want the last checkpoint and it not return other. It will return it(checkPointcollider.enabled = false;). 

        }
    }



}
