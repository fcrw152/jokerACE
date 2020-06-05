using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soulstk : MonoBehaviour
{
    public float speed = 1000;
    controller con;
    // Start is called before the first frame update
    void Start()
    {
        con = GameObject.Find("player").GetComponent<controller>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right *con.souldir* speed * Time.deltaTime, Space.World);
    }
}
