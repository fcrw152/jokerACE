using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rise : MonoBehaviour
{
    public Animator risedoor; 
    // Start is called before the first frame update
    void Start()
    {
        risedoor=GameObject.Find("risedoor").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        risedoor.SetBool("isrise", true);
        GameObject.Find("boss2").GetComponent<boss2>().enabled = true;
    }
}
