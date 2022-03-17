using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using EazyTools.SoundManager;
public class PuzzleGeneratorNew: MonoBehaviour {

    public AudioClip[] Sounds;

    public SpriteAtlas _atlas;

    public GameObject PuzzleObj;

    public Sprite _Image;
    public Sprite PuzzleImg;
    public Sprite ShablonImg;

    public Sprite[] PuzzlesImg;
    Texture2D[] PuzzlesTex;
    public Sprite[] ShablonOfPuzzle;

    Texture2D tex;
    Texture2D ImageTex;
    Texture2D PuzzleTex;
    Texture2D ShablonTex;
    Color[] fillPixels;
    Color EmptyColor;

    public Sprite[] MosaicPicts;
    public int MosaicIndex = 0;
    public GameObject Setka;
    public Sprite[] SetkaImgs;

    public float PuzzleWidth, PuzzleHeight;
    public float PictureWidth, PictureHeight;
    public float PuzzleStartX = 3;
    public float PuzzleStartY = 4;

    int PuzzlesXCount, PuzzlesYCount;

    public Vector2 ImagePosition;
    float PuzzleXCreate, PuzzleYCreate;
    GameObject PuzzleCreated;
    public float PuzzleXKoef, PuzzleYKoef;

    int NumPuzzle = 0;

    Texture2D[] AtlasTexturesPack;
    Rect[] AtlasRectsPack;
    Texture2D AtlasTexture;
    int[] IndexPivotSprite;

    GameObject[] Mosaics;


    public GameObject Scroll;

    public float DistanceToMovePuzzle = 200f;

    public int CollectedPuzzles = 0;
    public bool isVictory = false;
    public GameObject VictoryImage;
    GameObject VictoryImageObj;

    public GameObject[] SalutObjs;
    public GameObject MashaObj;

    GameObject GeneralObj;

    public GameObject BtHome;


    Texture2D _ImageTex;
    // Use this for initialization
    void Start () {
        

        

        GeneralObj = GameObject.Find("GeneralObj");
        PuzzleStartX = GeneralObj.GetComponent<GeneralScr>().LoadedXCount;
        PuzzleStartY = GeneralObj.GetComponent<GeneralScr>().LoadedYCount;
        _Image = _atlas.GetSprite(GeneralObj.GetComponent<GeneralScr>().LoadedPuzzleImg.name.Replace("_m",""));

        _ImageTex=UniScrs.GetSpriteAtlasTexture(_Image);

        if (GeneralObj.GetComponent<GeneralScr>().isMusic)
        {
            SoundManager.PlaySound(Sounds[2]);
        }
        // Debug.Log(_Image.textureRect);
        // GeneralObj.GetComponent<GeneralScr>().LoadedPuzzleImg;

        //byte[] kkk=_Image.texture.EncodeToEXR();
        //  Texture2D ttx = new Texture2D(2, 2);
        //  ImageConversion.LoadImage(ttx, kkk);


        CollectedPuzzles = 0;
        isVictory = false;
        InvokeRepeating("Timer", 1f, 0.3f);

        if (PuzzleStartX == 4 && PuzzleStartY == 3)
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

        Setka.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = _Image;

        VictoryImageObj = GameObject.Instantiate(VictoryImage, new Vector3(ImagePosition.x, ImagePosition.y), new Quaternion());
        VictoryImageObj.GetComponent<SpriteRenderer>().sprite = _Image;
        VictoryImageObj.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);

        tex = new Texture2D(PuzzleImg.texture.width, PuzzleImg.texture.height);
        //ImageTex = _Image.texture;
        ImageTex = UniScrs.GetSpriteAtlasTexture(_Image);

        PuzzleTex = PuzzleImg.texture;
        ShablonTex = ShablonImg.texture;
        EmptyColor = new Color(0, 0, 0, 0);
        PuzzlesTex = new Texture2D[PuzzlesImg.Length];
        AtlasTexturesPack = new Texture2D[(int)(PuzzleStartX * PuzzleStartY)];
        AtlasRectsPack = new Rect[(int)(PuzzleStartX * PuzzleStartY)];
        Mosaics = new GameObject[(int)(PuzzleStartX * PuzzleStartY)];
        IndexPivotSprite = new int[(int)(PuzzleStartX * PuzzleStartY)];
        Texture2D AtlasTexture = new Texture2D(2048, 2048);

      
        for (int i = 0; i < PuzzlesImg.Length; i++)
        {
            PuzzlesTex[i] = PuzzlesImg[i].texture;
        }

        PuzzleXKoef = 4f / PuzzleStartX;
        PuzzleYKoef = 3f / PuzzleStartY;

