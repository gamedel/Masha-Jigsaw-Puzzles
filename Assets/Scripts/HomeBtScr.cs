using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EazyTools.SoundManager;
public class HomeBtScr : MonoBehaviour {

    public GameObject LevelEndObj;
    public AudioClip ClickSo;
    bool isShow = false;
    GeneralScr _GeneralScr;
    public GameObject NoInternetObj;
    // Use this for initialization

    void Start()
    {
        _GeneralScr = GameObject.Find("GeneralObj").GetComponent<GeneralScr>();

    }
    public void OnClick()
    {
        if (GameObject.Find("GeneralObj").GetComponent<GeneralScr>().isMusic)
        {
            SoundManager.PlaySound(ClickSo, 0.5f, true);
        }

        if (GameObject.Find("MainObj").GetComponent<PuzzleGeneratorNew>().isVictory)
        {
            UniScrs.CreateUIObject(LevelEndObj, new Vector3(0, -50));
        }
        else
        {
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
    }

    void GotoMainLevel()
    {
        SceneManager.LoadScene("MainLevel");
    }


}
