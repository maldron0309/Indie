using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float velocidad;
    public float forceJump;
    public int jumpsMax;
    public LayerMask MaskFlood;

    private new BoxCollider2D boxCollider;
    private new Rigidbody2D rigidbody;
    private bool WatchRight;
    private int jumpsRestants;
    // Start is called before
    // the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        jumpsRestants = jumpsMax;
    }

    // Update is called once per frame
    void Update()
    {
        mecanicaMovimiento();
        processJump();
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

            if (Input.GetKeyDown(KeyCode.Space) && jumpsRestants>0) {
            jumpsRestants--;
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0f);
            rigidbody.AddForce(Vector2.up * forceJump, ForceMode2D.Impulse);
        
        }
    
    }




    void mecanicaMovimiento()
    {
        float inputMovimiento = Input.GetAxis("Horizontal");
        rigidbody.velocity = new Vector2(inputMovimiento * velocidad, rigidbody.velocity.y);
        GestionarOrientacion(inputMovimiento);
    }


    void GestionarOrientacion(float inputMovimiento) {
        if( (WatchRight == true && inputMovimiento > 0)|| (WatchRight == false && inputMovimiento < 0) ){
            WatchRight = !WatchRight;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }


    }
}
