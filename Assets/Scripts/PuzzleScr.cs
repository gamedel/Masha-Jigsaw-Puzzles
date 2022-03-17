using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using EazyTools.SoundManager;
public class PuzzleScr : MonoBehaviour {

    public Vector3 StartPosition;
    public GameObject PuzzleGenerator=null;
    public bool isMovable = true;

    public AudioClip[] AgaSo;




    bool isOn = false;
    bool isEnd = false;


    public int SiblingIndex = 0;
    // Use this for initialization
    void Start () {
        Invoke("OnObject", 2f);

	}

     void OnObject()
    {
        isOn = true;
        SiblingIndex = transform.GetSiblingIndex();
    }

    // Update is called once per frame
    void Update () {
		if (isOn && Vector3.Distance(transform.position, StartPosition) <= PuzzleGenerator.GetComponent<PuzzleGeneratorNew>().DistanceToMovePuzzle)
        {


            if (Vector3.Distance(transform.position, StartPosition) > 0.001f)
            {
                isMovable = false;
                transform.position += (StartPosition - transform.position) / 5f;
            }
            if (!isEnd && Vector3.Distance(transform.position, StartPosition) > 1f)
            {
                PuzzleGenerator.GetComponent<PuzzleGeneratorNew>().CollectedPuzzles++;
                    isEnd = true;
                GetComponent<SpriteRenderer>().sortingOrder = 0;
                transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 1;

                if (DBPlugin.isAnimNameHas(PuzzleGenerator.GetComponent<PuzzleGeneratorNew>().MashaObj, "Stand"))
                {
                    DBPlugin.PlayAnim(PuzzleGenerator.GetComponent<PuzzleGeneratorNew>().MashaObj, "Shake_Hand", 1);
                }

                if (GameObject.Find("GeneralObj").GetComponent<GeneralScr>().isMusic)
                {
                    SoundManager.PlaySound(AgaSo[Random.Range(0,2)]);
                }

                //Debug.Log("ok");

            }
        }

        if (isOn && transform.parent!=null && transform.localScale.x!= 1 / PuzzleGenerator.GetComponent<PuzzleGeneratorNew>().PuzzleXKoef * 0.6f)
        {
            transform.localScale = new Vector3(1 / PuzzleGenerator.GetComponent<PuzzleGeneratorNew>().PuzzleXKoef * 0.6f, 1 / PuzzleGenerator.GetComponent<PuzzleGeneratorNew>().PuzzleYKoef * 0.6f, 1);

        }
    }
}
