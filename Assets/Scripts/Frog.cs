using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Enemy
{
    private Rigidbody2D rb;
    private Collider2D coll;
    
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;


    [SerializeField] private float jumpLength = 2f;
    [SerializeField] private float jumpHeight = 2f;

    [SerializeField] private LayerMask ground;

    private bool facingLeft = true;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        //Transition from jump to fall
        if(anim.GetBool("Jumping"))
        {
            if(rb.velocity.y < 0.1f)
            {
                anim.SetBool("Falling", true);
                anim.SetBool("Jumping", false);
            }
        }

        if(coll.IsTouchingLayers(ground) && anim.GetBool("Falling"))
        {
            anim.SetBool("Falling", false);
        }
    }

    private void Move()
    {
        if(facingLeft)
        {   
            //Check if frog is beyond left cap
            if(transform.position.x > leftCap)
            {
                //Check if frog's sprite is facing right, if not make face right
                if(transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }

                //Check if frog is touching ground, if so jump
                if(coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(-jumpLength, jumpHeight);
                    anim.SetBool("Jumping", true);
                }
            }
            else
            {
                facingLeft = false; //if frog isn't beyond leftcap face right
            }

        }
        else
        {
            //Check if frog is beyond right cap
            if(transform.position.x < rightCap)
            {
                //Check if frog's sprite is facing left, if not make face left
                if(transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }

                //Check if frog is touching ground, if so jump
                if(coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(jumpLength, jumpHeight);
                    anim.SetBool("Jumping", true);
                }
            }
            else
            {
                facingLeft = true; //if frog isn't beyond rightcap face left
            }
        }
    }
}
