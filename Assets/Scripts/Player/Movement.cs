using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Movement : MonoBehaviour
{
    // Player movement related variables
    [Header("Movement Settings")]
    [SerializeField] private float velocidad;
    [SerializeField] private float velocityMoventBase;
    [SerializeField] private float velocityExtra;

    // Jump related variables
    [Header("Jump Settings")]
    [SerializeField] private float forceJump;
    [SerializeField] private int jumpsMax;
    [SerializeField] private LayerMask MaskFlood;
    private int jumpsRestants;

    // Health related variables
    [Header("Health Settings")]
    [SerializeField] private int health;
    [SerializeField] private float damageDelay; //seconds before player can be damaged again

    // Coin related variables
    [Header("Coin Settings")]
    [SerializeField] private TMP_Text coinCounter;
    private int coinNum = 0;

    // Sprint related variables
    [Header("Sprint Settings")]
    public float timeSprint;
    public int runMax;
    public float timeBetweenSprint;
    private float timeActualSprint;
    private float timeNextSprint;
    private bool run = true;
    private bool runing = true;
    private int runsRemaining;

    // Misc variables
    private bool rightStep = true;
    private bool isMidAir = false;
    private bool WatchRight;
    private const string DAMAGE = "doesDamage"; //DO NOT CHANGE (damage will break)
    private const string COIN = "Coin"; //DO NOT CHANGE (coins will break)
    private Camera mainCam;
    private Vector3 mousePos;
    private Movement movementScript;
    private new BoxCollider2D boxCollider;
    private new Rigidbody2D rigidbody;
    public Animator playerAnimator;

    private void Start()
    {
        InitializeComponents();

    }

    // Initialize all necessary components
    private void InitializeComponents()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        jumpsRestants = jumpsMax;
        movementScript = GetComponent<Movement>();
        mainCam = Camera.main;
        jumpsRestants = jumpsMax;
        timeActualSprint = timeSprint;
        runsRemaining = runMax; // Initializes the number of times you can run
    }


    private void Update()
    {
        HandleMovement();
        HandleJump();
        HandleRun();

        isMidAir = !IsOnGround();
        // playerAnimator.SetBool("isFalling", isMidAir); // Uncomment if needed

        HandleMouseDirection();
        UpdateAnimatorSpeed();
    }

    private void HandleMouseDirection()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0)
        {
            // Convert mouse position to world coordinates
            mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

            // Calculate rotation towards mouse position
            Vector2 direction = mousePos - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Uncomment if needed
            // if (rightStep)
            // {
            //     AudioManager.instance.PlayFootSteps("WalkRight");
            // }
            // else
            // {
            //     AudioManager.instance.PlayFootSteps("WalkLeft");
            // }

            // Apply rotation to the object
            // transform.rotation = Quaternion.Euler(0f, 0f, angle); // Uncomment if needed
        }
    }

    private void UpdateAnimatorSpeed()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        playerAnimator.SetFloat("Speed", Mathf.Abs(horizontalInput));
    }

    private bool IsOnGround()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y), 0f, Vector2.down, 0.2f, MaskFlood);
        return raycastHit.collider != null;
    }

    private void HandleJump()
    {
        if (IsOnGround())
        {
            jumpsRestants = jumpsMax;
            // playerAnimator.SetBool("isJumping", false); // Uncomment if needed
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpsRestants > 0)
        {
            jumpsRestants--;
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0f);
            rigidbody.AddForce(Vector2.up * forceJump, ForceMode2D.Impulse);
            AudioManager.instance.FootStepsSource.Stop();
            AudioManager.instance.PlaySfx("Jump");

            // playerAnimator.SetBool("isJumping", true); // Uncomment if needed
        }
    }

    private void HandleRun()
    {
        if (IsOnGround())
        {
            runsRemaining = runMax;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && run && runsRemaining > 0)
        {
            runsRemaining--; // Reduces the number of times you can run
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
        if (other.CompareTag(DAMAGE)) // add this tag to any object that deals damage to the player or it won't work (Squid-West)
        {
            StartCoroutine(DamageDelay());
        }
        else if (other.CompareTag(COIN))
        {
            coinNum++;
            coinCounter.text = coinNum.ToString();
        }
    }


    // This coroutine handles the delay between taking damage and applying the effect
    private IEnumerator DamageDelay()
    {
        health--;

        // Try to find the HealthBar in the scene and update it
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

        // If health reaches 0, respawn the player
        if (health <= 0)
        {
            Respawn respawn = FindObjectOfType<Respawn>();
            if (respawn == null)
            {
                Debug.LogError("Respawn not found!");
            }
            else
            {
                respawn.RespawnPlayer();
                Debug.Log("Player respawned at " + respawn.respawnPoint.transform.position);
            }
        }
    }

    // This function handles the player's movement mechanics
    private void HandleMovement()
    {
        float inputMovimiento = Input.GetAxis("Horizontal");
        rigidbody.velocity = new Vector2(inputMovimiento * velocidad, rigidbody.velocity.y);
        HandleOrientation(inputMovimiento);

        // Play walking sound if the player is moving and not in mid air
        if (!AudioManager.instance.FootStepsSource.isPlaying && !isMidAir && inputMovimiento != 0)
        {
            AudioManager.instance.PlayFootSteps("WalkLoop");
        }
        if (inputMovimiento == 0)
        {
            AudioManager.instance.FootStepsSource.Stop();
        }
    }

    // This function handles the player's orientation based on movement direction
    private void HandleOrientation(float inputMovimiento)
    {
        if ((WatchRight == true && inputMovimiento > 0) || (WatchRight == false && inputMovimiento < 0))
        {
            WatchRight = !WatchRight;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

}
