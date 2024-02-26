using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuberDucky : MonoBehaviour
{
    public GameObject spawnParticles;
    public GameObject Player;
    private Camera mainCam;
    private Vector3 mousePos;
    private Vector3 bulletSpawnPos;
    [SerializeField] private float offsetX;
    [SerializeField] private float offsetY;
    [SerializeField] private float offsetZ;
    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire;
    public float timer;
    public float timerBetweenFiring;
    public bool switched = false;
    [SerializeField] public int ammo = 20;
    [SerializeField] public int resetAmmo = 20;
    private Vector3 newPosition;
    void Start()
    {
        mainCam = Camera.main;
        bulletSpawnPos = new Vector3(bulletTransform.transform.position.x + offsetX, bulletTransform.transform.position.y + offsetY, bulletTransform.transform.position.z + offsetZ);
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
            AudioManager.instance.PlaySfx("Duck");
            //canFire = false;
            //  Instantiate(bullet, bulletSpawnPos, Quaternion.Euler(270, 0, 270));
            ammo--;
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
        newPosition = new Vector3(Player.transform.position.x + offsetX, Player.transform.position.y + offsetY, Player.transform.position.z + offsetZ);
        gameObject.transform.position = newPosition;
        print(angle);

        if (ammo <= 0)
        {
            gameObject.SetActive(false);
        }

    }

    private void OnEnable()
    {
        ammo = resetAmmo;
        Instantiate(spawnParticles, Player.transform.position, Quaternion.identity);
        //AudioManager.instance.PlaySfx("TransformEnd");
    }
    private void OnDisable()
    {
        Instantiate(spawnParticles, Player.transform.position, Quaternion.identity);
        //AudioManager.instance.PlaySfx("TransformEnd");
    }
}
