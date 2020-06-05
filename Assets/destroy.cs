using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip clip;
   public float hp = 3;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //public void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (Input.GetKeyDown(KeyCode.K))
    //    {
    //        hp -= 1;
            
    //        if (hp <= 0) { Destroy(gameObject); }
    //        Debug.Log("enter");
    //    }
    //}
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            hp -= 1;
            if (hp <= 0) { audioSource.PlayOneShot(clip);StartCoroutine(destroyobj()); }
            Debug.Log("stay");
        }
    }
    IEnumerator destroyobj()
    {

        yield return new WaitForSeconds(0.3f);

        Destroy(gameObject);

    }
}
