using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class enemyAi3 : MonoBehaviour
{
    [Range(0, 10)]
    public float AlertRadius;//半径
    [Range(0, 360)]
    public float Alertangle;//角度
    private void OnDrawGizmos()
    {
        Color color = Handles.color;
        Handles.color = Color.blue;
        Vector3 StartLine = Quaternion.Euler(0, -Alertangle, 0) * this.transform.forward;
        Handles.DrawSolidArc(this.transform.position, this.transform.up, StartLine, Alertangle, AlertRadius);
        Handles.color = color;
    }
    Rigidbody2D enemy3;
    public GameObject knight;
    public float healthy;//血量
    public float patroldistance;//巡逻距离
    public float pursuitdistance;//追击距离
    public float atkdistance;//攻击距离
    public float aktdamage;//攻击伤害
    public bool isrefresh;//是否会刷新
    public float movespeed;//移动速度
    public float pursuitspeed;//追击速度
    public bool isfind;//是否发现敌人
    public float lasttime;//上一次变更行动时间
    public float loadtime=2;//间隔时间
    // Start is called before the first frame update
    private void Awake()
    {
        healthy = 3;
        isrefresh = false;
        isfind = false;
        patroldistance = 3;
    }
    void Start()
    {
        enemy3 = gameObject.GetComponent<Rigidbody2D>();
        lasttime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, knight.transform.position) > patroldistance)
        {
            if (Time.time - lasttime > loadtime)
            {
                lasttime = Time.time;
                int random= Random.Range(0, 2);
                switch (random)
                {
                    case 0:
                        break;
                    case 1:
                        Vector3 rotate = transform.eulerAngles;
                        rotate.y = rotate.y + 180;
                        transform.eulerAngles = rotate;
                        enemy3.velocity = new Vector2(movespeed, 0);
                        movespeed = movespeed * -1;
                        break;
                    case 2:
                        break;
                }
            }
            
        }
        else
            {
                transform.LookAt(knight.transform);
                transform.Translate(Vector3.forward * Time.deltaTime * 2);
            }
    }
}
