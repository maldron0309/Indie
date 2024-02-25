using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnderPearl : MonoBehaviour
{
    public GameObject Player;
    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire;
    public float timer;
    public float timerBetweenFiring;
    public int ammo = 2;

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
            ammo --;
        }

        gameObject.transform.position = Player.transform.position;
    }

}
