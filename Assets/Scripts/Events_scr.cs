using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Events_scr : MonoBehaviour {

    public int index1 = 0;
    public int index2 = 0;
    public string object_script;

    public string method_name;

    public string args;
    public GameObject obb;

    public void ButtonEvent()
    {
        GameObject find = GameObject.Find(gameObject.GetComponent<Events_scr>().object_script);
        MonoBehaviour dwd = find.GetComponent(find.GetComponents<MonoBehaviour>()[gameObject.GetComponent<Events_scr>().index1].GetType().Name) as MonoBehaviour;
        dwd.StartCoroutine(gameObject.GetComponent<Events_scr>().method_name, gameObject.GetComponent<Events_scr>().args);
    }
}

public class Events_scr2 : MonoBehaviour
{
    public void Events_ex()
    {
        string hhh = gameObject.GetComponent<Events_scr>().object_script;
        GameObject find = GameObject.Find(gameObject.GetComponent<Events_scr>().object_script);
        MonoBehaviour dwd = find.GetComponent(find.GetComponents<MonoBehaviour>()[gameObject.GetComponent<Events_scr>().index1].GetType().Name) as MonoBehaviour;
        dwd.StartCoroutine(gameObject.GetComponent<Events_scr>().method_name, gameObject.GetComponent<Events_scr>().args);
    }

}



