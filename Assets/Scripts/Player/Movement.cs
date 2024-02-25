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

    public Animator playerAnimator;
    private int runsRemaining;
    private Camera mainCam;
    private Vector3 mousePos;
    private Movement movementScript;
    private new BoxCollider2D boxCollider;
    private new Rigidbody2D rigidbody;
    private bool WatchRight;
    private int jumpsRestants;
    private const string DAMAGE = "doesDamage"; //DO NOT CHANGE (damage will break)
    private const string COIN = "Coin"; //DO NOT CHANGE (coins will break)
    private int coinNum = 0;
    private bool isMidAir = false;

    public float velocityMoventBase;
    public float velocityExtra;
    public float timeSprint;
    public int runMax;
    private float timeActualSprint;
    private float timeNextSprint;
    public float timeBetweenSprint;
    private bool run = true;
    private bool runing = true;

    private bool rightStep = true;

    // Start is called before
    // the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
     //   audioWalk = GetComponent<AudioSource>();
       // audioJump = GetComponent<AudioSource>();
       // audioDeath = GetComponent<AudioSource>();
        jumpsRestants = jumpsMax;
        movementScript = GetComponent<Movement>();
        mainCam = Camera.main;

        jumpsRestants = jumpsMax;
        timeActualSprint = timeSprint;
        runsRemaining = runMax; // Inicializa la cantidad de veces que puede correr

    }

    // Update is called once per frame
    void Update()
    {
        mecanicaMovimiento();
        processJump();
        processRun();
        isMidAir = !stayInFlood();
        // playerAnimator.SetBool("isFalling", isMidAir);

        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0)
        {
            // Convertir la posici? del mouse a coordenadas del mundo
            mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

            // Calcular la rotaci? hacia la posici? del mouse
            Vector2 direction = mousePos - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            
            //if (rightStep)
            //{
            //    AudioManager.instance.PlayFootSteps("WalkRight");
            //}
            //else
            //{
            //    AudioManager.instance.PlayFootSteps("WalkLeft");
            //}
            // Aplicar la rotaci? al objeto
            //transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
        playerAnimator.SetFloat("Speed", Mathf.Abs(horizontalInput));
    }

    bool stayInFlood() {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y),0f,Vector2.down,0.2f, MaskFlood);
        return raycastHit.collider != null;
    }

    void processJump() {

        if (stayInFlood())
        {
            jumpsRestants = jumpsMax;
            // playerAnimator.SetBool("isJumping", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpsRestants>=0 && stayInFlood()) 
        {
            jumpsRestants--;
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0f);
            rigidbody.AddForce(Vector2.up * forceJump, ForceMode2D.Impulse);
            AudioManager.instance.FootStepsSource.Stop();
            AudioManager.instance.PlaySfx("Jump");

            // playerAnimator.SetBool("isJumping", true);
        }
    
    }

    void processRun()
    {
        if (stayInFlood())
        {
            runsRemaining = runMax;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && run && runsRemaining > 0)
        {
            runsRemaining--; // Reduce la cantidad de veces que puede correr
            velocidad = velocityExtra;
            runing = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            velocidad = velocityMoventBase;
            runing = false;
            run = true;
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
            //audioDeath.Play();
            Debug.Log("dead");

        }
    }





    void mecanicaMovimiento()
    {
        float inputMovimiento = Input.GetAxis("Horizontal");
        //rigidbody.velocity = new Vector2(inputMovimiento * velocidad, rigidbody.velocity.y);
        Vector2 targetVelocity = new Vector2(inputMovimiento * velocidad, rigidbody.velocity.y);
        Vector2 currentVelocity = rigidbody.velocity;

        rigidbody.AddForce((targetVelocity - currentVelocity) /** velocidad*/);
        GestionarOrientacion(inputMovimiento);

        if (!AudioManager.instance.FootStepsSource.isPlaying && !isMidAir && inputMovimiento != 0)
        {
            AudioManager.instance.PlayFootSteps("WalkLoop");
        }
        if (inputMovimiento == 0)
        {
            AudioManager.instance.FootStepsSource.Stop();
        }
    }



    void GestionarOrientacion(float inputMovimiento) {
        if( (WatchRight == true && inputMovimiento > 0)|| (WatchRight == false && inputMovimiento < 0) ){
            WatchRight = !WatchRight;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }


    }
}
