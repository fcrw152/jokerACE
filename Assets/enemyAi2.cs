using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAi2 : MonoBehaviour
{
    public float movespeed;
    public GameObject[] points;
    int point=1;
    float distance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, points[point].transform.position);
        transform.position = Vector2.MoveTowards(transform.position, points[point].transform.position,movespeed*Time.deltaTime);
        if (distance < 0.1) { Turn(); }
    }
    public void Turn()
    {
        Vector3 rotate= transform.eulerAngles;
        rotate.z = rotate.z + points[point].transform.eulerAngles.z;
        transform.eulerAngles= rotate;
        point++;
        if (point == points.Length) { point = 0; }
    }
}