        PuzzlesXCount = Mathf.CeilToInt(PictureWidth / (PuzzleWidth * PuzzleXKoef));
        PuzzlesYCount = Mathf.CeilToInt(PictureHeight / (PuzzleHeight * PuzzleYKoef));


        for (int i = 0; i < PuzzlesXCount; i++)
        {
            for (int j = 0; j < PuzzlesYCount; j++)
            {

                NumPuzzle = 0;

                if (j == 0 && i > 0 && i < PuzzlesXCount - 1)
                {
                    if ((i + j) % 2 == 0)
                    {
                        NumPuzzle = 8;
                    }
                    else
                    {
                        NumPuzzle = 12;
                    }
                }

                if (j == 0 && i == PuzzlesXCount - 1)
                {
                    if ((i + j) % 2 == 0)
                    {
                        NumPuzzle = 1;
                    }
                    else
                    {
                        NumPuzzle = 5;
                    }
                }

                if (j > 0 && j < PuzzlesYCount - 1 && i == PuzzlesXCount - 1)
                {

                    if ((i + j) % 2 == 0)
                    {
                        NumPuzzle = 9;
                    }
                    else
                    {
                        NumPuzzle = 13;
                    }
                }

                if (j == PuzzlesYCount - 1 && i == PuzzlesXCount - 1)
                {
                    if ((i + j) % 2 == 0)
                    {
                        NumPuzzle = 3;
                    }
                    else
                    {
                        NumPuzzle = 7;
                    }
                }

                if (j == PuzzlesYCount - 1 && i > 0 && i < PuzzlesXCount - 1)
                {
                    if ((i + j) % 2 == 0)
                    {
                        NumPuzzle = 10;
                    }
                    else
                    {
                        NumPuzzle = 14;
                    }
                }



                if (j == PuzzlesYCount - 1 && i == 0)
                {
                    if ((i + j) % 2 == 0)
                    {
                        NumPuzzle = 2;
                    }
                    else
                    {
                        NumPuzzle = 6;
                    }
                }


                if (j > 0 && j < PuzzlesYCount - 1 && i == 0)
                {

                    if ((i + j) % 2 == 0)
                    {
                        NumPuzzle = 11;
                    }
                    else
                    {
                        NumPuzzle = 15;
                    }
                }


                if (j > 0 && j < PuzzlesYCount - 1 && i > 0 && i < PuzzlesXCount - 1)
                {

                    if ((i + j) % 2 == 0)
                    {
                        NumPuzzle = 16;
                    }
                    else
                    {
                        NumPuzzle = 17;
                    }
                }


                GameObject puzzl = GameObject.Instantiate(PuzzleObj, new Vector3(ImagePosition.x - PictureWidth / 2 + PuzzleWidth / 2 * PuzzleXKoef + i * PuzzleWidth * PuzzleXKoef, ImagePosition.y + PictureHeight / 2 - PuzzleHeight / 2 * PuzzleYKoef - j * PuzzleHeight * PuzzleYKoef), new Quaternion());

                AtlasTexturesPack[i + j * PuzzlesXCount] = MakePuzzle(PuzzlesImg[NumPuzzle], i, j);
                Mosaics[i + j * PuzzlesXCount] = puzzl;
                IndexPivotSprite[i + j * PuzzlesXCount] = NumPuzzle;
                //  puzzl.GetComponent<SpriteRenderer>().sprite= MakePuzzle(PuzzlesImg[NumPuzzle],i,j);
                puzzl.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = ShablonOfPuzzle[NumPuzzle];
                puzzl.transform.GetChild(0).localScale = new Vector3(PuzzleXKoef, PuzzleYKoef, 1);

                puzzl.GetComponent<PuzzleScr>().PuzzleGenerator = gameObject;
                puzzl.GetComponent<PuzzleScr>().StartPosition = puzzl.transform.position;
                puzzl.transform.SetParent(Scroll.transform.GetChild(0).GetChild(0));
                puzzl.transform.SetSiblingIndex(Random.Range(0, PuzzlesXCount * PuzzlesYCount));

                puzzl.transform.localScale = new Vector3(1/PuzzleXKoef*0.6f, 1 / PuzzleYKoef * 0.6f, 1);

            }
        }

        AtlasRectsPack = AtlasTexture.PackTextures(AtlasTexturesPack, 2, 2048);

