using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowFps : MonoBehaviour {

    public float deltaTime;
    float fps;

    public bool isSetFps=true;
    public int FpsSet = 60;

    // Update is called once per frame
    void Update () {
        
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        fps = 1.0f / deltaTime;
        GetComponent<Text>().text = fps.ToString("0");
       
        if (isSetFps)
        {
            Application.targetFrameRate = FpsSet;
        }

    }
}
