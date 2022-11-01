using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void JumpedOn()
    {
        anim.SetTrigger("Death");
        rb.velocity = Vector2.zero;
    }

    public void Death()
    {
        Destroy(this.gameObject);
    }
}
