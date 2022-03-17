using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PuzzleGenerator : MonoBehaviour {

    public Sprite[] MosaicPicts;
    public int MosaicIndex = 0;

    public GameObject PuzzleMask;

    public float PuzzleWidth, PuzzleHeight;
    public float PictureWidth, PictureHeight;
    public float PuzzleStartX = 3;
    public float PuzzleStartY = 4;
    public GameObject Setka;
    public Sprite[] SetkaImgs;



    public Sprite[] TypesOfPuzzle;

    public Sprite[] ShablonOfPuzzle;

    int PuzzlesXCount, PuzzlesYCount;

    public Vector2 ImagePosition;

    float PuzzleXCreate, PuzzleYCreate;
    GameObject PuzzleCreated;

    float PuzzleXKoef, PuzzleYKoef;
    public GameObject Scroll;

    public float DistanceToMovePuzzle = 200f;
    // Use this for initialization
    void Start () {


        Application.targetFrameRate = 60;


      if (PuzzleStartX==4 && PuzzleStartY == 3)
        {
            Setka.GetComponent<SpriteRenderer>().sprite = SetkaImgs[0];
        }

        if (PuzzleStartX == 5 && PuzzleStartY == 4)
        {
            Setka.GetComponent<SpriteRenderer>().sprite = SetkaImgs[1];
        }

        if (PuzzleStartX == 6 && PuzzleStartY == 5)
        {
            Setka.GetComponent<SpriteRenderer>().sprite = SetkaImgs[2];
        }

        if (PuzzleStartX == 8 && PuzzleStartY == 6)
        {
            Setka.GetComponent<SpriteRenderer>().sprite = SetkaImgs[3];
        }


        Setka.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite= MosaicPicts[MosaicIndex];




        PuzzleXKoef = 4f / PuzzleStartX;
        PuzzleYKoef = 3f / PuzzleStartY;

        DistanceToMovePuzzle *= PuzzleXKoef;

        PuzzlesXCount = Mathf.CeilToInt(PictureWidth / (PuzzleWidth* PuzzleXKoef));
        PuzzlesYCount = Mathf.CeilToInt(PictureHeight / (PuzzleHeight * PuzzleYKoef));


        for (int i = 0; i < PuzzlesXCount; i++)
        {
            for (int j = 0; j < PuzzlesYCount; j++)
            {
                PuzzleXCreate = ImagePosition.x - PictureWidth / 2 + PuzzleWidth / 2f* PuzzleXKoef + PuzzleWidth * i* PuzzleXKoef;
                PuzzleYCreate = ImagePosition.y + PictureHeight / 2 - PuzzleHeight / 2f* PuzzleYKoef - PuzzleHeight * j* PuzzleYKoef;

                PuzzleCreated=GameObject.Instantiate(PuzzleMask, new Vector3(PuzzleXCreate, PuzzleYCreate), new Quaternion());
                PuzzleCreated.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = MosaicPicts[MosaicIndex];
                    
                PuzzleCreated.transform.localScale = new Vector3(PuzzleXKoef, PuzzleYKoef, 1);
                PuzzleCreated.transform.GetChild(0).localScale = new Vector3(PuzzleStartX / 4f, PuzzleStartY / 3f, 1);
                PuzzleCreated.transform.GetChild(0).localPosition = new Vector3(PictureWidth / 2f/ PuzzleXKoef - PuzzleWidth /2f - PuzzleWidth * i , -PictureHeight / 2f/ PuzzleYKoef + PuzzleHeight / 2f  + PuzzleHeight * j );
                
                
                


                if (j==0 && i>0 && i< PuzzlesXCount - 1)
                {
                    if ((i+j)%2 == 0)
                    {
                        PuzzleCreated.GetComponent<SpriteMask>().sprite = TypesOfPuzzle[8];
                        PuzzleCreated.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = ShablonOfPuzzle[8];
                    }
                    else
                    {
                        PuzzleCreated.GetComponent<SpriteMask>().sprite = TypesOfPuzzle[12];
                        PuzzleCreated.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = ShablonOfPuzzle[12];
                    }
                }

                if (j == 0 &&  i == PuzzlesXCount - 1)
                {
                    if ((i + j) % 2 == 0)
                    {
                        PuzzleCreated.GetComponent<SpriteMask>().sprite = TypesOfPuzzle[1];
                        PuzzleCreated.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = ShablonOfPuzzle[1];
                    }
                    else
                    {
                        PuzzleCreated.GetComponent<SpriteMask>().sprite = TypesOfPuzzle[5];
                        PuzzleCreated.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = ShablonOfPuzzle[5];
                    }
                }

                if (j > 0 && j< PuzzlesYCount - 1 && i == PuzzlesXCount - 1)
                {

                    if ((i + j) % 2 == 0)
                    {
                        PuzzleCreated.GetComponent<SpriteMask>().sprite = TypesOfPuzzle[9];
                        PuzzleCreated.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = ShablonOfPuzzle[9];
                    }
                    else
                    {
                        PuzzleCreated.GetComponent<SpriteMask>().sprite = TypesOfPuzzle[13];
                        PuzzleCreated.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = ShablonOfPuzzle[13];
                    }
                }

                if (j == PuzzlesYCount - 1 && i == PuzzlesXCount - 1)
                {
                    if ((i + j) % 2 == 0)
                    {
                        PuzzleCreated.GetComponent<SpriteMask>().sprite = TypesOfPuzzle[3];
                        PuzzleCreated.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = ShablonOfPuzzle[3];
                    }
                    else
                    {
                        PuzzleCreated.GetComponent<SpriteMask>().sprite = TypesOfPuzzle[7];
                        PuzzleCreated.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = ShablonOfPuzzle[7];
                    }
                }

                if (j == PuzzlesYCount - 1 && i > 0 && i < PuzzlesXCount - 1)
                {
                    if ((i + j) % 2 == 0)
                    {
                        PuzzleCreated.GetComponent<SpriteMask>().sprite = TypesOfPuzzle[10];
                        PuzzleCreated.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = ShablonOfPuzzle[10];
                    }
                    else
                    {
                        PuzzleCreated.GetComponent<SpriteMask>().sprite = TypesOfPuzzle[14];
                        PuzzleCreated.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = ShablonOfPuzzle[14];
                    }
                }



                if (j == PuzzlesYCount - 1 && i == 0)
                {
                    if ((i + j) % 2 == 0)
                    {
                        PuzzleCreated.GetComponent<SpriteMask>().sprite = TypesOfPuzzle[2];
                        PuzzleCreated.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = ShablonOfPuzzle[2];
                    }
                    else
                    {
                        PuzzleCreated.GetComponent<SpriteMask>().sprite = TypesOfPuzzle[6];
                        PuzzleCreated.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = ShablonOfPuzzle[6];
                    }
                }


                if (j > 0 && j < PuzzlesYCount - 1 && i == 0)
                {

                    if ((i + j) % 2 == 0)
                    {
                        PuzzleCreated.GetComponent<SpriteMask>().sprite = TypesOfPuzzle[11];
                        PuzzleCreated.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = ShablonOfPuzzle[11];
                    }
                    else
                    {
                        PuzzleCreated.GetComponent<SpriteMask>().sprite = TypesOfPuzzle[15];
                        PuzzleCreated.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = ShablonOfPuzzle[15];
                    }
                }


                if (j > 0 && j < PuzzlesYCount - 1 && i> 0 && i < PuzzlesXCount - 1)
                {

                    if ((i + j) % 2 == 0)
                    {
                        PuzzleCreated.GetComponent<SpriteMask>().sprite = TypesOfPuzzle[16];
                        PuzzleCreated.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = ShablonOfPuzzle[16];
                    }
                    else
                    {
                        PuzzleCreated.GetComponent<SpriteMask>().sprite = TypesOfPuzzle[17];
                        PuzzleCreated.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = ShablonOfPuzzle[17];
                    }
                }

                PuzzleCreated.GetComponent<PuzzleScr>().PuzzleGenerator = gameObject;
                PuzzleCreated.GetComponent<PuzzleScr>().StartPosition = PuzzleCreated.transform.position;
                PuzzleCreated.transform.SetParent(Scroll.transform.GetChild(0).GetChild(0));
                PuzzleCreated.transform.SetSiblingIndex(Random.Range(0,PuzzlesXCount*PuzzlesYCount));

            }
        }


    }

    // Update is called once per frame

    Vector3 DeltaMouse,MousePos;
    GameObject MosaicMove;

    

    public int TimeDeltaMouse = 15;
    public float DistanceDeltaMouse = 50f;

    int TimeNow=0;
    float XClick = 0;

    bool isTakeMosaic = false;
    void Update()
    {

        //Debug.Log("Working...");


        TimeNow++;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        MousePos = ray.origin;

        if (Input.GetMouseButtonDown(0))
        {
            
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            

            if (hit.collider != null && hit.collider.gameObject.tag=="Mosaic")
        {
              //  Debug.Log(hit.collider.gameObject.name);
                MosaicMove = hit.collider.gameObject;

                DeltaMouse = MousePos - MosaicMove.transform.position;
                XClick = MousePos.x;

                TimeNow = 0;
                isTakeMosaic = false;
            }
        }

        if (TimeNow == TimeDeltaMouse && MosaicMove!=null)
        {
            if (XClick- MousePos.x >= DistanceDeltaMouse)
            {
                MosaicMove.GetComponent<PuzzleScr>().SiblingIndex = MosaicMove.transform.GetSiblingIndex();
                isTakeMosaic = true;
                Scroll.GetComponent<ScrollRect>().vertical = false;
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (isTakeMosaic)
            {
                if (MosaicMove.transform.parent != null)
                {
                    MosaicMove.transform.parent = null;
                }

                if (MosaicMove.GetComponent<PuzzleScr>().isMovable)
                {

                    MosaicMove.transform.position = new Vector3(MousePos.x - DeltaMouse.x, MousePos.y - DeltaMouse.y, MosaicMove.transform.position.z);
                }
                else
                {
                  //  isTakeMosaic = false;
                }


                }

        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isTakeMosaic)
            {

                if (MosaicMove.GetComponent<PuzzleScr>().isMovable)
                {
                    MosaicMove.transform.SetParent(Scroll.transform.GetChild(0).GetChild(0));
                    MosaicMove.transform.SetSiblingIndex(MosaicMove.GetComponent<PuzzleScr>().SiblingIndex);
                }


                isTakeMosaic = false;
                Scroll.GetComponent<ScrollRect>().vertical = true;
            }
        }




       





    }

   
}
