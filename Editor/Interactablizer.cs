/*
 * This script SHOULD go in a folder named Editor.
 */
using UnityEngine;
using UnityEditor;

public class Interactablizer : EditorWindow {

    [MenuItem("VRTK4Tools/Interactablizer")]
    public static void ShowWindow() {
        GetWindow(typeof(Interactablizer));
    }

    #region Prefabs
    [SerializeField]
    private Object Secondary_None;
    [SerializeField]
    private Object Secondary_Scale;
    [SerializeField]
    private Object Secondary_Swap;
    [SerializeField]
    private Object Secondary_ControlDirection;
    #endregion

    private Object Interactable;

    string NameExtension = "Interactable";
    enum IntObjTypes { Secondary_None, Secondary_Scale, Secondary_Swap, Secondary_ControlDirection };
    IntObjTypes IntObjType;

    private bool ExtraSettings;

    void OnGUI() {

        GUILayout.Label("Select the objects to convert and the desired interaction.", EditorStyles.boldLabel, GUILayout.Width(1000));
        GUILayout.Space(5);
        GUILayout.Label("String that will be added to the name.", GUILayout.Width(1000));
        NameExtension = EditorGUILayout.TextField(NameExtension);
        GUILayout.Space(5);
        GUILayout.Label("Choose the interaction type.", GUILayout.Width(1000));
        IntObjType = (IntObjTypes)EditorGUILayout.EnumPopup(IntObjType);
        GUILayout.Space(5);
        ExtraSettings = EditorGUILayout.BeginToggleGroup("Settings", ExtraSettings);
        GUILayout.Label("WIP", EditorStyles.boldLabel);
        EditorGUILayout.EndToggleGroup();
        GUILayout.Space(5);

        Interactable = IntObjSelection(IntObjType);

        if (GUILayout.Button("Convert")) {
            if (Interactable != null && Selection.gameObjects != null) {
                ConfigureIntObjs();
                this.Close(); //This could be removed
            }
        }
    }

    private Object IntObjSelection(IntObjTypes t) {
        switch (IntObjType) {
            case IntObjTypes.Secondary_None:
                return Secondary_None;
            case IntObjTypes.Secondary_Scale:
                return Secondary_Scale;
            case IntObjTypes.Secondary_Swap:
                return Secondary_Swap;
            case IntObjTypes.Secondary_ControlDirection:
                return Secondary_ControlDirection;
            default:
                return null;
        }
    }

    private void ConfigureIntObjs() {
        foreach (GameObject go in Selection.gameObjects) {

            GameObject NewIntObj = (GameObject)PrefabUtility.InstantiatePrefab(Interactable);
            NewIntObj.name = go.name + " " + NameExtension + " (" + Interactable.name + ")";
            NewIntObj.transform.position = go.transform.position;
            NewIntObj.transform.rotation = go.transform.rotation;

            Transform meshes = NewIntObj.transform.GetChild(1);
            meshes.GetChild(0).gameObject.SetActive(false);
            go.transform.parent = meshes;
        }
    }

}
