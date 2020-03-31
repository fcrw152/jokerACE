using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{

    float dir;//角色的朝向
    Vector2 move;
    Animator playanimator;
    Rigidbody2D playrigibody2d;
    private bool isground;
    public Transform feet;
    public float feetradius;
    public LayerMask whatisground;
    public bool issprush=false;
    public float sprush = 0;

    float defendtime=5;
    bool isdefend;
    float jumpcount=0;
    public float atk=1;//玩家攻击力
    public float maxhealth=5;//最大血量
    public float cuhealth;//当前血量
    public Vector2 xspeed;//用于表示x轴方向的速度
    public Vector2 yspeed;//用于表示y轴方向的速度
    public float force;//y轴的加速 
    public float gravity;//自定重力
    public LayerMask whatisplayer;//获取碰撞层
    public Vector2 boxsize;//碰撞盒大小
    public float timeJump;//真跳跃的时间
    public float jumpTime;//最大的跳跃时间
    public GameObject attack1;
    public GameObject attack2;
    public GameObject attack3;//三种攻击的特效;
    public float rushtime;//冲刺准备时间
    public float storage;//蓄力剑气时间
    public GameObject image1;
    AudioSource audioSource;//
    public AudioClip clip1;
    public AudioClip clip2;
    public float soul;//恢角色拥有的灵魂
    public float money;//角色所拥有的金币
    // Start is called before the first frame update
    void Start()
    {
        
        playanimator = GetComponent<Animator>();
        playrigibody2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        dir = -1;
        attack1.SetActive(false);
        attack2.SetActive(false);
        attack3.SetActive(false);
        cuhealth = maxhealth;
        isdefend = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        changespeed();
        jump();
        attack();
        rush();
       
        hpup();
        //Debug.Log(playrigibody2d.position.y);
        if (isground) { playanimator.SetTrigger("isground"); }
        nextmove();
        if (isdefend) { defendtime -= Time.deltaTime; }
        if (defendtime < 0) { isdefend = false; }
    }
    public void Move()
    {
        float h = Input.GetAxis("Horizontal");
        if (dir < 0 && h > 0) { transform.Rotate(0, 180, 0); dir = 1;playanimator.SetTrigger("isrotate"); }
        else if (dir > 0 && h < 0) { transform.Rotate(0, -180, 0); dir = -1; playanimator.SetTrigger("isrotate"); }
        else if (dir <0&&h < 0) { playanimator.SetBool("isrun", true); }
        else if (dir > 0 && h > 0) { playanimator.SetBool("isrun", true); }
        if (h == 0)
        {
            playanimator.SetTrigger("isstop");
            playanimator.ResetTrigger("isrotate");
            playanimator.SetBool("isrun", false);
        }
        else { playanimator.ResetTrigger("isstop"); }
        if (Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.D)) { audioSource.PlayOneShot(clip2); }
        if (h == 0) { }
    }
    //起跳后改变速度
    public void changespeed()
    {
        
        if (isground) { yspeed.y = 0; }
        else { yspeed.y += -1 * gravity * Time.deltaTime; }
    }
    public bool checkground ()
    {
        
        float aryDistance = boxsize.y * 0.5f ;
        RaycastHit2D hit2D = Physics2D.BoxCast(transform.position, boxsize, 0, Vector2.down, 5f, whatisplayer);
        Debug.DrawLine(transform.position, transform.position + Vector3.down * aryDistance, Color.red, 6.0f);
        if (hit2D.collider != null)
        {

            float tempDistance = Vector2.Distance(transform.position, hit2D.point);
            if (tempDistance > (boxsize.y * 0.5f+0.05f ))
            {
                //transform.position += new Vector3(0, moveDistance.y, 0);
                return false;
            }
            else
            {
                
                return true;

                
            }
        }
        else
        {
            return false;
        }
    }
    public void jump()
    {
        
        if (isground == true && Input.GetKeyDown(KeyCode.Space))
        {Debug.Log(isground);
            yspeed.y += force;
            playanimator.SetTrigger("isjump");
            jumpcount += 1;
            timeJump = 0;
        }
        if (Input.GetKey(KeyCode.Space)&&jumpcount<=1)
        {
            Debug.Log(jumpcount);
            timeJump += Time.deltaTime;
            if (timeJump < jumpTime)
            {
                yspeed.y += force;
            }
              
        }
        if (Input.GetKeyUp(KeyCode.Space)) { timeJump = 0; }
        
        if (isground)
        { jumpcount = 0; }
    }
    public void nextmove() {
        int LookDir = 0;//定义移动的方向
            xspeed.x = 7;
            Vector2 movedistancex = xspeed * Time.deltaTime;
            Vector2 movedistancey = yspeed * Time.deltaTime;//下一帧的移动距离
        float h = Input.GetAxis("Horizontal");
       
        transform.position = transform.position + new Vector3(movedistancex.x*h,0 , 0);
        
        /////////////////////////////////////////////////////////////
        if (yspeed.y > 0) { LookDir = 1; }
            if (yspeed.y < 0) { LookDir = -1; }
            if (LookDir != 0)
            {
                RaycastHit2D box1 = Physics2D.BoxCast(transform.position, boxsize, 0, Vector2.up * LookDir, 5.0f, whatisplayer);
            if (box1.collider != null)
            {
                float Y1 = (float)System.Math.Round(box1.point.y, 1);//计算出发生碰撞的点的位置
                float distanceY1 = System.Math.Abs(Y1 - transform.position.y);
                if (distanceY1 > boxsize.y * 0.5f)
                {

                    float Y2 = 0;
                    float Y3 = 0;
                    Y3 = transform.position.y + movedistancey.y;//一帧后的位置的y值
                    if (LookDir > 0)
                    {
                        Y2 = Y1 - 0.5f * boxsize.y;//碰撞点减去半个身位，可上升的最大高度
                        if (Y3 <= Y2)//可上升空间大于一帧的上升距离
                        {
                           
                            transform.position = transform.position + new Vector3(0, movedistancey.y, 0);                            
                        }
                        if (Y3 > Y2)
                        { 
                            transform.position = new Vector3(transform.position.x, Y2, 0);
                        }
                    }
                    else
                    {
                        Y2 = Y1 + (0.5f * boxsize.y);//下降时Y1是负的，所以用加，距离地面的高度方向为-1
                        if (Y3 >= Y2)
                        {
                            transform.position = transform.position + new Vector3(0, movedistancey.y, 0);

                        }
                        if (Y3 < Y2)
                        {     
                            transform.position = new Vector3(transform.position.x, Y2, 0);
                        }
                        isground = false;
                    }
                }
            }
            else
            {
               
                transform.position = transform.position + new Vector3(0, movedistancey.y, 0);
            }
            }
            isground = checkground();
        
    }
    public void attack()
    {
        
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (Input.GetKey(KeyCode.W))//向上攻击
            {
                playanimator.SetTrigger("isattackup");
                StartCoroutine(Lookattack(attack2, 4));
                audioSource.PlayOneShot(clip1);
                return;
            }
            if (Input.GetKey(KeyCode.S) && !isground)//向下攻击
            {

            }
            else//左右攻击
            {
                playanimator.SetTrigger("isattack");
                StartCoroutine(Lookattack(attack1, 4));
                
            }
            audioSource.PlayOneShot(clip1);
        }
    }
    IEnumerator Lookattack(GameObject attack, int zhenshu)
    {
        attack.SetActive(true);
        for (int i = 0; i < zhenshu; i++)
        {
            yield return null;
        }
        attack.SetActive(false);
    }
    public void changehealth(float value)
    {
        if (isdefend) { return; } 
        if (isdefend == false) { cuhealth = cuhealth + value; isdefend = true; defendtime = 5;image1.SetActive(true); }
    }
    public void rush()
    {
        if (Input.GetKeyDown(KeyCode.Z)) { playanimator.SetBool("issprushstop", false); playanimator.SetBool("issprushready",true); }
        
        else if (Input.GetKey(KeyCode.Z))
        {
            playanimator.SetBool("issprushstop", false);
            playanimator.SetBool("issprushready", true);
            rushtime = rushtime + Time.deltaTime; 
            
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            
            if (rushtime < 2) { playanimator.SetBool("issprushstop", true);  }
            if (rushtime >= 2)
            {
               
                playrigibody2d.velocity=new Vector2(20*dir,0); rushtime = rushtime + Time.deltaTime;issprush = true; playanimator.SetBool("issprushready", false); playanimator.SetBool("issprushfly",true);
              
            }
            playanimator.SetBool("issprushready", false); rushtime = 0;
        }
        if (issprush)
            {
                   sprush = sprush + Time.deltaTime;
                if (sprush >= 1)
                { playrigibody2d.velocity = Vector2.zero; playanimator.SetBool("issprushstop", true); playanimator.SetBool("issprushfly", false); sprush = 0; issprush = false; }
            }
            
            
    }
   
    public void hpup()
    {
        if (cuhealth < maxhealth)
        { 
            if (Input.GetKeyDown(KeyCode.Q))
            {
                cuhealth = cuhealth + 1;
                image1.SetActive(false);
            }
        }
    }
}
