using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour
{
    //public Animator staffAnimator;
    public GameObject Player;
    //public GameObject boxCollider2D;
    private Camera mainCam;
    private Vector3 mousePos;
    private Vector3 newPosition;
    public Transform bulletTransform;
    public bool canFire;
    public float timer;
    public float timerBetweenFiring;
    public int ammo = 2;
    private bool colActive = false;
    [SerializeField] private float offsetX = 0;
    [SerializeField] private float offsetY = 0;
    [SerializeField] private float offsetZ = 0;

    [SerializeField] public float angle1;
    [SerializeField] public float angle2;

    [SerializeField] private int magnitude = 20;

    public Rigidbody2D playerRigidbody;

    void Start()
    {
        mainCam = Camera.main;

    }

    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = mousePos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timerBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }

        if (Input.GetMouseButtonDown(0) && canFire)
        {
            //staffAnimator.SetTrigger("");
            AudioManager.instance.PlaySfx("SwordSwing");
            canFire = false;
            dashFunction();
           // boxCollider2D.SetActive(true);
            ammo--;
        }

        newPosition = new Vector3(Player.transform.position.x + offsetX, Player.transform.position.y + offsetY, Player.transform.position.z + offsetZ);
        gameObject.transform.position = newPosition;
    }

    void dashFunction()
    {
        angle1 = Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * magnitude;
        angle2 = Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * magnitude;
        Vector3 forceVector = new Vector2(angle1, angle2);

        playerRigidbody.velocity = new Vector2(0.1f, 0.1f);
        playerRigidbody.AddForce(forceVector, ForceMode2D.Impulse);
        //gameObject.SetActive(false);
    }
}
