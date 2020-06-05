using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atk2effect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "emeny"|| collision.gameObject.tag == "ground") { StartCoroutine(delete()); }
    }
    IEnumerator delete()
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }
}
