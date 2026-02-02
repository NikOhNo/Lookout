using UnityEngine;

public class PMonaLisa : PaintingBase
{
    protected override void GetNextDialogue()
    {
        ReferenceManager.Instance.dialogueManager.DisplayDialogue(dialogue[0].giveTaskDialogue);
    }

    protected override void UpdateStatus()
    { 
        
    }
}
