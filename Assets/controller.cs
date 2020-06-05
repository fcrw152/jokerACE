using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class controller : MonoBehaviour
{
    //static controller instance;
    public bool isopen1=false;
    public float dir;//角色的朝向
    Vector2 move;
    Animator playanimator;
    Rigidbody2D playrigibody2d;
    private bool isground;
    public Transform feet;
    public float feetradius;
    public LayerMask whatisground;
    public bool issprush=false;
    public float sprush = 0;

    float defendtime=2;
    bool isdefend;
    float jumpcount=0;
    public  float atk;//玩家攻击力
    public int maxhealth=5;//最大血量
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
    public GameObject flash;//冲刺准备完成的闪光
    public float storage;//蓄力剑气时间
    
    AudioSource audioSource;//
    public AudioClip clip1;
    public AudioClip clip2;
    public float soul;//恢角色拥有的灵魂
    public float money;//角色所拥有的金币
    public Animator effectanimator;//特效播放器
    public Animator effectanimator1;//特效播放器
    public Animator effectanimator2;//特效播放器
    //冲刺的紫水晶
    public GameObject shuijin1;
    public GameObject shuijin2;
    public GameObject shuijin3;
    public GameObject shuijin4;
    public GameObject shuijin5;
    public GameObject shadoweffect;//暗影特效、
    public GameObject bag;//背包
    public GameObject esc;//退出
    public GameObject twicejumpeffect;//二段跳特效
    inventory inv;
    public GameObject kivai;//刀光撞击效果
    int x = 0;
    //血量显示  需要重构
    //public GameObject image1;
    //public GameObject image2;
    //public GameObject image3;
    //public GameObject image4;
    //public GameObject image5;
   public List<Image> hps = new List<Image>();
    public Image image6;
   public GameObject loading;
    public bool isequip ;
    // Start is called before the first frame update
    //private void Awake()
    //{
    //    if (instance == null)

    //    {  instance = this; DontDestroyOnLoad(this); }
    //}
    void Start()
    {
        
        soul = 100;
        image6.fillAmount = 100/100f;
        isequip = false;
        inv = GameObject.Find("itemdatabase").GetComponent<inventory>();
        shadoweffect.SetActive(false);
        flash.SetActive(false);
        effectanimator = transform.Find("rush/cyclone").GetComponent<Animator>();
        effectanimator.gameObject.SetActive(false);
        effectanimator1 = transform.Find("rush/airflow").GetComponent<Animator>();
        effectanimator1.gameObject.SetActive(false);
        effectanimator2 = transform.Find("shadowrush1/shadow1").GetComponent<Animator>();
        effectanimator2.gameObject.SetActive(false);
        loading = GetComponent<GameObject>();
        playanimator = GetComponent<Animator>();
        playrigibody2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        dir = -1;
        attack1.SetActive(false);
        attack2.SetActive(false);
        attack3.SetActive(false);
        cuhealth = maxhealth;
        isdefend = false;
        shuijin1.SetActive(false);
        shuijin2.SetActive(false);
        shuijin3.SetActive(false);
        shuijin4.SetActive(false);
        shuijin5.SetActive(false);
        atk = 2;
    }

    // Update is called once per frame
    void Update()
    {
        Npc();
        soulatk();
        equip();
        Move();
        changespeed();
        if (isground) {jumpcount = 0;playanimator.ResetTrigger("isattackdown"); }       
        jump();
        attack();
        rush();
        shadowrush();
        openbag();
        openesc();
        hpup();
        //Debug.Log(playrigibody2d.position.y);
        if (isground) { playanimator.SetTrigger("isground"); }
        nextmove();
        if (isdefend) { defendtime -= Time.deltaTime; }
        if (defendtime < 0) { isdefend = false; }
        if (isground) { playanimator.SetTrigger("isground"); } else { playanimator.ResetTrigger("isground"); }
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
        PlayRunAudio(h);
        
        //if (Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.D)) { audioSource.PlayOneShot(clip2); }
        //if (h == 0) { }
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
    bool isjump=false;
    GameObject effectobj;
    public void jump()
    {  
        if ( Input.GetKeyDown(KeyCode.Space))
        {
            isPlayAudioState = true;
            isjump = true;
            if (!isground &&jumpcount <=1)
            { Debug.Log(jumpcount); jumpcount = 2; Debug.Log("bubian"); }
            else { jumpcount++; Debug.Log("jiajia"); }
            if (jumpcount == 1)
            {
                yspeed.y += force;
                playanimator.SetTrigger("isjump");
                play("hero_jump");
            }
            if (jumpcount == 2) { playanimator.SetTrigger("isjumptwice"); yspeed.y = force; audioSource.PlayOneShot(clip2);
                effectobj = Instantiate(twicejumpeffect, transform.position, Quaternion.Euler(180, 0, 0));
                effectobj.transform.position = effectobj.transform.position +new Vector3(0, 0.5f, 0);
                StartCoroutine(Deletetwiceeffect()); }
           timeJump = 0;
            Debug.Log(jumpcount);}
        if (Input.GetKey(KeyCode.Space)&&isjump&&jumpcount<=2)
        {
            timeJump += Time.deltaTime;
            if (timeJump < jumpTime)
            {
                yspeed.y += force;
            }
         }
        if (Input.GetKeyUp(KeyCode.Space)) { isjump = false; timeJump = 0; isPlayAudioState = false; }
        if (yspeed.y >= 0 && yspeed.y < 1) { playanimator.SetBool("isslow", true); } else { playanimator.SetBool("isslow", false); }
        if (yspeed.y < 0) { playanimator.SetBool("isstopjump", true); playanimator.SetBool("isstopjump", false);  }   
    }
    IEnumerator Deletetwiceeffect()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(effectobj);
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
                if (distanceY1 > (boxsize.y * 0.5f))
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
                            transform.position = new Vector3(transform.position.x, Y2, 0);if (isopen1) { isPlayAudioState = true; if (yspeed.y < 0) play("hero_land_soft"); }
                            StartCoroutine(resetaudio());
                        }
                        isground = false;
                    }
                }
                else { }
                
            }
            else
            {
                
                transform.position = transform.position + new Vector3(0, movedistancey.y, 0);
            }
            }
            isground = checkground();
        
    }
    IEnumerator resetaudio()
    {

        yield return new WaitForSeconds(0.05f);
        isPlayAudioState = false;

    }
    public GameObject attacklr;
    public GameObject attackup;
    public void attack()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {    /*attacklr.SetActive(true);*/
            if (Input.GetKey(KeyCode.W))//向上攻击
            {   
                playanimator.SetTrigger("isattackup");
                StartCoroutine(Lookattack(attack2, 4));
                checkattack(2);
                return;
            }
            if (Input.GetKey(KeyCode.S) && !isground)//向下攻击
            {
                playanimator.SetTrigger("isattackdown");
                StartCoroutine(Lookattack(attack3, 4));
                checkattack(3);
                return;
            }
            else//左右攻击
            {
                playanimator.SetTrigger("isattack");
                StartCoroutine(Lookattack(attack1, 4));
                checkattack(dir);
            }
        }
    }
    IEnumerator Lookattack(/*GameObject attacklr1 *//*,*/GameObject attack, int zhenshu)
    {
        attack.SetActive(true);
        for (int i = 0; i < zhenshu; i++)
        {
            yield return null;
        }
        attack.SetActive(false);
        //attacklr1.SetActive(false);
    }
    public LayerMask unplayer;
    GameObject effcetobj;
    public void checkattack(float x)//检测玩家是否攻击到了物体
    {
        
        RaycastHit2D raycasthit2d=new RaycastHit2D();
        if (x == 1) { raycasthit2d = Physics2D.Raycast(transform.position, Vector2.right, 1.7f, unplayer); }
        if (x == -1) { raycasthit2d = Physics2D.Raycast(transform.position, Vector2.right*dir, 1.7f, unplayer); }
        if (x == 2) { raycasthit2d = Physics2D.Raycast(transform.position, Vector2.up, 1.7f, unplayer); }
        if (x ==3) { raycasthit2d = Physics2D.Raycast(transform.position, Vector2.down, 1.7f, unplayer); }
        if (raycasthit2d.collider != null)
        {
            effcetobj = Instantiate(kivai, raycasthit2d.point, Quaternion.identity);
            if (x == 1 || x == -1)
            {
                if (Input.GetKeyDown(KeyCode.K)) { isPlayAudioState = true; play("sword_hit_window_1"); }
                StartCoroutine(jituiLR(4));
                effcetobj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                StartCoroutine(destorykivai());
            }
            else if (x == 2)
            {
                if (Input.GetKeyDown(KeyCode.K)) { isPlayAudioState = true; play("sword_hit_window_1"); }
                StartCoroutine(jituiUP(4));
                effcetobj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -180));
                StartCoroutine(destorykivai());
            }
            else if (x == 3)
            {
                if (Input.GetKeyDown(KeyCode.K)) { isPlayAudioState = true; play("sword_hit_window_1"); }
                StartCoroutine(jituiDOWN(4));
                effcetobj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                StartCoroutine(destorykivai());
            }
        }
        else
        {
            isPlayAudioState = true; play("sword_1");
        }
        StartCoroutine(resetaudio2());
    }
    IEnumerator jituiLR(int zhenshu)
    {
        playrigibody2d.velocity = new Vector2(-10 * dir, 0);
        for (int i = 0; i < zhenshu; i++)
        {
            yield return null;
        }
        playrigibody2d.velocity = Vector2.zero;
        
    }
    IEnumerator jituiUP(int zhenshu)
    {
       
        for (int i = 0; i < zhenshu; i++)
        {
            yield return null;

        }
        playrigibody2d.velocity = Vector2.zero;
    }
    IEnumerator jituiDOWN(int zhenshu)
    {
        gravity = 0;
        
        for (int i = 0; i < zhenshu; i++)
        {
            //playrigibody2d.velocity = new Vector2(0, 20);
            yspeed.y = 6;
            jumpcount = 0;
            yield return null;
        }
        gravity = 20;
        //playrigibody2d.velocity = Vector2.zero;
    }
    IEnumerator destorykivai()//延迟删除刀光的复制体
    {

        yield return new WaitForSeconds(0.1f);
        Destroy(effcetobj);

    }
    IEnumerator resetaudio2()//重置声音开关
    {

        yield return new WaitForSeconds(0.2f);
        isPlayAudioState = false;

    }
    public void changehealth(float value)
    {
        if (isdefend) { return; }
        if (isdefend == false) { cuhealth = cuhealth + value; isdefend = true; defendtime = 2; }
        hpimgae();
    } //sb方法
    public void hpimgae()
    {
        
        for (int i = 0; i < cuhealth; i++)
        {
            hps[i].gameObject.SetActive(false);
        }
        for (int j =(int)cuhealth; j < maxhealth; j++)
        {
            hps[j].gameObject.SetActive(true);
        }
        //if (cuhealth == 5)
        //{
        //    image1.SetActive(false);
        //    image2.SetActive(false);
        //    image3.SetActive(false);
        //    image4.SetActive(false);
        //    image5.SetActive(false);
        //}
        //if (cuhealth == 4)
        //{
        //    image1.SetActive(false);
        //    image2.SetActive(false);
        //    image3.SetActive(false);
        //    image4.SetActive(false);
        //    image5.SetActive(true);
        //}
        //if (cuhealth == 3)
        //{
        //    image1.SetActive(false);
        //    image2.SetActive(false);
        //    image3.SetActive(false);
        //    image4.SetActive(true);
        //    image5.SetActive(true);
        //}
        //if (cuhealth == 2)
        //{
        //    image1.SetActive(false);
        //    image2.SetActive(false);
        //    image3.SetActive(true);
        //    image4.SetActive(true);
        //    image5.SetActive(true);
        //}
        //if (cuhealth == 1)
        //{
        //    image1.SetActive(false);
        //    image2.SetActive(true);
        //    image3.SetActive(true);
        //    image4.SetActive(true);
        //    image5.SetActive(true);
        //}
    }
    //冲刺
    public bool isPlayreadyAudioState = false;
    public bool isPlayAudioState = false;
    public void rush()
    {
        if (inv.items[0].Iscanrush == 0)
        {
            return;
        }
        //bool isfly=false;
        //if (!isground&&!isfly)
        //{
        //    return;
        //}
        if (Input.GetKeyDown(KeyCode.Z)) {
            isPlayAudioState = true;
            playanimator.SetBool("issprushstop", false);
            playanimator.SetBool("issprushready",true);
            effectanimator.gameObject.SetActive(true);
            effectanimator.SetTrigger("iscyclone");
            play("hero_super_dash_charge");
        } 
        else if (Input.GetKey(KeyCode.Z))
        {
            rushtime = rushtime + Time.deltaTime;
            if (rushtime > 0.1) { shuijin1.SetActive(true); }
            if (rushtime > 0.3) { shuijin2.SetActive(true); }
            if (rushtime > 0.5) { shuijin3.SetActive(true); }
            if (rushtime > 0.7) { shuijin4.SetActive(true); }
            if (rushtime > 0.9) { shuijin5.SetActive(true); }
            if (rushtime > 1.2&&!isPlayreadyAudioState) { isPlayreadyAudioState = true;  play("hero_super_dash_ready"); StartCoroutine(Lookflash(flash, 6)); } 
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            isPlayreadyAudioState = false;
            shuijin1.SetActive(false);shuijin2.SetActive(false);shuijin3.SetActive(false);shuijin4.SetActive(false);shuijin5.SetActive(false);
            effectanimator.gameObject.SetActive(false); effectanimator.SetTrigger("iscyclonestop");
            if (rushtime < 1.2) { playanimator.SetBool("issprushstop", true);  }
            if (rushtime >= 1.2)
            {
                effectanimator1.gameObject.SetActive(true);
                play("hero_super_dash_loop");
                gravity = 0;
                playrigibody2d.velocity=new Vector2(20*dir,0); rushtime = rushtime + Time.deltaTime;issprush = true; playanimator.SetBool("issprushready", false); playanimator.SetBool("issprushfly",true);
              
            }
            playanimator.SetBool("issprushready", false); rushtime = 0;
        }
        if (issprush)
            {
                   sprush = sprush + Time.deltaTime;
                if (sprush >= 1)
                {
                audioSource.Stop();
                effectanimator1.gameObject.SetActive(false);
                isPlayAudioState = false;
                playrigibody2d.velocity = Vector2.zero; playanimator.SetBool("issprushstop", true); playanimator.SetBool("issprushfly", false); gravity = 28;sprush = 0; issprush = false;  }
            }
    }
    IEnumerator Lookflash(GameObject flash, int zhenshu)
    {
        flash.SetActive(true);
        for (int i = 0; i < zhenshu; i++)
        {
            yield return null;
        }
        flash.SetActive(false);
    }
    //暗影冲刺
    public Collider2D collider2D;
    public void shadowrush()
    {
        RaycastHit2D raycastHit2d = Physics2D.Raycast(playrigibody2d.position, Vector2.right*dir, 3, LayerMask.GetMask("ground"));
        if (Input.GetKeyDown(KeyCode.X))
        {
            collider2D.enabled = false;
            isPlayAudioState = true;
            play("hero_shade_dash_2");
            shadoweffect.SetActive(true);
            playanimator.SetTrigger("isshadowrush");
            if (raycastHit2d.collider == null)
            {
                playrigibody2d.transform.Translate(Vector2.left  * 3f);
            }
            else
            {
                float x;
                x = Vector2.Distance(transform.position, raycastHit2d.point);
                playrigibody2d.transform.Translate(Vector2.left * (x-0.4f));
            }
            StartCoroutine(shadowdelete());
        }
    }
    IEnumerator shadowdelete()
    {
        
        yield return new WaitForSeconds(0.15f);
        
        effectanimator2.gameObject.SetActive(true);
        
            effectanimator2.SetTrigger("isshadow");
        yield return new WaitForSeconds(0.4f);
        shadoweffect.SetActive(false);
        play("hero_shade_dash_charge_pt_2");
        yield return new WaitForSeconds(0.4f);
        
        effectanimator2.gameObject.SetActive(false);
        isPlayAudioState = false;
        collider2D.enabled = true;

    }
    public GameObject hpre;
  public  float hpretime=0;
    bool isreinput=true;
    public void hpup()
    { 
        if (cuhealth < maxhealth&&soul>=30)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                playanimator.ResetTrigger("ishprestop");
                playanimator.SetTrigger("ishere");
                hpre.SetActive(true);   
            }
            if (Input.GetKey(KeyCode.Q))
            {
                if (isreinput)
                { 
                if (hpretime < 2)
                {
                    hpretime += Time.deltaTime;
                }
                else
                {
                        isPlayAudioState = true;
                        soul = soul - 30;
                    image6.fillAmount = soul / 100f;               
                    cuhealth = cuhealth + 1;
                    hpre.SetActive(false);
                    hpretime = 0;
                        playanimator.ResetTrigger("ishere");
                        
                        play("ui_save");
                        isreinput = false; playanimator.SetTrigger("ishprestop");
                        StartCoroutine(resetaudio2());
                    }
                }
            }
            if (Input.GetKeyUp(KeyCode.Q))
            {
                hpre.SetActive(false);
                hpretime = 0;
                isreinput = true;
                playanimator.ResetTrigger("ishere");
                playanimator.SetTrigger("ishprestop");
            }
        }
        hpimgae();
    }
    public bool isPlayRunAudioState;
    /// <summary>
    /// 播放行走声音
    /// </summary>
    /// <param name="h"></param>
    public void PlayRunAudio(float h)
    {

        if (h != 0 && isground && !isPlayRunAudioState)
        {
            isPlayRunAudioState = true;
            play("hero_run_footsteps_stone");
        }
        else if (h == 0 && isground && !isPlayAudioState)
        {
            audioSource.Stop();
            isPlayRunAudioState = false;
        }
        else if(!isground&& !isPlayAudioState)
        {
            audioSource.Stop();
            isPlayRunAudioState = false;
        }
    }
    public void play(string str)
    {
        AudioClip clip = (AudioClip)Resources.Load(str,typeof(AudioClip));
        audioSource.PlayOneShot(clip);
    }
    //public bool isopenbag=false;
    void openbag()
    {
        
        if (Input.GetKeyDown(KeyCode.I))
        {
            
            bag.SetActive(true);
        }
    }
     void openesc()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            esc.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "cross")
        {
            transform.position = new Vector2(-71, 12);
        }
        if (collision.tag == "door")
        {
            transform.position = new Vector2(-4.1f, 12);
        }
        if (collision.tag == "send")
        {
            transform.position = new Vector2(20f, 8.5f);
        }
        if (collision.tag == "Trap")
        {
            transform.position = new Vector2(64, -4);
        }
        if (collision.tag == "trap1")
        {
            transform.position = new Vector2(21, -125);
        }
        if (collision.tag == "trap2")
        {
            transform.position = new Vector2(27.9f, -111);
        }
        if (collision.tag == "trap3")
        {
            transform.position = new Vector2(9.7f, -125);
        }
        if (collision.tag == "trap4")
        {
            transform.position = new Vector2(-16, -127);
        }
        if (collision.tag == "trap5")
        {
            transform.position = new Vector2(-14, -106);
        }
        if (collision.tag == "trap6")
        {
            transform.position = new Vector2(-24, -87);
        }
        if (collision.tag == "trap7")
        {
            transform.position = new Vector2(89, -97);
        }
    }
    GameObject effectobj2;
   public bool isshake=false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isdefend) { return; }
        if (collision.gameObject.tag == "enemy"&&isdefend == false)
        {
            isdefend = true; defendtime =2;
            isshake = true;
            effectobj2 = Instantiate(hited, transform.position, Quaternion.identity);
            StartCoroutine(jituiLR(10));
            StartCoroutine(deletehited());
            StartCoroutine(shanshuo());
            isPlayAudioState = true;
            play("enemy_damage");
            cuhealth -= 1;
        }
        if (collision.gameObject.tag == "boss2akef" && isdefend == false)
        {
            isdefend = true; defendtime = 2;
            isshake = true;
            effectobj2 = Instantiate(hited, transform.position, Quaternion.identity);
            StartCoroutine(jituiLR(10));
            StartCoroutine(deletehited());
            StartCoroutine(shanshuo());
            isPlayAudioState = true;
            play("enemy_damage");
            cuhealth -= 1;
            collision.gameObject.SendMessage("Die");
        }
    }
    IEnumerator loading1()
    {
        yield return new WaitForSeconds(2f);
        loading.SetActive(false);  
    }
    public GameObject hited;
    IEnumerator deletehited()
    {      
        yield return new WaitForSeconds(0.3f);
        Destroy(effectobj2);
        isshake = false;
        isPlayAudioState = false;
    }
    public SpriteRenderer spriteRenderer;
    IEnumerator shanshuo()
    {

        yield return new WaitForSeconds(0.1f);
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.enabled = true;
    }
   public void equip()
    {
        //Debug.Log("检查是否装备");
        if (!isequip)
        {
            //Debug.Log("meizhuangbei");
            if (inv.items[1].ID!=-1) { atk = atk + inv.items[1].Atk; isequip = true; }
            
        }
        else {if (inv.items[1].ID == -1) { atk = 2;isequip = false; } }
    }
    public GameObject qingting;
    public GameObject duihua;
    bool isduihua=false;
    public void Npc()
    {
        RaycastHit2D hit = Physics2D.Raycast(playrigibody2d.position + Vector2.right * 0.2f,Vector2.right*dir, 0.1f, LayerMask.GetMask("npc"));
        if (hit)
        {
            if (!isduihua) {qingting.SetActive(true); }
            if (Input.GetKeyDown(KeyCode.E))
                {
                duihua.SetActive(true); qingting.SetActive(false);isduihua = true;
            }
        }
        else { qingting.SetActive(false); isduihua = false; duihua.SetActive(false); }   
    }
    public GameObject soulef1;
    public GameObject soulef2;
    public int souldir;
    public void soulatk()
    {
        if (Input.GetKeyDown(KeyCode.F) && soul > 40)
        {
            souldir = (int)dir;
            soulef1.SetActive(true);
            StartCoroutine(soulatkcopy());
            soul -= 40;
            image6.fillAmount = soul / 100f;
        }
    }
    IEnumerator soulatkcopy()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject obj;
        if (dir == -1)
        {
            obj = Instantiate(soulef2, transform.position, Quaternion.Euler(new Vector2(0, 0)));
        }
        else
        {
            obj = Instantiate(soulef2, transform.position, Quaternion.Euler(new Vector2(0, 180)));
        }
        soulef1.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        Destroy(obj);
    }
}
