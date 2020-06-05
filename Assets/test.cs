using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    camerashark camerashark1;
    // Start is called before the first frame update
    void Start()
    {
        camerashark1 =GameObject.Find("Main Camera").GetComponent<camerashark>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    StartCoroutine(camerashark1.Shake());
        //    Debug.Log("1111111111111");
        //}
    }
}
