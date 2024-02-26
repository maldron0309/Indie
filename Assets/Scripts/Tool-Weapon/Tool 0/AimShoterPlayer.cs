using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimWeapon : MonoBehaviour
{
    public GameObject spawnParticles;
    public GameObject Player;
    private Camera mainCam;
    private Vector3 mousePos;
    private Vector3 bulletSpawnPos;
    [SerializeField]private float offsetX;
    [SerializeField] private float offsetY;
    [SerializeField] private float offsetZ;
    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire;
    public float timer;
    public float timerBetweenFiring;
    public bool switched = false;

    void Start()
    {
        mainCam = Camera.main;
        bulletSpawnPos = new Vector3(bulletTransform.transform.position.x +offsetX, bulletTransform.transform.position.y +offsetY, bulletTransform.transform.position.z + offsetZ);
    }

    void Update()
    {
        bulletSpawnPos = new Vector3(bulletTransform.transform.position.x + offsetX, bulletTransform.transform.position.y + offsetY, bulletTransform.transform.position.z + offsetZ);
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
            canFire = false;
            Instantiate(bullet, bulletSpawnPos, Quaternion.Euler(270, 0, 270));
        }
        //
        if ((transform.rotation.eulerAngles.z >= 0f && transform.rotation.eulerAngles.z <= 90f) || (transform.rotation.eulerAngles.z >= 270f && transform.rotation.eulerAngles.z <= 360f))
        {
            bulletTransform.localScale = new Vector2(bulletTransform.localScale.x, 0.7f);
        }
        else
        {
            bulletTransform.localScale = new Vector2(bulletTransform.localScale.x, -0.7f);
        }
        
        //
        gameObject.transform.position = Player.transform.position;
        print(angle);
        
    }
    
    private void OnEnable()
    {
        Instantiate(spawnParticles, Player.transform.position, Quaternion.identity);
    }
}