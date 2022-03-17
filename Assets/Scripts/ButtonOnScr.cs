using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EazyTools.SoundManager;
public class ButtonOnScr : MonoBehaviour {

    public Sprite ImgOn;
    public Sprite ImgOff;

    public bool isOn=true;

    public GameObject setka;

    public AudioClip ClickSo;

    // Use this for initialization
    void Start () {
		
	}
	
	public void Push()
    {

        if (GameObject.Find("GeneralObj").GetComponent<GeneralScr>().isMusic)
        {
            SoundManager.PlaySound(ClickSo, 0.5f, true);
        }

        if (isOn)
        {
            isOn = false;
            GetComponent<Image>().sprite = ImgOff;
            setka.SetActive(false);
        }
        else
        {
            isOn = true;
            GetComponent<Image>().sprite = ImgOn;
            setka.SetActive(true);
        }
    }


}
