using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Movement : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private float forceJump;
    [SerializeField] private int jumpsMax;
    [SerializeField] private LayerMask MaskFlood;
    [SerializeField] private int health;
    [SerializeField] private float damageDelay; //seconds before player can be damaged again
    [SerializeField] private TMP_Text coinCounter;
    [SerializeField] private AudioSource audioWalk;
    [SerializeField] private AudioSource audioJump;
    [SerializeField] private AudioSource audioDeath;

    private new BoxCollider2D boxCollider;
    private new Rigidbody2D rigidbody;
    private bool WatchRight;
    private int jumpsRestants;
    private const string DAMAGE = "doesDamage"; //DO NOT CHANGE (damage will break)
    private const string COIN = "Coin"; //DO NOT CHANGE (coins will break)
    private int coinNum = 0;
    private bool isMidAir = false;
    // Start is called before
    // the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        audioWalk = GetComponent<AudioSource>();
        audioJump = GetComponent<AudioSource>();
        audioDeath = GetComponent<AudioSource>();
        jumpsRestants = jumpsMax;
    }

    // Update is called once per frame
    void Update()
    {
        mecanicaMovimiento();
        processJump();

        isMidAir = !stayInFlood();
    }

    bool stayInFlood() {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y),0f,Vector2.down,0.2f, MaskFlood);
        return raycastHit.collider != null;
    }

    void processJump() {

        if (stayInFlood())
        {
            jumpsRestants = jumpsMax;
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpsRestants>0) 
        {
            jumpsRestants--;
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0f);
            rigidbody.AddForce(Vector2.up * forceJump, ForceMode2D.Impulse);

            audioJump.Play();
        }
    
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(DAMAGE)) //add this tag to any object that deals damage to the player or it won't work (Squid-West)
        {
            StartCoroutine(DamageDelay());
        }
        else if (other.CompareTag(COIN))
        {
            coinNum++;

            coinCounter.text = coinNum.ToString();
        }
    }

    private IEnumerator DamageDelay()
    {
        health--;

        HealthBar healthBar = FindObjectOfType<HealthBar>();
        if (healthBar == null)
        {
            Debug.LogError("HealthBar not found!");
        }
        else
        {
            healthBar.ChangeHealth(health);
        }

        yield return new WaitForSeconds(damageDelay);

        if (health <= 0)
        {
            //SceneManager.LoadScene("GameOverScene"); //For when we get a game over screen/UI
            Destroy(gameObject); //Destroy self (temporary)
            audioDeath.Play();
            Debug.Log("dead");

        }
    }





    void mecanicaMovimiento()
    {
        float inputMovimiento = Input.GetAxis("Horizontal");
        rigidbody.velocity = new Vector2(inputMovimiento * velocidad, rigidbody.velocity.y);
        GestionarOrientacion(inputMovimiento);

        if (audioWalk.isPlaying)
        {

        }
        else
        {
            if (isMidAir == false)
            {
                if (inputMovimiento == 0)
                {
                    audioWalk.Stop();
                }
                else
                {
                    audioWalk.Play();
                }
            }
        }
    }


    void GestionarOrientacion(float inputMovimiento) {
        if( (WatchRight == true && inputMovimiento > 0)|| (WatchRight == false && inputMovimiento < 0) ){
            WatchRight = !WatchRight;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }


    }
}
