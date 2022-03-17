
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsCloseScr : MonoBehaviour {




    void Start()
    {
        Invoke("CheckInternet", 2f);
    }

    void CheckInternet()
    {
        Invoke("CheckInternet", 2f);

        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            GameObject.Destroy(gameObject);
        }
    }


   
}
