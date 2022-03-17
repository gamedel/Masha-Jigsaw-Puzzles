using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;
public class DBPlugin : MonoBehaviour {

    public static void SkinRefresh(GameObject self)
    {
        UnityEngine.Transform[] childs=new UnityEngine.Transform[self.transform.childCount];
        
        int ii=0;
        foreach (UnityEngine.Transform child in self.transform)
        {
            childs[ii] = child;
            childs[ii].transform.position = new Vector3(childs[ii].transform.position.x, childs[ii].transform.position.y, self.transform.position.z+ ((float)(self.transform.childCount - ii + 1)) / 10000);
            ii++;
        }
       
     
    }
    /*
    public static void ExtremalChangeSkin(GameObject self, string skin)
    {
        _changeArmatureData2(self.GetComponent<UnityArmatureComponent>(), skin);
        SkinRefresh(self);
        self.GetComponent<UnityArmatureComponent>().animation.Play();
    }
    */
    /*
    public static void ChangeSkin(GameObject self,string skin)
    {
        ChangeSkin(self, skin, null);
          //  _changeArmatureData(self.GetComponent<UnityArmatureComponent>(), skin);
          //  SkinRefresh(self);
           // self.GetComponent<UnityArmatureComponent>().animation.Play();
    }
    public static void ChangeSkin(GameObject self, string skin, string animation)
    {
        _changeArmatureData(self.GetComponent<UnityArmatureComponent>(), skin);
        SkinRefresh(self);
        self.GetComponent<UnityArmatureComponent>().animation.Play(animation);
    }*/
    public static bool isSkin(GameObject self, string skin)
    {
        
       if  (self.GetComponent<UnityArmatureComponent>().armatureName== skin)
        {
            return true;
        }
        else
        {
            return false;
        }
       
    }
    public static string GetSkin(GameObject self)
    {
        return self.GetComponent<UnityArmatureComponent>().armatureName;
    }


    public static void PlayAnim(GameObject self, string animation)
    {
         self.GetComponent<UnityArmatureComponent>().animation.Play(animation);
        //self.GetComponent<UnityArmatureComponent>().animation.GotoAndPlayByFrame(animation,10,-1);
       // self.GetComponent<UnityArmatureComponent>().animation.got
    }
    public static void PlayAnim(GameObject self, string animation,int times)
    {
        self.GetComponent<UnityArmatureComponent>().animation.Play(animation,times);

    }

    public static void PlayAnim(GameObject self, string animation, int times,bool isMirror)
    {

        self.GetComponent<UnityArmatureComponent>().animation.Play(animation, times);
        self.GetComponent<UnityArmatureComponent>().armature.flipX = isMirror;

    }

    public static bool isAnimFlip(GameObject self)
    {        
       return self.GetComponent<UnityArmatureComponent>().armature.flipX;
    }


    public static void PlayAnim(GameObject self, string animation, bool isMirror)
    {
        
            self.GetComponent<UnityArmatureComponent>().animation.Play(animation);
            self.GetComponent<UnityArmatureComponent>().armature.flipX = isMirror;
       
    }

    public static void StopAnim(GameObject self)
    {
       // self.GetComponent<UnityArmatureComponent>().zSpace = 0.003f;
        self.GetComponent<UnityArmatureComponent>().animation.Stop();
    }
    public static bool isAnimCompleted(GameObject self)
    {
        return self.GetComponent<UnityArmatureComponent>().animation.isCompleted;
    }
    public static bool isAnimPlaying(GameObject self)
    {
        return self.GetComponent<UnityArmatureComponent>().animation.isPlaying;
    }
    public static bool isAnimNameHas(GameObject self, string animation)
    {
        return self.GetComponent<UnityArmatureComponent>().animation.HasAnimation(animation);
    }
    public static string LastAnimName(GameObject self)
    {
        return self.GetComponent<UnityArmatureComponent>().animation.lastAnimationName;
    }
    public static bool LastAnimName(GameObject self,string animation)
    {
        return (self.GetComponent<UnityArmatureComponent>().animation.lastAnimationName== animation);
    }

    public static float AnimTimeScale(GameObject self)
    {
        //return self.GetComponent<UnityArmatureComponent>().animation;
        return self.GetComponent<UnityArmatureComponent>().animation.timeScale;
    }
    public static List<string> AnimNames(GameObject self)
    {
        return self.GetComponent<UnityArmatureComponent>().animation.animationNames;
    }

    /*
    private static void _changeArmatureData(UnityArmatureComponent _armatureComponent, string armatureName)
    {
        if (_armatureComponent.armatureName != armatureName)
        {

            var dragonBonesData = _armatureComponent.LoadData(_armatureComponent.dragonBonesJSON, _armatureComponent.textureAtlasJSON);

          //  UnityFactory.factory.RefreshAllTextureAtlas();

            Slot slot = null;
            if (_armatureComponent.armature != null)
            {
                slot = _armatureComponent.armature.parent;
                _armatureComponent.Dispose(false);
            }

            _armatureComponent.armatureName = armatureName;
            _armatureComponent = UnityFactory.factory.BuildArmatureComponent(_armatureComponent.armatureName, dragonBonesData.name, null, null, _armatureComponent.gameObject);

            if (slot != null)
            {
                slot.childArmature = _armatureComponent.armature;
            }

            _armatureComponent.sortingLayerName = _armatureComponent.sortingLayerName;
            _armatureComponent.sortingOrder = _armatureComponent.sortingOrder;


          //  _armatureComponent.armature.InvalidUpdate(null, true);
        }
    }
    private static void _changeArmatureData2(UnityArmatureComponent _armatureComponent, string armatureName)
    {
       

            var dragonBonesData = _armatureComponent.LoadData(_armatureComponent.dragonBonesJSON, _armatureComponent.textureAtlasJSON);

            //  UnityFactory.factory.RefreshAllTextureAtlas();

            Slot slot = null;
            if (_armatureComponent.armature != null)
            {
                slot = _armatureComponent.armature.parent;
                _armatureComponent.Dispose(false);
            }

            _armatureComponent.armatureName = armatureName;
            _armatureComponent = UnityFactory.factory.BuildArmatureComponent(_armatureComponent.armatureName, dragonBonesData.name, null, null, _armatureComponent.gameObject);

            if (slot != null)
            {
                slot.childArmature = _armatureComponent.armature;
            }

            _armatureComponent.sortingLayerName = _armatureComponent.sortingLayerName;
            _armatureComponent.sortingOrder = _armatureComponent.sortingOrder;


            //  _armatureComponent.armature.InvalidUpdate(null, true);
        
    }
    */
}
