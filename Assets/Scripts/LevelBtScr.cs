using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EazyTools.SoundManager;

public class LevelBtScr : MonoBehaviour {

    GameObject GeneralObj;
    public AudioClip ClickSo;
    // Use this for initialization
    public void CreateDifficultMenu()
    {
        
        GeneralObj = GameObject.Find("GeneralObj");
        GeneralObj.GetComponent<GeneralScr>().LoadedPuzzleImg = gameObject.transform.GetChild(1).GetComponent<Image>().sprite;
        GameObject.Instantiate(GeneralObj.GetComponent<GeneralScr>().MenuDifficultObj,new Vector3(),new Quaternion()).transform.SetParent(GameObject.Find("Canvas").transform);
        if (GeneralObj.GetComponent<GeneralScr>().isMusic)
        {
            SoundManager.PlaySound(ClickSo);
        }
    }
}
