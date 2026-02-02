// MyScriptableObjectEditor.cs
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DialogueScriptableObject))]
public class MyScriptableObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); 
        DialogueScriptableObject myScriptableObject = (DialogueScriptableObject)target;
        myScriptableObject.isSpecialDialogue = EditorGUILayout.Toggle("Is Special Dialogue?", myScriptableObject.isSpecialDialogue);
        if (myScriptableObject.isSpecialDialogue)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.LabelField("Special Dialogue:");

            myScriptableObject.specialDialogue =
                EditorGUILayout.TextArea(
                    myScriptableObject.specialDialogue,
                    GUILayout.Height(100)
                );
            EditorGUI.indentLevel--;
        }
        if (GUI.changed)
        {
            EditorUtility.SetDirty(myScriptableObject);
        }
    }
}