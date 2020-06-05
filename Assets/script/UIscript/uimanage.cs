using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class uimanage : MonoBehaviour
{
    public List<Button> buttons;//存储5个按钮
    Button start;
    Button setting;
    Button button2;
    Button button3;
    Button exit;
    GameObject buttonsobj;//buttons的组件
    public AudioSource audioSource;
    public AudioClip clip;
    public AudioClip clip1;
    // Start is called before the first frame update
    void Start()
    {
        init();
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void init()//把ui初始化到原先的位置
    {
        buttons = new List<Button>();
        start = transform.Find("buttons/start").GetComponent<Button>();
        setting = transform.Find("buttons/setting").GetComponent<Button>();
        button2 = transform.Find("buttons/Button (2)").GetComponent<Button>();
        button3 = transform.Find("buttons/Button (3)").GetComponent<Button>();
        exit = transform.Find("buttons/exit").GetComponent<Button>();
        buttons.Add(start);
        buttons.Add(setting);
        buttons.Add(button2);
        buttons.Add(button3);
        buttons.Add(exit);
        for (int i = 0; i < buttons.Count; i++)
        {
            var trigger = buttons[i].gameObject.AddComponent<EventTrigger>();
            RegisterEvent(trigger, buttons[i].gameObject);
        }
        
    }
    public void RegisterEvent(EventTrigger trigger, GameObject btnObj)
    {
        EventTrigger.Entry enterClick = new EventTrigger.Entry();
        enterClick.eventID = EventTriggerType.PointerEnter;

        EventTrigger.Entry exitClick = new EventTrigger.Entry();
        exitClick.eventID = EventTriggerType.PointerExit;
       
        GameObject leftIcon = btnObj.transform.Find("icon/Image/Image").gameObject; Debug.Log("wangyouyidong,xiaoshi");
        GameObject rightIcon = btnObj.transform.Find("icon/Image (1)/Image").gameObject;
        uimove leftMove = leftIcon.AddComponent<uimove>();
        leftMove.Init(0);
        uimove rightMove = rightIcon.AddComponent<uimove>();
        rightMove.Init(1);

        enterClick.callback.AddListener((o) =>
        {
            leftMove.MouseEnterClick();
            rightMove.MouseEnterClick();
            audioSource.PlayOneShot(clip);
            
        });
        
        exitClick.callback.AddListener((o) =>
        {
            leftMove.MouseExitClick();
            rightMove.MouseExitClick();
        });

        trigger.triggers.Add(enterClick);
        trigger.triggers.Add(exitClick);
    }
    public void StartBtnClick()
    {
        audioSource.PlayOneShot(clip1);
        StartCoroutine(load());
    }
    public void ExittBtnClick()
    {
        Application.Quit();
    }
    IEnumerator load()
    {
        
        yield return new WaitForSeconds(1.2f);
        audioSource.Stop();
        SceneManager.LoadScene("loading");
    }
    
}
