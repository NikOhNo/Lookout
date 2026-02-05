// MyScriptableObjectEditor.cs
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

[CustomEditor(typeof(DialogueScriptableObject))]
public class MyScriptableObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); 
        DialogueScriptableObject myScriptableObject = (DialogueScriptableObject)target;
        myScriptableObject.isSpecialDialogue = 
            EditorGUILayout.Toggle("Is Special Dialogue?", myScriptableObject.isSpecialDialogue);
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

        

        myScriptableObject.hasResponseChoices = 
            EditorGUILayout.Toggle("Has Response Choices", myScriptableObject.hasResponseChoices);
            
        if (myScriptableObject.hasResponseChoices)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("responseOptions"), 
                includeChildren: true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("correctResponseIndex"));
        }
        serializedObject.ApplyModifiedProperties();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(myScriptableObject);
        }
    }
}