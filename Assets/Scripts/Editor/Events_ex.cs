
using UnityEditor;
using UnityEngine;
using System.Reflection;

[CustomEditor(typeof(Events_scr))] //Сообщаем редактору, что это класс для кастомизации вкладки инспектора, компонента "Test"

class TestCustomize : Editor
{ //Наследуем наш класс кастомизации, от редактора Юнити

    [Header("Main")]

    private SerializedProperty index1;
    private SerializedProperty index2;
    private SerializedProperty object_script;
    private SerializedProperty args;
    private SerializedProperty method_name;
    private SerializedProperty obb;


    public void OnEnable()
    {
        index1 = serializedObject.FindProperty("index1");
        index2 = serializedObject.FindProperty("index2");
        object_script = serializedObject.FindProperty("object_script");
        args = serializedObject.FindProperty("args");
        method_name = serializedObject.FindProperty("method_name");
        obb = serializedObject.FindProperty("obb");
    }


    public override void OnInspectorGUI()
    { //Сообщаем редактору, что этот инспектор заменит прежний (встроеный)

        serializedObject.Update();



        object_script.stringValue = EditorGUILayout.TextField(object_script.stringValue);

        GameObject ob = null;
        GameObject ob0 = GameObject.Find(object_script.stringValue);

       
        
        if (ob0 != null)
        {

            ob = ob0;
            //  ob = EditorGUILayout.ObjectField(ob, typeof(GameObject)) as GameObject;


        }
        else
        {
            obb.objectReferenceValue = EditorGUILayout.ObjectField(obb.objectReferenceValue as GameObject, typeof(GameObject)) as GameObject;
            if (obb.objectReferenceValue != null)
            {
                ob = obb.objectReferenceValue as GameObject;
                object_script.stringValue = ob.name;
            }
        }
      

        


        if (ob != null)
        {
            //obb.objectReferenceValue = ob;

            MonoBehaviour[] mons = ((GameObject)(ob)).GetComponents<MonoBehaviour>();

            string[] mons_names = new string[mons.Length];

            for (int i = 0; i < mons.Length; i++)
            {
                mons_names[i] = mons[i].GetType().FullName;
            }

            index1.intValue = EditorGUILayout.Popup(index1.intValue, mons_names);

            if (mons.Length > 0)
            {

                BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;

                MethodInfo[] methods = MonoScript.FromMonoBehaviour(mons[Mathf.Min(index1.intValue, mons.Length - 1)]).GetClass().GetMethods(flags);
                string[] methods_names = new string[methods.Length];

                for (int i = 0; i < methods.Length; i++)
                {
                    methods_names[i] = methods[i].Name;
                }

                if (methods.Length > 0)
                {
                    index2.intValue = EditorGUILayout.Popup(index2.intValue, methods_names);

                    method_name.stringValue = methods_names[index2.intValue];

                    ParameterInfo[] params0 = methods[index2.intValue].GetParameters();

                    if (params0.Length > 0)
                    {

                        for (int i = 0; i < params0.Length; i++)
                        {
                            //args = serializedObject.FindProperty(string.Format("args.Array.data[{0}]", i));

                            args.stringValue = GUILayout.TextField(args.stringValue, 25);

                        }

                        //  int hh = 3; hh++;
                    }

                }

            }




        }



        if (GUI.changed)
        {
            serializedObject.ApplyModifiedProperties();
        }


    }


}






