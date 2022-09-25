using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //Start variables
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;

    [SerializeField] private int cherries = 0;
    [SerializeField] private TextMeshProUGUI cherryText;

    //State machine
    private enum State {idle, running, jumping, falling}
    private State state = State.idle;

    //Inspector Variables
    [SerializeField] private LayerMask ground;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        Movement();

        AnimationState();
        anim.SetInteger("state", (int)state); //set animation based on enum state
    }

    //When player collects cherries
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Collectable")
        {
            Destroy(collision.gameObject);
            cherries++;
            cherryText.text = cherries.ToString();
        }
    }

    //Player movement
    private void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");

        //moving left
        if(hDirection < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }
        //moving right
        else if(hDirection > 0 )
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            
        }

        //jumping
        if(Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            state = State.jumping;
        }
    }

    /*checks if player is jumping to change to falling state, when player touches ground change to idle state, 
    if player is not mid-air then check horizontal velocity and change to running state*/
    private void AnimationState()
    {
        if(state == State.jumping)
        {
             if(rb.velocity.y < 0.1f)
             {
                state = State.falling;
             }
        }
        else if(state == State.falling)
        {
            if(coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }

        else if(Mathf.Abs(rb.velocity.x) > 2f)
        {
            //Moving
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
    }
}
