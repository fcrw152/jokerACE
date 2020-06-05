using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shake : MonoBehaviour
{
    controller con;
    Animator animator;
    boss2 boss2copy;
    // Start is called before the first frame update
    void Start()
    {
        con = GameObject.Find("player").GetComponent<controller>();
        animator = GetComponent<Animator>();
        boss2copy = GameObject.Find("boss2").GetComponent<boss2>();
    }

    // Update is called once per frame
    void Update()
    {
        if (con.isshake) { animator.SetTrigger("isshake"); }
        else { animator.ResetTrigger("isshake"); }
        if(boss2copy.shake) { animator.SetTrigger("ismaxshake"); }
        else { animator.ResetTrigger("ismaxshake"); animator.SetTrigger("ismaxstop"); }
        
    }
}
