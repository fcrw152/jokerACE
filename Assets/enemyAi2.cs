using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAi2 : MonoBehaviour
{
    controller knight;
    public float healthy;//血量
    public float movespeed;//速度
    public GameObject[] points;
    int point=1;
    float distance;
    Rigidbody2D enemy2;
    // Start is called before the first frame update
    void Start()
    {
        healthy =4;
        knight = GameObject.Find("player").GetComponent<controller>();
        enemy2 = GetComponent<Rigidbody2D>();
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
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            healthy -= knight.atk;
            if (healthy <= 0) { Destroy(gameObject); }
            Debug.Log("stay");
            StartCoroutine(jitui(4));
        }
    }
    IEnumerator jitui(int zhenshu)
    {
        enemy2.velocity = new Vector2(knight.dir * 10, 0);
        for (int i = 0; i < zhenshu; i++)
        {
            yield return null;
        }
        enemy2.velocity = Vector2.zero;
    }
}
