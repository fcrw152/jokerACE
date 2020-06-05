using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss2 : MonoBehaviour
{
    public float Hp;
    public Collider2D Badycollider2d;
    public Collider2D Ballcollider2d;
    Animator Boss2animator;
    Rigidbody2D Bossrigidbady2d;
    controller con;
    public GameObject door;
    int x = 0;
    float changetime=0;
    float Ctime = 4;
    public bool shake; 
    // Start is called before the first frame update
    void Start()
    {
        Hp = 50;
        Boss2animator = GetComponent<Animator>();
        con = GameObject.Find("player").GetComponent<controller>();
        Bossrigidbady2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Bossrigidbady2d.velocity.y>2|| Bossrigidbady2d.velocity.y<-0.5f)
        {
            Vector2 pos = new Vector2();
            pos = Bossrigidbady2d.position;
            pos.x = pos.x + -1 * Time.deltaTime;
            Bossrigidbady2d.position = pos;
        }
        if (Bossrigidbady2d.velocity.y < -7) { Boss2animator.SetTrigger("isground"); }
        if (Hp <= 0) { Boss2animator.SetTrigger("isdead"); StartCoroutine(delete());door.SetActive(false); return;  }
        RaycastHit2D raycastHit2;
        raycastHit2 = Physics2D.Raycast(Bossrigidbady2d.position,Vector2.down,2.1f,LayerMask.GetMask("ground"));
        if (changetime < Ctime)
        {
            changetime += Time.deltaTime;
        }
        else 
        {
            if (x == 0) { x = Random.Range(0, 4); } else { x = Random.Range(0, 3); }
            changetime = 0; Debug.Log(changetime + "    111111111111111111"); Debug.Log(x + "    22222222222222222"); Debug.Log(changetime + "    3333333333333");
        }
        //后跳移动
        if (x == 1)
        {
            x = 0;
            if (raycastHit2)
            {
                Bossrigidbady2d.velocity = new Vector2(-0, 8);
                //Bossrigidbady2d.AddForce(new Vector2(40, 40));不能用力
                Boss2animator.SetTrigger("isjump"); Boss2animator.ResetTrigger("isground");
                StartCoroutine(jump());
            }
        }
        if (x == 2)
        {

            Boss2animator.SetTrigger("isattack1");
            StartCoroutine(attack1());
        }
        if (x == 3)
        {
            shake = true;
            Boss2animator.ResetTrigger("isattack2stop");
            Boss2animator.SetTrigger("isattack2");
            StartCoroutine(attack2());
        }
        
    }
    public Rigidbody2D atk1effect;
    public Rigidbody2D atk2effect;
    IEnumerator jump()
    {
        yield return new WaitForSeconds(0.1f);
             Boss2animator.ResetTrigger("isjump");
        yield return new WaitForSeconds(0.9f);
        Bossrigidbady2d.velocity = Vector2.zero;
    }
    IEnumerator attack1()
    {
        x = 0;
        GameObject obj;
        Vector2 pos;
        pos.x = transform.position.x + 2;
        pos.y= transform.position.y -1.5f;  
        yield return new WaitForSeconds(1.6f);
        obj = Instantiate(atk1effect.gameObject, pos,Quaternion.identity); Boss2animator.ResetTrigger("isattack1");
        yield return new WaitForSeconds(4f);
        Destroy(obj);
        
    }
    IEnumerator attack2()
    {
        x = 0;
        yield return new WaitForSeconds(1.5f);
        GameObject obj;
        Vector2 pos;
        pos.x = Random.Range(90,122);
        pos.y = 11;
        obj = Instantiate(atk2effect.gameObject, pos, Quaternion.identity); Boss2animator.ResetTrigger("isattack1");
        yield return new WaitForSeconds(0.5f);
        GameObject obj1;
        Vector2 pos1;
        pos1.x = Random.Range(90, 122);
        pos1.y = 11;
        obj1 = Instantiate(atk2effect.gameObject, pos1, Quaternion.identity); Boss2animator.ResetTrigger("isattack1");
        yield return new WaitForSeconds(0.5f);
        GameObject obj2;
        Vector2 pos2;
        pos2.x = Random.Range(90, 122);
        pos2.y = 11;
        obj2 = Instantiate(atk2effect.gameObject, pos2, Quaternion.identity); Boss2animator.ResetTrigger("isattack1");
        yield return new WaitForSeconds(0.5f);
        GameObject obj3;
        Vector2 pos3;
        pos3.x = Random.Range(90, 122);
        pos3.y = 11;
        obj3 = Instantiate(atk2effect.gameObject, pos3, Quaternion.identity); Boss2animator.ResetTrigger("isattack1");
        yield return new WaitForSeconds(0.5f);
        GameObject obj4;
        Vector2 pos4;
        pos4.x = Random.Range(90, 122);
        pos4.y = 11;
        obj4 = Instantiate(atk2effect.gameObject, pos4, Quaternion.identity); Boss2animator.ResetTrigger("isattack1");
        yield return new WaitForSeconds(1f);
        Boss2animator.ResetTrigger("isattack2");
        Boss2animator.SetTrigger("isattack2stop");
        shake = false;

    }
    IEnumerator jitui(int zhenshu)
    {
        Bossrigidbady2d.velocity = new Vector2(con.dir * 10, 0);
        for (int i = 0; i < zhenshu; i++)
        {
            yield return null;
        }
        Bossrigidbady2d.velocity = Vector2.zero;

    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(jitui(4));

            
            Hp -= con.atk;
            if (con.soul < 90) { con.soul += 10; } else { con.soul = 100; }
            con.image6.fillAmount = con.soul / 100f;



        }
    }
    IEnumerator delete()
    {

        yield return new WaitForSeconds(3);
         Destroy(gameObject); 
    }

}
