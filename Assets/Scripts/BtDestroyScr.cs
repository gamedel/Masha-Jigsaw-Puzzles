using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtDestroyScr : MonoBehaviour {

	// Use this for initialization
	public void DestroySelf()
    {
        GameObject.Destroy(gameObject.transform.parent.gameObject);
    }
}
