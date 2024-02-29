using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;

public class SwordCollision : MonoBehaviour
{

    public Rigidbody2D playerRigidbody;
    public GameObject sword;
    [SerializeField]private int magnitude =20;
    [SerializeField] public float angle1;
    [SerializeField] public float angle2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //angle1 = Mathf.Cos(sword.transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * magnitude;
        //angle2 = Mathf.Sin(sword.transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * magnitude;
        //Vector3 forceVector = new Vector2(angle1, angle2);

        //    playerRigidbody.velocity = new Vector2(0.1f,0.1f);
        //    playerRigidbody.AddForce(-forceVector, ForceMode2D.Impulse);

        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
                 if (collision.gameObject.CompareTag("spike")|| collision.gameObject.CompareTag("platform"))
                 {
                          angle1 = Mathf.Cos(sword.transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * magnitude;
                          angle2 = Mathf.Sin(sword.transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * magnitude;
                         Vector3 forceVector = new Vector2(angle1, angle2);

                        playerRigidbody.velocity = new Vector2(0.1f, 0.1f);
                      playerRigidbody.AddForce(-forceVector, ForceMode2D.Impulse);
                      gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("spike"))
        {
            angle1 = Mathf.Cos(sword.transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * magnitude;
            angle2 = Mathf.Sin(sword.transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * magnitude;
            Vector3 forceVector = new Vector2(angle1, angle2);

            playerRigidbody.velocity = new Vector2(0.1f, 0.1f);
            playerRigidbody.AddForce(-forceVector, ForceMode2D.Impulse);
            AudioManager.instance.PlaySfx("SwordClash");
           gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
