using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Camera cam;
    Vector2 movement;
    Vector2 mousepos;
    // Start is called before the first frame update
    void Start()
    {

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousepos = cam.ScreenToViewportPoint(Input.mousePosition);
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(rb.position * movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousepos - rb.position;
    }
}
