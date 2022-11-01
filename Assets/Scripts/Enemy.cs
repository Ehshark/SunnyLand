using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anim;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void JumpedOn()
    {
        anim.SetTrigger("Death");
    }

    public void Death()
    {
        Destroy(this.gameObject);
    }
}