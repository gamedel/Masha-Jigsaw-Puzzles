using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnableDisableScr : MonoBehaviour {



	// Use this for initialization
	void Start () {
         GetComponent<ScrollRect>().horizontal = true;


        //GetComponent<ScrollRect>().horizontal = false;
       
       // Canvas.ForceUpdateCanvases();
    }
	
	
}
