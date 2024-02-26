using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class donut : MonoBehaviour
{
    public GameObject spawnParticles;
    public GameObject Player;
    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire;
    public float timer;
    public float timerBetweenFiring;
    [SerializeField] public int ammo = 4;
    private Vector3 newPosition;

    [SerializeField] private float offsetX = 0;
    [SerializeField] private float offsetY = 0;
    [SerializeField] private float offsetZ = 0;

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

        if (Input.GetMouseButtonDown(0) && canFire && ammo > 0)
        {
            
            canFire = false;
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
            ammo--;
        }
        newPosition = new Vector3(Player.transform.position.x + offsetX, Player.transform.position.y + offsetY, Player.transform.position.z + offsetZ);
        gameObject.transform.position = newPosition;
        if (ammo <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        ammo = 4;

        Instantiate(spawnParticles, Player.transform.position, Quaternion.identity);
        //AudioManager.instance.PlaySfx("TransformEnd");
    }
    private void OnDisable()
    {

        Instantiate(spawnParticles, Player.transform.position, Quaternion.identity);
        //AudioManager.instance.PlaySfx("TransformEnd");
    }
}
