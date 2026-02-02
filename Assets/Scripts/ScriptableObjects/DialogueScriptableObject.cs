using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/Dialogue", order = 2)]
public class DialogueScriptableObject : ScriptableObject
{
    [TextArea(3, 20)]
    public string giveTaskDialogue;
    [TextArea(3, 20)]
    public string completeTaskDialogue;
    [TextArea(3, 20)]
    public string failTaskDialogue;
    [HideInInspector]
    public bool isSpecialDialogue = false;
    [TextArea]
    [HideInInspector]
    public string specialDialogue;
}
