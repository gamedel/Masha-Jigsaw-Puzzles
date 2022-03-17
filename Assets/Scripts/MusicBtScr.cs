using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EazyTools.SoundManager;
using UnityEngine.UI;
public class MusicBtScr : MonoBehaviour {

    public Sprite OnSpr, OffSpr;

    bool isOn = true;

    GameObject GeneralObj;

    public AudioClip ClickSo;
    void Start()
    {
        GeneralObj = GameObject.Find("GeneralObj");

        isOn=GeneralObj.GetComponent<GeneralScr>().isMusic;
        if (isOn)
        {
            GetComponent<Image>().sprite = OnSpr;
        }
        else
        {
            GetComponent<Image>().sprite = OffSpr;
        }

    }

    public void ClickButton()
    {

        if (GeneralObj.GetComponent<GeneralScr>().isMusic)
        {
            SoundManager.PlaySound(ClickSo);
        }
        if (isOn)
        {
            GeneralObj.GetComponent<GeneralScr>().isMusic = false;
            isOn = false;
            GetComponent<Image>().sprite = OffSpr;
            SoundManager.StopAllMusic();
        }
        else
        {
            GeneralObj.GetComponent<GeneralScr>().isMusic = true;
            isOn = true;
            GeneralObj.GetComponent<GeneralScr>().TimeForMusic = 0;
            GetComponent<Image>().sprite = OnSpr;
        }
    }
	


}
