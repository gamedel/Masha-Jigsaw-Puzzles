using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EazyTools.SoundManager;
public class LevelGotoScr : MonoBehaviour {

    public AudioClip ClickSo;
	// Use this for initialization
	public void GotoLevel(string LevelName)
    {
        if (GameObject.Find("GeneralObj").GetComponent<GeneralScr>().isMusic)
        {
            SoundManager.PlaySound(ClickSo,0.5f,true);
        }
        SceneManager.LoadScene(LevelName);
    }
}
