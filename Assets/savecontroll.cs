using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class savecontroll : MonoBehaviour
{
    Save1 save1;
    controller con;
    // Start is called before the first frame update
    void Start()
    {
        save1 = new Save1();
        con = GameObject.Find("player").GetComponent<controller>();
    }

    // Update is called once per frame
    void Update()
    {
        saved();
        loaded();
    }
    public void saved()
    {
        if (Input.GetKeyDown(KeyCode.V)) { savebyjson(); }
    }
    public void loaded()
    {
        if (Input.GetKeyDown(KeyCode.B)) { loadbyjson(); }
    }
    public void savebyjson()
    {
        save1.hp = con.cuhealth;
        save1.pos = con.transform.position;
        save1.atk = con.atk;
        string JsonString = JsonUtility.ToJson(save1);
        StreamWriter sr = new StreamWriter(Application.dataPath+"/JSONData.txt");
        sr.Write(JsonString);
        sr.Close();
        Debug.Log("==============v_v=================");
    }
    public void loadbyjson()
    {
        if (File.Exists(Application.dataPath + "/JSONData.txt"))
        {
            StreamReader sr = new StreamReader(Application.dataPath + "/JSONData.txt");
            string JsonString = sr.ReadToEnd();
            sr.Close();
            Save1 save = JsonUtility.FromJson<Save1>(JsonString);
            con.cuhealth = save.hp;
            con.transform.position = save.pos;
            con.atk = save.atk;
            con.hpimgae();
            Debug.Log("==============>_<=================");

        }
        else
        {
            Debug.Log("NOT found file");
        }
    }
}
