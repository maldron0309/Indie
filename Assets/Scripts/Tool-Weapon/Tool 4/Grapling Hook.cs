using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraplingHook : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public DistanceJoint2D distanceJoint2D;

    [SerializeField]private float grappleLength;
    [SerializeField]private LayerMask grappleLayer;

    private Vector3 grapplePoint;
    //public Animator staffAnimator;
    public GameObject player;
    //public GameObject boxCollider2D;
    private Camera mainCam;
    private Vector3 mousePos;
    private Vector3 clickedMousePos;
    private Vector3 newPosition;
    public Transform bulletTransform;
    public bool canFire;
    public float timer;
    public float timerBetweenFiring;
    public int ammo = 2;
    private bool colActive = false;
    private bool hasFired = false;
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
        distanceJoint2D.enabled = false;
        lineRenderer.enabled = false;
    }

    void Update()
    {
        

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
            RaycastHit2D hit = Physics2D.Raycast(
             origin: mainCam.ScreenToWorldPoint(Input.mousePosition),
             direction: Vector2.zero,
             distance: Mathf.Infinity,
             layerMask: grappleLayer
             );
            if (hit.collider != null)
            {
                grapplePoint = hit.point;
                grapplePoint.z = 0;
                distanceJoint2D.connectedAnchor = grapplePoint;
                distanceJoint2D.enabled = true;
                distanceJoint2D.distance = grappleLength;
                lineRenderer.SetPosition(0, grapplePoint);
                lineRenderer.SetPosition(1, transform.position);
                lineRenderer.enabled = true;

            }
            mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

            //staffAnimator.SetTrigger("");
            AudioManager.instance.PlaySfx("SwordSwing");
            canFire = false;

            
            if (!hasFired)
            {
                //dashFunction();
                hasFired = true;
            }

            // boxCollider2D.SetActive(true);
            ammo--;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            distanceJoint2D.enabled = false;
            lineRenderer.enabled = false;
            hasFired = false;
        }
        //else if (distanceJoint2D.enabled)
        //{
        //    lineRenderer.SetPosition(1, player.transform.position);
            
        //}
        if (lineRenderer.enabled)
        {
            lineRenderer.SetPosition(1, transform.position);
        }
        if (hasFired)
        {
            //dashFunction();
        }
        newPosition = new Vector3(player.transform.position.x + offsetX, player.transform.position.y + offsetY, player.transform.position.z + offsetZ);
        gameObject.transform.position = newPosition;
    }

    void dashFunction()
    {
        angle1 = Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * magnitude;
        angle2 = Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * magnitude;
        Vector3 forceVector = new Vector2(angle1, angle2);

        playerRigidbody.velocity = new Vector2(0.1f, 0.1f);
        playerRigidbody.AddForce(forceVector);
        //gameObject.SetActive(false);
    }
}

