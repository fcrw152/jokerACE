using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss1 : MonoBehaviour
{
    Rigidbody2D bossbody;
    public Animator boss;
    float time=0;
    public float HP=20;
    float changeTime=2;
    int i;int x;
    Transform playerPos;
    controller con;
    float speed=2;
    public Rigidbody2D n1;
    Rigidbody2D n1copy;
    public Rigidbody2D n2;
    Rigidbody2D n2copy;
    public Rigidbody2D n3;
    Rigidbody2D n3copy;
    public Rigidbody2D n4;
    Rigidbody2D n4copy;
    public Rigidbody2D n5;
    Rigidbody2D n5copy;
    public Rigidbody2D n6;
    Rigidbody2D n6copy;
    bool isdanger=false;
    AudioSource audioSource;
   public AudioClip clip1;
    public AudioClip clip2;
   public GameObject effect;
    public GameObject soul1;
    public GameObject magic;
    // Start is called before the first frame update
    void Start()
    {
        boss = GetComponent<Animator>();
        boss.ResetTrigger("isidel");
        boss.ResetTrigger("isjump");
        con = GameObject.Find("player").GetComponent<controller>();
       
        HP = 20;
        bossbody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();effect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0) { boss.SetTrigger("isdead");return; }
        if (HP < 10)
        {
            boss.SetTrigger("istwostate");
        }
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Vector2 tre = new Vector2(playerPos.position. x+72.52f, transform.position.y);
        if (time < changeTime) { time += Time.deltaTime; }
        else {
             i = Random.Range(0, 2);
             x = Random.Range(0, 10); time = 0;         
            }     
            if (i == 0)
                {
                    boss.SetTrigger("isidel"); boss.ResetTrigger("isjump");
                    if (x == 0 &&HP<10 ) { transform.position =new Vector2(31.5f, transform.position.y);boom1(); Invoke("delete1",1.5f);time = -3;}
                    if (x == 9 && HP < 10) { transform.position = new Vector2(1.5f, transform.position.y); boom2(); Invoke("delete2", 1.5f); time = -3; }
        }
            else
                {
                  boss.ResetTrigger("isidel"); boss.SetTrigger("isjump");
                  transform.position = Vector2.MoveTowards(transform.position,tre,  speed * Time.deltaTime);
                }
        
        
       
    }
    void boom1()
    {
        
        n1copy = Instantiate(n1, new Vector2(-71, 12), Quaternion.identity);
        n2copy = Instantiate(n2, new Vector2(-65, 12), Quaternion.identity);
        n3copy = Instantiate(n3, new Vector2(-45, 12), Quaternion.identity);
        n4copy = Instantiate(n4, new Vector2(-61, 12), Quaternion.identity);
        n5copy = Instantiate(n5, new Vector2(-48, 12), Quaternion.identity);
        x = 1;
    }
    void boom2()
    {
        n2copy = Instantiate(n2, new Vector2(-65, 12), Quaternion.identity);
        n3copy = Instantiate(n3, new Vector2(-45, 12), Quaternion.identity);
        n4copy = Instantiate(n4, new Vector2(-61, 12), Quaternion.identity);
        n5copy = Instantiate(n5, new Vector2(-48, 12), Quaternion.identity);
        n6copy = Instantiate(n6, new Vector2(-42, 12), Quaternion.identity);
        x = 1;
    }
    void delete1()
    {
        Destroy(n1copy.gameObject);
        Destroy(n2copy.gameObject);
        Destroy(n3copy.gameObject);
        Destroy(n4copy.gameObject);
        Destroy(n5copy.gameObject);
    }
    void delete2()
    {
        Destroy(n2copy.gameObject);
        Destroy(n3copy.gameObject);
        Destroy(n4copy.gameObject);
        Destroy(n5copy.gameObject);
        Destroy(n6copy.gameObject);
    }
    IEnumerator jitui(int zhenshu)
    {
        bossbody.velocity = new Vector2(con.dir * 10, 0);
        for (int i = 0; i < zhenshu; i++)
        {
            yield return null;
        }
        bossbody.velocity = Vector2.zero;

    }
   public GameObject obj1;
    public GameObject obj2;
    IEnumerator destroy()
    {
        yield return new WaitForSeconds(1f);
        audioSource.PlayOneShot(clip2);
        yield return new WaitForSeconds(0.28f);
        effect.SetActive(true);
        obj1 = Instantiate(soul1,new Vector2(-41.7f,1.1f), Quaternion.identity);
        obj2 = Instantiate(magic, new Vector2(-40.1f, 1.1f), Quaternion.identity);
        yield return new WaitForSeconds(0.28f);
        gameObject.SetActive(false); 
    }
   
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            //StartCoroutine(jitui(4));

            HP -= con.atk; /*(con.atk + inv.items[1].Atk)*/;
            if (HP <= 0) { audioSource.PlayOneShot(clip1); StartCoroutine(destroy()); }
            if (con.soul < 90) { con.soul += 10; } else { con.soul = 100; }
            con.image6.fillAmount = con.soul / 100f;
        }
    }
}
