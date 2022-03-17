using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EazyTools.SoundManager;

public class LoadLevelBtScr : MonoBehaviour {


    public int XCount, YCount;
    GameObject GeneralObj;
    public AudioClip ClickSo;
    public void GotoLevel()
    {
        if (GameObject.Find("GeneralObj").GetComponent<GeneralScr>().isMusic)
        {
            SoundManager.PlaySound(ClickSo);
        }
        GeneralObj = GameObject.Find("GeneralObj");
        GeneralObj.GetComponent<GeneralScr>().LoadedXCount = XCount;
        GeneralObj.GetComponent<GeneralScr>().LoadedYCount = YCount;
        SceneManager.LoadScene("GameLevel");
    }
}
