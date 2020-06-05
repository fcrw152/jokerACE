using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerashark : MonoBehaviour
{
    //// Start is called before the first frame update
    //private Transform ThisTransform = null;
    //public float ShakeTime = 2.0f;
    //public float ShakeAmount = 3.0f;
    //public float ShakeSpeed = 2.0f;
    //// Use this for initialization
    //void Start()
    //{
    //    ThisTransform = GetComponent<Transform>();
    //}

    //public IEnumerator Shake()
    //{
    //    Debug.Log("2222222222222");
    //    Vector3 OrigPosition = ThisTransform.localPosition;
    //    float ElapsedTime = 0.0f;
    //    while (ElapsedTime < ShakeTime)
    //    {
    //        Vector3 RandomPoint = OrigPosition + Random.insideUnitSphere * ShakeAmount;
    //        ThisTransform.localPosition = Vector3.Lerp(ThisTransform.localPosition, RandomPoint, Time.deltaTime * ShakeSpeed);
    //        yield return null;
    //        ElapsedTime += Time.deltaTime;
    //    }
    //    ThisTransform.localPosition = OrigPosition;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Z))
    //    {
    //        StartCoroutine(Shake());
    //        Debug.Log("1111111111111");
    //    }
    //}
    private Vector3 deltaPos = Vector3.zero;


    // Use this for initialization

    void Start()

    {

    }


    // Update is called once per frame

    void Update()

    {

        transform.localPosition -= deltaPos;

        deltaPos = Random.insideUnitSphere / 3.0f;

        transform.localPosition += deltaPos;

    }
}
