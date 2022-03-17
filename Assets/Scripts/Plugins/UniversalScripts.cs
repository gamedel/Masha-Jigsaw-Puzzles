using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Spine.Unity;
public class UniScrs: MonoBehaviour
    {
    

    #region BinaryFiles
    public static void LoadBinary(ref byte[] _bytes,string filename)
        {

        //   if (Application.platform == RuntimePlatform.Android)
        //    {
        //        _bytes = System.IO.File.ReadAllBytes(Application.persistentDataPath + "\\Resources\\" + filename);
        //    }
        //    else
        //   {
        TextAsset _tx = Resources.Load(filename) as TextAsset;

        if (_tx != null) {
            _bytes = _tx.bytes;
        }else
        {
            _bytes = new byte[0];
        }
       
          //  _bytes = System.IO.File.ReadAllBytes(Application.dataPath + "\\" + filename);
   //     }
        
    }

    public static void SaveBinary( byte[] _bytes, string filename)
    {
     
            System.IO.FileStream fs;
        if (Application.platform == RuntimePlatform.Android)
        {
            fs = System.IO.File.Create(Application.persistentDataPath + "\\Resources\\" + filename+".bytes");
        }
        else
        {
            fs = System.IO.File.Create(Application.dataPath + "\\Resources\\" + filename + ".bytes");
        }
            fs.Write(_bytes, 0, _bytes.Length);
            fs.Close(); 
    }

    #endregion


    static Vector3 _position;
    public static float[] _Cos = new float[360];
    public static float[] _Sin = new float[360];

    public static void CalcCosSin()
    {
        for (int i = 0; i < 360; i++)
        {
            _Cos[i] = Mathf.Cos(i * Mathf.PI / 180);
            _Sin[i] = Mathf.Sin(i * Mathf.PI / 180);
        }
    }


    public static Vector2 MouseXY()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return new Vector2(ray.origin.x, ray.origin.y);
    }

    public static Vector2 PositionXY(GameObject obj)
    {
        return new Vector2(obj.transform.position.x,obj.transform.position.y); 
    }
    public static Vector2 PositionXY(Transform obj)
    {
        return new Vector2(obj.position.x, obj.position.y);
    }

    public static Vector2 V3ToV2(Vector3 vec3)
    {
        return new Vector2(vec3.x, vec3.y);
    }
    public static Vector3 V2ToV3(Vector2 vec2)
    {
        return new Vector3(vec2.x, vec2.y,0);
    }
    public static RaycastHit2D BoxCastNotObject(GameObject ignoreObject,Vector2 origin, Vector2 size,float angle,Vector2 direction,float distance)
    {
        RaycastHit2D box=new RaycastHit2D();

        RaycastHit2D[] boxes = Physics2D.BoxCastAll(origin, size, angle, direction, distance);

        foreach (RaycastHit2D hit in boxes)
        {
            if (hit.collider.gameObject != ignoreObject)
            {
                return hit;
            }
        }

        return box;
    }

    public static RaycastHit2D MouseRaycast2D()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return Physics2D.Raycast(pos, Vector2.zero);
    }

    public static Collider2D MouseCollider2D()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return Physics2D.Raycast(pos, Vector2.zero).collider;
    }





    public static Vector2 ObjectMakeDeltaMouse(GameObject obj)
    {
        return MouseXY() - new Vector2(obj.transform.position.x, obj.transform.position.y);
    }
    public static void ObjectMoveToMouse(GameObject obj,Vector2 delta_mouse,float koef)
    {
        obj.GetComponent<Rigidbody2D>().velocity = (MouseXY() - new Vector2(obj.transform.position.x, obj.transform.position.y) - delta_mouse) * koef;
    }

    public static GameObject GetObjectByPointS(Vector2 pos)
    {
        GameObject get_object = null;

        Collider2D[] colls = Physics2D.OverlapPointAll(pos);

        if (colls.Length > 0)
        {
            return colls[0].gameObject;
        }
        return get_object;
    }
    public static GameObject GetObjectByPointS(Vector2 pos, string[] tags_except)
    {
        GameObject get_object = null;
        bool not = false;
        Collider2D[] colls = Physics2D.OverlapPointAll(pos);

        for (int i = 0; i < colls.Length; i++)
        {
            not = false;
            if (tags_except.Length > 0)
            {
                for (int j = 0; j < tags_except.Length; j++)
                {
                    if (tags_except[j] == colls[i].gameObject.tag)
                    {
                        not = true;
                        break;

                    }

                }
                if (!not)
                {
                    return colls[i].gameObject;
                }
            }
            else
            {
               
                    return colls[i].gameObject;
                
            }
        }
        return get_object;
    }
    public static GameObject GetObjectByPointS(Vector2 pos, string[] tags_except,GameObject this_object_for_index)
    {
        GameObject get_object = null;
        bool not = false;
        Collider2D[] colls = Physics2D.OverlapPointAll(pos);

        for (int i = 0; i < colls.Length; i++)
        {
            not = false;
            if (tags_except.Length > 0)
            {
                for (int j = 0; j < tags_except.Length; j++)
                {
                    if (tags_except[j] == colls[i].gameObject.tag)
                    {
                        not = true;
                        break;

                    }
                    
                }
                if (!not && colls[i].gameObject.GetComponent<object_index_scr>().index != this_object_for_index.GetComponent<object_index_scr>().index)
                {
                    return colls[i].gameObject;
                }
            }
            else
            {
                if (colls[i].gameObject.GetComponent<object_index_scr>().index != this_object_for_index.GetComponent<object_index_scr>().index)
                {
                    return colls[i].gameObject;
                }
            }
        }
        return get_object;
    }


    public static GameObject GetObjectByPointTag(Vector2 pos, string[] tags_need)
    {
        GameObject get_object = null;
        Collider2D[] colls = Physics2D.OverlapPointAll(pos);

        for (int i = 0; i < colls.Length; i++)
        {
            if (tags_need.Length > 0)
            {
                for (int j = 0; j < tags_need.Length; j++)
                {
                    if (tags_need[j] == colls[i].gameObject.tag)
                    {
                        return colls[i].gameObject;

                    }

                }
               
            }
            else
            {

                    return colls[i].gameObject;

            }
        }
        return get_object;
    }



    public static GameObject GetObjectByPoint(GameObject this_object, Vector2 pos)
    {
        GameObject get_object = null;
        Quaternion rot_obj = Quaternion.Euler(this_object.transform.localRotation.eulerAngles);


        // float center = this_object.GetComponent<BoxCollider2D>().offset.x;
        float size_x = this_object.GetComponent<BoxCollider2D>().size.x;
        float size_y = this_object.GetComponent<BoxCollider2D>().size.y;

        float offset_x = this_object.GetComponent<BoxCollider2D>().offset.x;
        float offset_y = this_object.GetComponent<BoxCollider2D>().offset.y;

        Vector2 rotated_offset = RotatePointAroundPivot(new Vector2(offset_x, offset_y), new Vector2(), rot_obj);

        float center_of_object_x = this_object.transform.position.x + rotated_offset.x;
        float center_of_object_y = this_object.transform.position.y + rotated_offset.y;


        Vector2[] points = new Vector2[4];

        points[0] = RotatePointAroundPivot(new Vector2(-size_x / 2 + offset_x, size_y / 2 + offset_y), new Vector2(), rot_obj);
        points[1] = RotatePointAroundPivot(new Vector2(size_x / 2 + offset_x, size_y / 2 + offset_y), new Vector2(), rot_obj);
        points[2] = RotatePointAroundPivot(new Vector2(-size_x / 2 + offset_x, -size_y / 2 + offset_y), new Vector2(), rot_obj);
        points[3] = RotatePointAroundPivot(new Vector2(size_x / 2 + offset_x, -size_y / 2 + offset_y), new Vector2(), rot_obj);



        float left = Mathf.Min(points[0].x, points[1].x, points[2].x, points[3].x);
        float right = Mathf.Max(points[0].x, points[1].x, points[2].x, points[3].x);
        float up = Mathf.Max(points[0].y, points[1].y, points[2].y, points[3].y);
        float down = Mathf.Min(points[0].y, points[1].y, points[2].y, points[3].y);

        Vector2 coord_collide = new Vector2(center_of_object_x, center_of_object_y);

        if (pos.x > 0) { coord_collide.x = coord_collide.x - rotated_offset.x + right + pos.x; }
        if (pos.x < 0) { coord_collide.x = coord_collide.x - rotated_offset.x + left + pos.x; }

        if (pos.y > 0) { coord_collide.y = coord_collide.y - rotated_offset.y + up + pos.y; }
        if (pos.y < 0) { coord_collide.y = coord_collide.y - rotated_offset.y + down + pos.y; }


        Collider2D[] colls = Physics2D.OverlapPointAll(coord_collide);

        for (int i = 0; i < colls.Length; i++)
        {
            if (colls[i].gameObject != this_object)
            {
                return colls[i].gameObject;

            }

        }


        return get_object;
    }
    public static GameObject GetObjectByPoint(GameObject this_object, Vector2 pos, string[] names_except)
    {
        GameObject get_object = null;
        Quaternion rot_obj = Quaternion.Euler(this_object.transform.localRotation.eulerAngles);


        // float center = this_object.GetComponent<BoxCollider2D>().offset.x;
        float size_x = this_object.GetComponent<BoxCollider2D>().size.x;
        float size_y = this_object.GetComponent<BoxCollider2D>().size.y;

        float offset_x = this_object.GetComponent<BoxCollider2D>().offset.x;
        float offset_y = this_object.GetComponent<BoxCollider2D>().offset.y;

        Vector2 rotated_offset = RotatePointAroundPivot(new Vector2(offset_x, offset_y), new Vector2(), rot_obj);

        float center_of_object_x = this_object.transform.position.x + rotated_offset.x;
        float center_of_object_y = this_object.transform.position.y + rotated_offset.y;


        Vector2[] points = new Vector2[4];

        points[0] = RotatePointAroundPivot(new Vector2(-size_x / 2 + offset_x, size_y / 2 + offset_y), new Vector2(), rot_obj);
        points[1] = RotatePointAroundPivot(new Vector2(size_x / 2 + offset_x, size_y / 2 + offset_y), new Vector2(), rot_obj);
        points[2] = RotatePointAroundPivot(new Vector2(-size_x / 2 + offset_x, -size_y / 2 + offset_y), new Vector2(), rot_obj);
        points[3] = RotatePointAroundPivot(new Vector2(size_x / 2 + offset_x, -size_y / 2 + offset_y), new Vector2(), rot_obj);



        float left = Mathf.Min(points[0].x, points[1].x, points[2].x, points[3].x);
        float right = Mathf.Max(points[0].x, points[1].x, points[2].x, points[3].x);
        float up = Mathf.Max(points[0].y, points[1].y, points[2].y, points[3].y);
        float down = Mathf.Min(points[0].y, points[1].y, points[2].y, points[3].y);

        Vector2 coord_collide = new Vector2(center_of_object_x, center_of_object_y);

        if (pos.x > 0) { coord_collide.x = coord_collide.x - rotated_offset.x + right + pos.x; }
        if (pos.x < 0) { coord_collide.x = coord_collide.x - rotated_offset.x + left + pos.x; }

        if (pos.y > 0) { coord_collide.y = coord_collide.y - rotated_offset.y + up + pos.y; }
        if (pos.y < 0) { coord_collide.y = coord_collide.y - rotated_offset.y + down + pos.y; }


        Collider2D[] colls = Physics2D.OverlapPointAll(coord_collide);

        for (int i = 0; i < colls.Length; i++)
        {
            if (colls[i].gameObject != this_object)
            {
                if (names_except.Length > 0)
                {
                    for (int j = 0; j < names_except.Length; j++)
                    {
                        if (names_except[j] != colls[i].gameObject.name)
                        {
                            return colls[i].gameObject;
                        }
                    }
                }
                else
                {
                    return colls[i].gameObject;
                }
            }

        }


        return get_object;
    }



    static Vector2 RotatePointAroundPivot(Vector2 point, Vector2 pivot, Quaternion angles)
    {
        Vector2 dir = point - pivot; // get point direction relative to pivot
        dir = angles * dir; // rotate it
        point = dir + pivot; // calculate rotated point
        return point; // return it
    }


    public static int CountObjects(string obj)
    {
        int count = 0;

        GameObject[] objs = FindObjectsOfType<GameObject>();

        foreach (GameObject ob in objs)
        {
            if (ob.name == obj)
            {
                count++;
            }
        }
            return count;
    }

    public static int CountObjectsWithTag(string tag)
    {
        //int count = 0;

        GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);

        
        return objs.Length;
    }

    public static GameObject RandomObject(GameObject[] objs)
    {
        if (objs.Length > 0)
        {
            return objs[Random.Range(0, objs.Length)];
        }
        else
        {

            return null;
        }

    }
    public static string RandomString(string[] strs)
    {
        if (strs.Length > 0)
        {
            return strs[Random.Range(0, strs.Length)];
        }
        else
        {

            return "";
        }

    }

    public static int RandomWeightedChoose(float[] weights)
    {

        float SumWeights=0;
        float current=0, last=0;

        for (int i = 0; i < weights.Length; i++)
        {
            SumWeights += weights[i];
        }
        float RandomWeight=Random.Range(0, SumWeights);

        for (int i = 0; i < weights.Length; i++)
        {
            
            if (i == 0)
            {
                current = weights[i];
                if (RandomWeight >= 0 && RandomWeight < weights[i])
                {
                    return i;
                }
            }
            else
            {
                last = current;
                current = last + weights[i];
                //weights[i] = weights[i - 1] + weights[i];
                if (RandomWeight >= last && RandomWeight < current)
                {
                    return i;
                }
            }
        }
        return 0;
    }

    public static float SqrtDistance2(Vector3 vec1, Vector3 vec2)
    {
        Vector2 heading;

        heading.x = vec1.x - vec2.x;
        heading.y = vec1.y - vec2.y;

        return heading.x * heading.x + heading.y * heading.y;
    }

    public static float SqrtDistance2(Vector2 vec1, Vector2 vec2)
    {
        Vector2 heading;

        heading.x = vec1.x - vec2.x;
        heading.y = vec1.y - vec2.y;

        return heading.x * heading.x + heading.y * heading.y;
    }
    public static float SqrtMagnitude2(Vector3 vec1)
    {

        return vec1.x * vec1.x + vec1.y * vec1.y;
    }
    public static float SqrtMagnitude2(Vector2 vec1)
    {

        return vec1.x * vec1.x + vec1.y * vec1.y;
    }

    //__________________________________________________________________________________Angles and Moves_________________________________________________________________
    public static float AngleBetween2(Vector2 vec1, Vector2 vec2)
    {
        //Vector2 diference = vec2 - vec1;
        //float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
        return From180To360(Vector2.Angle(Vector2.right, vec2 - vec1) * ((vec2.y < vec1.y) ? -1.0f : 1.0f));
    }
    public static float From180To360(float start_angle)
    {
        float angle = start_angle;
        if (angle < 0)
        {
            angle = 360 + angle;
        }
        return angle;

    }

    public static float AngleDifference(float angle1, float angle2)
    {
        float diff = (angle2 - angle1 + 180) % 360 - 180;
        return diff < -180 ? diff + 360 : diff;
    }
    public static bool IsAngleInRange(float targetAngle,float min, float max)
    {
        var normalisedMin = min > 0 ? min : 2 * Mathf.PI + min;
        var normalisedMax = max > 0 ? max : 2 * Mathf.PI + max;
        var normalisedTarget = targetAngle > 0 ? targetAngle : 2 * Mathf.PI + targetAngle;

        return normalisedMin <= normalisedTarget && normalisedTarget <= normalisedMax;
    }

    //public static bool IsAngleInRange(int x, int a, int b)
    //{
    //    b = modN(b - a);
    //    x = modN(x - a);

    //    if (b < 180)
    //    {
    //        return x < b;
    //    }
    //    else
    //    {
    //        return b < x;
    //    }
    //}

    public static int modN(int x)
    {
        const int N = 360;
        int m = x % N;
        if (m < 0)
        {
            m += N;
        }
        return m;
    }


    public static bool IsAngleInDelta(float angle, float delta, float check_angle)
    {
        return IsAngleInRange((int)check_angle, (int)(angle - delta / 2), (int)(angle + delta / 2));

    }

    public static bool IsObjectInField(Transform ObjSee, float AngleSee, GameObject ObjInField, float FieldAngle, float FieldDistance)
    {
        if (IsAngleInDelta(AngleSee, FieldAngle, AngleBetween2(PositionXY(ObjSee), PositionXY(ObjInField))) && SqrtDistance2(PositionXY(ObjSee), PositionXY(ObjInField)) <= FieldDistance* FieldDistance)
        {
            return true;
        }

        return false;
    }

    public static bool IsObjectInField(GameObject ObjSee, float AngleSee, GameObject ObjInField, float FieldAngle, float FieldDistance)
    {
        if (IsAngleInDelta(AngleSee, FieldAngle, AngleBetween2(PositionXY(ObjSee), PositionXY(ObjInField)))  && SqrtDistance2(PositionXY(ObjSee), PositionXY(ObjInField)) <= FieldDistance* FieldDistance)
        {
            return true;
        }

        return false;
    }
    public static bool IsObjectInField(GameObject ObjSee,GameObject ObjInField,float FieldAngle,float FieldDistance)
    {
        if (IsAngleInDelta(ObjSee.transform.eulerAngles.z /*+ 90*/, FieldAngle, AngleBetween2(PositionXY(ObjSee),PositionXY(ObjInField))) && SqrtDistance2(PositionXY(ObjSee), PositionXY(ObjInField)) <= FieldDistance* FieldDistance)
        {
            return true;
        }

        return false;
    }

    public static bool IsObjectInField(GameObject ObjSee, GameObject ObjInField, float FieldAngle, float FieldDistance,float ObjSee_AnglePlus)
    {
        if (IsAngleInDelta(ObjSee.transform.eulerAngles.z + ObjSee_AnglePlus, FieldAngle, AngleBetween2(PositionXY(ObjSee), PositionXY(ObjInField))) && Vector2.Distance(PositionXY(ObjSee), PositionXY(ObjInField)) <= FieldDistance)
        {
            return true;
        }

        return false;
    }

    public static bool IsPointInField(GameObject ObjSee, Vector2 ObjInField, float FieldAngle, float FieldDistance)
    {
        if (IsAngleInDelta(ObjSee.transform.eulerAngles.z /*+ 90*/, FieldAngle, AngleBetween2(PositionXY(ObjSee), ObjInField)) && Vector2.Distance(PositionXY(ObjSee), ObjInField) <= FieldDistance)
        {
            return true;
        }

        return false;
    }
    public static bool IsPointInField(GameObject ObjSee, Vector2 ObjInField, float FieldAngle, float FieldDistance, float ObjSee_AnglePlus)
    {
        if (IsAngleInDelta(ObjSee.transform.eulerAngles.z +ObjSee_AnglePlus, FieldAngle, AngleBetween2(PositionXY(ObjSee), ObjInField)) && Vector2.Distance(PositionXY(ObjSee), ObjInField) <= FieldDistance)
        {
            return true;
        }

        return false;
    }


    public static void RotateToPoint(GameObject RotObj, Vector2 TargPoint, float speed)
    {
        RotateToPoint(RotObj,TargPoint, speed, 0f);

    }

    public static void RotateToPoint(GameObject RotObj, Vector2 TargPoint, float speed, float RotObj_AnglePlus)
    {
        RotObj.transform.eulerAngles = new Vector3(0, 0, Mathf.MoveTowardsAngle(RotObj.transform.eulerAngles.z, AngleBetween2(PositionXY(RotObj), TargPoint) - RotObj_AnglePlus, speed * Time.deltaTime));
       // RotObj.transform.eulerAngles.Set(0,0,Mathf.MoveTowardsAngle(RotObj.transform.eulerAngles.z, AngleBetween2(PositionXY(RotObj), TargPoint) - RotObj_AnglePlus, speed * Time.deltaTime));
    }
  

    public static void RotateToPoint(Transform RotObj, Vector2 TargPoint, float speed, float RotObj_AnglePlus)
    {
        RotObj.eulerAngles = new Vector3(0, 0, Mathf.MoveTowardsAngle(RotObj.eulerAngles.z, AngleBetween2(PositionXY(RotObj), TargPoint) - RotObj_AnglePlus, speed * Time.deltaTime));
    }
    public static void MoveToAngle(GameObject MoveObj,float angle,float speed)
    {    
        MoveToAngle(MoveObj, angle, speed, 0);
    }
    public static void MoveToAngle(GameObject MoveObj, float angle, float speed, float MoveObj_AnglePlus)
    {
        // Quaternion.Euler(0, 0, transform.eulerAngles.z + 90) * Vector2.right
        //Vector3 qua = Quaternion.Euler(0, 0, angle + MoveObj_AnglePlus) * Vector2.right;

    MoveObj.transform.position += Quaternion.Euler(0, 0, angle + MoveObj_AnglePlus) * Vector2.right * speed * Time.deltaTime;
    }

    public static GameObject CreateUIObject(GameObject _Prefab, Vector3 Position = new Vector3(), float Angle = 0, Vector3 Scale = new Vector3())
    {
        GameObject obj = GameObject.Instantiate(_Prefab, Position, Quaternion.Euler(0, 0, Angle));
        obj.transform.SetParent(GameObject.Find("Canvas").transform);
        obj.GetComponent<RectTransform>().localPosition = Position;
        obj.GetComponent<RectTransform>().anchoredPosition = Position;
        obj.GetComponent<RectTransform>().localScale = (Scale.x == 0 ? new Vector3(1, 1, 1) : Scale);


        return obj;
    }
    public static void MoveToAngle(Transform MoveObj, float angle, float speed, float MoveObj_AnglePlus)
    {
        // Quaternion.Euler(0, 0, transform.eulerAngles.z + 90) * Vector2.right
        //Vector3 qua = Quaternion.Euler(0, 0, angle + MoveObj_AnglePlus) * Vector2.right;

         MoveObj.position += Quaternion.Euler(0, 0, angle + MoveObj_AnglePlus) * Vector2.right * speed * Time.deltaTime;
        //MoveObj.GetComponent<Rigidbody2D>().velocity= Quaternion.Euler(0, 0, angle + MoveObj_AnglePlus) * Vector2.right * speed;
      
    }

    //__________________________________________________________________________________Sprites_________________________________________________________________

       public static Texture2D GetSpriteAtlasTexture(Sprite sprt) //Get sprite`s texture from atlas
    {
        Rect _rect = sprt.textureRect;
        Texture2D sprt_tex = new Texture2D((int)_rect.width, (int)_rect.height);

        sprt_tex.SetPixels(sprt.texture.GetPixels((int)_rect.x, (int)_rect.y, (int)_rect.width, (int)_rect.height));
        sprt_tex.Apply();

        return sprt_tex;
    }

    //_________________________________________________________________________________________________________________________________________________________



    public static float FloatTo2DP(float number)
    {

        return Mathf.Round(number * 100f) / 100f;
    }




    public static void PlayerPrefsBoolSet(string name,bool _bool)
    {
        PlayerPrefs.SetInt(name, _bool ? 1 : 0);
        PlayerPrefs.Save();
    }

    public static bool PlayerPrefsBoolGet(string name,bool DefaultValue=false)
    {
       return PlayerPrefs.GetInt(name, DefaultValue ? 1 : 0) ==1?true:false;
    }

    public static void PlayerPrefsIntArraySet(string name, int[] array)
    {
        string StrArray = "";

        for (int i = 0; i < array.Length; i++)
        {
            if (i > 0) { StrArray += "|"; }

            StrArray += array[i].ToString();
            
        }

        PlayerPrefs.SetString(name, StrArray);
        PlayerPrefs.Save();

    }

   public static int[] PlayerPrefsIntArrayGet(string name)
    {

        string Str = PlayerPrefs.GetString(name,"");
        string[] StrArray;
        int[] IntArray;
        if (Str == "")
        {
            return new int[] { };
        }else
        {
            StrArray = Str.Split('|');
            IntArray = new int[StrArray.Length];
            for (int i = 0; i < StrArray.Length; i++)
            {
                IntArray[i] = int.Parse(StrArray[i]);
            }

            return IntArray;
        }



    }

    public static string IntArrayToStr(int[] array)
    {
        string newstr = "";

   
        for (int i = 0; i < array.Length; i++)
        {
            if (i > 0) { newstr += "|"; }
            newstr += array[i].ToString();
        }
        return newstr;
    }


    /*
    public static string AnimName (GameObject obj)
    {
        return obj.GetComponent<SkeletonAnimation>().state.GetCurrent(0).Animation.name;
    }

    public static bool isAnimComplete(GameObject obj)
    {
        return obj.GetComponent<SkeletonAnimation>().state.GetCurrent(0).IsComplete;
    }


    public static void SetAnim(GameObject obj, string Anim, bool isLoop)
    {
        obj.GetComponent<SkeletonAnimation>().state.SetAnimation(0, Anim, isLoop);
    }

    public static void SetAnim(GameObject obj, string Anim)
    {
        SetAnim(obj, Anim, false);
    }
    */
}

