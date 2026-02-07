using UnityEngine;

public class PJuliet : PaintingBase, IInteractable
{
    protected override void Start()
    {
        base.Start();
        taskManager.RomeoTaskComplete += TaskComplete;

        nextDialogueToBeShown = dialogue[currentDialogueIndex].giveTaskDialogue;
    }

    protected override void TaskComplete()
    {
        base.TaskComplete();
        taskManager.isCarryingRomeosMessageForJuliet = false;
    }

    public override void DialogueEnded()
    {
        base.DialogueEnded();
        if (paintingState == PaintingState.WAITINGFORTASKCOMPLETION)
        {
            taskManager.isCarryingJulietsMessageForRomeo = true;
        }
    }

}
