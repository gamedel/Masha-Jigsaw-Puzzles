using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using EazyTools.SoundManager;
using UnityEngine.Advertisements;

public class GeneralScr : MonoBehaviour {


    public Sprite[] PuzzleImgs;
    public Sprite[] MiniPuzzleImgs;
    public GameObject LevelObj;
    public GameObject MenuDifficultObj;
    public AudioClip[] Music;
    public AudioClip[] Sounds;


    public Sprite LoadedPuzzleImg;
    public int LoadedXCount, LoadedYCount;

    public bool isMusic = true;

    int CurrentPage=1, MaxPages, MaxLevels;

    public int TimeForMusic=-1;
    bool isMusicEnd = false;
    AudioClip LastMusic=null;

    public GameObject NoInternetObj;

    public bool NoAds = false;
    public bool isAdsReady = false;
    public int AdsTimer = 300;
    public bool isFirstRun = true;

    void OnEnable()
    {
        CurrentPage = 1;
        MaxLevels = PuzzleImgs.Length;
        MaxPages = (int)Mathf.Ceil((float)MaxLevels / 6f);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Use this for initialization
    void Start () {
        isFirstRun = UniScrs.PlayerPrefsBoolGet("isFirstRun", true);
        UniScrs.PlayerPrefsBoolSet("isFirstRun", false);

        if (isFirstRun) { AdsTimer = 300; } else
        {
            AdsTimer = 90;
        }



        Application.targetFrameRate = 60;
        CurrentPage = 1;
        MaxLevels = PuzzleImgs.Length;
        MaxPages = (int)Mathf.Ceil((float)MaxLevels / 6f);

        TimeForMusic = 10 * 60;



        Advertisement.Initialize("2669121",false);

        Invoke("Timer", 1f);
        //Invoke("StartPlayMusic", 10f);
    }


    void Timer()
    {
        Invoke("Timer", 1f);

        if (AdsTimer > 0)
        {
            AdsTimer--;
        }

        if (AdsTimer==0 && !isAdsReady)
        {
            isAdsReady = true;
        }

    }

    void StartPlayMusic()
    {
        if (isMusic)
        {
            // SoundManager.isSoundPlaying
            LastMusic = Music[Random.Range(0, 2)];
            SoundManager.PlayMusic(LastMusic, 0.5f, false, true);
        }
    }

    int MusicFirstChance = 2;
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        MusicFirstChance--;

        if (MusicFirstChance < 1)
        {
            isMusicEnd = true;
            TimeForMusic = 20 * 60;
            SoundManager.StopAllMusic();
        }

        if (scene.name == "MainLevel")
        {
            LevelsCreate(CurrentPage);
        }
    }


    // Update is called once per frame
    void Update () {
        //   Debug.Log(SoundManager.isSoundPlaying(Music[0]) || SoundManager.isSoundPlaying(Music[1]));
        if (TimeForMusic > 0)
        {
            TimeForMusic--;
        }

        if (isMusic && TimeForMusic==0)
        {
            // SoundManager.isSoundPlaying
            TimeForMusic = -1;
            if (LastMusic == null)
            {
                LastMusic = Music[Random.Range(0, 2)];
            }
            else
            {
                isMusicEnd = false;
                for (int i = 0; i < Music.Length; i++)
                {
                    if (LastMusic != Music[i])
                    {
                        LastMusic = Music[i];
                        break;
                    }
                }
            }
            
            SoundManager.PlayMusic(LastMusic, 0.5f, false, true);
        }


        if (!SoundManager.isSoundPlaying(LastMusic) && !isMusicEnd)
        {
            isMusicEnd = true;
            TimeForMusic = 180 * 60;
        }



    }


    


    int CurrentLevel;
    GameObject LvObj;
    public void LevelsCreate(int page)
    {

        foreach (GameObject OldLevelObj in GameObject.FindGameObjectsWithTag("LevelBts"))
        {
            GameObject.Destroy(OldLevelObj);
        }

        GameObject.Find("PageTx").GetComponent<Text>().text = page.ToString();

        CurrentLevel = (page - 1) * 6;
        for (int y = 0; y < 2; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                CurrentLevel++;

                if (CurrentLevel <= MaxLevels)
                {
                    LvObj = GameObject.Instantiate(LevelObj, new Vector3(-401 + 400 * x, 110 - 250 * y), new Quaternion());
                    LvObj.transform.SetParent(GameObject.Find("Canvas").transform);
                    LvObj.transform.GetChild(1).GetComponent<Image>().sprite = MiniPuzzleImgs[CurrentLevel - 1];
                }
                else
                {
                    break;
                }

                    
            }
        }
    }

   public void LeftPage()
    {
        if (CurrentPage > 1)
        {
            CurrentPage--;
            LevelsCreate(CurrentPage);
            if (isMusic)
            {
                SoundManager.PlaySound(Sounds[1]);
            }
        }
    }
    public void RightPage()
    {
        if (CurrentPage < MaxPages)
        {
            CurrentPage++;
            LevelsCreate(CurrentPage);
            if (isMusic)
            {
                SoundManager.PlaySound(Sounds[1]);
            }
        }
    }










    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                //
                // YOUR CODE TO REWARD THE GAMER
                // Give coins etc.
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }



















    bool first = false;
    void Awake()
    {
        DontDestroyOnLoad(this);

        if (FindObjectsOfType(GetType()).Length == 1)
        {
            first = true;
        }

        if (!first)
        {
            Destroy(gameObject);
        }    
    }

}
