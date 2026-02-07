using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/Dialogue", order = 2)]
public class DialogueScriptableObject : ScriptableObject
{
    [HideInInspector]
    public bool isTaskDialogue = false;
    [HideInInspector]
    [TextArea(3, 20)]
    public string giveTaskDialogue;
    [HideInInspector]
    [TextArea(3, 20)]
    public string completeTaskDialogue;
    [HideInInspector]
    [TextArea(3, 20)]
    public string failTaskDialogue;
    [HideInInspector]
    public bool hasNextDialogue = false;
    [HideInInspector]
    public DialogueScriptableObject nextDialogue;
    [HideInInspector]
    public bool isSpecialDialogue = false;
    [TextArea]
    [HideInInspector]
    public string specialDialogue;
    [HideInInspector]
    public bool hasResponseChoices = false;
    [HideInInspector]
    [TextArea]
    public string[] responseOptions;
    [HideInInspector]
    public int correctResponseIndex;
}
