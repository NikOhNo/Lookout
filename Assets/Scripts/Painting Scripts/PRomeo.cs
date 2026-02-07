using System.Threading.Tasks.Sources;
using UnityEngine;

public class PRomeo : PaintingBase, IInteractable
{
    protected override void Start()
    {
        base.Start();
        
        taskManager.JulietTaskComplete += TaskComplete;
        taskManager.JulietTaskGive += () => UpdateState(PaintingState.WAITINGFORTASKCOMPLETION);
    }

    public override void Interact()
    {
        if (paintingState == PaintingState.WAITINGFORTASKCOMPLETION)
        {
            nextDialogueToBeShown = dialogue[currentDialogueIndex].specialDialogue;
            UpdateState(PaintingState.WAITINGFORRESPONSE);
        }
        else if (paintingState == PaintingState.WAITINGFORRESPONSE)
        {
            nextDialogueToBeShown = dialogue[currentDialogueIndex].specialDialogue;
        }
        base.Interact();
    }

    protected override void CheckForAndPlayNextDialogue()
    {
        base.CheckForAndPlayNextDialogue();
    }

}
