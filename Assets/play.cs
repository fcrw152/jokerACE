using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class play : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        move();
    }
    void move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        transform.Translate(Vector2.right * h * 3 * Time.fixedDeltaTime);
        transform.Translate(Vector2.up * v * 3 * Time.fixedDeltaTime);

    }
}
