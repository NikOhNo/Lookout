using System.Threading.Tasks.Sources;
using UnityEditor;
using UnityEngine;

public class PRomeo : PaintingBase, IInteractable
{
    protected override void Start()
    {
        base.Start();
        
        taskManager.JulietTaskComplete += TaskComplete;
        UpdateState(PaintingState.WAITINGFORTASKCOMPLETION);
    }

    public override void Interact()
    {
        if (paintingState == PaintingState.WAITINGFORTASKCOMPLETION)
        {
            if (taskManager.isCarryingJulietsMessageForRomeo)
            {
                nextDialogueToBeShown = dialogue[currentDialogueIndex].specialDialogue;
                UpdateState(PaintingState.WAITINGFORRESPONSE);
            }
            else
            { 
                //Play oh so yearnful where is my love dialogue type shit
            }
        }
        base.Interact();
    }

    protected override void TaskComplete()
    {
        base.TaskComplete();
        taskManager.isCarryingJulietsMessageForRomeo = false;
    }

    public override void DialogueEnded()
    {
        base.DialogueEnded();
        if (paintingState == PaintingState.WAITINGFORTASKCOMPLETION)
        {
            taskManager.isCarryingRomeosMessageForJuliet = true;
        }
    }

}
