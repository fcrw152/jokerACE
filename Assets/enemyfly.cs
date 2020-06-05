using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class enemyfly : MonoBehaviour
{
    Rigidbody2D enemyfly1;
    public AIPath aipath;
    controller con;
    float healthy;
    inventory inv;
    // Start is called before the first frame update
    void Start()
    {
        enemyfly1 = GetComponent<Rigidbody2D>();
        healthy = 6;
        con = GameObject.Find("player").GetComponent<controller>();
        inv = GameObject.Find("itemdatabase").GetComponent<inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (aipath.desiredVelocity.x > 0) { transform.localScale = new Vector3(-2,2,2); }
        if (aipath.desiredVelocity.x < 0) { transform.localScale = new Vector3(2, 2,2); }
    }
    IEnumerator jitui(int zhenshu)
    {
        enemyfly1.velocity = new Vector2(con.dir * 10, 0);
        for (int i = 0; i < zhenshu; i++)
        {
            yield return null;
        }
        enemyfly1.velocity = Vector2.zero;

    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(jitui(4));
            Debug.Log(inv.items[1].Atk + "``````````````");
            healthy -= (con.atk + inv.items[1].Atk);
            if (healthy <= 0) { Destroy(gameObject); }
        }
    }
}
