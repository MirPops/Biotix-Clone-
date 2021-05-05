using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 10)
        {
            //print(Input.touchCount);
            //Touch touch = Input.GetTouch(0);
            //print($"deltapos = {touch.deltaPosition}");
            //print($"phase = {touch.phase}");
            //print($"pos = {touch.position}");
            //print($"tapcount = {touch.tapCount}");
        }


    }

    public void click()
    {
        print("button pressed");
    }
}
