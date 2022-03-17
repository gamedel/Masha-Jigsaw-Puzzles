using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EazyTools.SoundManager;
public class LevelEndScr : MonoBehaviour {

    public GameObject NoInternetObj;
    public AudioClip ClickSo;
    bool isShow = false;
    GeneralScr _GeneralScr;
	// Use this for initialization
	void Start () {
        _GeneralScr = GameObject.Find("GeneralObj").GetComponent<GeneralScr>();

    }
	
	public void Clicked()
    {
        if (GameObject.Find("GeneralObj").GetComponent<GeneralScr>().isMusic)
        {
            SoundManager.PlaySound(ClickSo, 0.5f, true);
        }

        if (_GeneralScr.NoAds || !_GeneralScr.isAdsReady)
            {
                SceneManager.LoadScene("MainLevel");
            }
            else
            {
            
                if (Application.internetReachability != NetworkReachability.NotReachable)
                {
                if (!isShow)
                {
                    isShow = true;
                    _GeneralScr.isAdsReady = false;
                    _GeneralScr.AdsTimer = 90;
                    _GeneralScr.ShowRewardedAd();
                    Invoke("GotoMainLevel", 2f);
                }
                }
                else
                {
                UniScrs.CreateUIObject(NoInternetObj);
                }
            }
        
    }



    void GotoMainLevel()
    {
        SceneManager.LoadScene("MainLevel");
    }

}
