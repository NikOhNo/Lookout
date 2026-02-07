// MyScriptableObjectEditor.cs
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

[CustomEditor(typeof(DialogueScriptableObject))]
public class MyScriptableObjectEditor : Editor
{
    SerializedProperty isTaskDialogue;
    SerializedProperty giveTaskDialogue;
    SerializedProperty completeTaskDialogue;
    SerializedProperty failTaskDialogue;

    SerializedProperty isSpecialDialogue;
    SerializedProperty specialDialogue;

    SerializedProperty hasResponseChoices;
    SerializedProperty responseOptions;
    SerializedProperty correctResponseIndex;

    SerializedProperty hasNextDialogue;
    SerializedProperty nextDialogue;

    void OnEnable()
    {
        isTaskDialogue = serializedObject.FindProperty("isTaskDialogue");
        giveTaskDialogue = serializedObject.FindProperty("giveTaskDialogue");
        completeTaskDialogue = serializedObject.FindProperty("completeTaskDialogue");
        failTaskDialogue = serializedObject.FindProperty("failTaskDialogue");

        isSpecialDialogue = serializedObject.FindProperty("isSpecialDialogue");
        specialDialogue = serializedObject.FindProperty("specialDialogue");

        hasResponseChoices = serializedObject.FindProperty("hasResponseChoices");
        responseOptions = serializedObject.FindProperty("responseOptions");
        correctResponseIndex = serializedObject.FindProperty("correctResponseIndex");

        hasNextDialogue = serializedObject.FindProperty("hasNextDialogue");
        nextDialogue = serializedObject.FindProperty("nextDialogue");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update(); // 🔑 REQUIRED

        EditorGUILayout.PropertyField(isTaskDialogue);
        if (isTaskDialogue.boolValue)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(giveTaskDialogue);
            EditorGUILayout.PropertyField(completeTaskDialogue);
            EditorGUILayout.PropertyField(failTaskDialogue);
            EditorGUI.indentLevel--;
        }

        EditorGUILayout.PropertyField(isSpecialDialogue);
        if (isSpecialDialogue.boolValue)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.LabelField("Special Dialogue");
            specialDialogue.stringValue =
                EditorGUILayout.TextArea(
                    specialDialogue.stringValue,
                    GUILayout.Height(100)
                );
            EditorGUI.indentLevel--;
        }

        EditorGUILayout.PropertyField(hasResponseChoices);
        if (hasResponseChoices.boolValue)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(responseOptions, true);
            EditorGUILayout.PropertyField(correctResponseIndex);
            EditorGUI.indentLevel--;
        }

        EditorGUILayout.PropertyField(hasNextDialogue);
        if (hasNextDialogue.boolValue)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(nextDialogue);
            EditorGUI.indentLevel--;
        }

        serializedObject.ApplyModifiedProperties(); // 🔑 REQUIRED
    }
}