using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemonworld : MonoBehaviour
{
    
    // Start is called before the first frame update
    public int id;
    public inventory inv;
    void Start()
    {
        inv = GameObject.Find("itemdatabase").GetComponent<inventory>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.tag == "Player")
        {
            inv.Additem(id);
            Destroy(gameObject);
            
        }
            
        
    }
}
