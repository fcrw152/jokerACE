using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class enemyAi3 : MonoBehaviour
{
    [Header("Scanning settings")]
    [Tooltip("The angle of the forward of the view cone. 0 is forward of the sprite, 90 is up, 180 behind etc.")]
    [Range(0.0f, 360.0f)]
    public float viewDirection = 0.0f;
    [Range(0.0f, 360.0f)]
    public float viewFov;
    public float viewDistance;
    [Tooltip("Time in seconds without the target in the view cone before the target is considered lost from sight")]
    public float timeBeforeTargetLost = 3.0f;
    //[Range(0, 10)]
    //public float AlertRadius;//半径
    //[Range(0, 360)]
    //public float Alertangle;//角度
    //private void OnDrawGizmos()
    //{
    //    Color color = Handles.color;
    //    Handles.color = Color.blue;
    //    Vector3 StartLine = Quaternion.Euler(0, -Alertangle, 0) * this.transform.forward;
    //    Handles.DrawSolidArc(this.transform.position, this.transform.up, StartLine, Alertangle, AlertRadius);
    //    Handles.color = color;
    //}
    [Header("1111111111")]
    Rigidbody2D enemy3;
    Animator enemyanimator;
    public GameObject knight;
    public GameObject left, right;
    public bool faceleft;//面向
    public float healthy;//血量
    public float patroldistance;//巡逻距离
    public float pursuitdistance;//追击距离
    public float atkdistance;//攻击距离
    public float aktdamage;//攻击伤害
    public bool isrefresh;//是否会刷新
    public float movespeed;//移动速度
    public float pursuitspeed=2;//追击速度
    public bool isfind;//是否发现敌人
    public float lasttime;//上一次变更行动时间
    public float loadtime=2;//间隔时间
    public controller con;
    public inventory inv;
    // Start is called before the first frame update
    private void Awake()
    {
        
        isrefresh = false;
        isfind = false;
        patroldistance = 3;
        faceleft = true;
        healthy = 6;
    }
    void Start()
    {
        enemyanimator = gameObject.GetComponent<Animator>();
        enemy3 = gameObject.GetComponent<Rigidbody2D>();
        lasttime = 0;
        con = GameObject.Find("player").GetComponent<controller>();
        inv = GameObject.Find("itemdatabase").GetComponent<inventory>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        move();
    }
    void move()
    {
        RaycastHit2D raycastHit2d;
        if (faceleft)
        {
            raycastHit2d = Physics2D.Raycast(enemy3.position-new Vector2(1f,0), new Vector2(-1, 0), 2.5f, LayerMask.GetMask("player"));
            
        }
        else
        {
            raycastHit2d = Physics2D.Raycast(enemy3.position + new Vector2(1f, 0), new Vector2(1, 0), 2.5f, LayerMask.GetMask("player"));
            
        }
        if (raycastHit2d.collider==null)
        {
            enemyanimator.SetBool("isfind", false);
            
            float distance1 = Vector2.Distance(enemy3.transform.position, left.transform.position);
            float distance2 = Vector2.Distance(enemy3.transform.position, right.transform.position);
            
            if (faceleft)
                {
                   transform.position = Vector2.MoveTowards(transform.position, left.transform.position, movespeed * Time.fixedDeltaTime);
                   if (distance1<0.4)
                   {         
                   Vector3 rotate = enemy3.transform.eulerAngles;
                    rotate.y = rotate.y + 180;
                  enemy3.transform.eulerAngles = rotate;
                    faceleft = false;         
                   }
                 }
             if (!faceleft)
                 {
                   transform.position = Vector2.MoveTowards(transform.position, right.transform.position, movespeed * Time.fixedDeltaTime);
                   if (distance2<0.4)
                   {
                    Vector3 rotate = transform.eulerAngles;
                    rotate.y = rotate.y -180;
                    transform.eulerAngles = rotate;
                    faceleft = true;
                   }
                  }
            }
        else
            {
            enemyanimator.SetBool("isfind", true);
           
            if (raycastHit2d.collider != null)
            {

                if (faceleft)
                {
                     enemy3.transform.Translate(Vector2.left*pursuitspeed*Time.fixedDeltaTime);
                }
                else
                {
                    enemy3.transform.Translate(Vector2.left * pursuitspeed * Time.fixedDeltaTime);
                }
            }
        }
    }
    //public void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (Input.GetKeyDown(KeyCode.K))
    //    {
    //        healthy -= con.atk;
            
    //        if (healthy <= 0) { Destroy(gameObject); }
            
    //    }
    //}
    IEnumerator jitui( int zhenshu)
    {
        enemy3.velocity = new Vector2(con.dir*10, 0);
        for (int i = 0; i < zhenshu; i++)
        {
            yield return null;
        }
        enemy3.velocity = Vector2.zero;

    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(jitui(4));

            StartCoroutine(delete(4));
            healthy -= con.atk;
            if (con.soul < 90) { con.soul += 10; } else { con.soul = 100; }
            con.image6.fillAmount = con.soul / 100f;
            
            
            
        }
    }
    IEnumerator delete(int zhenshu)
    {
        for (int i = 0; i < zhenshu; i++)
        {
            yield return null;
        }
        if (healthy <= 0) { Destroy(gameObject); }
    }
    
}
