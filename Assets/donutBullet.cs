using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class donutBullet : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private GameObject Player;
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;
    public int damage = 100;
    public int magnitude = 15;
    public GameObject brealParticles;
    [SerializeField] private float destroyTime = 1f;


    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlaySfx("DonutThrow");
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
          
            if (collision.gameObject.CompareTag("platform"))
            {
               
                AudioManager.instance.PlaySfx("DonutBreak");
               
            }
            destroyEvent();
            print("Plax");
        }
    }
    private void destroyEvent()
    {
        AudioManager.instance.PlaySfx("DonutBreak");
        Instantiate(brealParticles, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
