using UnityEngine;
using System.Collections.Generic;

public class IdleDialogueScriptableObject : ScriptableObject
{
    [TextArea(3, 20)]
    public List<string> idleDialogue = new();
}
