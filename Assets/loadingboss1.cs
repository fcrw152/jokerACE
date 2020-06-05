using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class loadingboss1 : MonoBehaviour
{
    
    float time;
    // Start is called before the first frame update
    void Start()
    {
       
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (time < 4)
        { time += Time.deltaTime; }
        else
        {
              SceneManager.LoadScene("SampleScene");
        }
    }
}