public class TimerClass
{

    public static int count_timers0;


    public TimerClass(int count_timers)
    {
        count_timers0 = count_timers;
    }


    int[] now_timers = new int[count_timers0];
    public int[] timers = new int[count_timers0];
    public bool[] isloop = new bool[count_timers0];
    bool[] timer_start = new bool[count_timers0];

    public void Timer_start(int numTimer, int Time)
    {
        Timer_start(numTimer, Time, false);
    }

    public void Timer_start(int numTimer, int Time,bool isLoop)
    {
        timer_start[numTimer] = true;
        timers[numTimer] = Time;
        isloop[numTimer] = isLoop;
    }

    public void Timer_stop(int numTimer)
    {
        timer_start[numTimer] = false;
    }


    public void TimerStep()
    {
        for (int i = 0; i < timers.Length; i++)
        {
            if (timer_start[i] == true)
            {
                if (now_timers[i] < timers[i])
                {
                    now_timers[i]++;

                }
                else
                {
                    if (isloop[i] == true)
                    {

                        now_timers[i] = 0;
                    }

                }

            }
        }

    }


    public bool isTimeEnd(int numTime)
    {
        if (now_timers[numTime] == timers[numTime] && timer_start[numTime])
        {
            if (!isloop[numTime]) { timer_start[numTime] = false; }
            return true;
        }
        else
        {
            return false;
        }

    }





}




