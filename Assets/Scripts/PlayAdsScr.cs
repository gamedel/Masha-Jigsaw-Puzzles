using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAdsScr : MonoBehaviour {







	// Use this for initialization
	void Start () {
		
	}


    public void Clicked()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {

            UniScrs.CreateUIObject(GameObject.Find("GeneralObj").GetComponent<GeneralScr>().NoInternetObj);
            
        }
        else
        {

        }
    }
}