        for (int i = 0; i < PuzzlesXCount; i++)
        {
            for (int j = 0; j < PuzzlesYCount; j++)
            {

                Mosaics[i + j * PuzzlesXCount].GetComponent<SpriteRenderer>().sprite = Sprite.Create(AtlasTexture, new Rect(AtlasRectsPack[i + j * PuzzlesXCount].x * AtlasTexture.width, AtlasRectsPack[i + j * PuzzlesXCount].y * AtlasTexture.height, AtlasRectsPack[i + j * PuzzlesXCount].width * AtlasTexture.width, AtlasRectsPack[i + j * PuzzlesXCount].height * AtlasTexture.height), GetSpritePivot(PuzzlesImg[IndexPivotSprite[i + j * PuzzlesXCount]]), 1f);
            }
        }

    }



    void Timer()
    {
        if (isVictory)
        {
            GameObject.Instantiate(UniScrs.RandomObject(SalutObjs), new Vector3(ImagePosition.x - PictureWidth / 2 + Random.Range(0, PictureWidth), ImagePosition.y - PictureHeight / 2 + Random.Range(0, PictureHeight)), new Quaternion());
        }
    }


    // Update is called once per frame

    Vector3 DeltaMouse, MousePos;
    GameObject MosaicMove;

    public int TimeDeltaMouse = 15;
    public float DistanceDeltaMouse = 50f;

    int TimeNow = 0;
    float XClick = 0;
    float YClick = 0;

    bool isTakeMosaic = false;
    void Update () {
        
        if (DBPlugin.isAnimCompleted(MashaObj))
        {
            DBPlugin.PlayAnim(MashaObj, "Stand");
        }


        if (isVictory)
        {
            if (GeneralObj.GetComponent<GeneralScr>().isMusic)
            {
                if (!SoundManager.isSoundPlaying(Sounds[3]))
                {
                    SoundManager.PlaySound(Sounds[3]);
                }
            }

            if (VictoryImageObj.GetComponent<SpriteRenderer>().color.a < 1)
            {
                VictoryImageObj.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 0.02f);
            }
        }
        else
        {
            if (CollectedPuzzles >= PuzzlesXCount * PuzzlesYCount)
            {
                DBPlugin.PlayAnim(MashaObj, "Hlop",1);
                isVictory = true;
                BtHome.GetComponent<Animator>().enabled = true;            }
        }



        TimeNow++;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        MousePos = ray.origin;

        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);



            if (hit.collider != null && hit.collider.gameObject.tag == "Mosaic")
            {
                //  Debug.Log(hit.collider.gameObject.name);
                MosaicMove = hit.collider.gameObject;

                DeltaMouse = MousePos - MosaicMove.transform.position;
                XClick = MousePos.x;
                YClick = MousePos.y;

                TimeNow = 0;
                isTakeMosaic = false;
            }
        }

        if (TimeNow == TimeDeltaMouse && MosaicMove != null)
        {
            if (XClick - MousePos.x >= DistanceDeltaMouse && (Mathf.Abs(XClick - MousePos.x)> Mathf.Abs(YClick - MousePos.y)))
            {
                MosaicMove.GetComponent<PuzzleScr>().SiblingIndex = MosaicMove.transform.GetSiblingIndex();
                isTakeMosaic = true;
                Scroll.GetComponent<ScrollRect>().vertical = false;
                MosaicMove.transform.localScale = new Vector3(1 / PuzzleXKoef * 0.6f, 1 / PuzzleYKoef * 0.6f, 1);
                MosaicMove.transform.localScale = new Vector3(MosaicMove.transform.localScale.x * PuzzleXKoef / 0.6f, MosaicMove.transform.localScale.y * PuzzleYKoef / 0.6f, 1);
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (isTakeMosaic)
            {
                if (MosaicMove.transform.parent != null)
                {
                   // MosaicMove.transform.parent = null;
                    MosaicMove.transform.SetParent(null);
                    MosaicMove.transform.localScale = new Vector3(1 / PuzzleXKoef * 0.6f, 1 / PuzzleYKoef * 0.6f, 1);
                    MosaicMove.transform.localScale = new Vector3(MosaicMove.transform.localScale.x * PuzzleXKoef / 0.6f, MosaicMove.transform.localScale.y * PuzzleYKoef / 0.6f, 1);
                    MosaicMove.GetComponent<SpriteRenderer>().sortingOrder = 7;
                    MosaicMove.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 8;
                }

                if (MosaicMove.GetComponent<PuzzleScr>().isMovable)
                {

                     MosaicMove.transform.position = new Vector3(MousePos.x - DeltaMouse.x, MousePos.y - DeltaMouse.y, MosaicMove.transform.position.z);
                  //  MosaicMove.transform.position= Vector3.MoveTowards(MosaicMove.transform.position, new Vector3(MousePos.x - DeltaMouse.x, MousePos.y - DeltaMouse.y, MosaicMove.transform.position.z),Vector3.Distance(new Vector3(MousePos.x - DeltaMouse.x, MousePos.y - DeltaMouse.y, MosaicMove.transform.position.z), MosaicMove.transform.position)*100f * Time.deltaTime);

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
                    MosaicMove.transform.localScale = new Vector3(1 / PuzzleXKoef * 0.6f, 1 / PuzzleYKoef * 0.6f, 1);


                    MosaicMove.GetComponent<SpriteRenderer>().sortingOrder = 5;
                    MosaicMove.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 6;

                    if (DBPlugin.isAnimNameHas(MashaObj, "Stand") && MousePos.x < 400)
                    {
                        if (GeneralObj.GetComponent<GeneralScr>().isMusic )
                        {
                            SoundManager.PlaySound(Sounds[Random.Range(0,2)]);
                        }
                        DBPlugin.PlayAnim(MashaObj, "Shake_Head",1);
                    }
                }


                isTakeMosaic = false;
                Scroll.GetComponent<ScrollRect>().vertical = true;
            }
        }




    }

    public void build()
    {
       
    }



    int XX, YY, WW, HH,XX0,YY0;
    int XXa, YYa, WWa, HHa;
    
    Texture2D MakePuzzle(Sprite PuzzleInd, int XCoord,int YCoord)
    {

        Texture2D MainPuzzleTexture = new Texture2D((int)(PuzzleInd.texture.width* PuzzleXKoef), (int)(PuzzleInd.texture.height* PuzzleYKoef));

        XX0 = (int)(PuzzleWidth / 2* PuzzleXKoef + XCoord * PuzzleWidth* PuzzleXKoef - PuzzleInd.pivot.x* PuzzleXKoef);
        YY0 = (int)(ImageTex.height - PuzzleHeight/2* PuzzleYKoef - YCoord * PuzzleHeight* PuzzleYKoef - PuzzleInd.pivot.y* PuzzleYKoef);

        XX = Mathf.Max(XX0,0); XXa = Mathf.Abs(Mathf.Min(XX0, 0));
        YY = Mathf.Max(YY0, 0); YYa = Mathf.Abs(Mathf.Min(YY0, 0));
        WW = Mathf.Min(MainPuzzleTexture.width-XXa, ImageTex.width - XX); WWa = Mathf.Max((int)(MainPuzzleTexture.width - XXa - (ImageTex.width - XX)), 0);
        HH = Mathf.Min(MainPuzzleTexture.height-YYa, ImageTex.height - YY); HHa = Mathf.Max((int)(MainPuzzleTexture.height - YYa - (ImageTex.height - YY)), 0);


       

        MainPuzzleTexture.SetPixels(XXa, YYa,WW- XXa-WWa, HH-YYa-HHa,ImageTex.GetPixels(XX, YY, WW, HH));

        Texture2D VremShablon = new Texture2D(PuzzleInd.texture.width, PuzzleInd.texture.height, PuzzleInd.texture.format,false);
        Graphics.CopyTexture(PuzzleInd.texture, VremShablon);
        TextureScale.Bilinear(VremShablon, (int)(PuzzleInd.texture.width* PuzzleXKoef), (int)(PuzzleInd.texture.height * PuzzleYKoef));



         for (int i = 0; i < MainPuzzleTexture.width; i++)
         {
             for (int j = 0; j < MainPuzzleTexture.height; j++)
             {
                 if (VremShablon.GetPixel(i, j).a < 0.1f)
                 {
                    MainPuzzleTexture.SetPixel(i, j, EmptyColor);     
                 }
             }
         }
         

        MainPuzzleTexture.Apply();
        return MainPuzzleTexture;
        /*
        Sprite spr = Sprite.Create(MainPuzzleTexture, new Rect(0.0f, 0.0f, MainPuzzleTexture.width, MainPuzzleTexture.height), GetSpritePivot(PuzzleInd), 1f);
        spr.name = "hui";
        return spr;*/
    }


    public Vector2 GetSpritePivot(Sprite sprite)
    {
        Bounds bounds = sprite.bounds;
        var pivotX = -bounds.center.x / bounds.extents.x / 2 + 0.5f;
        var pivotY = -bounds.center.y / bounds.extents.y / 2 + 0.5f;

        return new Vector2(pivotX, pivotY);
    }
    Color _AlphaBlend(Color src, Color dest)
    {
        return Color.Lerp(src, dest, 1 - src.a);
    }
}
