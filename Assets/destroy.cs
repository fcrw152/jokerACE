using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy : MonoBehaviour
{
   public float hp = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            hp -= 1;
            if (hp <= 0) { Destroy(gameObject); }
            Debug.Log("enter");
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            hp -= 1;
            if (hp <= 0) { Destroy(gameObject); }
            Debug.Log("stay");
        }
    }
}
