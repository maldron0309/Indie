using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
   
    private Collider2D[] InExplosionRadius =null;
    [SerializeField] private float explosionForceMulti = 5;
    [SerializeField] private float explosionRadius = 5;
    private Rigidbody2D playerRb;
    //private GameObject explosionCollider;
    private GameObject Player;
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;
    public int damage = 100;
    public int magnitude = 15;
    public GameObject breakParticles;
    [SerializeField] private float destroyTime = 5f;


    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlaySfx("RocketShoot");
        Player = GameObject.FindGameObjectWithTag("player");
        playerRb = Player.GetComponent<Rigidbody2D>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);


       
        Invoke("destroyEvent", destroyTime); ;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Player.transform.position = gameObject.transform.position;
        if (GameManager.instance.playerDied)
        {
            destroyEvent();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
        }
        else
        {
            //explosionCollider.SetActive(true);
           
            
            if (collision.gameObject.CompareTag("platform"))
            {
                explode();
                print("Yup");
                AudioManager.instance.PlaySfx("RocketImpact");
                
            }
            destroyEvent();
        }
    }

    void explode()
    {
        InExplosionRadius = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach(Collider2D o in InExplosionRadius)
        {
            Rigidbody2D o_Rigidbody = o.GetComponent<Rigidbody2D>();
            if(o_Rigidbody != null)
            {
                Vector2 distanceVector = o.transform.position - transform.position;
                if (distanceVector.magnitude > 0)
                {
                    float explosionForce = explosionForceMulti /distanceVector.magnitude;
                    o_Rigidbody.AddForce(distanceVector.normalized *explosionForce);
                }
            }
        }
    }
    private void destroyEvent()
    {
        AudioManager.instance.PlaySfx("RocketImpact");
        Instantiate(breakParticles, gameObject.transform.position, Quaternion.identity);
       // explosionCollider.SetActive(false);
        Destroy(gameObject);
       
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
